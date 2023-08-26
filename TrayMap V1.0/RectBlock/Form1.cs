using CommonUI;
using CommonUI.RectBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectBlock
{
    public partial class Form1 : Form
    {
        private RectTraySet _tray;
        public Form1()
        {
            InitializeComponent();

            _tray = new RectTraySet(54);

            rectangleBlock1.ThisTraySet = _tray;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            string s = "";
            for(int i = 0;i< _tray.Length;i++)
            {
                s +=  _tray.StatusList[i] ? "1" : "0";

                s += "  ";
            }

            label1.Text = s;
        }
    }
}
