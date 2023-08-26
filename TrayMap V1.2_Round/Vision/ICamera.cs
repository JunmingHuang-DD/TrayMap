// ===============================================================================
// Project Name        :    Vision
// Project Description :    
// ===============================================================================
// Class Name          :    ICamera
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/13 14:05:41
// Update Time         :    2014/10/13 14:05:41
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;


namespace Incube.Vision
{


    //public delegate void ImageEventHandler(object sender, ImageEventArgs e);
    public enum CameraTriggerSource
    {
        Software,//软触发
        Hardware,//硬触发
        None
    }

    public interface ITranslate
    {
        event ImageEventHandler FrameUpdated;
    }

    public delegate void ImageEventHandler(object sender, ImageEventArgs e);

    public class ImageEventArgs : EventArgs
    {
        public Image _Image { get; set; }

        public Transform Data { get; set; }

        public IntPtr Buffer { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ImageEventArgs()
        {

        }

        public ImageEventArgs(Image img)
        {
            _Image = img;
        }

        public ImageEventArgs(Image img, Transform tf)
        {
            _Image = img;
            Data = tf;
        }

        public ImageEventArgs(IntPtr buffer, int width, int height)
        {
            Buffer = buffer;
            Width = width;
            Height = height;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CameraParam : ICloneable
    {
        /// <summary>
        /// camera ID, this ID is usually used by distinguish the camera in camera API
        /// </summary>
        [ReadOnly(true), Description("Camera ID")]
        public string ID { get; set; }

        [ReadOnly(true), Description("Camera name")]
        public string Name { get; set; }

        [ReadOnly(true), Description("minimeter per pixel, use for unit conversion")]
        public double MPP { get; set; }

        [ReadOnly(true), Description("camera exposure setting, unit is us")]
        public double Exposure { get; set; }

        [ReadOnly(true)]
        public int Width { get; set; }

        [ReadOnly(true)]
        public int Height { get; set; }

        public override string ToString()
        {
            return "Camera Setting";
        }

        public object Clone()
        {
            CameraParam param = new CameraParam();

            param.ID = ID;
            param.Name = Name;
            param.MPP = MPP;
            param.Exposure = Exposure;
            param.Width = Width;
            param.Height = Height;

            return param;
        }
    }


    public interface ICamera : ITranslate
    {
        CameraParam Param { get; set; } //参数

        int Width { get; }

        int Height { get; }

        string Name { get; }

        double Exposure { get; set; }

        bool Connected { get; }

        Image LastImage { get; }

        bool Connect(); 

        void Disconnect();

        void Grab(bool waitForDone = true);

        void StartContious(CameraTriggerSource trigger = CameraTriggerSource.None);

        void Stop();


        void SaveImage(string fileNmae);//保存图片
    }


    


}
