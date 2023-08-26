using HalconDotNet;
using Incube.Vision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisionHalcon
{
    public partial class VisionProcess
    {

        public PointF FitCircle(List<PointF> points)
        {
            HObject ho_Contour;


            // Local control variables 

            HTuple hv_Row = null, hv_Column = null, hv_Radius = null;
            HTuple hv_StartPhi = null, hv_EndPhi = null, hv_PointOrder = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Contour);

            ho_Contour.Dispose();

            HTuple x = new HTuple(points.Select(p => p.X).ToArray());
            HTuple y = new HTuple(points.Select(p => p.Y).ToArray());

            HOperatorSet.GenContourPolygonXld(out ho_Contour, y, x);
            HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_Row,
                out hv_Column, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

            ho_Contour.Dispose();

            return new PointF(hv_Column[0].F, hv_Row[0].F);
        }
    }
}
