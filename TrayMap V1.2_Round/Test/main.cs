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

        string jasonPahth = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WaferTray.json");

        public main()
        {
            InitializeComponent();
           
            LoadTray();

            //tray1.ThisTraySet = _tray;
            //tray1.IsShowName = true;

            ColoredOutput.AttachOutput(textBox1);

            roundWafer1.Refresh();
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
            //    tray1.ThisTraySet = _tray;
            }
         }



    }
}
