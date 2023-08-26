//#define Dummy_Test

using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using Incube.Vision;
using System.Threading;

namespace VisionHalcon
{
    public partial class VisionProcess
    {
        public enum FunctionType
        {
            None,
            ProcessLabel,
            ProcessHeatSink,
            ProcessPicker,
            downCalibrate,
            upCalibrate,
            stickCenter,
            MaterialCenter,
            CheckAgain,
            Surface,
        }

        private Logger _Logger = LogManager.GetLogger("VisionProcess");

        private ICamera _Camera;
        private HWindowControl _HWindow;
        private HWindow _HWnd;
        private Queue<HObject> _ImageQueue = new Queue<HObject>();
        private bool _IsDownCamera;

        private Thread _ProcessThread;
        private bool _SaveRawImage = false;
        private string _SaveImagePath = "";
        private HObject _LastImage;

        #region properties
        public HWindowControl HWindow
        {
            get { return _HWindow; }
            set
            {
                _HWindow = value;
                _HWnd = _HWindow.HalconWindow;
                _HWindow.ImagePart = new System.Drawing.Rectangle(0, 0, _Camera.Width, _Camera.Height);
            }
        }

        /// <summary>
        /// what function to perform on image
        /// </summary>
        public FunctionType ProcessType { get; set; }

        /// <summary>
        /// whether save result image
        /// </summary>
        public bool SaveResultImage { get; set; }

        public ICamera Camera { get { return _Camera; } }
        #endregion

        public event TransformEventHandler VisionResultUpdated;


        public VisionProcess(ICamera camera, bool isDownCamera = false)
        {
            _Camera = camera;

            if (_Camera != null)
            {
                _Camera.FrameUpdated += _Camera_FrameUpdated;
            }

            ProcessType = FunctionType.None;
            _IsDownCamera = isDownCamera;

            SaveResultImage = true;

            //使用线程处理图片
            _ProcessThread = new Thread(ProcessImage);
            _ProcessThread.IsBackground = true;
            _ProcessThread.Start();
        }

        void _Camera_FrameUpdated(object sender, ImageEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Get Image");
            //_Logger.DebugFormat("[Vision] Get Frame");

            //Stopwatch timer = Stopwatch.StartNew();

            HObject img = GenImage(e.Width, e.Height, e.Buffer);

            if (_SaveRawImage && _SaveImagePath.Length > 0)
            {
                SaveImage(img, _SaveImagePath);
                _SaveRawImage = false;
            }

            _ImageQueue.Enqueue(img);


            

            //timer.Stop();


        }

        private HObject GenImage(int width, int height, IntPtr dataPtr)
        {
            //if (_Image != null)
            //{
            //    _Image.Dispose();
            //}

            //this creation will copy the data to image
            HObject image = null;
            HOperatorSet.GenImage1(out image, "byte", width, height, dataPtr);
            //HOperatorSet.WriteImage(_Image, "png", 0, "E:\\ccc.png");

            //display image
            if (_HWindow != null)
            {
                image.DispObj(_HWindow.HalconWindow);
            }

            return image;
        }

