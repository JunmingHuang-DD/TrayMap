using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonUI
{
    public partial class RoundWafer : UserControl
    {

        private Size _waferSize = new Size(60, 60); //Wafer 的直径 100mm

        private PointF _mPP = new PointF(0, 0);  //每像素对应的mm

        private PointF _blockSize = new PointF(0.5f, 5);

        private List<RectangleF> _showRect = new List<RectangleF>();

        public RoundWafer()
        {
            InitializeComponent();

            this.Resize += new EventHandler(RoundWafer_SizeChanged);

            _mPP = new PointF((float)(_waferSize.Width*1.0 / this.Width), (float)(_waferSize.Height *1.0/ this.Height));

            for(int i = 0;i<10;i++)
            {
                _showRect.Add(new RectangleF((float)(_waferSize.Width/2 / _mPP.X - (0.8 /_mPP.X)*i) , _waferSize.Height/2 / _mPP.Y, _blockSize.X / _mPP.X, _blockSize.Y / _mPP.Y));
            }
            

        }

        private void RoundWafer_Paint(object sender, PaintEventArgs e)
        {

            RectangleF rect = new RectangleF(0,0, this.Width, this.Height);
            e.Graphics.FillEllipse(new SolidBrush(Color.White), rect);

            foreach(var r in _showRect)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), r);
            }

            e.Graphics.Dispose();
        }

        private void RoundWafer_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void RoundWafer_Move(object sender, EventArgs e)
        {

        }


        private void RoundWafer_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
