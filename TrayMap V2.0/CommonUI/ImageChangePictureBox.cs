using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    /// <summary>
    /// 用于显示在多张小图片组成的图片中的某一张
    /// 类似于显示窗体上的Close按钮
    /// </summary>
    public class ImageChangePictureBox : PictureBox
    {
        /// <summary>
        /// 需要显示的图片序号
        /// </summary>
        private int _ImageIndex = 0;

        /// <summary>
        /// 图片的间隔，X方向
        /// </summary>
        public int ImageOffset { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (IsDisposed) return;

            if (Image != null && _ImageIndex > 0)
            {
                using (Matrix transform = pe.Graphics.Transform)
                {
                    transform.Translate(-ImageOffset * _ImageIndex, 0);

                    pe.Graphics.Transform = transform;
                }
            }

            base.OnPaint(pe);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _ImageIndex = 1;

            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _ImageIndex = 0;

            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _ImageIndex = 2;

            Invalidate();

            base.OnMouseDown(e);
        }
    }
}
