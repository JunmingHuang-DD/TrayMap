using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motion
{
   public class PointDataElement
    {
        public int Index;
        public double X;
        public double Y;
        public double R;
        public string Name;
        public int row;
        public int col;
        public int BoardIndex;
        public int Active;
    }

    public class jsonFile
    {
        public string id;
        public string axisName_x;
        public string axisName_y;
        /// <summary>
        /// 参考 示教原点名称
        /// </summary>
        public string RefPoint;
        public bool s_loop;
        public bool left_to_right;
        public double pitch_x;
        public double pitch_y;
        public int rowIndex;
        public int colIndex;
        public int totalRow;
        public int totalCol;

        public List<PointDataElement> module;
        public List<PointDataElement> total;
    }
}
