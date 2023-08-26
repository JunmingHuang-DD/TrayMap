// ===============================================================================
// Project Name        :    Vision
// Project Description :    
// ===============================================================================
// Class Name          :    Line
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/7 16:06:21
// Update Time         :    2014/10/7 16:06:21
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Incube.Vision
{
    /// <summary>
    /// 使用点线式方程
    /// </summary>
    public class Line
    {
        /// <summary>
        /// 直线的斜率
        /// </summary>
        public double Slope { get; protected set; }

        /// <summary>
        /// 相当于y=kx+b中的b的值
        /// </summary>
        public double Offset { get; protected set; }

        /// <summary>
        /// 直线的角度
        /// </summary>
        public double Angle
        {
            get
            {
                if (Slope != double.MaxValue)
                {
                    return Math.Atan(Slope) * 180 / Math.PI;
                }
                else
                    return 90;
            }
        }

        /// <summary>
        /// 点线式直线，当斜率为无穷大时，用double.MaxValue代替
        /// </summary>
        /// <param name="point"></param>
        /// <param name="slope"></param>
        public Line(PointF point, double slope)
        {
            Slope = slope;
            Offset = point.Y - slope * point.X;
        }

        public Line(PointF point1, PointF point2)
        {
            if ((point2.X - point1.X) != 0)
            {
                Slope = (double)(point2.Y - point1.Y) / (double)(point2.X - point1.X);
                Offset = point1.Y - Slope * point1.X;
            }
            else
            {
                Slope = double.MaxValue;
                Offset = point1.X;
            }
        }

        /// <summary>
        /// y=kx+b形式直线，当斜率为无穷大时，用double.MaxValue代替
        /// </summary>
        /// <param name="slope"></param>
        /// <param name="offset"></param>
        public Line(double slope, double offset)
        {
            Slope = slope;
            Offset = offset;
        }


        /// <summary>
        /// 求两直线的交点
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public PointF InterSect(Line another)
        {
            PointF intersect = new PointF(-1000, -1000);
            if (Math.Abs(another.Slope - this.Slope) < 0.01)
            {
                //return PointF.Empty; //parallel lines
                return intersect;
            }


            if (this.Slope == double.MaxValue)
            {
                intersect.X = (float)(this.Offset);
                intersect.Y = (float)(another.Slope * intersect.X + another.Offset);
            }
            else if (another.Slope == double.MaxValue)
            {
                intersect.X = (float)(another.Offset);
                intersect.Y = (float)(this.Slope * intersect.X + this.Offset);
            }
            else
            {
                intersect.X = (float)((another.Offset - this.Offset) / (this.Slope - another.Slope));
                intersect.Y = (float)(this.Slope * intersect.X + this.Offset);
            }


            return intersect;
        }

        /// <summary>
        /// 平移直线， 右移为正，左移为负数
        /// </summary>
        /// <param name="xOffset"></param>
        /// <returns></returns>
        public Line OffsetLine(float xOffset)
        {
            Line newLine = new Line(this.Slope, Offset - this.Slope * xOffset);

            return newLine;

        }

        //把Y点代入直线求X点
        public double GetXFromY(double newY)
        {
            double newX = 0;

            newX = (newY - Offset) / Slope;

            return newX;

        }

        /// <summary>
        /// 求一点到直线的距离
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double PointDistance(PointF point)
        {
            double distance = 0;

            if (Math.Abs(Slope) <= 0.01)
            {
                distance = point.Y;
            }
            else if (Slope == double.MaxValue)
            {
                distance = point.X;
            }
            else
            {
                //求得过该点的垂线
                Line p = new Line(-1 / this.Slope, point.Y + 1 / this.Slope * point.X);

                //垂线和直线交点
                PointF inter = InterSect(p);

                distance = Math.Sqrt(Math.Pow(inter.X - point.X, 2) + Math.Pow(inter.Y - point.Y, 2));
            }

            return distance;
        }

        /// <summary>
        /// 根据X坐标计算Y坐标
        /// </summary>
        /// <param name="xPos"></param>
        /// <returns></returns>
        public float GetPointY(float xPos)
        {
            return (float)(Slope * xPos + Offset);
        }

        /// <summary>
        /// 根据Y坐标计算X坐标
        /// </summary>
        /// <param name="yPos"></param>
        /// <returns></returns>
        public float GetPointX(float yPos)
        {
            if (Slope == double.MaxValue)
            {
                return (float)Offset;
            }
            else
            {
                return (float)((yPos - Offset) / Slope);
            }
        }

    }

}
