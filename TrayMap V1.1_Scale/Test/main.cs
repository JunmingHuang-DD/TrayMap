using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;
using CommonUI;
using Incube.Motion;
using System.IO;
using System.Threading;

namespace Test
{
    public partial class main : Form
    {
       private TraySet _tray;

       private  bool _bStartAutoRefalshTray = false;

        string jasonPahth = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WaferTray.json");

        public main()
        {
            InitializeComponent();
           
            LoadTray();

            tray1.ThisTraySet = _tray;
            tray1.IsShowName = true;

            ColoredOutput.AttachOutput(textBox1);

            ColoredOutputF.AttachOutput(fastColoredTextBox1);
        }

        private void LoadTray()
        {          
            _tray = new TraySet(jasonPahth);
        }

        private void btnEditTray_Click(object sender, EventArgs e)
        {
            TrayEditor td1 = new TrayEditor(jasonPahth);
            if (td1.ShowDialog() == DialogResult.OK)
            {
                _tray = new TraySet(jasonPahth);
                tray1.ThisTraySet = _tray;
            }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            _tray.MoveNext();

            textBox1.Text = "";

            textBox1.AppendText($"Name: {_tray.MainList.Current.Name} \r\n");

            textBox1.AppendText($"Index = {_tray.MainList.Current.Index} \r\n");

            textBox1.AppendText($"loc [{_tray.MainList.Current.Loc.X},{_tray.MainList.Current.Loc.Y} ] \r\n");

            textBox1.AppendText($"pos [{_tray.MainList.Current.Pos.X},{_tray.MainList.Current.Pos.Y} ] \r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool t = _tray.GetProductNotTest("L");

            MessageBox.Show($"{t}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _tray.SetPos(3, 0, new PointF(0, 50));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TagTestForm dlg = new TagTestForm(_tray);

            dlg.ShowDialog();
        }

    }
}
