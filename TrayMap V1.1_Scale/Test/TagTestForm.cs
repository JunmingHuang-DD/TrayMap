using Incube.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class TagTestForm : Form
    {
        private TraySet _tray;

        public TagTestForm(TraySet tray)
        {
            InitializeComponent();

            _tray = tray;

            tray1.ThisTraySet = tray;

            stackTray1.ThisTraySet = tray;
            stackTray1.IsShowName = true;

            tray1.Refresh();
        }


        private void glassButton1_Click(object sender, EventArgs e)
        {
            _tray.MoveNext();

            stackTray1.AutoScrollDown();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