        void ProcessImage()
        {
            while (true)
            {
                Thread.Sleep(5);

                if (_ImageQueue.Count < 1)
                {
                    continue;
                }

                if (_LastImage != null)
                {
                    _LastImage.Dispose();
                    _LastImage = null;
                }

                _LastImage = _ImageQueue.Dequeue();
                if (_LastImage == null)
                {
                    continue;
                }

                //the process should be async
                switch (ProcessType)
                {
                    case FunctionType.None:

                        break;
                    case FunctionType.ProcessLabel:
                        Transform lb = null;
                        if (_IsDownCamera)
                        {
                            //lb = FindLabel_2(img, SaveResultImage);
                        }
                        else
                        {
                            FindLabel_1(_LastImage, SaveResultImage);
                        }


                        break;
                    case FunctionType.ProcessHeatSink:
                        FindLens(_LastImage, SaveResultImage);
                        //if (VisionResultUpdated != null)
                        //{
                        //    VisionResultUpdated(this, new TranformEventArgs(lb));
                        //}
                        break;
                    case FunctionType.ProcessPicker:
                        //lb = FindNozzle(img, SaveResultImage);
                        //if (VisionResultUpdated != null)
                        //{
                        //    VisionResultUpdated(this, new TranformEventArgs(lb));
                        //}
                        break;
                    case FunctionType.downCalibrate:
                        downCalibration(_LastImage, SaveResultImage);
                        break;
                    case FunctionType.upCalibrate:
                        upCalibration(_LastImage, SaveResultImage);
                        break;
                    case FunctionType.stickCenter:
                        FindSticker(_LastImage, SaveResultImage);
                        break;
                    case FunctionType.MaterialCenter:
                        FindMaterial(_LastImage, SaveResultImage);
                        break;
                    case FunctionType.CheckAgain:
                        FindAgain(_LastImage, SaveResultImage);
                        break;
                        
                    case FunctionType.Surface:
                        FindSurface(_LastImage, false);
                        break;

                    default:

                        break;
                }
            }
        }


        //private void UpdateResult(IAsyncResult ar)
        //{
        //    Transform tf = (ar.AsyncState as Func<HObject, bool, Transform>).EndInvoke(ar);

        //    Debug.WriteLine("[Vision] Get vision result {0}", tf.ToString());
        //    _Logger.Info("[Vision] Get vision result {0}", tf.ToString());

        //    if (VisionResultUpdated != null)
        //    {
        //        VisionResultUpdated(this, new TranformEventArgs(tf));
        //    }
        //}

        public HObject ReadImage(string path)
        {
            HObject image = null;

            //HOperatorSet.GenEmptyObj(out image);
            //image.Dispose();

            HOperatorSet.ReadImage(out image, path);

            //display image
            HOperatorSet.DispObj(image, _HWindow.HalconWindow);

            //HOperatorSet.WriteImage(image, "png", 0, "E:\\test something.png");

            _ImageQueue.Enqueue(image);

            return image;
        }

        public void SaveImage(string path)
        {
            if (_LastImage != null)
            {
                SaveImage(_LastImage, path);
            }
            else
            {
                _SaveImagePath = path;
                _SaveRawImage = true;
            }
        }

        private void SaveImage(HObject img, string path)
        {
            string dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }


            HOperatorSet.WriteImage(img, "png", 0, path); //"C:\\Users\\Administrator\\Pictures\\test.png"
        }


        /// <summary>
        /// 定位保护膜中心, 上视相机拍照
        /// </summary>
        /// <returns></returns>
        public Transform FindLabel_1(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);
            Stopwatch timer = Stopwatch.StartNew();
            if (saveResultImage)
            {
                SaveImage(image, string.Format("D:\\Images\\CHU\\UP_Label_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }

            System.Diagnostics.Debug.WriteLine("Label I am processing...");


            try
            {
#if !Dummy_Test

#else

                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif
            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images\\UP_Label_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }



            timer.Stop();
            _Logger.Info("[Find Label] Get label position on picker (x y r), {0:F3}, {1:F3}, {2:F3}, duration, {3:F3}s", tf.X, tf.Y, tf.R, timer.Elapsed.TotalSeconds);

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }


        /// <summary>
        /// 定位镜头中心 
        /// </summary>
        /// <returns></returns>
        public Transform FindLens(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);

            if (saveResultImage)
            {
                //SaveImage(image, string.Format("D:\\Images\\Down_Lens_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }

            //System.Diagnostics.Debug.WriteLine("I am processing...");
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test

#else
                    tf.X = 0;
                    tf.Y = 0;
                    tf.R = 0;
#endif
            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images_Fail\\Down_Lens_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            timer.Stop();

            System.Diagnostics.Debug.WriteLine("Image process done.");
            _Logger.Info("[Find Lens] Get lens position in tray (x y r), {0:F3}, {1:F3}, {2:F3}, duration, {3:F3}s", tf.X, tf.Y, tf.R, timer.Elapsed.TotalSeconds);

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }

        public Transform downCalibration(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);

            if (saveResultImage)
            {
                SaveImage(image, "D:\\AAAAAAAALCD\\DownLeft.png");
            }
            HObject region;
            HTuple hv_result;

           get_blueSticker_caliberte2(image, out region, out hv_result);
 
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);
                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                }

#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\AAAAAAAALCD\\DownLeft1.png", DateTime.Now));
            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }

        public Transform upCalibration(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);

            if (saveResultImage)
            {
                SaveImage(image, "D:\\AAAAAAAALCD\\DownRight.png");
            }
            HObject region;
            HTuple hv_result;

            get_blueSticker_caliberte1(image, out region, out hv_result);

            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);

                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                }

