using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUI.RectBlock
{
    public class RectTraySet
    {
        /// <summary>
        /// 产品有无集合 true 当前位置有产品;false 当前位置为空
        /// </summary>
        public List<bool> StatusList = new List<bool>();

        public int Length { get; set; }

        public RectTraySet(int num)
        {
            for (int i = 0; i < num; i++)
            {
                StatusList.Add(false);
            }

            Length = num;
        }

    }
}
