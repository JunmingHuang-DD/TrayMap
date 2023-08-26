using CommonUI;
using Incube.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderTray
{
    public partial class Form1 : Form
    {
        DifferenceLenghtTray _tray;

        string jasonPahth = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WaferTray.json");

        public Form1()
        {
            InitializeComponent();

            LoadTray();

            tray1.ThisTraySet = _tray;
        }

        private void LoadTray()
        {
            _tray = new DifferenceLenghtTray(jasonPahth);
        }

        private void btnEditTray_Click(object sender, EventArgs e)
        {
            TrayEditor td1 = new TrayEditor(jasonPahth);
            if (td1.ShowDialog() == DialogResult.OK)
            {
                _tray = new DifferenceLenghtTray(jasonPahth);
                tray1.ThisTraySet = _tray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _tray.SetCurrentState(TrayState.TestPass);

            _tray.MoveNext(true);

            tray1.Refresh();

            labelInfo.Text = $"({_tray.MainList.Current.Loc.Y},{_tray.MainList.Current.Loc.X}),length = {_tray.MainList.Current.Length:F2}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _tray.SkipAll();

            _tray.SetPos(0, 0, 0, 50, 0,10.1);
            _tray.SetPos(1, 0, 0, 50, 0, 10);
            _tray.SetPos(2, 0, 0, 50, 0, 10);
            //_tray.SetPos(3, 0, 0, 50, 0, 10);
            //_tray.SetPos(4, 0, 0, 50, 0, 10.4);
            //_tray.SetPos(5, 0, 0, 50, 0, 10.3);
            //_tray.SetPos(6, 0, 0, 50, 0, 10);
            //_tray.SetPos(7, 0, 0, 50, 0, 10);
            //_tray.SetPos(8, 0, 0, 50, 0, 10);

            _tray.SetPos(0, 1, 0, 50, 0, 12.1);
            //_tray.SetPos(1, 1, 0, 50, 0, 12);
            //_tray.SetPos(2, 1, 0, 50, 0, 12);
            //_tray.SetPos(3, 1, 0, 50, 0, 12);
            //_tray.SetPos(4, 1, 0, 50, 0, 12.4);
            //_tray.SetPos(5, 1, 0, 50, 0, 12.3);
            //_tray.SetPos(6, 1, 0, 50, 0, 12);
            //_tray.SetPos(7, 1, 0, 50, 0, 12);
            //_tray.SetPos(8, 1, 0, 50, 0, 12);

            _tray.SetPos(0, 2, 0, 50, 0, 5.1);
            _tray.SetPos(1, 2, 0, 50, 0, 5);
            _tray.SetPos(2, 2, 0, 50, 0, 5);
            //_tray.SetPos(3, 2, 0, 50, 0, 5);
            //_tray.SetPos(4, 2, 0, 50, 0, 5.4);
            //_tray.SetPos(5, 2, 0, 50, 0, 5.3);
            //_tray.SetPos(6, 2, 0, 50, 0, 5);
            //_tray.SetPos(7, 2, 0, 50, 0, 5);
            //_tray.SetPos(8, 2, 0, 50, 0, 5);

            _tray.SetPos(0, 3, 0, 50, 0, 18.1);
            //_tray.SetPos(1, 3, 0, 50, 0, 18);
            //_tray.SetPos(2, 3, 0, 50, 0, 18);
            //_tray.SetPos(3, 3, 0, 50, 0, 18);
            //_tray.SetPos(4, 3, 0, 50, 0, 18.4);
            //_tray.SetPos(5, 3, 0, 50, 0, 18.3);
            //_tray.SetPos(6, 3, 0, 50, 0, 18);
            //_tray.SetPos(7, 3, 0, 50, 0, 18);
            //_tray.SetPos(8, 3, 0, 50, 0, 18);

            _tray.SetPos(0, 4, 0, 50, 0, 15.1);
            _tray.SetPos(1, 4, 0, 50, 0, 15);
            _tray.SetPos(2, 4, 0, 50, 0, 15);
            //_tray.SetPos(3, 4, 0, 50, 0, 15);
            //_tray.SetPos(4, 4, 0, 50, 0, 15.4);
            //_tray.SetPos(5, 4, 0, 50, 0, 15.3);
            //_tray.SetPos(6, 4, 0, 50, 0, 15);
            //_tray.SetPos(7, 4, 0, 50, 0, 15);
            //_tray.SetPos(8, 4, 0, 50, 0, 15);

            _tray.GetFirstStart(true);

            tray1.Refresh();
        }
    }
}
