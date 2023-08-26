using HalconDotNet;
using Incube.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VisionHalcon
{
    public partial class VisionProcess
    {
       
        public void get_blueSticker_result(HObject ho_Image, out HObject ho_outRegion,
            out HTuple hv_result)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Image1, ho_SelectedChannel, ho_LowerRegion;
            HObject ho_UpperRegion, ho_ImageScaled, ho_ImageClosing;
            HObject ho_Region, ho_RegionClosing1, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_RegionOpening3, ho_ConnectedRegions5;
            HObject ho_SelectedRegions, ho_RegionClosing, ho_RegionOpening1;
            HObject ho_RegionOpening2, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_RegionTrans, ho_Rectangle3, ho_Cross1, ho_RegionErosion;
            HObject ho_ImageReduced1, ho_Region3, ho_ConnectedRegions4;
            HObject ho_SelectedRegions2, ho_RegionDifference3, ho_ConnectedRegions7;
            HObject ho_RegionClosing10, ho_ConnectedRegions8, ho_RegionFillUp2;
            HObject ho_RegionOpening5, ho_RegionOpening6, ho_ConnectedRect;
            HObject ho_RegionErosion1, ho_RegionDifference1, ho_Rectangle1;
            HObject ho_Rectangle2, ho_RegionUnion1, ho_RegionIntersection;
            HObject ho_RegionDifference2, ho_RegionIntersection1, ho_ImageReduced;
            HObject ho_SelectedRegions5, ho_RegionTrans1, ho_Rectangle4;
            HObject ho_Rectangle, ho_RegionBorder, ho_RegionIntersection2;
            HObject ho_ConnectedRegions2;


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_LowerLimit = new HTuple();
            HTuple hv_UpperLimit = new HTuple(), hv_Index = null, hv_Mult = null;
            HTuple hv_Add = null, hv_MinGray = null, hv_MaxGray = null;
            HTuple hv_Range = null, hv_Number = null, hv_Row1 = null;
            HTuple hv_Column1 = null, hv_Phi2 = null, hv_Length12 = null;
            HTuple hv_Length22 = null, hv_Mean = null, hv_Number1 = null;
            HTuple hv_Area2 = null, hv_RowY2 = null, hv_ColumnX2 = null;
            HTuple hv_Deg = null, hv_Row = null, hv_Column = null;
            HTuple hv_Phi1 = null, hv_Length11 = null, hv_Length21 = null;
            HTuple hv_Row3 = null, hv_Column3 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_Phi3 = null, hv_Length13 = null;
            HTuple hv_Length23 = null, hv_Deg1 = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageClosing);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions5);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_Cross1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions7);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing10);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions8);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp2);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening5);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening6);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRect);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions5);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle4);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionBorder);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);

            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "HTuplesA.lib", out hv_HTuples);

            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;
            if ((int)(new HTuple((new HTuple(((hv_HTuples.TupleSelect(1))).TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_LowerLimit = (hv_HTuples.TupleSelect(1))[1];
                if (hv_HTuples == null)
                    hv_HTuples = new HTuple();
                hv_HTuples[1] = ((hv_HTuples.TupleSelect(1))).TupleSelect(0);
            }
            else
            {
                hv_LowerLimit = 0.0;
            }
            if ((int)(new HTuple((new HTuple(((hv_HTuples.TupleSelect(2))).TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_UpperLimit = (hv_HTuples.TupleSelect(2))[1];
                if (hv_HTuples == null)
                    hv_HTuples = new HTuple();
                hv_HTuples[2] = ((hv_HTuples.TupleSelect(2))).TupleSelect(0);
            }
            else
            {
                hv_UpperLimit = 255.0;
            }
            hv_Index = 1;
            hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()) / ((hv_HTuples.TupleSelect(
                2)) - (hv_HTuples.TupleSelect(1)));
            hv_Add = ((-hv_Mult) * (hv_HTuples.TupleSelect(1))) + hv_LowerLimit;
            ho_outRegion.Dispose();
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            ho_Image1.Dispose();
            HOperatorSet.ScaleImage(ho_Image, out ho_Image1, hv_Mult, hv_Add);
            ho_SelectedChannel.Dispose();
            HOperatorSet.AccessChannel(ho_Image1, out ho_SelectedChannel, hv_Index);
            HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray,
                out hv_MaxGray, out hv_Range);
            ho_LowerRegion.Dispose();
            HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(
                hv_LowerLimit))).TupleMin(), hv_LowerLimit);
            ho_UpperRegion.Dispose();
            HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
            HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out OTemp[0], hv_LowerLimit,
                "fill");
            ho_SelectedChannel.Dispose();
            ho_SelectedChannel = OTemp[0];
            HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out OTemp[0], hv_UpperLimit,
                "fill");
            ho_SelectedChannel.Dispose();
            ho_SelectedChannel = OTemp[0];
            ho_ImageScaled.Dispose();
            HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageScaled, 1, 1);
            ho_ImageClosing.Dispose();
            HOperatorSet.GrayClosingShape(ho_ImageScaled, out ho_ImageClosing, 55, 60, "rectangle");
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageScaled, out ho_Region, hv_HTuples.TupleSelect(
                3), hv_HTuples.TupleSelect(4));
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region, out ho_RegionClosing1, 60, 650);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionClosing1, out ho_ConnectedRegions);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
            //opening_circle (RegionFillUp, RegionOpening3, 250.5)
            ho_RegionOpening3.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_RegionOpening3, 210, 410);
            ho_ConnectedRegions5.Dispose();
            HOperatorSet.Connection(ho_RegionOpening3, out ho_ConnectedRegions5);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions5, out ho_SelectedRegions, ((new HTuple("area")).TupleConcat(
                "rect2_len2")).TupleConcat("rect2_len1"), "and", (((new HTuple(50000)).TupleConcat(
                hv_HTuples.TupleSelect(7)))).TupleConcat(hv_HTuples.TupleSelect(5)), (((new HTuple(9900000)).TupleConcat(
                hv_HTuples.TupleSelect(8)))).TupleConcat(hv_HTuples.TupleSelect(6)));
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
            if ((int)(new HTuple(hv_Number.TupleEqual(0))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -1;
                hv_result[1] = -1;
                hv_result[2] = -1;
                hv_result[3] = -1;
                ho_Image1.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_ImageClosing.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening3.Dispose();
                ho_ConnectedRegions5.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionOpening1.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_Rectangle3.Dispose();
                ho_Cross1.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionDifference3.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionClosing10.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRect.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_ImageReduced.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Rectangle4.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionBorder.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_ConnectedRegions2.Dispose();

                return;
            }
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingRectangle1(ho_SelectedRegions, out ho_RegionClosing, 610,
                30);
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionClosing, out ho_RegionOpening1, 310,
                20);
            ho_RegionOpening2.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionOpening1, out ho_RegionOpening2, 50,
                410);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionOpening2, out ho_ConnectedRegions1);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1, "max_area",
                70);
            //
            //rectangle
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_SelectedRegions1, out ho_RegionTrans, "rectangle2");

            HOperatorSet.SmallestRectangle2(ho_RegionTrans, out hv_Row1, out hv_Column1,
                out hv_Phi2, out hv_Length12, out hv_Length22);
            ho_Rectangle3.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row1, hv_Column1, hv_Phi2, hv_Length12,
                hv_Length22);
            HOperatorSet.TupleMean(hv_Phi2, out hv_Mean);
            //if (abs(Length12-Length22)>100 or Number1=0)
            //result := [-2, -2, -2, -2]
            //return ()
            //endif
            HOperatorSet.AreaCenter(ho_RegionTrans, out hv_Area2, out hv_RowY2, out hv_ColumnX2);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY2, hv_ColumnX2, 160, hv_Phi2);
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_RegionTrans, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Mean, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreaterEqual(45))) != 0)
            {
                hv_Deg = hv_Deg - 90;
            }
            else if ((int)((new HTuple(hv_Deg.TupleGreaterEqual(0))).TupleAnd(new HTuple(hv_Deg.TupleLess(
                45)))) != 0)
            {
                hv_Deg = hv_Deg.Clone();
            }
            else if ((int)((new HTuple(hv_Deg.TupleLess(0))).TupleAnd(new HTuple(hv_Deg.TupleGreaterEqual(
                -45)))) != 0)
            {
                hv_Deg = hv_Deg.Clone();
            }
            else if ((int)((new HTuple(hv_Deg.TupleLess(-45))).TupleAnd(new HTuple(hv_Deg.TupleGreaterEqual(
                -90)))) != 0)
            {
                hv_Deg = hv_Deg + 90;
            }


            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX2);
            hv_result = hv_result.TupleConcat(hv_RowY2);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);


            ho_Image1.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
            ho_ImageScaled.Dispose();
            ho_ImageClosing.Dispose();
            ho_Region.Dispose();
            ho_RegionClosing1.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionOpening3.Dispose();
            ho_ConnectedRegions5.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionClosing.Dispose();
            ho_RegionOpening1.Dispose();
            ho_RegionOpening2.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionTrans.Dispose();
            ho_Rectangle3.Dispose();
            ho_Cross1.Dispose();
            ho_RegionErosion.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Region3.Dispose();
            ho_ConnectedRegions4.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionDifference3.Dispose();
            ho_ConnectedRegions7.Dispose();
            ho_RegionClosing10.Dispose();
            ho_ConnectedRegions8.Dispose();
            ho_RegionFillUp2.Dispose();
            ho_RegionOpening5.Dispose();
            ho_RegionOpening6.Dispose();
            ho_ConnectedRect.Dispose();
            ho_RegionErosion1.Dispose();
            ho_RegionDifference1.Dispose();
            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_RegionUnion1.Dispose();
            ho_RegionIntersection.Dispose();
            ho_RegionDifference2.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_ImageReduced.Dispose();
            ho_SelectedRegions5.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Rectangle4.Dispose();
            ho_Rectangle.Dispose();
            ho_RegionBorder.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_ConnectedRegions2.Dispose();

            return;


            //*******
            HOperatorSet.SmallestRectangle2(ho_RegionTrans, out hv_Row, out hv_Column, out hv_Phi1,
                out hv_Length11, out hv_Length21);
            ho_RegionErosion.Dispose();
            HOperatorSet.ErosionCircle(ho_RegionTrans, out ho_RegionErosion, 30.5);
            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageClosing, ho_RegionErosion, out ho_ImageReduced1
                );
            ho_Region3.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region3, 235, 255);
            ho_ConnectedRegions4.Dispose();
            HOperatorSet.Connection(ho_Region3, out ho_ConnectedRegions4);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            //opening_circle (ConnectedRegions4, RegionOpening, 8.5)
            //connection (RegionOpening, ConnectedRegions3)
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShapeStd(ho_ConnectedRegions4, out ho_SelectedRegions2, "max_area",
                70);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionDifference3.Dispose();
            HOperatorSet.Difference(ho_RegionErosion, ho_SelectedRegions2, out ho_RegionDifference3
                );
            ho_ConnectedRegions7.Dispose();
            HOperatorSet.Connection(ho_RegionDifference3, out ho_ConnectedRegions7);
            ho_RegionClosing10.Dispose();
            HOperatorSet.ClosingCircle(ho_ConnectedRegions7, out ho_RegionClosing10, 23.5);
            ho_ConnectedRegions8.Dispose();
            HOperatorSet.Connection(ho_RegionClosing10, out ho_ConnectedRegions8);
            ho_RegionFillUp2.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions8, out ho_RegionFillUp2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionOpening5.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp2, out ho_RegionOpening5, 15, 360);
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionOpening6.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionOpening5, out ho_RegionOpening6, 300,
                15);
            ho_ConnectedRect.Dispose();
            HOperatorSet.Connection(ho_RegionOpening6, out ho_ConnectedRect);

            //*******************8
            ho_RegionErosion1.Dispose();
            HOperatorSet.ErosionCircle(ho_RegionErosion, out ho_RegionErosion1, 213.5);
            ho_RegionDifference1.Dispose();
            HOperatorSet.Difference(ho_RegionErosion, ho_RegionErosion1, out ho_RegionDifference1
                );
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row, hv_Column, hv_Phi1, hv_Length11 / 1.6,
                hv_Length21);
            ho_Rectangle2.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Row, hv_Column, hv_Phi1, hv_Length11,
                hv_Length21 / 1.6);
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionUnion1.Dispose();
            HOperatorSet.Union2(ho_Rectangle1, ho_Rectangle2, out ho_RegionUnion1);

            ho_RegionIntersection.Dispose();
            HOperatorSet.Intersection(ho_Rectangle1, ho_Rectangle2, out ho_RegionIntersection
                );
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionDifference2.Dispose();
            HOperatorSet.Difference(ho_RegionUnion1, ho_RegionIntersection, out ho_RegionDifference2
                );
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_RegionDifference1, ho_RegionDifference2, out ho_RegionIntersection1
                );
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageScaled, ho_RegionDifference1, out ho_ImageReduced
                );

            ho_SelectedRegions5.Dispose();
            HOperatorSet.SelectShapeStd(ho_ConnectedRect, out ho_SelectedRegions5, "max_area",
                70);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_SelectedRegions5, out ho_RegionTrans1, "convex");
            HOperatorSet.CountObj(ho_RegionTrans1, out hv_Number1);
            HOperatorSet.SmallestRectangle2(ho_RegionTrans1, out hv_Row3, out hv_Column3,
                out hv_Phi, out hv_Length1, out hv_Length2);
            ho_Rectangle4.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_Row3, hv_Column3, hv_Phi, hv_Length1 - 80,
                hv_Length2);
            HOperatorSet.SmallestRectangle2(ho_Rectangle4, out hv_Row2, out hv_Column2, out hv_Phi3,
                out hv_Length13, out hv_Length23);
            HOperatorSet.TupleDeg(hv_Phi3, out hv_Deg1);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row3, hv_Column3, hv_Phi, hv_Length1 + 50,
                500);
            ho_RegionBorder.Dispose();
            HOperatorSet.Boundary(ho_RegionTrans1, out ho_RegionBorder, "inner");
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_RegionBorder, ho_Rectangle, out ho_RegionIntersection2
                );
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionIntersection2, out ho_ConnectedRegions2);
            HOperatorSet.SmallestRectangle2(ho_ConnectedRegions2, out hv_Row1, out hv_Column1,
                out hv_Phi2, out hv_Length12, out hv_Length22);
            ho_Rectangle3.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row1, hv_Column1, hv_Phi2, hv_Length12,
                hv_Length22);
            HOperatorSet.TupleMean(hv_Phi2, out hv_Mean);
            if ((int)((new HTuple(((((hv_Length1 - hv_Length2)).TupleAbs())).TupleGreater(100))).TupleOr(
                new HTuple(hv_Number1.TupleEqual(0)))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -2;
                hv_result[1] = -2;
                hv_result[2] = -2;
                hv_result[3] = -2;
                ho_Image1.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_ImageClosing.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening3.Dispose();
                ho_ConnectedRegions5.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionOpening1.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_Rectangle3.Dispose();
                ho_Cross1.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionDifference3.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionClosing10.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRect.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_ImageReduced.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Rectangle4.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionBorder.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_ConnectedRegions2.Dispose();

                return;
            }
            HOperatorSet.AreaCenter(ho_RegionTrans1, out hv_Area2, out hv_RowY2, out hv_ColumnX2);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY2, hv_ColumnX2, 160, hv_Phi1);
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_RegionTrans1, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Mean, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreaterEqual(45))) != 0)
            {
                hv_Deg = hv_Deg - 90;
            }
            else if ((int)((new HTuple(hv_Deg.TupleGreaterEqual(0))).TupleAnd(new HTuple(hv_Deg.TupleLess(
                45)))) != 0)
            {
                hv_Deg = hv_Deg.Clone();
            }
            else if ((int)((new HTuple(hv_Deg.TupleLess(0))).TupleAnd(new HTuple(hv_Deg.TupleGreaterEqual(
                -45)))) != 0)
            {
                hv_Deg = hv_Deg.Clone();
            }
            else if ((int)((new HTuple(hv_Deg.TupleLess(-45))).TupleAnd(new HTuple(hv_Deg.TupleGreaterEqual(
                -90)))) != 0)
            {
                hv_Deg = hv_Deg + 90;
            }


            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX2);
            hv_result = hv_result.TupleConcat(hv_RowY2);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);


            ho_Image1.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
            ho_ImageScaled.Dispose();
            ho_ImageClosing.Dispose();
            ho_Region.Dispose();
            ho_RegionClosing1.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionOpening3.Dispose();
            ho_ConnectedRegions5.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionClosing.Dispose();
            ho_RegionOpening1.Dispose();
            ho_RegionOpening2.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionTrans.Dispose();
            ho_Rectangle3.Dispose();
            ho_Cross1.Dispose();
            ho_RegionErosion.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Region3.Dispose();
            ho_ConnectedRegions4.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionDifference3.Dispose();
            ho_ConnectedRegions7.Dispose();
            ho_RegionClosing10.Dispose();
            ho_ConnectedRegions8.Dispose();
            ho_RegionFillUp2.Dispose();
            ho_RegionOpening5.Dispose();
            ho_RegionOpening6.Dispose();
            ho_ConnectedRect.Dispose();
            ho_RegionErosion1.Dispose();
            ho_RegionDifference1.Dispose();
            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_RegionUnion1.Dispose();
            ho_RegionIntersection.Dispose();
            ho_RegionDifference2.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_ImageReduced.Dispose();
            ho_SelectedRegions5.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Rectangle4.Dispose();
            ho_Rectangle.Dispose();
            ho_RegionBorder.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_ConnectedRegions2.Dispose();

            return;
        }

        public void get_blackMaterial_result(HObject ho_Image, out HObject ho_outRegion,
      out HTuple hv_result)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_SelectedChannel, ho_LowerRegion;
            HObject ho_UpperRegion, ho_ImageScaled, ho_Region, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_SelectedRegions, ho_RegionErosion;
            HObject ho_ROI_1_0, ho_ROI_1_1, ho_RegionUnion, ho_ImageResult;
            HObject ho_ImageReduced, ho_Region1, ho_RegionClosing, ho_ConnectedRegions1;
            HObject ho_RegionFillUp1, ho_SelectedRegions3, ho_RegionOpening;
            HObject ho_RegionClosing3, ho_ConnectedRegions2, ho_RegionFillUp2;
            HObject ho_RegionClosing1, ho_RegionClosing2, ho_ConnectedRegions3;
            HObject ho_RegionFillUp3, ho_SelectedRegions2, ho_RegionOpening1;
            HObject ho_ConnectedRegions4, ho_SelectedRegions1, ho_Cross1;

            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = ho_Image.CopyObj(1, -1);


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_LowerLimit = new HTuple();
            HTuple hv_UpperLimit = new HTuple(), hv_Index = null, hv_Mult = null;
            HTuple hv_Add = null, hv_MinGray = null, hv_MaxGray = null;
            HTuple hv_Range = null, hv_Number = null, hv_Number1 = null;
            HTuple hv_Number2 = null, hv_Row3 = null, hv_Column3 = null;
            HTuple hv_Phi = null, hv_Length1 = null, hv_Length2 = null;
            HTuple hv_Area2 = null, hv_RowY2 = null, hv_ColumnX2 = null;
            HTuple hv_Deg = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ROI_1_0);
            HOperatorSet.GenEmptyObj(out ho_ROI_1_1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Cross1);

            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "HTuplesB.lib", out hv_HTuples);

            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;
            if ((int)(new HTuple((new HTuple(((hv_HTuples.TupleSelect(1))).TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_LowerLimit = (hv_HTuples.TupleSelect(1))[1];
                if (hv_HTuples == null)
                    hv_HTuples = new HTuple();
                hv_HTuples[1] = ((hv_HTuples.TupleSelect(1))).TupleSelect(0);
            }
            else
            {
                hv_LowerLimit = 0.0;
            }
            if ((int)(new HTuple((new HTuple(((hv_HTuples.TupleSelect(2))).TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_UpperLimit = (hv_HTuples.TupleSelect(2))[1];
                if (hv_HTuples == null)
                    hv_HTuples = new HTuple();
                hv_HTuples[2] = ((hv_HTuples.TupleSelect(2))).TupleSelect(0);
            }
            else
            {
                hv_UpperLimit = 255.0;
            }
            hv_Index = 1;
            hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()) / ((hv_HTuples.TupleSelect(
                2)) - (hv_HTuples.TupleSelect(1)));
            hv_Add = ((-hv_Mult) * (hv_HTuples.TupleSelect(1))) + hv_LowerLimit;
            ho_outRegion.Dispose();
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.ScaleImage(ho_Image_COPY_INP_TMP, out OTemp[0], hv_Mult, hv_Add);
            ho_Image_COPY_INP_TMP.Dispose();
            ho_Image_COPY_INP_TMP = OTemp[0];
            ho_SelectedChannel.Dispose();
            HOperatorSet.AccessChannel(ho_Image_COPY_INP_TMP, out ho_SelectedChannel, hv_Index);
            HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray,
                out hv_MaxGray, out hv_Range);
            ho_LowerRegion.Dispose();
            HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(
                hv_LowerLimit))).TupleMin(), hv_LowerLimit);
            ho_UpperRegion.Dispose();
            HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
            HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out OTemp[0], hv_LowerLimit,
                "fill");
            ho_SelectedChannel.Dispose();
            ho_SelectedChannel = OTemp[0];
            HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out OTemp[0], hv_UpperLimit,
                "fill");
            ho_SelectedChannel.Dispose();
            ho_SelectedChannel = OTemp[0];
            ho_ImageScaled.Dispose();
            HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageScaled, 1, 1);
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageScaled, out ho_Region, hv_HTuples.TupleSelect(
                3), hv_HTuples.TupleSelect(4));
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp, out ho_SelectedRegions, ((new HTuple("area")).TupleConcat(
                "rect2_len2")).TupleConcat("rect2_len1"), "and", (((new HTuple(50000)).TupleConcat(
                hv_HTuples.TupleSelect(7)))).TupleConcat(hv_HTuples.TupleSelect(5)), (((new HTuple(9.48442e+006)).TupleConcat(
                hv_HTuples.TupleSelect(8)))).TupleConcat(hv_HTuples.TupleSelect(6)));
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
            if ((int)(new HTuple(hv_Number.TupleEqual(0))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -1;
                hv_result[1] = -1;
                hv_result[2] = -1;
                hv_result[3] = -1;
                ho_Image_COPY_INP_TMP.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionErosion.Dispose();
                ho_ROI_1_0.Dispose();
                ho_ROI_1_1.Dispose();
                ho_RegionUnion.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionClosing3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionOpening1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            ho_RegionErosion.Dispose();
            HOperatorSet.ErosionCircle(ho_SelectedRegions, out ho_RegionErosion, 30.5);
            ho_ROI_1_0.Dispose();
            HOperatorSet.GenRegionRuns(out ho_ROI_1_0, ((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(230)).TupleConcat(231)).TupleConcat(232)).TupleConcat(233)).TupleConcat(
                234)).TupleConcat(235)).TupleConcat(236)).TupleConcat(237)).TupleConcat(238)).TupleConcat(
                239)).TupleConcat(240)).TupleConcat(241)).TupleConcat(242)).TupleConcat(243)).TupleConcat(
                244)).TupleConcat(245)).TupleConcat(246)).TupleConcat(247)).TupleConcat(248)).TupleConcat(
                249)).TupleConcat(250)).TupleConcat(251)).TupleConcat(252)).TupleConcat(253)).TupleConcat(
                254)).TupleConcat(255)).TupleConcat(256)).TupleConcat(257)).TupleConcat(258)).TupleConcat(
                259)).TupleConcat(260)).TupleConcat(261)).TupleConcat(262)).TupleConcat(263)).TupleConcat(
                264)).TupleConcat(265)).TupleConcat(266)).TupleConcat(267)).TupleConcat(268)).TupleConcat(
                269)).TupleConcat(270)).TupleConcat(271)).TupleConcat(272)).TupleConcat(273)).TupleConcat(
                274)).TupleConcat(275)).TupleConcat(276)).TupleConcat(277)).TupleConcat(278)).TupleConcat(
                279)).TupleConcat(280)).TupleConcat(281)).TupleConcat(282)).TupleConcat(283)).TupleConcat(
                284)).TupleConcat(285)).TupleConcat(286)).TupleConcat(287)).TupleConcat(288)).TupleConcat(
                289)).TupleConcat(290)).TupleConcat(291)).TupleConcat(292)).TupleConcat(293)).TupleConcat(
                294)).TupleConcat(295)).TupleConcat(296)).TupleConcat(297)).TupleConcat(298)).TupleConcat(
                299)).TupleConcat(300)).TupleConcat(301)).TupleConcat(302)).TupleConcat(303)).TupleConcat(
                304)).TupleConcat(305)).TupleConcat(306)).TupleConcat(307), ((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(2384)).TupleConcat(2383)).TupleConcat(2382)).TupleConcat(2381)).TupleConcat(
                2380)).TupleConcat(2374)).TupleConcat(2374)).TupleConcat(2374)).TupleConcat(
                2374)).TupleConcat(2359)).TupleConcat(2358)).TupleConcat(2357)).TupleConcat(
                2356)).TupleConcat(2355)).TupleConcat(2348)).TupleConcat(2344)).TupleConcat(
                2340)).TupleConcat(2336)).TupleConcat(2323)).TupleConcat(2322)).TupleConcat(
                2321)).TupleConcat(2320)).TupleConcat(2319)).TupleConcat(2317)).TupleConcat(
                2316)).TupleConcat(2315)).TupleConcat(2314)).TupleConcat(2313)).TupleConcat(
                2311)).TupleConcat(2309)).TupleConcat(2307)).TupleConcat(2305)).TupleConcat(
                2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(
                2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(
                2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(
                2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(
                2303)).TupleConcat(2302)).TupleConcat(2302)).TupleConcat(2302)).TupleConcat(
                2302)).TupleConcat(2302)).TupleConcat(2302)).TupleConcat(2301)).TupleConcat(
                2301)).TupleConcat(2301)).TupleConcat(2301)).TupleConcat(2301)).TupleConcat(
                2301)).TupleConcat(2301)).TupleConcat(2300)).TupleConcat(2300)).TupleConcat(
                2300)).TupleConcat(2300)).TupleConcat(2300)).TupleConcat(2300)).TupleConcat(
                2299)).TupleConcat(2299)).TupleConcat(2299)).TupleConcat(2299)).TupleConcat(
                2299)).TupleConcat(2299)).TupleConcat(2298)).TupleConcat(2298)).TupleConcat(
                2298)).TupleConcat(2298), ((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(
                2404)).TupleConcat(2404)).TupleConcat(2404)).TupleConcat(2403)).TupleConcat(
                2402)).TupleConcat(2400)).TupleConcat(2399)).TupleConcat(2399)).TupleConcat(
                2399)).TupleConcat(2399)).TupleConcat(2399)).TupleConcat(2399)).TupleConcat(
                2399)).TupleConcat(2399)).TupleConcat(2399)).TupleConcat(2399)).TupleConcat(
                2388)).TupleConcat(2387)).TupleConcat(2386)).TupleConcat(2385)).TupleConcat(
                2384)).TupleConcat(2383)).TupleConcat(2382)).TupleConcat(2380)).TupleConcat(
                2379)).TupleConcat(2373)).TupleConcat(2371)).TupleConcat(2369)).TupleConcat(
                2367)).TupleConcat(2365));
            ho_ROI_1_1.Dispose();
            HOperatorSet.GenRegionRuns(out ho_ROI_1_1, (((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(230)).TupleConcat(231)).TupleConcat(232)).TupleConcat(233)).TupleConcat(
                234)).TupleConcat(235)).TupleConcat(236)).TupleConcat(237)).TupleConcat(238)).TupleConcat(
                239)).TupleConcat(240)).TupleConcat(241)).TupleConcat(242)).TupleConcat(243)).TupleConcat(
                244)).TupleConcat(245)).TupleConcat(245)).TupleConcat(246)).TupleConcat(246)).TupleConcat(
                247)).TupleConcat(247)).TupleConcat(248)).TupleConcat(248)).TupleConcat(249)).TupleConcat(
                249)).TupleConcat(250)).TupleConcat(250)).TupleConcat(251)).TupleConcat(251)).TupleConcat(
                252)).TupleConcat(252)).TupleConcat(253)).TupleConcat(253)).TupleConcat(254)).TupleConcat(
                254)).TupleConcat(255)).TupleConcat(255)).TupleConcat(256)).TupleConcat(256)).TupleConcat(
                257)).TupleConcat(257)).TupleConcat(258)).TupleConcat(258)).TupleConcat(259)).TupleConcat(
                259)).TupleConcat(260)).TupleConcat(260)).TupleConcat(261)).TupleConcat(261)).TupleConcat(
                262)).TupleConcat(262)).TupleConcat(263)).TupleConcat(263)).TupleConcat(264)).TupleConcat(
                264)).TupleConcat(265)).TupleConcat(265)).TupleConcat(266)).TupleConcat(266)).TupleConcat(
                267)).TupleConcat(267)).TupleConcat(268)).TupleConcat(268)).TupleConcat(269)).TupleConcat(
                269)).TupleConcat(270)).TupleConcat(270)).TupleConcat(271)).TupleConcat(271)).TupleConcat(
                272)).TupleConcat(272)).TupleConcat(273)).TupleConcat(273)).TupleConcat(274)).TupleConcat(
                274)).TupleConcat(275)).TupleConcat(275)).TupleConcat(276)).TupleConcat(276)).TupleConcat(
                277)).TupleConcat(277)).TupleConcat(278)).TupleConcat(278)).TupleConcat(279)).TupleConcat(
                279)).TupleConcat(280)).TupleConcat(280)).TupleConcat(281)).TupleConcat(281)).TupleConcat(
                282)).TupleConcat(282)).TupleConcat(283)).TupleConcat(283)).TupleConcat(284)).TupleConcat(
                284)).TupleConcat(285)).TupleConcat(285)).TupleConcat(286)).TupleConcat(286)).TupleConcat(
                287)).TupleConcat(287)).TupleConcat(288)).TupleConcat(288)).TupleConcat(289)).TupleConcat(
                289)).TupleConcat(290)).TupleConcat(290)).TupleConcat(291)).TupleConcat(291)).TupleConcat(
                292)).TupleConcat(292)).TupleConcat(293)).TupleConcat(293)).TupleConcat(294)).TupleConcat(
                294)).TupleConcat(295)).TupleConcat(295)).TupleConcat(296)).TupleConcat(296)).TupleConcat(
                297)).TupleConcat(297)).TupleConcat(298)).TupleConcat(298)).TupleConcat(299)).TupleConcat(
                299)).TupleConcat(300)).TupleConcat(300)).TupleConcat(301)).TupleConcat(301)).TupleConcat(
                302)).TupleConcat(302)).TupleConcat(303)).TupleConcat(303)).TupleConcat(304)).TupleConcat(
                304)).TupleConcat(305)).TupleConcat(305)).TupleConcat(306)).TupleConcat(306)).TupleConcat(
                307)).TupleConcat(307)).TupleConcat(308)).TupleConcat(308)).TupleConcat(309)).TupleConcat(
                309)).TupleConcat(310)).TupleConcat(310)).TupleConcat(311)).TupleConcat(311)).TupleConcat(
                312)).TupleConcat(312)).TupleConcat(313)).TupleConcat(313)).TupleConcat(314)).TupleConcat(
                314)).TupleConcat(315)).TupleConcat(315)).TupleConcat(316)).TupleConcat(316)).TupleConcat(
                317)).TupleConcat(317)).TupleConcat(318)).TupleConcat(318)).TupleConcat(319)).TupleConcat(
                319)).TupleConcat(320)).TupleConcat(320)).TupleConcat(321)).TupleConcat(321)).TupleConcat(
                322)).TupleConcat(322)).TupleConcat(323)).TupleConcat(323)).TupleConcat(324)).TupleConcat(
                324)).TupleConcat(325)).TupleConcat(325)).TupleConcat(326)).TupleConcat(326)).TupleConcat(
                327)).TupleConcat(327)).TupleConcat(328)).TupleConcat(328)).TupleConcat(329)).TupleConcat(
                329)).TupleConcat(330)).TupleConcat(330)).TupleConcat(331)).TupleConcat(331)).TupleConcat(
                332)).TupleConcat(332)).TupleConcat(333)).TupleConcat(333)).TupleConcat(334)).TupleConcat(
                334)).TupleConcat(335)).TupleConcat(335)).TupleConcat(336)).TupleConcat(336)).TupleConcat(
                337)).TupleConcat(337)).TupleConcat(338)).TupleConcat(338)).TupleConcat(339)).TupleConcat(
                339)).TupleConcat(340)).TupleConcat(340)).TupleConcat(341)).TupleConcat(341)).TupleConcat(
                342)).TupleConcat(342)).TupleConcat(343)).TupleConcat(343)).TupleConcat(344)).TupleConcat(
                344)).TupleConcat(345)).TupleConcat(345)).TupleConcat(346)).TupleConcat(346)).TupleConcat(
                347)).TupleConcat(347)).TupleConcat(348)).TupleConcat(348)).TupleConcat(349)).TupleConcat(
                349)).TupleConcat(350)).TupleConcat(350)).TupleConcat(351)).TupleConcat(351)).TupleConcat(
                352)).TupleConcat(352)).TupleConcat(353)).TupleConcat(353)).TupleConcat(354)).TupleConcat(
                354)).TupleConcat(355)).TupleConcat(355)).TupleConcat(356)).TupleConcat(356)).TupleConcat(
                357)).TupleConcat(357)).TupleConcat(358)).TupleConcat(358)).TupleConcat(359)).TupleConcat(
                359)).TupleConcat(360)).TupleConcat(360)).TupleConcat(361)).TupleConcat(361)).TupleConcat(
                362)).TupleConcat(362)).TupleConcat(363)).TupleConcat(363)).TupleConcat(364)).TupleConcat(
                364)).TupleConcat(365)).TupleConcat(365)).TupleConcat(366)).TupleConcat(366)).TupleConcat(
                367)).TupleConcat(367)).TupleConcat(368)).TupleConcat(368)).TupleConcat(369)).TupleConcat(
                369)).TupleConcat(370)).TupleConcat(370)).TupleConcat(371)).TupleConcat(371)).TupleConcat(
                372)).TupleConcat(372)).TupleConcat(373)).TupleConcat(373)).TupleConcat(374)).TupleConcat(
                374)).TupleConcat(375)).TupleConcat(375)).TupleConcat(376)).TupleConcat(376)).TupleConcat(
                377)).TupleConcat(377)).TupleConcat(378)).TupleConcat(378)).TupleConcat(379)).TupleConcat(
                379)).TupleConcat(380)).TupleConcat(380)).TupleConcat(381)).TupleConcat(381)).TupleConcat(
                382)).TupleConcat(382)).TupleConcat(383)).TupleConcat(383)).TupleConcat(384)).TupleConcat(
                384)).TupleConcat(385)).TupleConcat(385)).TupleConcat(386)).TupleConcat(386)).TupleConcat(
                387)).TupleConcat(387)).TupleConcat(388)).TupleConcat(388)).TupleConcat(389)).TupleConcat(
                389)).TupleConcat(390)).TupleConcat(390)).TupleConcat(391)).TupleConcat(391)).TupleConcat(
                392)).TupleConcat(392)).TupleConcat(393)).TupleConcat(393)).TupleConcat(394)).TupleConcat(
                395)).TupleConcat(396)).TupleConcat(397)).TupleConcat(398)).TupleConcat(399)).TupleConcat(
                400)).TupleConcat(401)).TupleConcat(402)).TupleConcat(403)).TupleConcat(404)).TupleConcat(
                405)).TupleConcat(406)).TupleConcat(407)).TupleConcat(408)).TupleConcat(409)).TupleConcat(
                410)).TupleConcat(411)).TupleConcat(412)).TupleConcat(413)).TupleConcat(414)).TupleConcat(
                415)).TupleConcat(416)).TupleConcat(417)).TupleConcat(418)).TupleConcat(419)).TupleConcat(
                420)).TupleConcat(421)).TupleConcat(422)).TupleConcat(423)).TupleConcat(424)).TupleConcat(
                425)).TupleConcat(426)).TupleConcat(427)).TupleConcat(428)).TupleConcat(429)).TupleConcat(
                430)).TupleConcat(431)).TupleConcat(432)).TupleConcat(433)).TupleConcat(434)).TupleConcat(
                435)).TupleConcat(436)).TupleConcat(437)).TupleConcat(438)).TupleConcat(439)).TupleConcat(
                440)).TupleConcat(441)).TupleConcat(442)).TupleConcat(443)).TupleConcat(444)).TupleConcat(
                445)).TupleConcat(446)).TupleConcat(447)).TupleConcat(448)).TupleConcat(449)).TupleConcat(
                450)).TupleConcat(451)).TupleConcat(452)).TupleConcat(453)).TupleConcat(454)).TupleConcat(
                455)).TupleConcat(456)).TupleConcat(457)).TupleConcat(458)).TupleConcat(459)).TupleConcat(
                460)).TupleConcat(461)).TupleConcat(462)).TupleConcat(463)).TupleConcat(464)).TupleConcat(
                465)).TupleConcat(466)).TupleConcat(467)).TupleConcat(468)).TupleConcat(469)).TupleConcat(
                470)).TupleConcat(471)).TupleConcat(471)).TupleConcat(472)).TupleConcat(472)).TupleConcat(
                473)).TupleConcat(473)).TupleConcat(474)).TupleConcat(474)).TupleConcat(475)).TupleConcat(
                475)).TupleConcat(476)).TupleConcat(476)).TupleConcat(477)).TupleConcat(477)).TupleConcat(
                478)).TupleConcat(478)).TupleConcat(479)).TupleConcat(479)).TupleConcat(480)).TupleConcat(
                481)).TupleConcat(482)).TupleConcat(483)).TupleConcat(484)).TupleConcat(485)).TupleConcat(
                486)).TupleConcat(487)).TupleConcat(488)).TupleConcat(489)).TupleConcat(490)).TupleConcat(
                491)).TupleConcat(492)).TupleConcat(493)).TupleConcat(494)).TupleConcat(495)).TupleConcat(
                496)).TupleConcat(497)).TupleConcat(498)).TupleConcat(499)).TupleConcat(500)).TupleConcat(
                501)).TupleConcat(502)).TupleConcat(503)).TupleConcat(504)).TupleConcat(505)).TupleConcat(
                506)).TupleConcat(507)).TupleConcat(508)).TupleConcat(509)).TupleConcat(510)).TupleConcat(
                511)).TupleConcat(512)).TupleConcat(513)).TupleConcat(514)).TupleConcat(515)).TupleConcat(
                516)).TupleConcat(517)).TupleConcat(518)).TupleConcat(519)).TupleConcat(520)).TupleConcat(
                521)).TupleConcat(522)).TupleConcat(523)).TupleConcat(524)).TupleConcat(525)).TupleConcat(
                526)).TupleConcat(527)).TupleConcat(528)).TupleConcat(529)).TupleConcat(530)).TupleConcat(
                531)).TupleConcat(532)).TupleConcat(533)).TupleConcat(534)).TupleConcat(535)).TupleConcat(
                536)).TupleConcat(537)).TupleConcat(538)).TupleConcat(539)).TupleConcat(540)).TupleConcat(
                541)).TupleConcat(542)).TupleConcat(543)).TupleConcat(544)).TupleConcat(545)).TupleConcat(
                546)).TupleConcat(547)).TupleConcat(548)).TupleConcat(549)).TupleConcat(550)).TupleConcat(
                551)).TupleConcat(552)).TupleConcat(553)).TupleConcat(554)).TupleConcat(555)).TupleConcat(
                556)).TupleConcat(557)).TupleConcat(558)).TupleConcat(559)).TupleConcat(560)).TupleConcat(
                561)).TupleConcat(562)).TupleConcat(563)).TupleConcat(564)).TupleConcat(565)).TupleConcat(
                566)).TupleConcat(567)).TupleConcat(568)).TupleConcat(569)).TupleConcat(570)).TupleConcat(
                571)).TupleConcat(572)).TupleConcat(573)).TupleConcat(574)).TupleConcat(575)).TupleConcat(
                576)).TupleConcat(577)).TupleConcat(578)).TupleConcat(579)).TupleConcat(580)).TupleConcat(
                581)).TupleConcat(582)).TupleConcat(583)).TupleConcat(584)).TupleConcat(585)).TupleConcat(
                586)).TupleConcat(587)).TupleConcat(588)).TupleConcat(589)).TupleConcat(590)).TupleConcat(
                591)).TupleConcat(592)).TupleConcat(593)).TupleConcat(594)).TupleConcat(595)).TupleConcat(
                596)).TupleConcat(597)).TupleConcat(598)).TupleConcat(599)).TupleConcat(600)).TupleConcat(
                601)).TupleConcat(602)).TupleConcat(603)).TupleConcat(604)).TupleConcat(605)).TupleConcat(
                606)).TupleConcat(607)).TupleConcat(608)).TupleConcat(609)).TupleConcat(610),
                (((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(2207)).TupleConcat(2206)).TupleConcat(2205)).TupleConcat(2204)).TupleConcat(
                2203)).TupleConcat(2176)).TupleConcat(2175)).TupleConcat(2173)).TupleConcat(
                2172)).TupleConcat(2171)).TupleConcat(2169)).TupleConcat(2167)).TupleConcat(
                2165)).TupleConcat(2163)).TupleConcat(2151)).TupleConcat(2150)).TupleConcat(
                2247)).TupleConcat(2148)).TupleConcat(2247)).TupleConcat(2147)).TupleConcat(
                2247)).TupleConcat(2146)).TupleConcat(2247)).TupleConcat(2145)).TupleConcat(
                2258)).TupleConcat(2144)).TupleConcat(2259)).TupleConcat(2143)).TupleConcat(
                2260)).TupleConcat(2142)).TupleConcat(2261)).TupleConcat(2025)).TupleConcat(
                2262)).TupleConcat(2023)).TupleConcat(2262)).TupleConcat(2022)).TupleConcat(
                2262)).TupleConcat(2020)).TupleConcat(2262)).TupleConcat(2004)).TupleConcat(
                2262)).TupleConcat(2003)).TupleConcat(2262)).TupleConcat(2002)).TupleConcat(
                2262)).TupleConcat(2001)).TupleConcat(2262)).TupleConcat(2000)).TupleConcat(
                2262)).TupleConcat(1999)).TupleConcat(2262)).TupleConcat(1998)).TupleConcat(
                2268)).TupleConcat(1996)).TupleConcat(2268)).TupleConcat(1995)).TupleConcat(
                2268)).TupleConcat(1994)).TupleConcat(2268)).TupleConcat(1993)).TupleConcat(
                2268)).TupleConcat(1992)).TupleConcat(2268)).TupleConcat(1991)).TupleConcat(
                2268)).TupleConcat(1990)).TupleConcat(2268)).TupleConcat(1984)).TupleConcat(
                2268)).TupleConcat(1984)).TupleConcat(2268)).TupleConcat(1984)).TupleConcat(
                2268)).TupleConcat(1984)).TupleConcat(2268)).TupleConcat(1984)).TupleConcat(
                2268)).TupleConcat(1984)).TupleConcat(2269)).TupleConcat(1984)).TupleConcat(
                2269)).TupleConcat(1984)).TupleConcat(2270)).TupleConcat(1984)).TupleConcat(
                2270)).TupleConcat(1954)).TupleConcat(2271)).TupleConcat(1953)).TupleConcat(
                2271)).TupleConcat(1951)).TupleConcat(2272)).TupleConcat(1950)).TupleConcat(
                2272)).TupleConcat(1948)).TupleConcat(2273)).TupleConcat(1945)).TupleConcat(
                2273)).TupleConcat(1942)).TupleConcat(2273)).TupleConcat(1938)).TupleConcat(
                2273)).TupleConcat(1935)).TupleConcat(2273)).TupleConcat(1927)).TupleConcat(
                2273)).TupleConcat(1925)).TupleConcat(2273)).TupleConcat(1922)).TupleConcat(
                2273)).TupleConcat(1920)).TupleConcat(2273)).TupleConcat(1908)).TupleConcat(
                2273)).TupleConcat(1906)).TupleConcat(2273)).TupleConcat(1904)).TupleConcat(
                2273)).TupleConcat(1902)).TupleConcat(2273)).TupleConcat(1900)).TupleConcat(
                2273)).TupleConcat(1898)).TupleConcat(2273)).TupleConcat(1897)).TupleConcat(
                2273)).TupleConcat(1895)).TupleConcat(2273)).TupleConcat(1894)).TupleConcat(
                2273)).TupleConcat(1893)).TupleConcat(2273)).TupleConcat(1891)).TupleConcat(
                2273)).TupleConcat(1889)).TupleConcat(2273)).TupleConcat(1887)).TupleConcat(
                2273)).TupleConcat(1885)).TupleConcat(2273)).TupleConcat(1878)).TupleConcat(
                2273)).TupleConcat(1877)).TupleConcat(2273)).TupleConcat(1875)).TupleConcat(
                2273)).TupleConcat(1874)).TupleConcat(2273)).TupleConcat(1868)).TupleConcat(
                2273)).TupleConcat(1867)).TupleConcat(2273)).TupleConcat(1866)).TupleConcat(
                2273)).TupleConcat(1865)).TupleConcat(2273)).TupleConcat(1864)).TupleConcat(
                2273)).TupleConcat(1863)).TupleConcat(2273)).TupleConcat(1861)).TupleConcat(
                2274)).TupleConcat(1860)).TupleConcat(2275)).TupleConcat(1858)).TupleConcat(
                2277)).TupleConcat(1857)).TupleConcat(2278)).TupleConcat(1856)).TupleConcat(
                2278)).TupleConcat(1855)).TupleConcat(2278)).TupleConcat(1854)).TupleConcat(
                2278)).TupleConcat(1853)).TupleConcat(2278)).TupleConcat(1847)).TupleConcat(
                2278)).TupleConcat(1847)).TupleConcat(2278)).TupleConcat(1847)).TupleConcat(
                2278)).TupleConcat(1847)).TupleConcat(2278)).TupleConcat(1847)).TupleConcat(
                2278)).TupleConcat(1842)).TupleConcat(2278)).TupleConcat(1842)).TupleConcat(
                2279)).TupleConcat(1842)).TupleConcat(2279)).TupleConcat(1842)).TupleConcat(
                2280)).TupleConcat(1842)).TupleConcat(2280)).TupleConcat(1842)).TupleConcat(
                2281)).TupleConcat(1842)).TupleConcat(2281)).TupleConcat(1842)).TupleConcat(
                2282)).TupleConcat(1842)).TupleConcat(2282)).TupleConcat(1842)).TupleConcat(
                2283)).TupleConcat(1842)).TupleConcat(2284)).TupleConcat(1842)).TupleConcat(
                2284)).TupleConcat(1842)).TupleConcat(2285)).TupleConcat(1842)).TupleConcat(
                2285)).TupleConcat(1841)).TupleConcat(2286)).TupleConcat(1840)).TupleConcat(
                2286)).TupleConcat(1839)).TupleConcat(2287)).TupleConcat(1838)).TupleConcat(
                2287)).TupleConcat(1837)).TupleConcat(2288)).TupleConcat(1837)).TupleConcat(
                2288)).TupleConcat(1837)).TupleConcat(2288)).TupleConcat(1837)).TupleConcat(
                2288)).TupleConcat(1837)).TupleConcat(2288)).TupleConcat(1837)).TupleConcat(
                2288)).TupleConcat(1837)).TupleConcat(2288)).TupleConcat(1837)).TupleConcat(
                2288)).TupleConcat(1837)).TupleConcat(2288)).TupleConcat(1832)).TupleConcat(
                2288)).TupleConcat(1832)).TupleConcat(2288)).TupleConcat(1832)).TupleConcat(
                2288)).TupleConcat(1832)).TupleConcat(2288)).TupleConcat(1832)).TupleConcat(
                2288)).TupleConcat(1832)).TupleConcat(2288)).TupleConcat(1832)).TupleConcat(
                2289)).TupleConcat(1832)).TupleConcat(2289)).TupleConcat(1832)).TupleConcat(
                2289)).TupleConcat(1832)).TupleConcat(2289)).TupleConcat(1832)).TupleConcat(
                2290)).TupleConcat(1832)).TupleConcat(2290)).TupleConcat(1832)).TupleConcat(
                2290)).TupleConcat(1832)).TupleConcat(2291)).TupleConcat(1832)).TupleConcat(
                2291)).TupleConcat(1832)).TupleConcat(2291)).TupleConcat(1832)).TupleConcat(
                2291)).TupleConcat(1832)).TupleConcat(2292)).TupleConcat(1832)).TupleConcat(
                2292)).TupleConcat(1832)).TupleConcat(2292)).TupleConcat(1832)).TupleConcat(
                2292)).TupleConcat(1832)).TupleConcat(2293)).TupleConcat(1832)).TupleConcat(
                2293)).TupleConcat(1832)).TupleConcat(2293)).TupleConcat(1832)).TupleConcat(
                2294)).TupleConcat(1832)).TupleConcat(2294)).TupleConcat(1832)).TupleConcat(
                2294)).TupleConcat(1832)).TupleConcat(2295)).TupleConcat(1831)).TupleConcat(
                2295)).TupleConcat(1829)).TupleConcat(2296)).TupleConcat(1828)).TupleConcat(
                2296)).TupleConcat(1827)).TupleConcat(2296)).TupleConcat(1827)).TupleConcat(
                2297)).TupleConcat(1827)).TupleConcat(2297)).TupleConcat(1827)).TupleConcat(
                2297)).TupleConcat(1827)).TupleConcat(2298)).TupleConcat(1827)).TupleConcat(
                2298)).TupleConcat(2299)).TupleConcat(2300)).TupleConcat(2301)).TupleConcat(
                2302)).TupleConcat(2303)).TupleConcat(2305)).TupleConcat(2306)).TupleConcat(
                2307)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(
                2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(
                2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(
                2308)).TupleConcat(2308)).TupleConcat(2308)).TupleConcat(2309)).TupleConcat(
                2310)).TupleConcat(2311)).TupleConcat(2312)).TupleConcat(2313)).TupleConcat(
                2315)).TupleConcat(2316)).TupleConcat(2317)).TupleConcat(2318)).TupleConcat(
                2318)).TupleConcat(2319)).TupleConcat(2319)).TupleConcat(2319)).TupleConcat(
                2320)).TupleConcat(2320)).TupleConcat(2321)).TupleConcat(2321)).TupleConcat(
                2321)).TupleConcat(2322)).TupleConcat(2322)).TupleConcat(2322)).TupleConcat(
                2323)).TupleConcat(2323)).TupleConcat(2324)).TupleConcat(2324)).TupleConcat(
                2325)).TupleConcat(2325)).TupleConcat(2326)).TupleConcat(2326)).TupleConcat(
                2327)).TupleConcat(2327)).TupleConcat(2328)).TupleConcat(2329)).TupleConcat(
                2330)).TupleConcat(2331)).TupleConcat(2332)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2338)).TupleConcat(
                2333)).TupleConcat(2338)).TupleConcat(2333)).TupleConcat(2338)).TupleConcat(
                2333)).TupleConcat(2338)).TupleConcat(2333)).TupleConcat(2338)).TupleConcat(
                2333)).TupleConcat(2338)).TupleConcat(2333)).TupleConcat(2338)).TupleConcat(
                2333)).TupleConcat(2338)).TupleConcat(2333)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2332)).TupleConcat(2330)).TupleConcat(2329)).TupleConcat(
                2328)).TupleConcat(2328)).TupleConcat(2328)).TupleConcat(2328)).TupleConcat(
                2328)).TupleConcat(2318)).TupleConcat(2318)).TupleConcat(2318)).TupleConcat(
                2318)).TupleConcat(2318)).TupleConcat(2318)).TupleConcat(2318)).TupleConcat(
                2318)).TupleConcat(2318)).TupleConcat(2318), (((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                (new HTuple(2313)).TupleConcat(2313)).TupleConcat(2313)).TupleConcat(2313)).TupleConcat(
                2313)).TupleConcat(2313)).TupleConcat(2313)).TupleConcat(2313)).TupleConcat(
                2313)).TupleConcat(2313)).TupleConcat(2303)).TupleConcat(2303)).TupleConcat(
                2303)).TupleConcat(2303)).TupleConcat(2308)).TupleConcat(2207)).TupleConcat(
                2308)).TupleConcat(2207)).TupleConcat(2308)).TupleConcat(2207)).TupleConcat(
                2308)).TupleConcat(2207)).TupleConcat(2308)).TupleConcat(2196)).TupleConcat(
                2309)).TupleConcat(2195)).TupleConcat(2310)).TupleConcat(2194)).TupleConcat(
                2311)).TupleConcat(2193)).TupleConcat(2312)).TupleConcat(2192)).TupleConcat(
                2318)).TupleConcat(2185)).TupleConcat(2318)).TupleConcat(2182)).TupleConcat(
                2318)).TupleConcat(2180)).TupleConcat(2318)).TupleConcat(2177)).TupleConcat(
                2318)).TupleConcat(2176)).TupleConcat(2318)).TupleConcat(2176)).TupleConcat(
                2318)).TupleConcat(2176)).TupleConcat(2318)).TupleConcat(2176)).TupleConcat(
                2318)).TupleConcat(2176)).TupleConcat(2318)).TupleConcat(2165)).TupleConcat(
                2319)).TupleConcat(2164)).TupleConcat(2321)).TupleConcat(2162)).TupleConcat(
                2322)).TupleConcat(2161)).TupleConcat(2323)).TupleConcat(2160)).TupleConcat(
                2324)).TupleConcat(2158)).TupleConcat(2324)).TupleConcat(2156)).TupleConcat(
                2325)).TupleConcat(2154)).TupleConcat(2325)).TupleConcat(2152)).TupleConcat(
                2326)).TupleConcat(2139)).TupleConcat(2326)).TupleConcat(2137)).TupleConcat(
                2327)).TupleConcat(2134)).TupleConcat(2327)).TupleConcat(2132)).TupleConcat(
                2328)).TupleConcat(2130)).TupleConcat(2328)).TupleConcat(2129)).TupleConcat(
                2328)).TupleConcat(2128)).TupleConcat(2328)).TupleConcat(2127)).TupleConcat(
                2328)).TupleConcat(2126)).TupleConcat(2328)).TupleConcat(2124)).TupleConcat(
                2328)).TupleConcat(2123)).TupleConcat(2328)).TupleConcat(2122)).TupleConcat(
                2328)).TupleConcat(2121)).TupleConcat(2328)).TupleConcat(2115)).TupleConcat(
                2328)).TupleConcat(2113)).TupleConcat(2328)).TupleConcat(2111)).TupleConcat(
                2328)).TupleConcat(2109)).TupleConcat(2328)).TupleConcat(2107)).TupleConcat(
                2333)).TupleConcat(2104)).TupleConcat(2333)).TupleConcat(2101)).TupleConcat(
                2333)).TupleConcat(2099)).TupleConcat(2333)).TupleConcat(2096)).TupleConcat(
                2333)).TupleConcat(2094)).TupleConcat(2333)).TupleConcat(2093)).TupleConcat(
                2333)).TupleConcat(2092)).TupleConcat(2333)).TupleConcat(2091)).TupleConcat(
                2333)).TupleConcat(2090)).TupleConcat(2333)).TupleConcat(2088)).TupleConcat(
                2333)).TupleConcat(2086)).TupleConcat(2333)).TupleConcat(2083)).TupleConcat(
                2333)).TupleConcat(2081)).TupleConcat(2333)).TupleConcat(2079)).TupleConcat(
                2333)).TupleConcat(2077)).TupleConcat(2333)).TupleConcat(2075)).TupleConcat(
                2333)).TupleConcat(2073)).TupleConcat(2333)).TupleConcat(2071)).TupleConcat(
                2333)).TupleConcat(2038)).TupleConcat(2334)).TupleConcat(2036)).TupleConcat(
                2334)).TupleConcat(2033)).TupleConcat(2335)).TupleConcat(2031)).TupleConcat(
                2335)).TupleConcat(2029)).TupleConcat(2336)).TupleConcat(2028)).TupleConcat(
                2336)).TupleConcat(2026)).TupleConcat(2337)).TupleConcat(2025)).TupleConcat(
                2337)).TupleConcat(2024)).TupleConcat(2338)).TupleConcat(2023)).TupleConcat(
                2338)).TupleConcat(2022)).TupleConcat(2339)).TupleConcat(2020)).TupleConcat(
                2339)).TupleConcat(2019)).TupleConcat(2339)).TupleConcat(2003)).TupleConcat(
                2340)).TupleConcat(2001)).TupleConcat(2340)).TupleConcat(1999)).TupleConcat(
                2341)).TupleConcat(1997)).TupleConcat(2341)).TupleConcat(1995)).TupleConcat(
                2341)).TupleConcat(1993)).TupleConcat(2342)).TupleConcat(1991)).TupleConcat(
                2342)).TupleConcat(1989)).TupleConcat(2342)).TupleConcat(1987)).TupleConcat(
                2343)).TupleConcat(1985)).TupleConcat(2343)).TupleConcat(1982)).TupleConcat(
                2343)).TupleConcat(1980)).TupleConcat(2343)).TupleConcat(1977)).TupleConcat(
                2344)).TupleConcat(1975)).TupleConcat(2344)).TupleConcat(1968)).TupleConcat(
                2344)).TupleConcat(1967)).TupleConcat(2344)).TupleConcat(1966)).TupleConcat(
                2345)).TupleConcat(1965)).TupleConcat(2345)).TupleConcat(1964)).TupleConcat(
                2345)).TupleConcat(1962)).TupleConcat(2345)).TupleConcat(1960)).TupleConcat(
                2345)).TupleConcat(1957)).TupleConcat(2346)).TupleConcat(1955)).TupleConcat(
                2346)).TupleConcat(1944)).TupleConcat(2346)).TupleConcat(1944)).TupleConcat(
                2346)).TupleConcat(1944)).TupleConcat(2347)).TupleConcat(1944)).TupleConcat(
                2347)).TupleConcat(1944)).TupleConcat(2347)).TupleConcat(1942)).TupleConcat(
                2347)).TupleConcat(1939)).TupleConcat(2347)).TupleConcat(1937)).TupleConcat(
                2348)).TupleConcat(1934)).TupleConcat(2348)).TupleConcat(1932)).TupleConcat(
                2348)).TupleConcat(1931)).TupleConcat(2348)).TupleConcat(1930)).TupleConcat(
                2349)).TupleConcat(1929)).TupleConcat(2349)).TupleConcat(1928)).TupleConcat(
                2349)).TupleConcat(1922)).TupleConcat(2350)).TupleConcat(1921)).TupleConcat(
                2350)).TupleConcat(1919)).TupleConcat(2351)).TupleConcat(1918)).TupleConcat(
                2351)).TupleConcat(1912)).TupleConcat(2352)).TupleConcat(1911)).TupleConcat(
                2352)).TupleConcat(1910)).TupleConcat(2353)).TupleConcat(1909)).TupleConcat(
                2353)).TupleConcat(1908)).TupleConcat(2354)).TupleConcat(1903)).TupleConcat(
                2355)).TupleConcat(1903)).TupleConcat(2355)).TupleConcat(1903)).TupleConcat(
                2356)).TupleConcat(1903)).TupleConcat(2356)).TupleConcat(1902)).TupleConcat(
                2357)).TupleConcat(1901)).TupleConcat(2357)).TupleConcat(1900)).TupleConcat(
                2358)).TupleConcat(1899)).TupleConcat(2358)).TupleConcat(1898)).TupleConcat(
                2359)).TupleConcat(1893)).TupleConcat(2359)).TupleConcat(1893)).TupleConcat(
                2359)).TupleConcat(1893)).TupleConcat(2359)).TupleConcat(1893)).TupleConcat(
                2359)).TupleConcat(1887)).TupleConcat(2359)).TupleConcat(1886)).TupleConcat(
                2359)).TupleConcat(1885)).TupleConcat(2359)).TupleConcat(1884)).TupleConcat(
                2359)).TupleConcat(1883)).TupleConcat(2359)).TupleConcat(1882)).TupleConcat(
                2359)).TupleConcat(1881)).TupleConcat(2359)).TupleConcat(1879)).TupleConcat(
                2359)).TupleConcat(1878)).TupleConcat(2359)).TupleConcat(1867)).TupleConcat(
                2359)).TupleConcat(1866)).TupleConcat(2359)).TupleConcat(1865)).TupleConcat(
                2359)).TupleConcat(1864)).TupleConcat(2359)).TupleConcat(1863)).TupleConcat(
                2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(
                2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(
                2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(
                2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(
                2359)).TupleConcat(2359)).TupleConcat(2359)).TupleConcat(2358)).TupleConcat(
                2357)).TupleConcat(2355)).TupleConcat(2354)).TupleConcat(2353)).TupleConcat(
                2352)).TupleConcat(2351)).TupleConcat(2350)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2333)).TupleConcat(2349)).TupleConcat(
                2333)).TupleConcat(2349)).TupleConcat(2333)).TupleConcat(2349)).TupleConcat(
                2333)).TupleConcat(2349)).TupleConcat(2333)).TupleConcat(2349)).TupleConcat(
                2333)).TupleConcat(2349)).TupleConcat(2333)).TupleConcat(2349)).TupleConcat(
                2333)).TupleConcat(2349)).TupleConcat(2333)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(
                2349)).TupleConcat(2349)).TupleConcat(2349)).TupleConcat(2350)).TupleConcat(
                2351)).TupleConcat(2352)).TupleConcat(2353)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(
                2354)).TupleConcat(2354)).TupleConcat(2354)).TupleConcat(2353)).TupleConcat(
                2352)).TupleConcat(2351)).TupleConcat(2350)).TupleConcat(2349)).TupleConcat(
                2348)).TupleConcat(2346)).TupleConcat(2345)).TupleConcat(2343)).TupleConcat(
                2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(2338)).TupleConcat(
                2338)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(2333)).TupleConcat(
                2333)).TupleConcat(2333)).TupleConcat(2333));
            ho_RegionUnion.Dispose();
            HOperatorSet.Union2(ho_ROI_1_1, ho_ROI_1_0, out ho_RegionUnion);
            ho_ImageResult.Dispose();
            HOperatorSet.PaintRegion(ho_RegionUnion, ho_ImageScaled, out ho_ImageResult,
                0, "fill");

            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageResult, ho_RegionErosion, out ho_ImageReduced
                );
            ho_Region1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 0, 210);
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region1, out ho_RegionClosing, 35, 10);
            HOperatorSet.ClosingRectangle1(ho_RegionClosing, out OTemp[0], 10, 40);
            ho_RegionClosing.Dispose();
            ho_RegionClosing = OTemp[0];
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing, out ho_ConnectedRegions1);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions1, out ho_RegionFillUp1);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions3.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp1, out ho_SelectedRegions3, "area", "and",
                390150, 9.48442e+006);
            HOperatorSet.CountObj(ho_SelectedRegions3, out hv_Number1);
            if ((int)(new HTuple(hv_Number1.TupleEqual(0))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -11;
                hv_result[1] = -11;
                hv_result[2] = -11;
                hv_result[3] = -11;
                ho_Image_COPY_INP_TMP.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionErosion.Dispose();
                ho_ROI_1_0.Dispose();
                ho_ROI_1_1.Dispose();
                ho_RegionUnion.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionClosing3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionOpening1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_SelectedRegions3, out ho_RegionOpening, 6.5);
            ho_RegionClosing3.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionOpening, out ho_RegionClosing3, 280,
                10);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionClosing3, out ho_ConnectedRegions2);
            ho_RegionFillUp2.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions2, out ho_RegionFillUp2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionFillUp2, out ho_RegionClosing1, 30, 950);
            ho_RegionClosing2.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionClosing1, out ho_RegionClosing2, 200,
                10);
            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionClosing2, out ho_ConnectedRegions3);
            ho_RegionFillUp3.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions3, out ho_RegionFillUp3);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShapeStd(ho_RegionFillUp3, out ho_SelectedRegions2, "max_area",
                70);
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningRectangle1(ho_SelectedRegions2, out ho_RegionOpening1, 50,
                80);
            ho_ConnectedRegions4.Dispose();
            HOperatorSet.Connection(ho_RegionOpening1, out ho_ConnectedRegions4);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions4, out ho_SelectedRegions1, "area",
                "and", 290150, 9.48442e+006);
            HOperatorSet.CountObj(ho_ImageScaled, out hv_Number2);
            if ((int)(new HTuple(hv_Number2.TupleLess(1))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -12;
                hv_result[1] = -12;
                hv_result[2] = -12;
                hv_result[3] = -12;
                ho_Image_COPY_INP_TMP.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionErosion.Dispose();
                ho_ROI_1_0.Dispose();
                ho_ROI_1_1.Dispose();
                ho_RegionUnion.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionClosing3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionOpening1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            //select_shape_std (ConnectedRegions4, SelectedRegions1, 'max_area', 70)
            HOperatorSet.SmallestRectangle2(ho_SelectedRegions1, out hv_Row3, out hv_Column3,
                out hv_Phi, out hv_Length1, out hv_Length2);
            HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area2, out hv_RowY2, out hv_ColumnX2);
            if ((int)(new HTuple(((hv_Length1.TupleMax2(hv_Length2))).TupleLess(500))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -22;
                hv_result[1] = -22;
                hv_result[2] = -22;
                hv_result[3] = -22;
                ho_Image_COPY_INP_TMP.Dispose();
                ho_SelectedChannel.Dispose();
                ho_LowerRegion.Dispose();
                ho_UpperRegion.Dispose();
                ho_ImageScaled.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionErosion.Dispose();
                ho_ROI_1_0.Dispose();
                ho_ROI_1_1.Dispose();
                ho_RegionUnion.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionClosing3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionFillUp2.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionOpening1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            //gen_region_line (RegionLines1, RowY2, ColumnX2-80, RowY2, ColumnX2+80)
            //gen_region_line (RegionLines2, RowY2+80, ColumnX2, RowY2-80, ColumnX2)
            //concat_obj (RegionLines1, RegionLines2, Concat1)
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY2, hv_ColumnX2, 160, hv_Phi);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_SelectedRegions1, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreater(50))) != 0)
            {
                hv_Deg = 90 - hv_Deg;
            }
            else if ((int)(new HTuple(hv_Deg.TupleLess(-50))) != 0)
            {
                hv_Deg = -90 - hv_Deg;
            }

            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX2);
            hv_result = hv_result.TupleConcat(hv_RowY2);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);


            ho_Image_COPY_INP_TMP.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
            ho_ImageScaled.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionErosion.Dispose();
            ho_ROI_1_0.Dispose();
            ho_ROI_1_1.Dispose();
            ho_RegionUnion.Dispose();
            ho_ImageResult.Dispose();
            ho_ImageReduced.Dispose();
            ho_Region1.Dispose();
            ho_RegionClosing.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionFillUp1.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_RegionOpening.Dispose();
            ho_RegionClosing3.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_RegionFillUp2.Dispose();
            ho_RegionClosing1.Dispose();
            ho_RegionClosing2.Dispose();
            ho_ConnectedRegions3.Dispose();
            ho_RegionFillUp3.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionOpening1.Dispose();
            ho_ConnectedRegions4.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_Cross1.Dispose();

            return;
        }
        public void get_blueSticker_result_again(HObject ho_Image, out HObject ho_outRegion,
            out HTuple hv_result)
        {


            // Local iconic variables 

            HObject ho_ImageScaled, ho_Region, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_SelectedRegions, ho_RegionOpening;
            HObject ho_RegionTrans, ho_Cross1;


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_Number = null;
            HTuple hv_Row3 = null, hv_Column3 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Area2 = null;
            HTuple hv_RowY2 = null, hv_ColumnX2 = null, hv_Deg = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross1);

            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "HTuplesC.lib", out hv_HTuples);

            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;
            ho_outRegion.Dispose();
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            ho_ImageScaled.Dispose();
            HOperatorSet.ScaleImage(ho_Image, out ho_ImageScaled, 4.39655, -200);
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageScaled, out ho_Region, hv_HTuples.TupleSelect(
                3), hv_HTuples.TupleSelect(4));
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp, out ho_SelectedRegions, ((new HTuple("area")).TupleConcat(
                "rect2_len2")).TupleConcat("rect2_len1"), "and", (((new HTuple(50000)).TupleConcat(
                hv_HTuples.TupleSelect(7)))).TupleConcat(hv_HTuples.TupleSelect(5)), (((new HTuple(9.48442e+006)).TupleConcat(
                hv_HTuples.TupleSelect(8)))).TupleConcat(hv_HTuples.TupleSelect(6)));
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
            if ((int)(new HTuple(hv_Number.TupleEqual(0))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -1;
                hv_result[1] = -1;
                hv_result[2] = -1;
                hv_result[3] = -1;
                ho_ImageScaled.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionTrans.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_SelectedRegions, out ho_RegionOpening, 10.5);

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionOpening, out ho_RegionTrans, "convex");
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            HOperatorSet.SmallestRectangle2(ho_RegionTrans, out hv_Row3, out hv_Column3,
                out hv_Phi, out hv_Length1, out hv_Length2);
            HOperatorSet.AreaCenter(ho_RegionTrans, out hv_Area2, out hv_RowY2, out hv_ColumnX2);

            //gen_region_line (RegionLines1, RowY2, ColumnX2-80, RowY2, ColumnX2+80)
            //gen_region_line (RegionLines2, RowY2+80, ColumnX2, RowY2-80, ColumnX2)
            //concat_obj (RegionLines1, RegionLines2, Concat1)
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY2, hv_ColumnX2, 160, hv_Phi);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_RegionTrans, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreater(50))) != 0)
            {
                hv_Deg = 90 - hv_Deg;
            }
            else if ((int)(new HTuple(hv_Deg.TupleLess(-50))) != 0)
            {
                hv_Deg = -90 - hv_Deg;
            }

            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX2);
            hv_result = hv_result.TupleConcat(hv_RowY2);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);


            ho_ImageScaled.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionOpening.Dispose();
            ho_RegionTrans.Dispose();
            ho_Cross1.Dispose();

            return;
        }

        public void get_blueSurface_result(HObject ho_Image1, out HObject ho_outRegion,
      out HTuple hv_result)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_hoBrightImg, ho_hoImg, ho_hoImg1;
            HObject ho_ImageMean, ho_ImageSubAA, ho_ROI_0, ho_TMP_Region;
            HObject ho_ImageSub, ho_ImageEmphasize, ho_ImageOpening;
            HObject ho_Regions, ho_RegionOpening2, ho_ConnectedRegions7;
            HObject ho_RegionFillUp4, ho_RegionClosing7, ho_ConnectedRegions1;
            HObject ho_SelectedRegions, ho_RegionFillUp, ho_RegionOpening5;
            HObject ho_RegionOpening6, ho_ConnectedRegions8, ho_RegionDet0;
            HObject ho_RegionDet, ho_RegionE, ho_RegionDet1, ho_RectangleUD = null;
            HObject ho_RectangleLR = null, ho_RegionDifference, ho_RegionOpening;
            HObject ho_ConnectedRegions, ho_SortedRegionsUD, ho_RegionOpening1;
            HObject ho_SortedRegionsLR, ho_ObjectSelectedUD1, ho_ObjectSelectedUD2;
            HObject ho_ObjectSelectedLR1, ho_ObjectSelectedLR2, ho_ObjectsConcat3;
            HObject ho_RegionUnion1, ho_ImageReduced1, ho_Region, ho_ConnectedRegions2;
            HObject ho_ImageReduced, ho_Regions1, ho_RegionOpening3;
            HObject ho_ConnectedRegions3, ho_RegionFillUp1, ho_SelectedRegions1;
            HObject ho_SortedRegions, ho_SelectedAAAAAAA, ho_SelectedBBBBBB;
            HObject ho_ObjectsConcat1, ho_SelectedCCCCC, ho_SelectedDDDDD;
            HObject ho_ObjectsConcat, ho_ObjectsConcat2, ho_RegionUnion;
            HObject ho_Cross1, ho_RRegionUD1, ho_RRegionUD2, ho_RRegionLR1;
            HObject ho_RRegionLR2, ho_ResultLine, ho_XLDTrans;


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_Number2 = null;
            HTuple hv_RowC = null, hv_ColC = null, hv_Phi = null, hv_LC1 = null;
            HTuple hv_LC2 = null, hv_Area1 = null, hv_Row3 = null;
            HTuple hv_Column2 = null, hv_NumberA = null, hv_NumberB = null;
            HTuple hv_Row2 = null, hv_Column1 = null, hv_Length11 = null;
            HTuple hv_Length21 = null, hv_Area = null, hv_RowY = null;
            HTuple hv_ColumnX = null, hv_Deg = null, hv_Param = null;
            HTuple hv_InputParam = null, hv_ResultRowUD1 = null, hv_ResultColUD1 = null;
            HTuple hv_ResultRowUD2 = null, hv_ResultColUD2 = null;
            HTuple hv_ResultRowLR1 = null, hv_ResultColLR1 = null;
            HTuple hv_ResultRowLR2 = null, hv_ResultColLR2 = null;
            HTuple hv_Row = null, hv_Col = null, hv_Row1 = null, hv_Column = null;
            HTuple hv_Phi1 = null, hv_Length1 = null, hv_Length2 = null;
            HTuple hv_PointOrder = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_hoBrightImg);
            HOperatorSet.GenEmptyObj(out ho_hoImg);
            HOperatorSet.GenEmptyObj(out ho_hoImg1);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_ImageSubAA);
            HOperatorSet.GenEmptyObj(out ho_ROI_0);
            HOperatorSet.GenEmptyObj(out ho_TMP_Region);
            HOperatorSet.GenEmptyObj(out ho_ImageSub);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImageOpening);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions7);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp4);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing7);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening5);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening6);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions8);
            HOperatorSet.GenEmptyObj(out ho_RegionDet0);
            HOperatorSet.GenEmptyObj(out ho_RegionDet);
            HOperatorSet.GenEmptyObj(out ho_RegionE);
            HOperatorSet.GenEmptyObj(out ho_RegionDet1);
            HOperatorSet.GenEmptyObj(out ho_RectangleUD);
            HOperatorSet.GenEmptyObj(out ho_RectangleLR);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsUD);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsLR);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedUD1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedUD2);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedLR1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedLR2);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat3);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Regions1);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedAAAAAAA);
            HOperatorSet.GenEmptyObj(out ho_SelectedBBBBBB);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat1);
            HOperatorSet.GenEmptyObj(out ho_SelectedCCCCC);
            HOperatorSet.GenEmptyObj(out ho_SelectedDDDDD);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_Cross1);
            HOperatorSet.GenEmptyObj(out ho_RRegionUD1);
            HOperatorSet.GenEmptyObj(out ho_RRegionUD2);
            HOperatorSet.GenEmptyObj(out ho_RRegionLR1);
            HOperatorSet.GenEmptyObj(out ho_RRegionLR2);
            HOperatorSet.GenEmptyObj(out ho_ResultLine);
            HOperatorSet.GenEmptyObj(out ho_XLDTrans);

            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;
            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "HTuplesF.lib", out hv_HTuples);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            ho_hoBrightImg.Dispose();
            HOperatorSet.ScaleImage(ho_Image1, out ho_hoBrightImg, 1, -(hv_HTuples.TupleSelect(
                2)));
            ho_hoImg.Dispose();
            HOperatorSet.ScaleImage(ho_hoBrightImg, out ho_hoImg, hv_HTuples.TupleSelect(
                1), 0);
            ho_hoImg1.Dispose();
            HOperatorSet.ScaleImage(ho_hoBrightImg, out ho_hoImg1, (hv_HTuples.TupleSelect(
                1)) + 3, 0);

            ho_ImageMean.Dispose();
            HOperatorSet.MeanImage(ho_hoImg1, out ho_ImageMean, 150, 150);
            ho_ImageSubAA.Dispose();
            HOperatorSet.SubImage(ho_ImageMean, ho_hoImg1, out ho_ImageSubAA, 1, 128);

            //gen_rectangle1 (ROI_0, -0.5, 151.375, 98.9605, 2459.88)
            //gen_rectangle1 (TMP_Region, 35.6674, 2343.44, 1862.12, 2571.25)
            //union2 (ROI_0, TMP_Region, ROI_0)
            //gen_rectangle1 (ROI_1, 1893.14, 310.555, 1911.9, 2348.21)
            ho_ROI_0.Dispose();
            HOperatorSet.GenRectangle1(out ho_ROI_0, 17.5837, 45.0625, 85.3977, 2566.19);
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 4.02093, 2444.69, 1938.98, 2586.44);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 1880.21, 29.875, 1923.16, 2480.13);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 8.54186, 10.5, 1925.42, 150.125);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_ImageSub.Dispose();
            HOperatorSet.PaintRegion(ho_ROI_0, ho_hoImg, out ho_ImageSub, 255, "fill");
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_ImageSub, out ho_ImageEmphasize, 10, 10, 5);
            ho_ImageOpening.Dispose();
            HOperatorSet.GrayOpeningRect(ho_ImageEmphasize, out ho_ImageOpening, 11, 11);
            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_ImageOpening, out ho_Regions, 0, 230);
            ho_RegionOpening2.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions, out ho_RegionOpening2, 10.5);
            ho_ConnectedRegions7.Dispose();
            HOperatorSet.Connection(ho_RegionOpening2, out ho_ConnectedRegions7);
            ho_RegionFillUp4.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions7, out ho_RegionFillUp4);
            ho_RegionClosing7.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionFillUp4, out ho_RegionClosing7, 25.5);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing7, out ho_ConnectedRegions1);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_hoImg, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions, "max_area",
                70);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
            ho_RegionOpening5.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_RegionOpening5, 5, 950);
            ho_RegionOpening6.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionOpening5, out ho_RegionOpening6, 800,
                5);
            ho_ConnectedRegions8.Dispose();
            HOperatorSet.Connection(ho_RegionOpening6, out ho_ConnectedRegions8);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_hoImg, HDevWindowStack.GetActive());
            }
            ho_RegionDet0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions8, out ho_RegionDet0, "area", "and",
                50150, 6.57938e+06);
            HOperatorSet.CountObj(ho_RegionDet0, out hv_Number2);
            if ((int)(new HTuple(hv_Number2.TupleEqual(0))) != 0)
            {
                ho_hoBrightImg.Dispose();
                ho_hoImg.Dispose();
                ho_hoImg1.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSubAA.Dispose();
                ho_ROI_0.Dispose();
                ho_TMP_Region.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageOpening.Dispose();
                ho_Regions.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionFillUp4.Dispose();
                ho_RegionClosing7.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionDet0.Dispose();
                ho_RegionDet.Dispose();
                ho_RegionE.Dispose();
                ho_RegionDet1.Dispose();
                ho_RectangleUD.Dispose();
                ho_RectangleLR.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegionsUD.Dispose();
                ho_RegionOpening1.Dispose();
                ho_SortedRegionsLR.Dispose();
                ho_ObjectSelectedUD1.Dispose();
                ho_ObjectSelectedUD2.Dispose();
                ho_ObjectSelectedLR1.Dispose();
                ho_ObjectSelectedLR2.Dispose();
                ho_ObjectsConcat3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions1.Dispose();
                ho_RegionOpening3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegions.Dispose();
                ho_SelectedAAAAAAA.Dispose();
                ho_SelectedBBBBBB.Dispose();
                ho_ObjectsConcat1.Dispose();
                ho_SelectedCCCCC.Dispose();
                ho_SelectedDDDDD.Dispose();
                ho_ObjectsConcat.Dispose();
                ho_ObjectsConcat2.Dispose();
                ho_RegionUnion.Dispose();
                ho_Cross1.Dispose();
                ho_RRegionUD1.Dispose();
                ho_RRegionUD2.Dispose();
                ho_RRegionLR1.Dispose();
                ho_RRegionLR2.Dispose();
                ho_ResultLine.Dispose();
                ho_XLDTrans.Dispose();

                return;
            }
            ho_RegionDet.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionDet0, out ho_RegionDet, "rectangle2");


            HOperatorSet.DilationRectangle1(ho_RegionDet, out OTemp[0], 101, 101);
            ho_RegionDet.Dispose();
            ho_RegionDet = OTemp[0];
            ho_RegionE.Dispose();
            HOperatorSet.ErosionCircle(ho_RegionDet0, out ho_RegionE, 200);
            ho_RegionDet1.Dispose();
            HOperatorSet.Difference(ho_RegionDet, ho_RegionE, out ho_RegionDet1);
            HOperatorSet.SmallestRectangle2(ho_RegionDet1, out hv_RowC, out hv_ColC, out hv_Phi,
                out hv_LC1, out hv_LC2);
            if ((int)((new HTuple(((hv_Phi.TupleDeg())).TupleLess(45))).TupleAnd(new HTuple(((hv_Phi.TupleDeg()
                )).TupleGreater(-45)))) != 0)
            {
                ho_RectangleUD.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleUD, hv_RowC, hv_ColC, hv_Phi, hv_LC1 / 1.5,
                    hv_LC2);
                ho_RectangleLR.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleLR, hv_RowC - 150, hv_ColC, hv_Phi,
                    hv_LC1, hv_LC2 / 1.9);
            }
            else
            {
                ho_RectangleUD.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleUD, hv_RowC - 150, hv_ColC, hv_Phi - (3.1415926 / 2),
                    hv_LC1 / 1.9, hv_LC2);
                ho_RectangleLR.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleLR, hv_RowC, hv_ColC, hv_Phi - (3.1415926 / 2),
                    hv_LC1, hv_LC2 / 1.5);
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RectangleUD, ho_RegionE, out ho_RegionDifference);
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening, 50.5);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
            ho_SortedRegionsUD.Dispose();
            HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegionsUD, "first_point",
                "true", "row");
            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RectangleLR, ho_RegionE, out ho_RegionDifference);
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening1, 50.5);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionOpening1, out ho_ConnectedRegions);
            ho_SortedRegionsLR.Dispose();
            HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegionsLR, "first_point",
                "true", "column");
            ///////
            HTuple hv_Number1 = null;
            HTuple hv_Number3 = null;
            HOperatorSet.CountObj(ho_SortedRegionsUD, out hv_Number1);
            HOperatorSet.CountObj(ho_SortedRegionsLR, out hv_Number3);
            if ((int)((new HTuple(hv_Number1.TupleNotEqual(2))).TupleOr(new HTuple(hv_Number3.TupleNotEqual(
                2)))) != 0)
            {
                ho_hoBrightImg.Dispose();
                ho_hoImg.Dispose();
                ho_hoImg1.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSubAA.Dispose();
                ho_ROI_0.Dispose();
                ho_TMP_Region.Dispose();
                //ho_ROI_A.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageOpening.Dispose();
                ho_Regions.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionFillUp4.Dispose();
                ho_RegionClosing7.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionDet0.Dispose();
                ho_RegionDet.Dispose();
                ho_RegionE.Dispose();
                ho_RegionDet1.Dispose();
                ho_RectangleUD.Dispose();
                ho_RectangleLR.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegionsUD.Dispose();
                ho_RegionOpening1.Dispose();
                ho_SortedRegionsLR.Dispose();
                ho_ObjectSelectedUD1.Dispose();
                ho_ObjectSelectedUD2.Dispose();
                ho_ObjectSelectedLR1.Dispose();
                ho_ObjectSelectedLR2.Dispose();
                //ho_Recte.Dispose();
                ho_ObjectsConcat3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions2.Dispose();
                //ho_ImageResultA.Dispose();
                //ho_Region1.Dispose();
                //ho_RegionClosing.Dispose();
                //ho_RegionClosing1.Dispose();
                //ho_ConnectedRegions4.Dispose();
                //ho_RegionFillUp2.Dispose();
                //ho_SelectedRegions2.Dispose();
                //ho_RegionClosingA.Dispose();
                //ho_ConnectedRegions5.Dispose();
                //ho_SelectedRegions3.Dispose();
                //ho_Region2.Dispose();
                //ho_RegionUnion3.Dispose();
                //ho_RegionClosing2.Dispose();
                //ho_ConnectedRegions10.Dispose();
                //ho_RegionFillUp3.Dispose();
                //ho_RegionOpening7.Dispose();
                //ho_RegionOpening8.Dispose();
                //ho_ConnectedRegions6.Dispose();
                //ho_SelectedRegions4.Dispose();
                ho_Cross1.Dispose();
                //ho_RegionIntersection.Dispose();
                //ho_ConnectedRegions9.Dispose();
                //ho_RegionUnion2.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions1.Dispose();
                ho_RegionOpening3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegions.Dispose();
                ho_SelectedAAAAAAA.Dispose();
                ho_SelectedBBBBBB.Dispose();
                ho_ObjectsConcat1.Dispose();
                ho_SelectedCCCCC.Dispose();
                ho_SelectedDDDDD.Dispose();
                ho_ObjectsConcat.Dispose();
                ho_ObjectsConcat2.Dispose();
                ho_RegionUnion.Dispose();
                ho_RRegionUD1.Dispose();
                ho_RRegionUD2.Dispose();
                ho_RRegionLR1.Dispose();
                ho_RRegionLR2.Dispose();
                ho_ResultLine.Dispose();
                ho_XLDTrans.Dispose();

                return;
            }
            ho_ObjectSelectedUD1.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsUD, out ho_ObjectSelectedUD1, 1);
            ho_ObjectSelectedUD2.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsUD, out ho_ObjectSelectedUD2, 2);
            ho_ObjectSelectedLR1.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsLR, out ho_ObjectSelectedLR1, 1);
            ho_ObjectSelectedLR2.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsLR, out ho_ObjectSelectedLR2, 2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_ObjectsConcat3.Dispose();
            HOperatorSet.ConcatObj(ho_SortedRegionsUD, ho_SortedRegionsLR, out ho_ObjectsConcat3
                );
            ho_RegionUnion1.Dispose();
            HOperatorSet.Union1(ho_ObjectsConcat3, out ho_RegionUnion1);
            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageSubAA, ho_RegionUnion1, out ho_ImageReduced1
                );
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region, 200, 255);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            HOperatorSet.OpeningRectangle1(ho_Region, out OTemp[0], 3, 5);
            ho_Region.Dispose();
            ho_Region = OTemp[0];
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions2);



            //**********

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageSubAA, ho_ObjectSelectedUD1, out ho_ImageReduced
                );
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions2);
            ho_Regions1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Regions1, 0, 185);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_RegionOpening3.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions1, out ho_RegionOpening3, 1.5);
            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionOpening3, out ho_ConnectedRegions3);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions3, out ho_RegionFillUp1);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp1, out ho_SelectedRegions1, "area", "and",
                10050, 9009999);
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "first_point",
                "true", "row");
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area1, out hv_Row3, out hv_Column2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedAAAAAAA.Dispose();
            HOperatorSet.SelectShape(ho_SortedRegions, out ho_SelectedAAAAAAA, "row", "and",
                (hv_Row3.TupleMax()) - 20, (hv_Row3.TupleMax()) + 20);

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ObjectSelectedUD2, HDevWindowStack.GetActive());
            }
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageSubAA, ho_ObjectSelectedUD2, out ho_ImageReduced
                );
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions2);
            ho_Regions1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Regions1, 0, 182);
            ho_RegionOpening3.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions1, out ho_RegionOpening3, 1.5);
            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionOpening3, out ho_ConnectedRegions3);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions3, out ho_RegionFillUp1);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp1, out ho_SelectedRegions1, "area", "and",
                10050, 9009999);
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "first_point",
                "true", "row");
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area1, out hv_Row3, out hv_Column2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedBBBBBB.Dispose();
            HOperatorSet.SelectShape(ho_SortedRegions, out ho_SelectedBBBBBB, "row", "and",
                (hv_Row3.TupleMin()) - 20, (hv_Row3.TupleMin()) + 20);
            ho_ObjectsConcat1.Dispose();
            HOperatorSet.ConcatObj(ho_SelectedBBBBBB, ho_SelectedAAAAAAA, out ho_ObjectsConcat1
                );
            HOperatorSet.CountObj(ho_ObjectsConcat1, out hv_NumberA);



            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ObjectSelectedLR2, HDevWindowStack.GetActive());
            }
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageSubAA, ho_ObjectSelectedLR2, out ho_ImageReduced
                );
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions2);
            ho_Regions1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Regions1, 0, 162);
            ho_RegionOpening3.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions1, out ho_RegionOpening3, 1.5);
            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionOpening3, out ho_ConnectedRegions3);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions3, out ho_RegionFillUp1);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp1, out ho_SelectedRegions1, "area", "and",
                10050, 9009999);
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "first_point",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area1, out hv_Row3, out hv_Column2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedCCCCC.Dispose();
            HOperatorSet.SelectShape(ho_SortedRegions, out ho_SelectedCCCCC, "column", "and",
                (hv_Column2.TupleMin()) - 20, (hv_Column2.TupleMin()) + 20);


            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ObjectSelectedLR1, HDevWindowStack.GetActive());
            }
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageSubAA, ho_ObjectSelectedLR1, out ho_ImageReduced
                );
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions2);
            ho_Regions1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Regions1, 0, 190);
            ho_RegionOpening3.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions1, out ho_RegionOpening3, 1.5);
            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionOpening3, out ho_ConnectedRegions3);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions3, out ho_RegionFillUp1);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_RegionFillUp1, out ho_SelectedRegions1, "area", "and",
                10050, 9009999);
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, "first_point",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area1, out hv_Row3, out hv_Column2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ImageSubAA, HDevWindowStack.GetActive());
            }
            ho_SelectedDDDDD.Dispose();
            HOperatorSet.SelectShape(ho_SortedRegions, out ho_SelectedDDDDD, "column", "and",
                (hv_Column2.TupleMax()) - 20, (hv_Column2.TupleMax()) + 20);

            ho_ObjectsConcat.Dispose();
            HOperatorSet.ConcatObj(ho_SelectedDDDDD, ho_SelectedCCCCC, out ho_ObjectsConcat
                );
            ho_ObjectsConcat2.Dispose();
            HOperatorSet.ConcatObj(ho_ObjectsConcat1, ho_ObjectsConcat, out ho_ObjectsConcat2
                );
            HOperatorSet.CountObj(ho_ObjectsConcat2, out hv_NumberB);
            if ((int)(new HTuple(hv_NumberB.TupleNotEqual(4))) != 0)
            {
                ho_hoBrightImg.Dispose();
                ho_hoImg.Dispose();
                ho_hoImg1.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSubAA.Dispose();
                ho_ROI_0.Dispose();
                ho_TMP_Region.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageOpening.Dispose();
                ho_Regions.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionFillUp4.Dispose();
                ho_RegionClosing7.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionDet0.Dispose();
                ho_RegionDet.Dispose();
                ho_RegionE.Dispose();
                ho_RegionDet1.Dispose();
                ho_RectangleUD.Dispose();
                ho_RectangleLR.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegionsUD.Dispose();
                ho_RegionOpening1.Dispose();
                ho_SortedRegionsLR.Dispose();
                ho_ObjectSelectedUD1.Dispose();
                ho_ObjectSelectedUD2.Dispose();
                ho_ObjectSelectedLR1.Dispose();
                ho_ObjectSelectedLR2.Dispose();
                ho_ObjectsConcat3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions1.Dispose();
                ho_RegionOpening3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegions.Dispose();
                ho_SelectedAAAAAAA.Dispose();
                ho_SelectedBBBBBB.Dispose();
                ho_ObjectsConcat1.Dispose();
                ho_SelectedCCCCC.Dispose();
                ho_SelectedDDDDD.Dispose();
                ho_ObjectsConcat.Dispose();
                ho_ObjectsConcat2.Dispose();
                ho_RegionUnion.Dispose();
                ho_Cross1.Dispose();
                ho_RRegionUD1.Dispose();
                ho_RRegionUD2.Dispose();
                ho_RRegionLR1.Dispose();
                ho_RRegionLR2.Dispose();
                ho_ResultLine.Dispose();
                ho_XLDTrans.Dispose();

                return;
            }
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_ObjectsConcat2, out ho_RegionUnion);
            ho_RegionDet.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionUnion, out ho_RegionDet, "rectangle2");

            HOperatorSet.SmallestRectangle2(ho_RegionDet, out hv_Row2, out hv_Column1, out hv_Phi,
                out hv_Length11, out hv_Length21);
            HOperatorSet.AreaCenter(ho_RegionDet, out hv_Area, out hv_RowY, out hv_ColumnX);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY, hv_ColumnX, 160, hv_Phi);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_RegionDet, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreater(50))) != 0)
            {
                hv_Deg = 90 - hv_Deg;
            }
            else if ((int)(new HTuple(hv_Deg.TupleLess(-50))) != 0)
            {
                hv_Deg = -90 - hv_Deg;
            }

            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX);
            hv_result = hv_result.TupleConcat(hv_RowY);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);

            ho_hoBrightImg.Dispose();
            ho_hoImg.Dispose();
            ho_hoImg1.Dispose();
            ho_ImageMean.Dispose();
            ho_ImageSubAA.Dispose();
            ho_ROI_0.Dispose();
            ho_TMP_Region.Dispose();
            ho_ImageSub.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_ImageOpening.Dispose();
            ho_Regions.Dispose();
            ho_RegionOpening2.Dispose();
            ho_ConnectedRegions7.Dispose();
            ho_RegionFillUp4.Dispose();
            ho_RegionClosing7.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionOpening5.Dispose();
            ho_RegionOpening6.Dispose();
            ho_ConnectedRegions8.Dispose();
            ho_RegionDet0.Dispose();
            ho_RegionDet.Dispose();
            ho_RegionE.Dispose();
            ho_RegionDet1.Dispose();
            ho_RectangleUD.Dispose();
            ho_RectangleLR.Dispose();
            ho_RegionDifference.Dispose();
            ho_RegionOpening.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SortedRegionsUD.Dispose();
            ho_RegionOpening1.Dispose();
            ho_SortedRegionsLR.Dispose();
            ho_ObjectSelectedUD1.Dispose();
            ho_ObjectSelectedUD2.Dispose();
            ho_ObjectSelectedLR1.Dispose();
            ho_ObjectSelectedLR2.Dispose();
            ho_ObjectsConcat3.Dispose();
            ho_RegionUnion1.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_ImageReduced.Dispose();
            ho_Regions1.Dispose();
            ho_RegionOpening3.Dispose();
            ho_ConnectedRegions3.Dispose();
            ho_RegionFillUp1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_SortedRegions.Dispose();
            ho_SelectedAAAAAAA.Dispose();
            ho_SelectedBBBBBB.Dispose();
            ho_ObjectsConcat1.Dispose();
            ho_SelectedCCCCC.Dispose();
            ho_SelectedDDDDD.Dispose();
            ho_ObjectsConcat.Dispose();
            ho_ObjectsConcat2.Dispose();
            ho_RegionUnion.Dispose();
            ho_Cross1.Dispose();
            ho_RRegionUD1.Dispose();
            ho_RRegionUD2.Dispose();
            ho_RRegionLR1.Dispose();
            ho_RRegionLR2.Dispose();
            ho_ResultLine.Dispose();
            ho_XLDTrans.Dispose();

            return;






            hv_Param = new HTuple();
            hv_Param[0] = 0;
            hv_Param[1] = 55;
            hv_Param[2] = 1;
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 3;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionUD1.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedUD1, out ho_RRegionUD1, hv_InputParam,
                out hv_ResultRowUD1, out hv_ResultColUD1);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 2;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionUD2.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedUD2, out ho_RRegionUD2, hv_InputParam,
                out hv_ResultRowUD2, out hv_ResultColUD2);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 1;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionLR1.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedLR1, out ho_RRegionLR1, hv_InputParam,
                out hv_ResultRowLR1, out hv_ResultColLR1);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 0;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);

            ho_RRegionLR2.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedLR2, out ho_RRegionLR2, hv_InputParam,
                out hv_ResultRowLR2, out hv_ResultColLR2);

            hv_Row = new HTuple();
            hv_Row = hv_Row.TupleConcat(hv_ResultRowUD1);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowUD2);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowLR1);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowLR2);
            hv_Col = new HTuple();
            hv_Col = hv_Col.TupleConcat(hv_ResultColUD1);
            hv_Col = hv_Col.TupleConcat(hv_ResultColUD2);
            hv_Col = hv_Col.TupleConcat(hv_ResultColLR1);
            hv_Col = hv_Col.TupleConcat(hv_ResultColLR2);
            ho_ResultLine.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_ResultLine, hv_Row, hv_Col);
            HOperatorSet.SmallestRectangle2Xld(ho_ResultLine, out hv_Row1, out hv_Column,
                out hv_Phi1, out hv_Length1, out hv_Length2);
            ho_XLDTrans.Dispose();
            HOperatorSet.GenRectangle2ContourXld(out ho_XLDTrans, hv_Row1, hv_Column, hv_Phi1,
                hv_Length1, hv_Length2);

            HOperatorSet.AreaCenterXld(ho_XLDTrans, out hv_Area, out hv_RowY, out hv_ColumnX,
                out hv_PointOrder);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY, hv_ColumnX, 160, hv_Phi);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_XLDTrans, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreater(50))) != 0)
            {
                hv_Deg = 90 - hv_Deg;
            }
            else if ((int)(new HTuple(hv_Deg.TupleLess(-50))) != 0)
            {
                hv_Deg = -90 - hv_Deg;
            }

            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX);
            hv_result = hv_result.TupleConcat(hv_RowY);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);




            ho_hoBrightImg.Dispose();
            ho_hoImg.Dispose();
            ho_hoImg1.Dispose();
            ho_ImageMean.Dispose();
            ho_ImageSubAA.Dispose();
            ho_ROI_0.Dispose();
            ho_TMP_Region.Dispose();
            ho_ImageSub.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_ImageOpening.Dispose();
            ho_Regions.Dispose();
            ho_RegionOpening2.Dispose();
            ho_ConnectedRegions7.Dispose();
            ho_RegionFillUp4.Dispose();
            ho_RegionClosing7.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionOpening5.Dispose();
            ho_RegionOpening6.Dispose();
            ho_ConnectedRegions8.Dispose();
            ho_RegionDet0.Dispose();
            ho_RegionDet.Dispose();
            ho_RegionE.Dispose();
            ho_RegionDet1.Dispose();
            ho_RectangleUD.Dispose();
            ho_RectangleLR.Dispose();
            ho_RegionDifference.Dispose();
            ho_RegionOpening.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SortedRegionsUD.Dispose();
            ho_RegionOpening1.Dispose();
            ho_SortedRegionsLR.Dispose();
            ho_ObjectSelectedUD1.Dispose();
            ho_ObjectSelectedUD2.Dispose();
            ho_ObjectSelectedLR1.Dispose();
            ho_ObjectSelectedLR2.Dispose();
            ho_ObjectsConcat3.Dispose();
            ho_RegionUnion1.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_ImageReduced.Dispose();
            ho_Regions1.Dispose();
            ho_RegionOpening3.Dispose();
            ho_ConnectedRegions3.Dispose();
            ho_RegionFillUp1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_SortedRegions.Dispose();
            ho_SelectedAAAAAAA.Dispose();
            ho_SelectedBBBBBB.Dispose();
            ho_ObjectsConcat1.Dispose();
            ho_SelectedCCCCC.Dispose();
            ho_SelectedDDDDD.Dispose();
            ho_ObjectsConcat.Dispose();
            ho_ObjectsConcat2.Dispose();
            ho_RegionUnion.Dispose();
            ho_Cross1.Dispose();
            ho_RRegionUD1.Dispose();
            ho_RRegionUD2.Dispose();
            ho_RRegionLR1.Dispose();
            ho_RRegionLR2.Dispose();
            ho_ResultLine.Dispose();
            ho_XLDTrans.Dispose();

            return;
        }

        public void get_blueSurface_result1111(HObject ho_Image1, out HObject ho_outRegion,
      out HTuple hv_result)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_hoBrightImg, ho_hoImg, ho_ImageMean;
            HObject ho_ImageSubAA, ho_ROI_0, ho_TMP_Region, ho_ImageSub;
            HObject ho_ImageEmphasize, ho_ImageOpening, ho_Regions;
            HObject ho_RegionOpening2, ho_ConnectedRegions7, ho_RegionFillUp4;
            HObject ho_RegionClosing7, ho_ConnectedRegions1, ho_SelectedRegions;
            HObject ho_RegionFillUp, ho_RegionOpening5, ho_RegionOpening6;
            HObject ho_ConnectedRegions8, ho_RegionDet0, ho_RegionDet;
            HObject ho_RegionE, ho_RegionDet1, ho_RectangleUD = null;
            HObject ho_RectangleLR = null, ho_RegionDifference, ho_RegionOpening;
            HObject ho_ConnectedRegions, ho_SortedRegionsUD, ho_RegionOpening1;
            HObject ho_SortedRegionsLR, ho_ObjectSelectedUD1, ho_ObjectSelectedUD2;
            HObject ho_ObjectSelectedLR1, ho_ObjectSelectedLR2, ho_RRegionUD1;
            HObject ho_RRegionUD2, ho_RRegionLR1, ho_RRegionLR2, ho_ResultLine;
            HObject ho_XLDTrans, ho_Cross1;


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_Number2 = null;
            HTuple hv_RowC = null, hv_ColC = null, hv_Phi = null, hv_LC1 = null;
            HTuple hv_LC2 = null, hv_Param = null, hv_InputParam = null;
            HTuple hv_ResultRowUD1 = null, hv_ResultColUD1 = null;
            HTuple hv_ResultRowUD2 = null, hv_ResultColUD2 = null;
            HTuple hv_ResultRowLR1 = null, hv_ResultColLR1 = null;
            HTuple hv_ResultRowLR2 = null, hv_ResultColLR2 = null;
            HTuple hv_Row = null, hv_Col = null, hv_Row1 = null, hv_Column = null;
            HTuple hv_Phi1 = null, hv_Length1 = null, hv_Length2 = null;
            HTuple hv_Area = null, hv_RowY = null, hv_ColumnX = null;
            HTuple hv_PointOrder = null, hv_Deg = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_hoBrightImg);
            HOperatorSet.GenEmptyObj(out ho_hoImg);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_ImageSubAA);
            HOperatorSet.GenEmptyObj(out ho_ROI_0);
            HOperatorSet.GenEmptyObj(out ho_TMP_Region);
            HOperatorSet.GenEmptyObj(out ho_ImageSub);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImageOpening);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions7);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp4);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing7);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening5);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening6);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions8);
            HOperatorSet.GenEmptyObj(out ho_RegionDet0);
            HOperatorSet.GenEmptyObj(out ho_RegionDet);
            HOperatorSet.GenEmptyObj(out ho_RegionE);
            HOperatorSet.GenEmptyObj(out ho_RegionDet1);
            HOperatorSet.GenEmptyObj(out ho_RectangleUD);
            HOperatorSet.GenEmptyObj(out ho_RectangleLR);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsUD);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsLR);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedUD1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedUD2);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedLR1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelectedLR2);
            HOperatorSet.GenEmptyObj(out ho_RRegionUD1);
            HOperatorSet.GenEmptyObj(out ho_RRegionUD2);
            HOperatorSet.GenEmptyObj(out ho_RRegionLR1);
            HOperatorSet.GenEmptyObj(out ho_RRegionLR2);
            HOperatorSet.GenEmptyObj(out ho_ResultLine);
            HOperatorSet.GenEmptyObj(out ho_XLDTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross1);

            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;
            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "HTuplesF.lib", out hv_HTuples);

            ho_outRegion.Dispose();
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            ho_hoBrightImg.Dispose();
            HOperatorSet.ScaleImage(ho_Image1, out ho_hoBrightImg, 1, -(hv_HTuples.TupleSelect(
                2)));
            ho_hoImg.Dispose();
            HOperatorSet.ScaleImage(ho_hoBrightImg, out ho_hoImg, hv_HTuples.TupleSelect(
                1), 0);
            ho_ImageMean.Dispose();
            HOperatorSet.MeanImage(ho_hoImg, out ho_ImageMean, 150, 150);
            ho_ImageSubAA.Dispose();
            HOperatorSet.SubImage(ho_ImageMean, ho_hoImg, out ho_ImageSubAA, 1, 128);

            //gen_rectangle1 (ROI_0, -0.5, 151.375, 98.9605, 2459.88)
            //gen_rectangle1 (TMP_Region, 35.6674, 2343.44, 1862.12, 2571.25)
            //union2 (ROI_0, TMP_Region, ROI_0)
            //gen_rectangle1 (ROI_1, 1893.14, 310.555, 1911.9, 2348.21)
            ho_ROI_0.Dispose();
            HOperatorSet.GenRectangle1(out ho_ROI_0, 17.5837, 45.0625, 85.3977, 2566.19);
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 4.02093, 2444.69, 1938.98, 2586.44);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 1880.21, 29.875, 1923.16, 2480.13);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_TMP_Region.Dispose();
            HOperatorSet.GenRectangle1(out ho_TMP_Region, 8.54186, 10.5, 1925.42, 150.125);
            HOperatorSet.Union2(ho_ROI_0, ho_TMP_Region, out OTemp[0]);
            ho_ROI_0.Dispose();
            ho_ROI_0 = OTemp[0];
            ho_ImageSub.Dispose();
            HOperatorSet.PaintRegion(ho_ROI_0, ho_hoImg, out ho_ImageSub, 255, "fill");
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_ImageSub, out ho_ImageEmphasize, 10, 10, 5);
            ho_ImageOpening.Dispose();
            HOperatorSet.GrayOpeningRect(ho_ImageEmphasize, out ho_ImageOpening, 11, 11);
            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_ImageOpening, out ho_Regions, 0, 230);
            ho_RegionOpening2.Dispose();
            HOperatorSet.OpeningCircle(ho_Regions, out ho_RegionOpening2, 20.5);
            ho_ConnectedRegions7.Dispose();
            HOperatorSet.Connection(ho_RegionOpening2, out ho_ConnectedRegions7);
            ho_RegionFillUp4.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions7, out ho_RegionFillUp4);
            ho_RegionClosing7.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionFillUp4, out ho_RegionClosing7, 25.5);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing7, out ho_ConnectedRegions1);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions, "max_area",
                70);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
            ho_RegionOpening5.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_RegionOpening5, 5, 950);
            ho_RegionOpening6.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionOpening5, out ho_RegionOpening6, 800,
                5);
            ho_ConnectedRegions8.Dispose();
            HOperatorSet.Connection(ho_RegionOpening6, out ho_ConnectedRegions8);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_hoImg, HDevWindowStack.GetActive());
            }
            ho_RegionDet0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions8, out ho_RegionDet0, "area", "and",
                50150, 6.57938e+06);
            HOperatorSet.CountObj(ho_RegionDet0, out hv_Number2);
            if ((int)(new HTuple(hv_Number2.TupleEqual(0))) != 0)
            {
                ho_hoBrightImg.Dispose();
                ho_hoImg.Dispose();
                ho_ImageMean.Dispose();
                ho_ImageSubAA.Dispose();
                ho_ROI_0.Dispose();
                ho_TMP_Region.Dispose();
                ho_ImageSub.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageOpening.Dispose();
                ho_Regions.Dispose();
                ho_RegionOpening2.Dispose();
                ho_ConnectedRegions7.Dispose();
                ho_RegionFillUp4.Dispose();
                ho_RegionClosing7.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOpening5.Dispose();
                ho_RegionOpening6.Dispose();
                ho_ConnectedRegions8.Dispose();
                ho_RegionDet0.Dispose();
                ho_RegionDet.Dispose();
                ho_RegionE.Dispose();
                ho_RegionDet1.Dispose();
                ho_RectangleUD.Dispose();
                ho_RectangleLR.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegionsUD.Dispose();
                ho_RegionOpening1.Dispose();
                ho_SortedRegionsLR.Dispose();
                ho_ObjectSelectedUD1.Dispose();
                ho_ObjectSelectedUD2.Dispose();
                ho_ObjectSelectedLR1.Dispose();
                ho_ObjectSelectedLR2.Dispose();
                ho_RRegionUD1.Dispose();
                ho_RRegionUD2.Dispose();
                ho_RRegionLR1.Dispose();
                ho_RRegionLR2.Dispose();
                ho_ResultLine.Dispose();
                ho_XLDTrans.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            ho_RegionDet.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionDet0, out ho_RegionDet, "rectangle2");
            HOperatorSet.DilationRectangle1(ho_RegionDet, out OTemp[0], 101, 101);
            ho_RegionDet.Dispose();
            ho_RegionDet = OTemp[0];
            ho_RegionE.Dispose();
            HOperatorSet.ErosionCircle(ho_RegionDet0, out ho_RegionE, 200);
            ho_RegionDet1.Dispose();
            HOperatorSet.Difference(ho_RegionDet, ho_RegionE, out ho_RegionDet1);
            HOperatorSet.SmallestRectangle2(ho_RegionDet1, out hv_RowC, out hv_ColC, out hv_Phi,
                out hv_LC1, out hv_LC2);
            if ((int)((new HTuple(((hv_Phi.TupleDeg())).TupleLess(45))).TupleAnd(new HTuple(((hv_Phi.TupleDeg()
                )).TupleGreater(-45)))) != 0)
            {
                ho_RectangleUD.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleUD, hv_RowC, hv_ColC, hv_Phi, hv_LC1 / 1.5,
                    hv_LC2);
                ho_RectangleLR.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleLR, hv_RowC - 150, hv_ColC, hv_Phi,
                    hv_LC1, hv_LC2 / 1.9);
            }
            else
            {
                ho_RectangleUD.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleUD, hv_RowC - 150, hv_ColC, hv_Phi - (3.1415926 / 2),
                    hv_LC1 / 1.9, hv_LC2);
                ho_RectangleLR.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleLR, hv_RowC, hv_ColC, hv_Phi - (3.1415926 / 2),
                    hv_LC1, hv_LC2 / 1.5);
            }

            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RectangleUD, ho_RegionE, out ho_RegionDifference);
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening, 50.5);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
            ho_SortedRegionsUD.Dispose();
            HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegionsUD, "first_point",
                "true", "row");
            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RectangleLR, ho_RegionE, out ho_RegionDifference);
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening1, 50.5);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionOpening1, out ho_ConnectedRegions);
            ho_SortedRegionsLR.Dispose();
            HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegionsLR, "first_point",
                "true", "column");
            ho_ObjectSelectedUD1.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsUD, out ho_ObjectSelectedUD1, 1);
            ho_ObjectSelectedUD2.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsUD, out ho_ObjectSelectedUD2, 2);
            ho_ObjectSelectedLR1.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsLR, out ho_ObjectSelectedLR1, 1);
            ho_ObjectSelectedLR2.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsLR, out ho_ObjectSelectedLR2, 2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            hv_Param = new HTuple();
            hv_Param[0] = 0;
            hv_Param[1] = 60;
            hv_Param[2] = 1;
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 3;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionUD1.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedUD1, out ho_RRegionUD1, hv_InputParam,
                out hv_ResultRowUD1, out hv_ResultColUD1);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 2;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionUD2.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedUD2, out ho_RRegionUD2, hv_InputParam,
                out hv_ResultRowUD2, out hv_ResultColUD2);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 1;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);
            ho_RRegionLR1.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedLR1, out ho_RRegionLR1, hv_InputParam,
                out hv_ResultRowLR1, out hv_ResultColLR1);
            hv_InputParam = new HTuple();
            hv_InputParam[0] = 0;
            hv_InputParam = hv_InputParam.TupleConcat(hv_Param);

            ho_RRegionLR2.Dispose();
            DetContour(ho_ImageSubAA, ho_ObjectSelectedLR2, out ho_RRegionLR2, hv_InputParam,
                out hv_ResultRowLR2, out hv_ResultColLR2);
            hv_Row = new HTuple();
            hv_Row = hv_Row.TupleConcat(hv_ResultRowUD1);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowUD2);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowLR1);
            hv_Row = hv_Row.TupleConcat(hv_ResultRowLR2);
            hv_Col = new HTuple();
            hv_Col = hv_Col.TupleConcat(hv_ResultColUD1);
            hv_Col = hv_Col.TupleConcat(hv_ResultColUD2);
            hv_Col = hv_Col.TupleConcat(hv_ResultColLR1);
            hv_Col = hv_Col.TupleConcat(hv_ResultColLR2);
            ho_ResultLine.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_ResultLine, hv_Row, hv_Col);
            HOperatorSet.SmallestRectangle2Xld(ho_ResultLine, out hv_Row1, out hv_Column,
                out hv_Phi1, out hv_Length1, out hv_Length2);
            ho_XLDTrans.Dispose();
            HOperatorSet.GenRectangle2ContourXld(out ho_XLDTrans, hv_Row1, hv_Column, hv_Phi1,
                hv_Length1, hv_Length2);

            HOperatorSet.AreaCenterXld(ho_XLDTrans, out hv_Area, out hv_RowY, out hv_ColumnX,
                out hv_PointOrder);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_RowY, hv_ColumnX, 160, hv_Phi);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image1, HDevWindowStack.GetActive());
            }
            ho_outRegion.Dispose();
            HOperatorSet.ConcatObj(ho_Cross1, ho_XLDTrans, out ho_outRegion);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            if ((int)(new HTuple(hv_Deg.TupleGreater(50))) != 0)
            {
                hv_Deg = 90 - hv_Deg;
            }
            else if ((int)(new HTuple(hv_Deg.TupleLess(-50))) != 0)
            {
                hv_Deg = -90 - hv_Deg;
            }

            hv_result = new HTuple();
            hv_result = hv_result.TupleConcat(hv_ColumnX);
            hv_result = hv_result.TupleConcat(hv_RowY);
            hv_result = hv_result.TupleConcat(hv_Deg);
            hv_result = hv_result.TupleConcat(1);




            ho_hoBrightImg.Dispose();
            ho_hoImg.Dispose();
            ho_ImageMean.Dispose();
            ho_ImageSubAA.Dispose();
            ho_ROI_0.Dispose();
            ho_TMP_Region.Dispose();
            ho_ImageSub.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_ImageOpening.Dispose();
            ho_Regions.Dispose();
            ho_RegionOpening2.Dispose();
            ho_ConnectedRegions7.Dispose();
            ho_RegionFillUp4.Dispose();
            ho_RegionClosing7.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionOpening5.Dispose();
            ho_RegionOpening6.Dispose();
            ho_ConnectedRegions8.Dispose();
            ho_RegionDet0.Dispose();
            ho_RegionDet.Dispose();
            ho_RegionE.Dispose();
            ho_RegionDet1.Dispose();
            ho_RectangleUD.Dispose();
            ho_RectangleLR.Dispose();
            ho_RegionDifference.Dispose();
            ho_RegionOpening.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SortedRegionsUD.Dispose();
            ho_RegionOpening1.Dispose();
            ho_SortedRegionsLR.Dispose();
            ho_ObjectSelectedUD1.Dispose();
            ho_ObjectSelectedUD2.Dispose();
            ho_ObjectSelectedLR1.Dispose();
            ho_ObjectSelectedLR2.Dispose();
            ho_RRegionUD1.Dispose();
            ho_RRegionUD2.Dispose();
            ho_RRegionLR1.Dispose();
            ho_RRegionLR2.Dispose();
            ho_ResultLine.Dispose();
            ho_XLDTrans.Dispose();
            ho_Cross1.Dispose();

            return;
        }


        public void DetContour(HObject ho_Image, HObject ho_Region, out HObject ho_ResultLine,
        HTuple hv_SetParam, out HTuple hv_ResultLineRow, out HTuple hv_ResultLineCol)
        {



            // Local iconic variables 

            HObject ho_Contour;


            // Local control variables 

            HTuple hv_DirFlag = null, hv_EdgeType = null;
            HTuple hv_EdgeIntensity = null, hv_Transition = new HTuple();
            HTuple hv_SmoothData = null, hv_Width = null, hv_Height = null;
            HTuple hv_RowDet = null, hv_ColDet = null, hv_DetNum = null;
            HTuple hv_SegmentWidth = null, hv_Row0 = new HTuple();
            HTuple hv_Col0 = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Col1 = new HTuple(), hv_RDis = new HTuple();
            HTuple hv_Len = new HTuple(), hv_ColCenter = new HTuple();
            HTuple hv_i = new HTuple(), hv_Row = new HTuple(), hv_Col = new HTuple();
            HTuple hv_MeasureHandle = new HTuple(), hv_RowEdge = new HTuple();
            HTuple hv_ColEdge = new HTuple(), hv_ExpDefaultCtrlDummyVar = new HTuple();
            HTuple hv_CDis = new HTuple(), hv_RowCenter = new HTuple();
            HTuple hv_RowBegin = null, hv_ColBegin = null, hv_RowEnd = null;
            HTuple hv_ColEnd = null, hv_Nr = null, hv_Nc = null, hv_Dist = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ResultLine);
            HOperatorSet.GenEmptyObj(out ho_Contour);

            hv_ResultLineRow = new HTuple();
            hv_ResultLineCol = new HTuple();
            //读取参数
            hv_DirFlag = hv_SetParam[0];
            hv_EdgeType = hv_SetParam[1];
            hv_EdgeIntensity = hv_SetParam[2];
            if ((int)(new HTuple(hv_EdgeType.TupleEqual(0))) != 0)
            {
                hv_Transition = "positive";
            }
            else
            {
                hv_Transition = "negative";
            }
            hv_SmoothData = hv_SetParam[3];
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Region, HDevWindowStack.GetActive());
            }
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_RowDet = new HTuple();
            hv_ColDet = new HTuple();
            hv_DetNum = 300;
            hv_SegmentWidth = 5;
            if ((int)(new HTuple(hv_DirFlag.TupleEqual(0))) != 0)
            {
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row0, out hv_Col0, out hv_Row1,
                    out hv_Col1);
                hv_RDis = ((hv_Row1 - hv_Row0) + 0.0) / (hv_DetNum + 1);
                hv_Len = ((hv_Col1 - hv_Col0) + 0.0) / 2;
                hv_ColCenter = ((hv_Col1 + hv_Col0) + 0.0) / 2;
                HTuple end_val21 = hv_DetNum;
                HTuple step_val21 = 1;
                for (hv_i = 1; hv_i.Continue(end_val21, step_val21); hv_i = hv_i.TupleAdd(step_val21))
                {
                    hv_Row = hv_Row0 + (hv_i * hv_RDis);
                    hv_Col = hv_ColCenter.Clone();
                    HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Col, (new HTuple(0)).TupleRad()
                        , hv_Len, hv_SegmentWidth, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                    HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, hv_SmoothData, hv_EdgeIntensity,
                        hv_Transition, "first", out hv_RowEdge, out hv_ColEdge, out hv_ExpDefaultCtrlDummyVar,
                        out hv_ExpDefaultCtrlDummyVar);
                    HOperatorSet.TupleConcat(hv_RowDet, hv_RowEdge, out hv_RowDet);
                    HOperatorSet.TupleConcat(hv_ColDet, hv_ColEdge, out hv_ColDet);
                    HOperatorSet.CloseMeasure(hv_MeasureHandle);
                }
            }
            else if ((int)(new HTuple(hv_DirFlag.TupleEqual(1))) != 0)
            {
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row0, out hv_Col0, out hv_Row1,
                    out hv_Col1);
                hv_RDis = ((hv_Row1 - hv_Row0) + 0.0) / (hv_DetNum + 1);
                hv_Len = ((hv_Col1 - hv_Col0) + 0.0) / 2;
                hv_ColCenter = (hv_Col1 + hv_Col0) / 2;
                HTuple end_val35 = hv_DetNum;
                HTuple step_val35 = 1;
                for (hv_i = 1; hv_i.Continue(end_val35, step_val35); hv_i = hv_i.TupleAdd(step_val35))
                {
                    hv_Row = hv_Row0 + (hv_i * hv_RDis);
                    hv_Col = hv_ColCenter.Clone();
                    HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Col, (new HTuple(180)).TupleRad()
                        , hv_Len, hv_SegmentWidth, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                    HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, hv_SmoothData, hv_EdgeIntensity,
                        hv_Transition, "first", out hv_RowEdge, out hv_ColEdge, out hv_ExpDefaultCtrlDummyVar,
                        out hv_ExpDefaultCtrlDummyVar);
                    HOperatorSet.TupleConcat(hv_RowDet, hv_RowEdge, out hv_RowDet);
                    HOperatorSet.TupleConcat(hv_ColDet, hv_ColEdge, out hv_ColDet);
                    HOperatorSet.CloseMeasure(hv_MeasureHandle);
                }
            }
            else if ((int)(new HTuple(hv_DirFlag.TupleEqual(2))) != 0)
            {
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row0, out hv_Col0, out hv_Row1,
                    out hv_Col1);
                hv_CDis = ((hv_Col1 - hv_Col0) + 0.0) / (hv_DetNum + 1);
                hv_Len = ((hv_Row1 - hv_Row0) + 0.0) / 2;
                hv_RowCenter = ((hv_Row1 + hv_Row0) + 0.0) / 2;
                HTuple end_val49 = hv_DetNum;
                HTuple step_val49 = 1;
                for (hv_i = 1; hv_i.Continue(end_val49, step_val49); hv_i = hv_i.TupleAdd(step_val49))
                {
                    hv_Row = hv_RowCenter.Clone();
                    hv_Col = hv_Col0 + (hv_i * hv_CDis);
                    HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Col, (new HTuple(-90)).TupleRad()
                        , hv_Len, hv_SegmentWidth, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                    HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, hv_SmoothData, hv_EdgeIntensity,
                        hv_Transition, "first", out hv_RowEdge, out hv_ColEdge, out hv_ExpDefaultCtrlDummyVar,
                        out hv_ExpDefaultCtrlDummyVar);
                    HOperatorSet.TupleConcat(hv_RowDet, hv_RowEdge, out hv_RowDet);
                    HOperatorSet.TupleConcat(hv_ColDet, hv_ColEdge, out hv_ColDet);
                    HOperatorSet.CloseMeasure(hv_MeasureHandle);
                }
            }
            else if ((int)(new HTuple(hv_DirFlag.TupleEqual(3))) != 0)
            {
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row0, out hv_Col0, out hv_Row1,
                    out hv_Col1);
                hv_CDis = ((hv_Col1 - hv_Col0) + 0.0) / (hv_DetNum + 1);
                hv_Len = ((hv_Row1 - hv_Row0) + 0.0) / 2;
                hv_RowCenter = ((hv_Row1 + hv_Row0) + 0.0) / 2;
                HTuple end_val64 = hv_DetNum;
                HTuple step_val64 = 1;
                for (hv_i = 1; hv_i.Continue(end_val64, step_val64); hv_i = hv_i.TupleAdd(step_val64))
                {
                    hv_Row = hv_RowCenter.Clone();
                    hv_Col = hv_Col0 + (hv_i * hv_CDis);
                    HOperatorSet.GenMeasureRectangle2(hv_Row, hv_Col, (new HTuple(90)).TupleRad()
                        , hv_Len, hv_SegmentWidth, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                    //gen_rectangle2 (Rectangle, Row, Col, rad(90), Len, SegmentWidth)
                    HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, hv_SmoothData, hv_EdgeIntensity,
                        hv_Transition, "first", out hv_RowEdge, out hv_ColEdge, out hv_ExpDefaultCtrlDummyVar,
                        out hv_ExpDefaultCtrlDummyVar);
                    HOperatorSet.TupleConcat(hv_RowDet, hv_RowEdge, out hv_RowDet);
                    HOperatorSet.TupleConcat(hv_ColDet, hv_ColEdge, out hv_ColDet);
                    HOperatorSet.CloseMeasure(hv_MeasureHandle);
                }
            }
            else
            {
                ho_Contour.Dispose();

                return;
            }
            if ((int)(new HTuple((new HTuple(hv_RowDet.TupleLength())).TupleLess(1))) != 0)
            {
                ho_Contour.Dispose();

                return;
            }
            ho_Contour.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_RowDet, hv_ColDet);
            HOperatorSet.FitLineContourXld(ho_Contour, "tukey", -1, 0, 5, 2, out hv_RowBegin,
                out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr, out hv_Nc, out hv_Dist);
            ho_ResultLine.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_ResultLine, hv_RowBegin.TupleConcat(
                hv_RowEnd), hv_ColBegin.TupleConcat(hv_ColEnd));
            hv_ResultLineRow = new HTuple();
            hv_ResultLineRow = hv_ResultLineRow.TupleConcat(hv_RowBegin);
            hv_ResultLineRow = hv_ResultLineRow.TupleConcat(hv_RowEnd);
            hv_ResultLineCol = new HTuple();
            hv_ResultLineCol = hv_ResultLineCol.TupleConcat(hv_ColBegin);
            hv_ResultLineCol = hv_ResultLineCol.TupleConcat(hv_ColEnd);


            ho_Contour.Dispose();

            return;
        }



    }
}
