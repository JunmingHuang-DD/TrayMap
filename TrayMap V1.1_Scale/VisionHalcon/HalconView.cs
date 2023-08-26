using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace VisionHalcon
{
    public partial class HalconView : UserControl
    {
        private bool _DrawCross = true;

        /// <summary>
        /// the Halcon window
        /// </summary>
        public HWindowControl HWind { get { return hWindowControl1; } }


        /// <summary>
        /// set the display image size
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size ImageSize
        {
            get { return hWindowControl1.ImagePart.Size; }
            set { hWindowControl1.ImagePart = new Rectangle(new Point(), value); }
        }



        public HalconView()
        {
            InitializeComponent();
        }

        private void HalconView_Resize(object sender, EventArgs e)
        {
            hWindowControl1.Size = this.Size;
            hWindowControl1.WindowSize = this.Size;
        }


        //private void DrawCross()
        //{
        //    //hWindowControl1.HalconWindow.DispCross(Width / 2.0, Height / 2, Width, 0);
        //}

        //private void hWindowControl1_Paint(object sender, PaintEventArgs e)
        //{
        //    //if (_DrawCross)
        //    //{
        //    //    Pen pen = new Pen(Brushes.Red, 3);
        //    //    e.Graphics.DrawLine(pen, this.Width / 2, 0, this.Width / 2, this.Height);
        //    //    e.Graphics.DrawLine(pen, 0, this.Height / 2, this.Width, this.Height / 2);
        //    //}
        //}
    }
}
