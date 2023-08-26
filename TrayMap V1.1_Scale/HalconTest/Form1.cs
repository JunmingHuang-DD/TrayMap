using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Threading;
using Vision.Camera;

namespace HalconTest
{
    public partial class Form1 : Form
    {
        VisionHalcon.VisionProcess _VP;
        Thread _Thread;

        BaslerCam _Camera;
        HObject _Image;

        public string FileNema { get; set; }

        public Form1()
        {
            InitializeComponent();


            _Camera = new BaslerCam(new Incube.Vision.CameraParam() { ID = "21814411", Exposure = 3000, MPP = 0.1, Name = "Test" });

            _VP = new VisionHalcon.VisionProcess(_Camera);
            _VP.HWindow = halconView1.HWind;

            _Thread = new Thread(new ThreadStart(Nothing));
            _Thread.IsBackground = true;
            //_Thread.Start();
        }


        private void buttonOpen_Click(object sender, EventArgs e)
        {
            //_VP.FitCircle(new List<PointF>() { new PointF(2, 0), new PointF(1, 1), new PointF(0, 0), new PointF(1, -1) });

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_Image != null)
                {
                    _Image.Dispose();
                }


                HOperatorSet.ReadImage(out _Image, openFileDialog1.FileName);

                HTuple width, height;
                HOperatorSet.GetImageSize(_Image, out width, out height);
                halconView1.ImageSize = new Size(width.I, height.I);

                //display image
                HOperatorSet.DispObj(_Image, halconView1.HWind.HalconWindow);

                return;
            }

            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //halconView1.IsDrawCross = checkBox1.Checked;
        }

        private void Nothing()
        {
            //while (true)
            //{
            //    if (FileNema != null && FileNema.Length > 1)
            //    {
            //        _VP.ReadImage(FileNema);
            //        FileNema = "";
            //    }

            //    Thread.Sleep(500);
            //}
        }

        private void buttonTrigger_Click(object sender, EventArgs e)
        {
            if (!_Camera.Connected)
            {
                MessageBox.Show("Connect the camera first");
                return;
            }

            _Camera.StartContious(Incube.Vision.CameraTriggerSource.Hardware);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            _Camera.Connect();

            MessageBox.Show("Connected: " + _Camera.Connected.ToString());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Camera.Disconnect();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _VP.SaveImage("E:\\Test\\test something.png");
        }

        private void buttonLens_Click(object sender, EventArgs e)
        {
            if (_Image == null)
            {
                return;   
            }

            HObject outRegion = new HObject();
            HTuple rsult = new HTuple();
            //_VP._FindLens(_Image, out outRegion, out rsult);

            if (rsult.Length >= 4 && rsult[3].I == 1)
            {
                //
                halconView1.HWind.HalconWindow.SetDraw("margin");
                halconView1.HWind.HalconWindow.SetColor("red");
                outRegion.DispObj(halconView1.HWind.HalconWindow);
            }

            if (outRegion != null)
            {
                outRegion.Dispose();
            }
        }

        private void buttonLabel_Click(object sender, EventArgs e)
        {
            if (_Image == null)
            {
                return;
            }

            HObject outRegion = new HObject();
            HTuple rsult = new HTuple();
            //_VP._FindLabel(_Image, out outRegion, out rsult);

            if (rsult.Length >= 4 && rsult[3].I == 1)
            {
                //
                halconView1.HWind.HalconWindow.SetDraw("margin");
                halconView1.HWind.HalconWindow.SetColor("red");
                outRegion.DispObj(halconView1.HWind.HalconWindow);
            }

            if (outRegion != null)
            {
                outRegion.Dispose();
            }
        }
    }
}
