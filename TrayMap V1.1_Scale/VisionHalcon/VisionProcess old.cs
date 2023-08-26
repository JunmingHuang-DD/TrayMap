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
        }

        private Logger _Logger = LogManager.GetLogger("VisionProcess");

        private ICamera _Camera;
        private HWindowControl _HWindow;
        private HWindow _HWnd;
        private Queue<HObject> _ImageQueue = new Queue<HObject>();
        private bool _IsDownCamera;


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
        }

        void _Camera_FrameUpdated(object sender, ImageEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Get Image");
            //_Logger.DebugFormat("[Vision] Get Frame");

            //Stopwatch timer = Stopwatch.StartNew();

            HObject img = GenImage(e.Width, e.Height, e.Buffer);

            _ImageQueue.Enqueue(img);

            if (_ImageQueue.Count > 2)
            {
                HObject image = _ImageQueue.Dequeue();
                image.Dispose();
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
                        Func<HObject, bool, Transform> f1 = new Func<HObject, bool, Transform>(FindLabel_1);
                        f1.BeginInvoke(img, SaveResultImage, UpdateResult, f1);
                        //lb = FindLabel_1(img, SaveResultImage);
                    }


                    break;
                case FunctionType.ProcessHeatSink:
                    Func<HObject, bool, Transform> f3 = new Func<HObject, bool, Transform>(FindLens);
                    f3.BeginInvoke(img, SaveResultImage, UpdateResult, f3);

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
                    Func<HObject, bool, Transform> f4 = new Func<HObject, bool, Transform>(downCalibration);
                    f4.BeginInvoke(img, SaveResultImage, UpdateResult, f4);
                    break;
                case FunctionType.upCalibrate:
                    Func<HObject, bool, Transform> f5 = new Func<HObject, bool, Transform>(upCalibration);
                    f5.BeginInvoke(img, SaveResultImage, UpdateResult, f5);
                    break;
                case FunctionType.stickCenter:
                    Func<HObject, bool, Transform> f6 = new Func<HObject, bool, Transform>(FindSticker);
                    f6.BeginInvoke(img, SaveResultImage, UpdateResult, f6);
                    break;
                case FunctionType.MaterialCenter:
                    Func<HObject, bool, Transform> f7 = new Func<HObject, bool, Transform>(FindMaterial);
                    f7.BeginInvoke(img, SaveResultImage, UpdateResult, f7);
                    break;
                case FunctionType.CheckAgain:
                    Func<HObject, bool, Transform> f8 = new Func<HObject, bool, Transform>(FindAgain);
                    f8.BeginInvoke(img, SaveResultImage, UpdateResult, f8);
                    break;
                default:
                    
                    break;
            }

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

        private void UpdateResult(IAsyncResult ar)
        {
            Transform tf = (ar.AsyncState as Func<HObject, bool, Transform>).EndInvoke(ar);

            Debug.WriteLine("[Vision] Get vision result {0}", tf.ToString());
            _Logger.Info("[Vision] Get vision result {0}", tf.ToString());

            if (VisionResultUpdated != null)
            {
                VisionResultUpdated(this, new TranformEventArgs(tf));
            }
        }

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
            SaveImage(_ImageQueue.Peek(), path);
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
                //SaveImage(image, string.Format("D:\\Images\\UP_Label_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
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
                SaveImage(image, string.Format("D:\\Images_Fail\\UP_Label_Result_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
            }



            timer.Stop();
            _Logger.Info("[Find Label] Get label position on picker (x y r), {0:F3}, {1:F3}, {2:F3}, duration, {3:F3}s", tf.X, tf.Y, tf.R, timer.Elapsed.TotalSeconds);

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

            System.Diagnostics.Debug.WriteLine("calibrate process done.");
  
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

            System.Diagnostics.Debug.WriteLine("calibrate process done.");

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
                    SaveImage(image, string.Format("D:\\Images\\up\\UP_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
                }
#else
                tf.X = 0;
                tf.Y = 0;
                tf.R = 0;
#endif



            }
            catch
            {
                SaveImage(image, string.Format("D:\\Images\\up\\UP_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));

            }
            timer.Stop();

            System.Diagnostics.Debug.WriteLine("calibrate process done.");
            GC.Collect();


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
                    SaveImage(image, string.Format("D:\\Images\\down\\Down_{0:yyyyMMddHHmmssfff}.png", DateTime.Now));
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

            System.Diagnostics.Debug.WriteLine("calibrate process done.");

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

            System.Diagnostics.Debug.WriteLine("calibrate process done.");

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

            System.Diagnostics.Debug.WriteLine("calibrate process done.");
            GC.Collect();


            return tf;
        }

    }
}
