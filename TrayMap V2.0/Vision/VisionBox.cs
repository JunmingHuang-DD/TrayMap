using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Emgu.CV.UI;
using System.Drawing;

namespace Incube.Vision
{
    public partial class VisionBox : PanAndZoomPictureBox//ImageBox
    {

        public bool ShowCross { get; set; }

        //private Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>[] _Emgu_Image_Arr = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>[2];
        //private Image [] _Image_Arr = new Image[2];
        //private int _use = 0;

        //private void Delete_Emgu_image()
        //{
        //    int iDelete = _use == 0 ? 1 : 0;
        //    if (_Image_Arr[iDelete] != null)
        //    {
        //        _Image_Arr[iDelete].Dispose();
        //        _Image_Arr[iDelete] = null;
        //        GC.Collect();
        //    }
        //    _use = iDelete;
        //}

        public ITranslate AddTranslate
        {
            set
            {
                value.FrameUpdated += new ImageEventHandler(Image_FrameUpdated);
            }
        }
        public ITranslate DelTranslate
        {
            set
            {
                value.FrameUpdated -= new ImageEventHandler(Image_FrameUpdated);
            }
        }


        public VisionBox()
        {
            InitializeComponent();
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            //this.FunctionalMode = FunctionalModeOption.Minimum;
        }


        public VisionBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        ~VisionBox()
        {
            //OS 决定什么时候做这一步，不一定关掉立马执行
            DisposeImage();
        }


        void Image_FrameUpdated(object sender, ImageEventArgs e)
        {
            
            this.Image = e._Image;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (ShowCross)
            {
                //draw cross
                pe.Graphics.DrawLine(Pens.Red, new Point(this.Width / 2, 0), new Point(this.Width / 2, this.Height));
                pe.Graphics.DrawLine(Pens.Red, new Point(0, this.Height / 2), new Point(this.Width, this.Height / 2));
            }
        }

        public void DisposeImage()
        {
            this.Image = null;
            
        }


    }
}
