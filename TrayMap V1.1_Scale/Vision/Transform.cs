// ===============================================================================
// Project Name        :    Motion
// Project Description :    
// ===============================================================================
// Class Name          :    Transform
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/18 11:06:58
// Update Time         :    2014/10/18 11:06:58
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Incube.Vision
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Transform
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double R { get; set; }

        public double X1 { get; set; }

        public double Y1 { get; set; }

        /// <summary>
        /// width
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// height
        /// </summary>
        public double H { get; set; }


        public Transform()
            : this(0, 0, 0, 0, 0)
        {

        }

        public Transform(double x, double y, double r)
            : this(x, y, r, 0, 0)
        {
            
        }

        public Transform(double x, double y, double r,double w,double h)
        {
            X = x;
            Y = y;
            R = r;
            W = w;
            H = h;
        }

        public static Transform operator -(Transform a, Transform b)
        {
            return new Transform(a.X - b.X, a.Y - b.Y, a.R - b.R,a.W - b.W,a.H - b.H);
        }

        public static Transform operator +(Transform a, Transform b)
        {
            return new Transform(a.X + b.X, a.Y + b.Y, a.R + b.R, a.W + b.W, a.H + b.H);
        }

        public override string ToString()
        {
            return string.Format("X={0:F3},Y={1:F3},R={2:F3}", X, Y, R);
        }
    }


    public class TranformEventArgs : EventArgs
    {
        public Transform Data { get; set; }

        public TranformEventArgs(Transform data)
        {
            Data = data;
        }
    }

    public delegate void TransformEventHandler(object sender, TranformEventArgs e);



    [TypeConverter(typeof(Point3DConverter))]
    [Serializable]
    public struct Point3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double R { get; set; }

        public Point3D(double x, double y, double r)
        {
            X = x;
            Y = y;
            R = r;
        }

        public Point3D(double x, double y)
        {
            X = x;
            Y = y;
            R = 0;
        }

        public override string ToString()
        {
            return $"X:{X.ToString("F2")},Y:{Y.ToString("F2")},R:{R.ToString("F2")}";
        }
    }


    public class Point3DConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Point3D))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            Type con = context.Instance.GetType();

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class PointFConvertor : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PointF))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            Type con = context.Instance.GetType();

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
