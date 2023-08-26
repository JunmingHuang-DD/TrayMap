using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUI.RectBlock;

namespace CommonUI
{

    public partial class RectangleBlock : UserControl
    {
        [Flags]
        public enum OperationType
        {
            NoneDefine = -4,
            None = -1,
            Skip = 1,
            PickCandinate = 2
        }

        private const float _MarginX = 1.1f, _MarginY = 1.1f;
        private float _Width, _Height;

        private RectTraySet _TraySet;

        private int _selectIndex = -1;

        private Color[] _TrayColor = { Color.Snow, Color.DarkGray, Color.Yellow, Color.Lime, Color.Red, Color.Salmon, Color.DarkGreen, Color.BlueViolet };
        private Color _FocusColor = Color.Blue;


        [EditorBrowsable(EditorBrowsableState.Never)]
        public RectTraySet ThisTraySet
        {
            set
            {
                if (value == null)
                {
                    return;
                }
            
                _TraySet = value;

                panel1.Invalidate();
            }
        }


        public RectangleBlock(): this(null)
        {
      
        }

        public RectangleBlock(RectTraySet tray)
        {
            InitializeComponent();

            typeof(Control).InvokeMember("DoubleBuffered",
              System.Reflection.BindingFlags.SetProperty |
              System.Reflection.BindingFlags.Instance |
              System.Reflection.BindingFlags.NonPublic,
              null, panel1, new object[] { true });


            ThisTraySet = tray;

            this.Resize += new EventHandler(Tray_SizeChanged);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        void Tray_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void Tray_Paint(object sender, PaintEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            Graphics g = e.Graphics;

            _Width = (float)(1.0 * (panel1.Width - _MarginX * (_TraySet.Length + 1)) / _TraySet.Length);
            _Width = _Width < 0 ? 1 : _Width;
            _Height = panel1.Height - _MarginY;

            for (int i = 0; i < _TraySet.Length; i++)
            {

                var loc = new PointF(i * (_Width + _MarginX) + _MarginX, 0);

                if (_TraySet.StatusList[i])
                {
                    g.FillRectangle(new SolidBrush(Color.Green),
                                    new RectangleF(loc, new SizeF(_Width, _Height)));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.LightGray),
                                    new RectangleF(loc, new SizeF(_Width, _Height)));
                }

                Font ft = new Font("Arail", (int)(_Height * 0.1));
                g.DrawString($"{i + 1}", ft, Brushes.Black, loc.X, _Height*0.4f);
            }
        }



        private void 全部选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i = 0;i< _TraySet.Length;i++)
            {
                _TraySet.StatusList[i] = true;
            }

            panel1.Invalidate();
        }

        private void 全部跳过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _TraySet.Length; i++)
            {
                _TraySet.StatusList[i] = false;
            }

            panel1.Invalidate();
        }

        private void Tray_MouseMove(object sender, MouseEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            int column = (int)((e.X - _MarginX) / (_Width + _MarginX));
            int row = (int)((e.Y - _MarginY) / (_Height + _MarginY));

            if (column < 0 || row < 0 || column >= _TraySet.Length)
            {
                _selectIndex = -1;
                return;
            }

            var dest = new RectangleF(column * (_Width + _MarginX) + _MarginX, 0, _Width, _Height);

            if (!dest.Contains(e.Location))
            {
                _selectIndex = -1;
                return;
            }

            _selectIndex = column;
        }

        private void Tray_Click(object sender, EventArgs e)
        {
            if (_TraySet == null || _selectIndex < 0 || _selectIndex > _TraySet.Length)
            {
                return;
            }

            _TraySet.StatusList[_selectIndex] = !_TraySet.StatusList[_selectIndex];

            panel1.Invalidate();
        }


    }
}
