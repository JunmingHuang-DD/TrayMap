using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Incube.Motion;


namespace CommonUI
{
    public partial class Tray : UserControl
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


        private TraySet _TraySet;
        private TrayLocation _SelectedLoc = null;
        private TrayLocation _PreSelectedLoc = null;
        private OperationType _Operation = OperationType.None;

        //Empty = 0, Untested = 1, InTesting = 2, TestPass = 3, TestFail = 4, SkipTest = 5, NextPick = 6,Rotate180 = 7
        private Color[] _TrayColor = { Color.Snow, Color.DarkGray, Color.Yellow, Color.Lime, Color.Red, Color.Salmon, Color.DarkGreen, Color.BlueViolet };
        private Color _FocusColor = Color.Blue;

        public bool IsShowCharacter { get; set; } //是否显示数字
        public bool IsShowName { get; set; } //是否显示数字

        public bool IsAutoUpdata { get; set; }

        public OperationType CurrentOperation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }
        //private bool bFindStart = false;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TraySet ThisTraySet
        {
            set
            {
                if (value == null)
                {
                    return;
                }

                if (_TraySet != null)
                {
                    _TraySet.StateChanged -= new EventHandler(_TraySet_StateChanged);
                }

                _TraySet = value;

                foreach (var item in _TraySet.Trayset)
                {
                    item.StateChanged += new EventHandler(tray_StateChanged);
                }

                panel1.Invalidate();
            }
        }


        public Tray()
            : this(null)
        {
            _Operation = OperationType.None;
        }

        public Tray(TraySet tray)
        {
            InitializeComponent();

            //clear flick effect
            //this.DoubleBuffered = true;
            typeof(Control).InvokeMember("DoubleBuffered",
                  System.Reflection.BindingFlags.SetProperty |
                  System.Reflection.BindingFlags.Instance |
                  System.Reflection.BindingFlags.NonPublic,
                  null, panel1, new object[] { true });


            ThisTraySet = tray;
        
            this.Resize += new EventHandler(Tray_SizeChanged);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        void _TraySet_StateChanged(object sender, EventArgs e)
        {
            this.Enabled = _TraySet.Activated;
        }

        void Tray_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        void tray_StateChanged(object sender, EventArgs e)
        {
            if(IsAutoUpdata)
               panel1.Invalidate();
        }

        private void Tray_Paint(object sender, PaintEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            var size = _TraySet.Size;

            _Width = (float)(1.0 * (panel1.Width - _MarginX * (size.Width + 1)) / size.Width);
            _Width = _Width < 0 ? 1 : _Width;
            _Height = (float)(1.0 * (panel1.Height - _MarginY * (size.Height + 1)) / size.Height);
            _Height = _Height < 0 ? 1.1f : _Height;

            //Font ft = new Font("Arail", 9);
            for (int i = 0; i < _TraySet.Size.Height; i++)
            {
                for (int j = 0; j < _TraySet.Size.Width; j++)
                {
                    
                    var loc = new PointF(j * (_Width + _MarginX) + _MarginX, i * (_Height + _MarginY) + _MarginY);

                    if (_TraySet[i, j].Activated)
                    {
                        g.FillRectangle(new SolidBrush(_TrayColor[(int)(_TraySet[i, j].Status)]),
                                        new RectangleF(loc, new SizeF(_Width, _Height)));
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.LightGray),
                                        new RectangleF(loc, new SizeF(_Width, _Height)));
                    }

                 //   if(_TraySet.Size.Height > 8)
                    {
                        if (IsShowCharacter && _Height > 2)
                        {
                            Font ft = new Font("Arail", (int)(_Height * 0.6));
                            g.DrawString($"{_TraySet[i, j].Index}", ft, Brushes.RoyalBlue, loc);
                        }
                        else if (IsShowName && _Height > 4)
                        {
                            Font ft = new Font("Arail", (int)(_Height * 0.3));
                            g.DrawString($"{_TraySet[i, j].Name}", ft, Brushes.RoyalBlue, loc);
                        }

                    }
                }
            }

            labelPos.Text = _TraySet.MainList.Current.ToString();
            labelName.Text = _TraySet.Id;
        }

        private void Tray_MouseMove(object sender, MouseEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            int column = (int)((e.X - _MarginX) / (_Width + _MarginX));
            int row = (int)((e.Y - _MarginY) / (_Height + _MarginY));

            if (column < 0 || row < 0 || column >= _TraySet.Size.Width || row >= _TraySet.Size.Height)
            {
                _SelectedLoc = null;
                return;
            }

            var dest = new RectangleF(column * (_Width + _MarginX) + _MarginX, row * (_Height + _MarginY) + _MarginY, _Width, _Height);

            if (!dest.Contains(e.Location))
            {
                _SelectedLoc = null;
                return;
            }

            _SelectedLoc = _TraySet[row, column];
            labelPos.Text = _SelectedLoc.ToString();
        }

        private void Tray_Click(object sender, EventArgs e)
        {
            if (_SelectedLoc == null)
            {
                _PreSelectedLoc = null;
                return;
            }

            switch (_Operation)
            {
                case OperationType.None:
                    break;
                case OperationType.Skip:
                    _TraySet.SetLocationState(_SelectedLoc.Loc.Y, _SelectedLoc.Loc.X, TrayState.SkipTest);
                    
                    if (_SelectedLoc == _TraySet.MainLoc)
                    {
                        _TraySet.MoveNext();
                    }

                    break;
                case OperationType.PickCandinate:
                    _TraySet.SetAsCurrent(_SelectedLoc.Loc.Y, _SelectedLoc.Loc.X);
                    break;
                default:
                    _SelectedLoc = null;
                    break;
            }

            _PreSelectedLoc = _SelectedLoc;
            labelPos.Text = _SelectedLoc.ToString();

            panel1.Invalidate();
        }



        private void Tray_Load(object sender, EventArgs e)
        {
            //_Operation = OperationType.PickCandinate;
            _Operation = OperationType.None;
        }

        private void 选择开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Operation = OperationType.PickCandinate;
        }

        private void 选择跳过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Operation = OperationType.Skip;
        }

        private void 全部跳过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TraySet.SkipAll();
            panel1.Invalidate();
            _Operation = OperationType.PickCandinate;
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TraySet.ResetAllState();
            panel1.Invalidate();
            _Operation = OperationType.None;
        }


    }

    public static class RectExtend
    {
        public static bool InRect(this Rectangle rect, Point pt)
        {
            return pt.X >= rect.Left && pt.X <= rect.Right &&
                pt.Y >= rect.Top && pt.Y <= rect.Bottom;
        }
    }

}
