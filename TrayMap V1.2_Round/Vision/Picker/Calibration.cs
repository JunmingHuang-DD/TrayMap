using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Incube.Vision.Picker
{
    public class Calibration
    {
        /// <summary>
        /// 一点围绕一点旋转后的坐标,这里是已经获取到了旋转中心坐标
        /// </summary>
        /// <param name="rc">旋转中心</param>
        /// <param name="curp">Current Point</param>
        /// <param name="a">Rotate angel</param>
        /// <returns>new Point </returns>
        public static PointF Get_Rotate_NewPoint(PointF rc, PointF curp, double a)
        {
            /****************************************/
            // 以旋转中心为原点建立坐标系
            // 以及当前点坐标，计算旋转a角后的坐标
            /***************************************/
            var p0 = rc;//圆心
            var p1 = curp;//已知点

            var x0 = p1.X - p0.X;//两点坐标差 X
            var y0 = p1.Y - p0.Y;//两点坐标差 Y

            var PowR = Math.Pow(x0, 2) + Math.Pow(y0, 2); //半径平方

            //判断象限
            if (x0 > 0 && y0 > 0)
            {
                y0 = Math.Abs(y0) * -1;
                x0 = Math.Abs(x0);
            }
            else if (x0 > 0 && y0 < 0)
            {
                y0 = Math.Abs(y0);
                x0 = Math.Abs(x0);
            }
            else if (x0 < 0 && y0 < 0)
            {
                y0 = Math.Abs(y0);
                x0 = Math.Abs(x0) * -1;
            }
            else
            {
                y0 = Math.Abs(y0) * -1;
                x0 = Math.Abs(x0) * -1;
            }


            var A = a * Math.PI / 180;   //未知点与已知点以圆心取直线的夹角
            var A0 = Math.Atan(y0 / x0); //已知点在坐标系中的角度

            var A1 = A0 + A;             //未知点在坐标系中的角度

            //解圆与点斜式直线方程
            var x1 = Math.Sqrt(PowR / (1 + Math.Pow(Math.Tan(A1), 2)));
            var y1 = Math.Tan(A1) * x1;
            var y1_1 = Math.Tan(A1) * x1 * -1;

            //直线与圆有两个交掉，根据与已知点距离取值
            var offsetP1_0 = Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
            var offsetP1_1 = Math.Sqrt(Math.Pow(-x1 - x0, 2) + Math.Pow(-y1 - y0, 2));
            if (A1 < Math.PI / 2)
            {
                if (offsetP1_0 > offsetP1_1)
                {
                    x1 = x1 * -1;
                }
            }
            else
            {
                if (offsetP1_0 < offsetP1_1)
                {
                    x1 = x1 * -1;
                }
            }
            y1 = Math.Tan(A1) * x1;

            PointF p2 = new PointF();

            //加上原点位置，即旋转后的位置
            p2.X = p0.X + (float)x1;
            p2.Y = p0.Y - (float)y1;
            return p2;
        }

        /// <summary>
        /// 计算相机与机械的夹角 get_angle(SDPNT op,SDPNT xp,SDPNT yp)
        /// </summary>
        /// <param name="op">起点坐标（像素）</param>
        /// <param name="xp">X轴单独移动后的坐标（像素）</param>
        /// <param name="yp">Y轴单独移动后的坐标（像素）</param>
        /// <returns>X/Y夹角</returns>
        public static PointF get_angle(PointF op, PointF xp, PointF yp)
        {
            PointF Angle = new PointF();

            Angle.X = (float)Math.Atan2((double)(xp.Y - op.Y), (double)(xp.X - op.X));
            Angle.X = (float)Math.Atan2((double)(yp.X - op.X), (double)(yp.Y - op.Y));

            Angle.X = (float)(Angle.X / Math.PI * 180.0);
            Angle.Y = (float)(Angle.Y / Math.PI * 180.0);   //转为角度

            return Angle;
        }

        /// <summary>
        /// 获取吸嘴旋转一周的旋转中心
        /// </summary>
        /// <param name="pixelList"></param>
        /// <returns></returns>
        public static PointF GetRotateCenter(List<PointF> pixelList)
        {
            var x_sum = pixelList.Sum((a) => a.X);
            var y_sum = pixelList.Sum((a) => a.Y);
            return new PointF(x_sum, y_sum);
        }

        public static PointF GetRotateCenterForCircleFit(List<PointF> pixelList)
        {
            return CircleFit.FitCenter(pixelList);
        }

        /// <summary>
        /// 计算MPP值
        /// </summary>
        /// <param name="sPos">开始位置拍照结果</param>
        /// <param name="ePos">结束位置拍照结果</param>
        /// <param name="moveRelativePos">机械位置相对位移量</param>
        /// <param name="xORy">计算X或者Y方向MPP</param>
        /// <returns>像素当量</returns>
        public static double Get_scale(PointF sPos, PointF ePos, double moveRelativePos,ChooseDirection xORy)
        {
            float pixelPitch = 0;
            if (xORy == ChooseDirection.X_DIR)
            {
                pixelPitch = ePos.X - sPos.X;
            }
            else
            {
                pixelPitch = ePos.Y - sPos.Y;
            }
            var mpp = moveRelativePos / Math.Abs(pixelPitch);
            return mpp;
        }

        /// <summary>
        /// 标记x方向或者y方向
        /// </summary>
        public enum ChooseDirection
        {
            X_DIR = 0,
            Y_DIR
        }
    }
}
