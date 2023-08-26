using Incube.Vision.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Incube.Vision.Picker
{
    public class vPicker
    {
        vpPickerParam _Param;
        public vPicker(vpPickerParam param)
        {           
            _Param = param;
        }

        /// <summary>
        /// 获取吸嘴旋转一周的旋转中心
        /// </summary>
        /// <param name="pixelList"></param>
        /// <returns></returns>
        public PointF GetRotateCenter(List<PointF> pixelList)
        {
            var x_sum = pixelList.Sum((a) => a.X);
            var y_sum = pixelList.Sum((a) => a.Y);
            return new PointF(x_sum, y_sum);
        }

        /// <summary>
        /// 以多点拟合圆的方法计算旋转中心
        /// </summary>
        /// <param name="pixelList"></param>
        /// <returns></returns>
        public PointF GetRotateCenterForCircleFit(List<PointF> pixelList)
        {
            return Calibration.GetRotateCenterForCircleFit(pixelList);
        }

        /// <summary>
        /// 获取旋转后的新点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public PointF GetRotateNewPoint(Point3D p, double a)
        {
            PointF curP = new PointF();
            curP.X = (float)p.X;
            curP.Y = (float)p.Y;
            return Calibration.Get_Rotate_NewPoint(_Param.Place_UpVision.RotateCenter,curP,a);
        }
        public PointF GetRotateNewPoint(PointF p, double a)
        {
            PointF curP = new PointF();
            curP.X = (float)p.X;
            curP.Y = (float)p.Y;
            return Calibration.Get_Rotate_NewPoint(_Param.Place_UpVision.RotateCenter, curP, a);
        }
        /// <summary>
        /// 获取实际取料位置
        /// </summary>
        /// <param name="machinePos">未加上相机计算值的目标位置</param>
        /// <param name="pixelPos">拍照结果</param>
        /// <param name="x_sameDirection">机械X方向与相机X方向同向 ？ 1 ： -1</param>
        /// <param name="y_sameDirection">机械X方向与相机Y方向同向 ？ 1 ： -1</param>
        /// <returns></returns>
        public Point3D GetPickPos(Point3D machinePos, Point3D pixelPos)
        {
            int dir_x = _Param.Pick.Direction.X_Direction ? 1 : -1;
            int dir_y = _Param.Pick.Direction.Y_Direction ? 1 : -1;
            int dir_R = _Param.Pick.Direction.Y_Direction ? 1 : -1;

            var x = pixelPos.X - _Param.Pick.Vpos.TeachPixel.X;
            var y = pixelPos.Y - _Param.Pick.Vpos.TeachPixel.Y;
            var r = pixelPos.Y - _Param.Pick.Vpos.TeachPixel.R;

            var m_x = machinePos.X + x * dir_x * _Param.Pick.ThisMpp.X_MPP;
            var m_y = machinePos.Y + y * dir_y * _Param.Pick.ThisMpp.Y_MPP;
            var m_r = machinePos.R + (r) * dir_R;

            return new Point3D(m_x, m_y, m_r);
        }

        /// <summary>
        /// 计算拍一个贴装一个的贴装位置
        /// </summary>
        /// <param name="machinePos"></param>
        /// <param name="upPixel"></param>
        /// <param name="dnPixel"></param>
        /// <returns></returns>
        public Point3D GetPlacePosOnebyone(Point3D machinePos, Point3D upPixel, Point3D dnPixel,double offsetR)
        {
            /*************************计算上视*************************/
            int up_dir_x = _Param.Place_UpVision.Direction.X_Direction ? 1 : -1;
            int up_dir_y = _Param.Place_UpVision.Direction.Y_Direction ? 1 : -1;
            int up_dir_R = _Param.Place_UpVision.Direction.R_Direction ? 1 : -1;

            var up_r = upPixel.R - _Param.Place_UpVision.Vpos.TeachPixel.R;

            var newPixel = GetRotateNewPoint(upPixel, up_r + offsetR);

            var up_x = newPixel.X - _Param.Place_UpVision.Vpos.TeachPixel.X;
            var up_y = newPixel.Y - _Param.Place_UpVision.Vpos.TeachPixel.Y;

            /*************************计算下视*************************/
            int dn_dir_x = _Param.Place_DnVision.Direction.X_Direction ? 1 : -1;
            int dn_dir_y = _Param.Place_DnVision.Direction.Y_Direction ? 1 : -1;
            int dn_dir_R = _Param.Place_DnVision.Direction.R_Direction ? 1 : -1;

            var dn_r = dnPixel.R - _Param.Place_DnVision.Vpos.TeachPixel.R;

            //计算吸嘴旋转下视偏移角度所产生的XY偏移量
            var curPixel = new Point3D(newPixel.X, newPixel.Y, upPixel.R);
            var dnNewPixel = GetRotateNewPoint(curPixel, dn_r * dn_dir_R * up_dir_R);
            var goback_X = curPixel.X - dnNewPixel.X;
            var goback_Y = curPixel.Y - dnNewPixel.Y;

            //下视XY偏移量
            var dn_x = dnPixel.X - _Param.Place_DnVision.Vpos.TeachPixel.X;
            var dn_y = dnPixel.Y - _Param.Place_DnVision.Vpos.TeachPixel.Y;

            var X = (up_x + goback_X) * _Param.Place_UpVision.ThisMpp.X_MPP * up_dir_x + dn_x * _Param.Place_DnVision.ThisMpp.X_MPP * dn_dir_x;
            var Y = (up_y + goback_Y) * _Param.Place_UpVision.ThisMpp.Y_MPP * up_dir_y+ dn_y * _Param.Place_DnVision.ThisMpp.Y_MPP * dn_dir_y;
            var R = up_r * up_dir_R + (dn_r) * dn_dir_R;

            return new Point3D(X + machinePos.X, Y + machinePos.Y, R + _Param.Place_DnVision.Vpos.TeachPos.R);
        }

        /// <summary>
        /// 计算单个mark点的贴装位置
        /// </summary>
        /// <param name="PitchPos"></param>
        /// <param name="upPixel"></param>
        /// <param name="dnPixel"></param>
        /// <returns></returns>
        public Point3D GetPlacePosOneMarkPos(Point3D Teach3D, PointF PitchPos, Point3D upPixel, Point3D dnPixel,double offsetR)
        {
            /*************************计算上视*************************/
            int up_dir_x = _Param.Place_UpVision.Direction.X_Direction ? 1 : -1;
            int up_dir_y = _Param.Place_UpVision.Direction.Y_Direction ? 1 : -1;
            int up_dir_R = _Param.Place_UpVision.Direction.R_Direction ? 1 : -1;

            var up_r = upPixel.R - _Param.Place_UpVision.Vpos.TeachPixel.R;

            var newPixel = GetRotateNewPoint(upPixel, up_r + offsetR);

            var up_x = newPixel.X - _Param.Place_UpVision.Vpos.TeachPixel.X;
            var up_y = newPixel.Y - _Param.Place_UpVision.Vpos.TeachPixel.Y;

            /*************************计算下视*************************/
            int dn_dir_x = _Param.Place_DnVision.Direction.X_Direction ? 1 : -1;
            int dn_dir_y = _Param.Place_DnVision.Direction.Y_Direction ? 1 : -1;
            int dn_dir_R = _Param.Place_DnVision.Direction.R_Direction ? 1 : -1;

            var dn_r = dnPixel.R - _Param.Place_DnVision.Vpos.TeachPixel.R;

            //计算吸嘴旋转下视偏移角度所产生的XY偏移量
            var curPixel = new Point3D(newPixel.X, newPixel.Y, upPixel.R);

            var dnNewPixel = GetRotateNewPoint(curPixel, dn_r * dn_dir_R * up_dir_R);
            var goback_X = curPixel.X - dnNewPixel.X;
            var goback_Y = curPixel.Y - dnNewPixel.Y;

            //下视XY偏移量
            var dn_x = dnPixel.X - _Param.Place_DnVision.Vpos.TeachPixel.X;
            var dn_y = dnPixel.Y - _Param.Place_DnVision.Vpos.TeachPixel.Y;

            //阵列偏移,这个计算需要验证
            int backRotateDir = _Param.Place_DnVision.ClockwiseIsVisionP ? 1 : -1;

            var backRotate = GetRotateNewPoint(PitchPos, dn_r);//这个角度这样做可能有问题
                                                                               //那要怎么算呢，仅XY轴方向有关啊
                                                                               //如果确定相机都是以左上为原点，右下为正方向

            var br_offset_x = Math.Abs(backRotate.X - PitchPos.X) * dn_dir_x * -1;
            var br_offset_y = Math.Abs(backRotate.Y - PitchPos.Y) * dn_dir_y * backRotateDir;


            //总和
            var X = (up_x + goback_X) * _Param.Place_UpVision.ThisMpp.X_MPP * up_dir_x + dn_x * _Param.Place_DnVision.ThisMpp.X_MPP * dn_dir_x
                + br_offset_x;

            var Y = (up_y + goback_Y) * _Param.Place_UpVision.ThisMpp.Y_MPP * up_dir_y + dn_y * _Param.Place_DnVision.ThisMpp.Y_MPP * dn_dir_y
                + br_offset_y;
            var R = up_r * up_dir_R + (dn_r) * dn_dir_R;

            var res_x = X + PitchPos.X + Teach3D.X;
            var res_y = Y + PitchPos.Y + Teach3D.Y;
            var res_r = R + Teach3D.R;
            return new Point3D(res_x,res_y,res_r);
        }

        public static string UseResources(string str)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Setting\language" + @"\Language.txt";

            StreamReader objReader = new StreamReader(filePath);
            string language = objReader.ReadLine();
            objReader.Close();

            if (language.Contains("English"))
            {
                return Resources_en.ResourceManager.GetString(str);
            }
            else if (language.Contains("Chinese"))
            {
                return Resources.ResourceManager.GetString(str);
            }
            else
            {
                return "Unknown linguistic environment!";
            }
        }
    }

    /// <summary>
    /// Picker Vision Param
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class vpPickerParam : ICloneable
    {
        [ReadOnly(true),LocalizedDisplayName("DisplayPickerName"),LocalizedDescription("DescripPickerName")]
        public string Name { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayPick"), LocalizedDescription("DescripPick"),Browsable(true)]
        public pVison Pick { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayPlaceUp"), LocalizedDescription("DescripPlaceUp"), Browsable(true)]
        public UpVision Place_UpVision { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayPlaceDn"), LocalizedDescription("DescripPlaceDn"), Browsable(true)]
        public pVison Place_DnVision { get; set; }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }


    /// <summary>
    /// 相机基本参数
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class pVison : ICloneable
    {
        [ReadOnly(true), LocalizedDisplayName("DisplayTeachPos"), LocalizedDescription("DescripTeachPos")]
        public vTeachPostion Vpos { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayMpp"), LocalizedDescription("DescripMpp")]
        public MPP ThisMpp { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayDirection"), LocalizedDescription("DescripDirection")]
        public VisionDirection Direction { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayAngel"), LocalizedDescription("DescripAngel")]
        public double CameraAngel { get; set; }

        [LocalizedDisplayName("DisplayCameraDir"), LocalizedDescription("DescripCameraDir"), Browsable(false)]
        public bool ClockwiseIsVisionP { get; set; }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return vPicker.UseResources("CameraData");
        }
    }
    /// <summary>
    /// 固定相机基本参数，继承于 相机基本参数
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class UpVision : pVison
    {
        [ReadOnly(true), LocalizedDisplayName("DisplayCenter"), LocalizedDescription("DescripCenter")]
        [TypeConverter(typeof(PointFConvertor))]
        public PointF RotateCenter { get; set; }
        public override string ToString()
        {
            return vPicker.UseResources("FixedCameraData");
        }
    }


    /// <summary>
    /// 相机与机械方向是否一致
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter)),Browsable(false)]
    [Serializable]
    public class VisionDirection : ICloneable
    {
        [LocalizedDisplayName("DisplayXDir"), LocalizedDescription("DescripXDir")]
        public bool X_Direction { get; set; }

        [LocalizedDisplayName("DisplayYDir"), LocalizedDescription("DescripYDir")]
        public bool Y_Direction { get; set; }

        [LocalizedDisplayName("DisplayRDir"), LocalizedDescription("DescripRDir")]

        public bool R_Direction { get; set; }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return vPicker.UseResources("CameraDir");
        }
    }

    /// <summary>
    /// 像素当量
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class MPP : ICloneable
    {
        [ReadOnly(true), LocalizedDisplayName("DisplayXMpp"), LocalizedDescription("DescripXMpp")]
        public double X_MPP { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayYMpp"), LocalizedDescription("DescripYMpp")]
        public double Y_MPP { get; set; }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return vPicker.UseResources("CameraMpp");
        }
    }

    /// <summary>
    /// 示教机械位置位置以及像素位
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class vTeachPostion : ICloneable
    {
        [ReadOnly(true), LocalizedDisplayName("DisplayPos"), LocalizedDescription("DescripPos")]
        [TypeConverter(typeof(Point3DConverter))]
        public Point3D TeachPos { get; set; }

        [ReadOnly(true), LocalizedDisplayName("DisplayPixel"), LocalizedDescription("DescripPixel"),TypeConverter(typeof(Point3DConverter))]
        public Point3D TeachPixel { get; set; }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return vPicker.UseResources("CameraCab");
        }
    }
}
