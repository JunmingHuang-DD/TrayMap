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

namespace Test
{
    public partial class OneByOneForm : Form
    {
        private TraySet _tray;

        string jasonPahth = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WaferTray.json");

        private List<TrayLocation> _trayList = new List<TrayLocation>();

        public OneByOneForm()
        {
            InitializeComponent();

            _tray = new TraySet(jasonPahth);

            trayOneByOne.ThisTraySet = _tray;
            trayOneByOne.Refresh();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_trayList.Count > 0)
            {
                _tray.SetLocationState(_trayList[0].Loc.Y, _trayList[0].Loc.X, TrayState.TestPass);

                _trayList.RemoveAt(0);

                if (_trayList.Count == 0) _tray.GetFirstStart();
            }
            else
            {
                _tray.SetCurrentState(TrayState.TestPass);
                _tray.MoveNext();
            }           

            trayOneByOne.Refresh();
        }

        /// <summary>
        /// 测试把位置全部放在list里边,然后模拟MoveNext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetArray_Click(object sender, EventArgs e)
        {
            _trayList.Clear();

            int cur = _tray.MainList.CurrentIndex;
            for (int i = 0; i < _tray.MainList.LocList.Count; i++)
            {
                if (_tray.MainList.LocList[i].Status == TrayState.Untested || _tray.MainList.LocList[i].Status == TrayState.InTesting || _tray.MainList.LocList[i].Status == TrayState.NextPick)
                {
                    if(i % 5 == 0)
                    {
                        _tray.MainList.CurrentIndex = i;
                        _trayList.Add(_tray.MainList.Current);
                    }
                    
                }
            }

            _tray.MainList.CurrentIndex = cur;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _tray.SetLocationState(3, 3, TrayState.TestPass);

            _tray.MoveNext();

            trayOneByOne.Refresh();
        }
    }
}
