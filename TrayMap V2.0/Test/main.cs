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

namespace Test
{
    public partial class main : Form
    {
        TraySet _tray;

        string jasonPahth = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WaferTray.json");
        public main()
        {
            InitializeComponent();
           
            LoadTray();

            tray1.ThisTraySet = _tray;
            tray1.IsShowName = true;
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

            richTextBoxInfo.Text = "";

            richTextBoxInfo.AppendText("loc _x :" + _tray.MainList.Current.Loc.X.ToString() + "\r\n");
            richTextBoxInfo.AppendText("loc _y :" + _tray.MainList.Current.Loc.Y.ToString() + "\r\n");

            richTextBoxInfo.AppendText("pos _x :" + _tray.MainList.Current.Pos.X.ToString("F3") + "\r\n");
            richTextBoxInfo.AppendText("pos _y :" + _tray.MainList.Current.Pos.Y.ToString("F3") + "\r\n");

            richTextBoxInfo.AppendText("Name :" + _tray.MainList.Current.Name + "\r\n");
        }

        private void btnSetRemainNG_Click(object sender, EventArgs e)
        {
            foreach(var t in _tray.Trayset)
            {
                if(t.Loc.X == _tray.MainList.Current.Loc.X)
                {
                    if(t.Status != TrayState.TestPass && t.Status != TrayState.TestPass)
                    {
                        t.Status = TrayState.TestFail;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pos = _tray.GetPosByName(textBoxName.Text);

            richTextBoxInfo.AppendText($"名称为{textBoxName.Text}的第一个位置 = lox({pos.Loc.X},{pos.Loc.Y})" + "\r\n");           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> l = _tray.GetLocationLabel();

            MessageBox.Show($"{string.Join(",", l.ToArray())}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _tray.SetCurrentLabel(textBoxLabel.Text);
            tray1.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _tray.IsShowMode_Down2Up = checkBox1.Checked;

            tray1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tray1.Size = new Size(900, 600);

            tray1.ThisTraySet = _tray;

            tray1.Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tray1.Size = new Size(400, 400);

            tray1.ThisTraySet = _tray;

            tray1.Refresh();
        }
    }
}
