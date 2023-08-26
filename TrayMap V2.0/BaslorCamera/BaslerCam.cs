using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

using Incube.Vision;
using PylonC.NET;

namespace Vision.Camera
{
    public class BaslerCam : ICamera
    {
        private RingBitmap _BitmapRing;
        private ImageProvider m_imageProvider; /* Create one image provider. */
        //private Bitmap m_bitmap = null; /* The bitmap is used for displaying the image. */
        private uint _CameraIndex = 0;
        private bool _IsLive = false;

        private double _Exposure;
        private string _ErrorMsg;
        private bool _ImageGrabbed = false;
        private bool _ConvertBmp = false; //whether to convert the image to BMP, this is not used for Halcon
        private CameraParam _Param;

        #region properties
        public string Name
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public double Exposure
        {
            get
            {
                _Exposure = m_imageProvider.Exposure;
                return _Exposure;
            }
            set
            {
                _Exposure = value;

                m_imageProvider.Exposure = _Exposure;
            }
        }

        public bool Connected
        {
            get;
            protected set;
        }

        public double MPP
        {
            get { return 0; }
        }

        public System.Drawing.Image LastImage
        {
            get { return _BitmapRing.Image; }
        }

        public string ErrorMsg 
        {
            get { return _ErrorMsg; }
            protected set
            {
                _ErrorMsg = value;

                if (ErrorUpdate != null)
                {
                    ErrorUpdate.BeginInvoke(this, null, null, null);
                }
            }
        }

        public double ColorImproveRed 
        {
            get { return m_imageProvider.ColorImprove_Red; }

            set { m_imageProvider.ColorImprove_Red = value; }
        }

        public double ColorImproveGreen
        {
            get { return m_imageProvider.ColorImprove_Green; }

            set { m_imageProvider.ColorImprove_Green = value; }
        }

        public CameraParam Param { get { return _Param; } set { _Param = value; } }

        public int Width { get; protected set; }

        public int Height { get; protected set; }
        #endregion

        public event ImageEventHandler FrameUpdated;
        public event EventHandler ErrorUpdate;


        public BaslerCam(CameraParam param)
        {
            _Param = param;
            Width = param.Width;
            Height = param.Height;

            //_CameraIndex = uint.Parse(_Param.ID);
            m_imageProvider = new ImageProvider(); /* Create one image provider. */

            _BitmapRing = new RingBitmap(3);

            /* Register for the events of the image provider needed for proper operation. */
            m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
            m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
            m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
        }


        public bool Connect()
        {
            if (Connected)
            {
                return true;
            }

            try
            {
                //var devices = DeviceEnumerator.EnumerateDevices();

                /* Open the image provider using the index from the device data. */
                m_imageProvider.Open(_Param.ID);

                System.Threading.Thread.Sleep(100);

                Exposure = _Param.Exposure;

                m_imageProvider.Height = _Param.Height;
                m_imageProvider.Width = _Param.Width;

                //ColorImproveRed = 3.0;
                //Console.WriteLine("the exposure, {0:F4}, color_improve: {1:F2}", Exposure, ColorImproveRed);

                Connected = true;

                StartContious(CameraTriggerSource.None);
            }
            catch (Exception e)
            {
                //m_imageProvider.Close();
                Console.WriteLine("相机连接失败");
                
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
                Connected = false;
            }

            return Connected;
        }

        public void Disconnect()
        {
            try
            {
                Stop();
                Thread.Sleep(100);

                m_imageProvider.Close();
                m_imageProvider.ReleaseImage();

                Connected = false;
            }
            catch
            {

            }
        }

        public void Grab(bool waitForDone = true)
        {
            _ImageGrabbed = false;

            //if (_IsLive)
            //{
            //    Stop();
            //}

            //OneShot();

            ContinuousShot(CameraTriggerSource.Software);
            SoftwareTrigger();

            if (waitForDone)
            {
                DateTime timeout = DateTime.Now + TimeSpan.FromSeconds(1);
                do
                {
                    Thread.Sleep(50);
                } while (!_ImageGrabbed && timeout > DateTime.Now);
            }
            

            //return _BitmapRing.Image;
        }

        //public void StartContious()
        //{
        //    ContinuousShot();
        //    _IsLive = true;
        //}