#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\AAAAAAAALCD\\DownRight1.png", DateTime.Now));
            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }

        public Transform FindSticker(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);

            if (saveResultImage)
            {
                //SaveImage(image, string.Format("D:\\Images\\up\\UP_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            HObject region;
            HTuple hv_result;



            get_blueSticker_result(image, out region, out hv_result);
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);

                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                }
                
                if(hv_result[3].I != 1)
                {
                    //SaveImage(image, string.Format("D:\\Images\\up\\UP_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
                }
#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                //SaveImage(image, string.Format("D:\\Images\\up\\UP_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));

            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");
            GC.Collect();

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));


            return tf;
        }

        public Transform FindMaterial(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000);

            if (saveResultImage)
            {
                //SaveImage(image, string.Format("D:\\Images\\down\\Down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            HObject region;
            HTuple hv_result;

            get_blackMaterial_result(image, out region, out hv_result);

            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);

                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                }

                if(hv_result[3].I != 1)
                {
                  //  SaveImage(image, string.Format("D:\\Images\\down\\Down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
                }
#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images\\down\\Down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }
   
        public Transform FindAgain(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000, -1000, -1000);

            if (saveResultImage)
            {
                SaveImage(image, string.Format("D:\\Images\\again\\again_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            HObject region;
            HTuple hv_result;

            get_blueSticker_result_again(image, out region, out hv_result);

            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);
                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                    tf.H = hv_result[3];
                    tf.W = hv_result[3];
                }
                
#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images\\again\\again_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }

        public Transform FindSurface(HObject image, bool saveResultImage = true)
        {
            Transform tf = new Transform(-1000, -1000, -1000, -1000, -1000);

            if (saveResultImage)
            {
                SaveImage(image, string.Format("D:\\Images\\down1\\down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            HObject region;
            HTuple hv_result;

            get_blueSurface_result(image, out region, out hv_result);

            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test
                if (hv_result.Length >= 4 && hv_result[3].I == 1)
                {
                    _HWnd.SetDraw("margin");
                    _HWnd.SetColor("red");
                    region.DispObj(_HWnd);
                    tf.X = hv_result[0];
                    tf.Y = hv_result[1];
                    tf.R = hv_result[2];
                    tf.H = hv_result[3];
                    tf.W = hv_result[3];
                }

#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images\\down1\\down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");

            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }


        public Transform FindSticker33()
        {
            Transform tf = new Transform(-1000, -1000, -1000);


            HObject region;
            HTuple hv_result;

            HObject ho_Image2222;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image2222);


            ho_Image2222.Dispose();
            HOperatorSet.ReadImage(out ho_Image2222, "./UP_V.png");

            get_blueSticker_result(ho_Image2222, out region, out hv_result);
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
#if !Dummy_Test

#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {

            }
            timer.Stop();

            //System.Diagnostics.Debug.WriteLine("calibrate process done.");
            GC.Collect();


            VisionResultUpdated?.Invoke(this, new TranformEventArgs(tf));

            return tf;
        }

    }
}
