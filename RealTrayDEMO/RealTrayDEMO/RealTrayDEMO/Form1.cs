using CommonUI;
using Incube.Motion;
using Incube.Vision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealTrayDEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// RealTray的对象，包含TrayMap图，map上每个产品的信息
        /// </summary>
        RealTraySet _TestSet;

        /// <summary>
        /// 设置Tray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            string jasonPahth = AppDomain.CurrentDomain.BaseDirectory+ "\\" + "TestRealTray" + ".json";
            RealTrayEditor realeditor = new RealTrayEditor(jasonPahth);
            realeditor.Show();
        }

        /// <summary>
        /// 加载Tray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            string jasonPahth = AppDomain.CurrentDomain.BaseDirectory + "\\" + "TestRealTray" + ".json";
            _TestSet = new RealTraySet(jasonPahth);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            realTrayControl1.SetTray(_TestSet);  //把Tray 关联到控件

            realTrayControl1.SelectTrayEventHandle += OnSelectTrayHandle;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _TestSet.InitWindow();  //绘制
        }

        /// <summary>
        /// 添加测试数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {


            List<Point3D> poss = new List<Point3D>
            {
                //new Point3D(-35.5,-31.5,5),
                //new Point3D(-30.5,-31.5,-5),
                //new Point3D(-25.5,-31.5,10),
                //new Point3D(-20.5,-31.5,-10),
                //new Point3D(-15.5,-31.5,15),
                //new Point3D(-10.5,-31.5,-15),
                //new Point3D(-5.5,-31.5,20),
                //new Point3D(0.5,-31.5,-20),
                //new Point3D(5.5,-31.5,25),
                //new Point3D(10.5,-31.5,-25),
                //new Point3D(15.5,-31.5,40),
                //new Point3D(20.5,-31.5,-40),
                //new Point3D(25.5,-31.5,60),
                //new Point3D(30.5,-31.5,-60),
                //new Point3D(35.5,-31.5,90),
                //new Point3D(40.5,-31.5,120),
                //new Point3D(45.5,-31.5,150),
                //new Point3D(50.5,-31.5,180),
                //new Point3D(55.5,-31.5,0),
                //new Point3D(60.5,-31.5,0),
                //new Point3D(65.5,-31.5,0),

                new Point3D(-35,-30,5),
                new Point3D(-30,-30,-5),
                new Point3D(-25,-30,10),
                new Point3D(-20,-30,-10),
                new Point3D(-15,-30,15),
                new Point3D(-10,-30,-15),
                new Point3D(-5,-30,20),
                new Point3D(0,-30,-20),
                new Point3D(5,-30,25),
                new Point3D(10,-30,-25),
                new Point3D(15,-30,40),
                new Point3D(20,-30,-40),
                new Point3D(25,-30,60),
                new Point3D(30,-30,-60),
                new Point3D(35,-30,90),
                new Point3D(40,-30,0),
                new Point3D(45,-30,0),
                new Point3D(50,-30,0),
                new Point3D(55,-30,0),
                new Point3D(60,-30,0),
                new Point3D(65,-30,0),

                new Point3D(-35,-20,0),
                new Point3D(-30,-20,0),
                new Point3D(-25,-20,0),
                new Point3D(-20,-20,0),
                new Point3D(-15,-20,0),
                new Point3D(-10,-20,0),
                new Point3D(-5,-20,0),
                new Point3D(0,-20,0),
                new Point3D(5,-20,0),
                new Point3D(10,-20,0),
                new Point3D(15,-20,0),
                new Point3D(20,-20,0),
                new Point3D(25,-20,0),
                new Point3D(30,-20,0),
                new Point3D(35,-20,0),
                new Point3D(40,-20,0),
                new Point3D(45,-20,0),
                new Point3D(50,-20,0),
                new Point3D(55,-20,0),
                new Point3D(60,-20,0),
                new Point3D(65,-20,0),

                new Point3D(-35,-10,0),
                new Point3D(-30,-10,0),
                new Point3D(-25,-10,0),
                new Point3D(-20,-10,0),
                new Point3D(-15,-10,0),
                new Point3D(-10,-10,0),
                new Point3D(-5,-10,0),
                new Point3D(0,-10,0),
                new Point3D(5,-10,0),
                new Point3D(10,-10,0),
                new Point3D(15,-10,0),
                new Point3D(20,-10,0),
                new Point3D(25,-10,0),
                new Point3D(30,-10,0),
                new Point3D(35,-10,0),
                new Point3D(40,-10,0),
                new Point3D(45,-10,0),
                new Point3D(50,-10,0),
                new Point3D(55,-10,0),
                new Point3D(60,-10,0),
                new Point3D(65,-10,0),

                new Point3D(-35,0,0),
                new Point3D(-30,0,0),
                new Point3D(-25,0,0),
                new Point3D(-20,0,0),
                new Point3D(-15,0,0),
                new Point3D(-10,0,0),
                new Point3D(-5,0,0),
                new Point3D(0,0,0),
                new Point3D(5,0,0),
                new Point3D(10,0,0),
                new Point3D(15,0,0),
                new Point3D(20,0,0),
                new Point3D(25,0,0),
                new Point3D(30,0,0),
                new Point3D(35,0,0),
                new Point3D(40,0,0),
                new Point3D(45,0,0),
                new Point3D(50,0,0),
                new Point3D(55,0,0),
                new Point3D(60,0,0),
                new Point3D(65,0,0),

            };

            _TestSet.AddProduct(poss);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _TestSet.MoveNext(ProductState.TestPass);

            labelCurInfo.Text = $"({_TestSet.MainLoc.PosInMachine.X:F3},{_TestSet.MainLoc.PosInMachine.Y:F3});Index = {_TestSet.MainLoc.Index};Tray已准备好={_TestSet.IsTrayReadly}";
        }

        private void OnSelectTrayHandle(object sender, StringEventArgs e)
        {
            int index = int.Parse(e.Message);

            this.BeginInvoke(new Action(() =>
            {
                labelCurInfo.Text = $"({_TestSet.Products[index - 1].PosInMachine.X:F3},{_TestSet.Products[index - 1].PosInMachine.Y:F3});Index = {_TestSet.Products[index - 1].Index}";
            }));
        }
    }
}
