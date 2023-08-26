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
        public void get_blueSticker_caliberte1(HObject ho_Image, out HObject ho_outRegion,
      out HTuple hv_result)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_SelectedChannel, ho_LowerRegion;
            HObject ho_UpperRegion, ho_ImageScaled, ho_Region, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_SelectedRegions, ho_RegionOpening;
            HObject ho_RegionTrans, ho_Cross1;

            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = ho_Image.CopyObj(1, -1);


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_LowerLimit = new HTuple();
            HTuple hv_UpperLimit = new HTuple(), hv_Index = null, hv_Mult = null;
            HTuple hv_Add = null, hv_MinGray = null, hv_MaxGray = null;
            HTuple hv_Range = null, hv_Number = null, hv_Row3 = null;
            HTuple hv_Column3 = null, hv_Phi = null, hv_Length1 = null;
            HTuple hv_Length2 = null, hv_Area2 = null, hv_RowY2 = null;
            HTuple hv_ColumnX2 = null, hv_Deg = null;

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
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross1);

            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "caliberteA.lib", out hv_HTuples);
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
                "rect2_len2")).TupleConcat("rect2_len1"), "and", (((new HTuple(20000)).TupleConcat(
                hv_HTuples.TupleSelect(7)))).TupleConcat(hv_HTuples.TupleSelect(5)), (((new HTuple(9.38699e+06)).TupleConcat(
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
                ho_RegionOpening.Dispose();
                ho_RegionTrans.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_SelectedRegions, out ho_RegionOpening, 10.5);
            //rectangle
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
            }
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionOpening, out ho_RegionTrans, "rectangle2");
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
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
                //dev_display (Image)
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


            ho_Image_COPY_INP_TMP.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
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
        public void get_blueSticker_caliberte2(HObject ho_Image, out HObject ho_outRegion,
     out HTuple hv_result)
        {


            // Local iconic variables 

            HObject ho_ImageEmphasize, ho_Regions, ho_ConnectedRegions2;
            HObject ho_SelectedRegions, ho_RegionFillUp, ho_RegionClosing;
            HObject ho_RegionOpening1, ho_RegionOpening2, ho_RegionTrans;
            HObject ho_Cross1;


            // Local control variables 

            HTuple hv_tray = null, hv_HTuples = null, hv_Number = null;
            HTuple hv_Row3 = null, hv_Column3 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Area2 = null;
            HTuple hv_RowY2 = null, hv_ColumnX2 = null, hv_Deg = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross1);

            hv_tray = "168LCD";
            HOperatorSet.ReadTuple(("D:/AAAAAAAALCD/" + hv_tray) + "caliberteE.lib", out hv_HTuples);
            hv_result = new HTuple();
            hv_result[0] = 0;
            hv_result[1] = 0;
            hv_result[2] = 0;
            hv_result[3] = 0;

            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 30, 25, 7);
            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize, out ho_Regions, 89, 187);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions2);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions, ((new HTuple("area")).TupleConcat(
                "rect2_len2")).TupleConcat("rect2_len1"), "and", (((new HTuple(20000)).TupleConcat(
                hv_HTuples.TupleSelect(7)))).TupleConcat(hv_HTuples.TupleSelect(5)), (((new HTuple(9.38699e+06)).TupleConcat(
                hv_HTuples.TupleSelect(8)))).TupleConcat(hv_HTuples.TupleSelect(6)));
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
            if ((int)(new HTuple(hv_Number.TupleEqual(0))) != 0)
            {
                hv_result = new HTuple();
                hv_result[0] = -1;
                hv_result[1] = -1;
                hv_result[2] = -1;
                hv_result[3] = -1;
                ho_ImageEmphasize.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionOpening1.Dispose();
                ho_RegionOpening2.Dispose();
                ho_RegionTrans.Dispose();
                ho_Cross1.Dispose();

                return;
            }
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionFillUp, out ho_RegionClosing, 53.5);
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionClosing, out ho_RegionOpening1, 410,
                10);
            ho_RegionOpening2.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionOpening1, out ho_RegionOpening2, 10,
                410);


            //rectangle
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionOpening2, out ho_RegionTrans, "rectangle2");
            if (HDevWindowStack.IsOpen())
            {
                //dev_display (Image)
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
                //dev_display (Image)
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


            ho_ImageEmphasize.Dispose();
            ho_Regions.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_RegionClosing.Dispose();
            ho_RegionOpening1.Dispose();
            ho_RegionOpening2.Dispose();
            ho_RegionTrans.Dispose();
            ho_Cross1.Dispose();

            return;
        }


    }


}