        //启动连续
        public void StartContious(CameraTriggerSource trigger = CameraTriggerSource.None)
        {
            ContinuousShot(trigger);//连续拍摄

            _IsLive = true;
        }

        public void SaveImage(string fileNmae)
        {
            _BitmapRing.Image.Save(fileNmae);
        }

        #region process ImageProvider events
        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback()
        {
            _IsLive = false;

            //if (InvokeRequired)
            //{
            //    /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
            //    BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
            //    return;
            //}

            ///* Enable device list update again */
            //updateDeviceListTimer.Start();

            ///* The image provider stopped grabbing. Enable the grab buttons. Disable the stop button. */
            //EnableButtons(m_imageProvider.IsOpen, false);
        }

        /* Handles the event related to the removal of a currently open device. */
        private void OnDeviceRemovedEventCallback()
        {

            /* Disable the buttons. */
            //EnableButtons(false, false);

            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();

            /* Since one device is gone, the list needs to be updated. */
            //UpdateDeviceList();
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback()
        {
            //if (InvokeRequired)
            //{
            //    /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
            //    BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
            //    return;
            //}

            ///* The image provider is ready to grab. Enable the grab buttons. */
            //EnableButtons(true, false);
        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback()
        {
            //if (InvokeRequired)
            //{
            //    /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
            //    BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
            //    return;
            //}

            ///* The image provider is closed. Disable all buttons. */
            //EnableButtons(false, false);
        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback()
        {
            //if (InvokeRequired)
            //{
            //    /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
            //    BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
            //    return;
            //}

            ///* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
            //updateDeviceListTimer.Stop();

            ///* The image provider is grabbing. Disable the grab buttons. Enable the stop button. */
            //EnableButtons(false, true);
        }

        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        {


            Console.WriteLine("Grab errror occure");
            ErrorMsg = grabException.Message + additionalErrorMessage;
        }


        /* Handles the event related to an image having been taken and waiting for processing. */
        private void OnImageReadyEventCallback()
        {
            //if (InvokeRequired)
            //{
            //    /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
            //    BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
            //    return;
            //}

            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image == null)
                {
                    return;
                }

                Width = image.Width;
                Height = image.Height;

                _ImageGrabbed = true;
                if (_ConvertBmp)
                {
                    _BitmapRing.CopyToNextBitmap_8bppIndexed(image.Width, image.Height, image.Buffer);

                    if (FrameUpdated != null)
                    {
                        FrameUpdated(this, new ImageEventArgs(_BitmapRing.Image));
                    }
                }
                else
                {
                    if (FrameUpdated != null)
                    {
                        FrameUpdated(this, new ImageEventArgs(image.BufferPtr, image.Width, image.Height));
                    }
                }



                /* The processing of the image is done. Release the image buffer. */
                m_imageProvider.ReleaseImage();
                /* The buffer can be used for the next image grabs. */

            }
            catch (Exception e)
            {
                Console.Write("Update image error:  {0} ", e.Message);
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
            }
        }
        #endregion

        /* Stops the image provider and handles exceptions. */
        public void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();

                _IsLive = false;
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
            }
        }

        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
            }
        }


        /* Starts the grabbing of one image and handles exceptions. */
        private void OneShot(int imageCount = 1, CameraTriggerSource trigger = CameraTriggerSource.None)
        {
            try
            {
                
                m_imageProvider.OneShot(imageCount, trigger); /* Starts the grabbing of one image. */

                if (trigger == CameraTriggerSource.Software)
                {
                    m_imageProvider.SendSoftTrigger();
                }

                _IsLive = imageCount > 1;
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
            }
        }



        /* Starts the grabbing of images until the grabbing is stopped and handles exceptions. */
        private void ContinuousShot(CameraTriggerSource trigger = CameraTriggerSource.None)
        {
            try
            {
                _IsLive = true;

                m_imageProvider.ContinuousShot(trigger); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                Console.WriteLine("Catched continue shot error");
                ErrorMsg = e.Message + m_imageProvider.GetLastErrorMessage();
            }
        }

        /// <summary>
        /// send software trigger to get image
        /// </summary>
        private void SoftwareTrigger()
        {
            m_imageProvider.SendSoftTrigger();
        }

        public void Dispose()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
