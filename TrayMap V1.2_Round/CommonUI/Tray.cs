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

        public string WaferSize { get; set; }

        public string TrayCurInfo { get; set; }

        /// <summary>
        /// 字体显示方式 0 显示行号和序号；1，只显示总序号；2，不显示
        /// </summary>
        public int ShowCharaterMode { get; set; }

        public OperationType CurrentOperation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }
        //private bool bFindStart = false;

        #region 内部变量
        private Graphics _g = null;
        private Image _imageCache = null;

        private float _zoomOld = 1.0f;
        private float _zoom = 1.0f;
        private float _zoomMin = 0.1f;
        private float _zoomMax = 1000f;

        /// <summary>
        /// 表格的左上角
        /// </summary>
        private PointF _gridLeftTop = new PointF(0, 0);

        private bool _leftButtonPress = false;

        private PointF _mousePosition = new PointF(0, 0);
        #endregion

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
            
        }

        public Tray(TraySet tray)
        {
            InitializeComponent();

            typeof(Control).InvokeMember("DoubleBuffered",
                  System.Reflection.BindingFlags.SetProperty |
                  System.Reflection.BindingFlags.Instance |
                  System.Reflection.BindingFlags.NonPublic,
                  null, panel1, new object[] { true });


            ThisTraySet = tray;

            _Operation = OperationType.None;
            this.Resize += new EventHandler(Tray_SizeChanged);
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
            panel1.Invalidate();
        }

        private void Tray_Paint(object sender, PaintEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            if (_imageCache == null)
            {
                _imageCache = new Bitmap(this.Width, this.Height);
            }

            if (_g == null)
            {
                _g = Graphics.FromImage(_imageCache);

            }
            _g.Clear(this.BackColor);

            DrawGrid(_g);

            e.Graphics.DrawImage(_imageCache, new Point(0, 0));
        }

        private void TrayMouseWheel(object sender, MouseEventArgs e)
        {
            var delta = e.Delta;
            if (Math.Abs(delta) < 10)
            {
                return;
            }

            var mousePosition = new PointF();
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
            _zoomOld = _zoom;

            if (delta < 0)
            {
                _zoom -= FetchStep(delta);
            }
            else if (delta > 0)
            {
                _zoom += FetchStep(delta);
            }
            if (_zoom < _zoomMin)
            {
                _zoom = _zoomMin;
            }
            else if (_zoom > _zoomMax)
            {
                _zoom = _zoomMax;
            }

            var zoomNew = _zoom;
            var zoomOld = _zoomOld;
            var deltaZoomNewToOld = zoomNew / zoomOld;

            //计算零点
            //任意比例下的鼠标点与零点的距离deltaPO1都等于鼠标点与零点在(0,0)且比例为1时的距离deltaPO乘以缩放比例zoom,
            //于是有deltaPO1=deltaPO*zoom,
            //记在第1种缩放比例zoom1下有deltaPO1=deltaPO*zoom1,
            //记在第2种缩放比例zoom2下有deltaPO2=deltaPO*zoom2,
            //将上面两式相比则有deltaPO2/deltaPO1=zoom2/zoom1,令deltaZoomNewToOld=zoom2/zoom1则有deltaPO2/deltaPO1=deltaZoomNewToOld
            //又鼠标点与零点的距离deltaPO=P(x,y)-O(x,y),代入得
            //O.x2=P.x-(P.x-O.x1)*deltaZoomNewToOld;
            //O.y2=P.y-(P.y-O.y1)*deltaZoomNewToOld;
            var zero = _gridLeftTop;
            zero.X = mousePosition.X - (mousePosition.X - zero.X) * deltaZoomNewToOld;
            zero.Y = mousePosition.Y - (mousePosition.Y - zero.Y) * deltaZoomNewToOld;
            _gridLeftTop = zero;

            this.Refresh();
            Console.Write($"scale = {_zoom:F3},MOVE = ({zero.X},{zero.Y}) \r\n");
        }

        private void CalculateScale(double height = 30)
        {
            double Y = _TraySet.Size.Height * height + _MarginY * (_TraySet.Size.Height + 1);

            _zoom = (float) ( (Y * 1.0) / (panel1.Height * 1.0));
        }

        private void CalculateOffset(double height = 30)
        {
            _gridLeftTop = new PointF(0, -(float)(-panel1.Height * 0.5 +  _TraySet.MainList.Current.Loc.Y * height + _MarginY * (_TraySet.MainList.Current.Loc.Y + 1)));
        }

        public void AutoScrollDown(double height = 30)
        {
            CalculateScale(height);
            CalculateOffset(height);
            panel1.Invalidate();
        }

        #region FetchStep
        /// <summary>
        /// 获取缩放的步进
        /// </summary>
        /// <returns></returns>
        private float FetchStep(float delta)
        {
            if (_zoom == 1)
            {
                return delta > 0 ? 1 : 0.05f;
            }
            else
            {
                return _zoom >= 1 ? 1 : 0.05f;
            }
        }
        #endregion
        #region DrawGrid
        /// <summary>
        /// 绘制表格
        /// </summary>
        /// <param name="g"></param>
        private void DrawGrid(Graphics g)
        {
            float cellWidth = _zoom * panel1.Width;
            float cellHeight = _zoom * panel1.Height;

            var size = _TraySet.Size;

            _Width = (float)(1.0 * (cellWidth - _MarginX * (size.Width + 1)) / size.Width);
            _Width = _Width < 0 ? 1 : _Width;
            _Height = (float)(1.0 * (cellHeight - _MarginY * (size.Height + 1)) / size.Height);
            _Height = _Height < 0 ? 1.1f : _Height;

            int ldIndex = 0;
            int spIndex = 0;
            int ldshowIndex = 0;

            for (int j = 0; j < _TraySet.Size.Width; j++)
            {
                for (int i = 0; i < _TraySet.Size.Height; i++) // 先列后行
                 {
                    var loc = new PointF(j * (_Width + _MarginX) + _MarginX + _gridLeftTop.X, i * (_Height + _MarginY) + _MarginY + _gridLeftTop.Y);

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

                    if (IsShowCharacter && _Height > 2)
                    {
                        Font ft = new Font("Arail", (int)(_Height * 0.6));
                      
                        if(_TraySet[i, j].Status != TrayState.SkipTest)
                        {
                            ldshowIndex++;
                            if(ShowCharaterMode == 0)
                            {
                                g.DrawString($"{i + 1}__{ldshowIndex}", ft, Brushes.RoyalBlue, new PointF(loc.X, loc.Y));
                            }
                            else if(ShowCharaterMode == 1)
                            {
                                g.DrawString($"{ldshowIndex}", ft, Brushes.RoyalBlue, new PointF(loc.X + _Width/2, loc.Y));
                            }                          
                        }
                        
                    }
                    else if (IsShowName && _Height > 4)
                    {
                        int penWidth = (int)(_Height * 0.5);
                        Font ft = new Font("Arail", penWidth);

                        int showIndex = 0;
                        if(_TraySet[i, j].Name == "L")
                        {
                            ldIndex++;
                            showIndex = ldIndex;
                        }
                        else
                        {
                            spIndex++;
                            showIndex = spIndex;
                        }
                        g.DrawString($"{_TraySet[i, j].Name}_{showIndex}", ft, Brushes.RoyalBlue, loc.X /*+ _Width/2*/,loc.Y + _Height/2 - penWidth/2);
                    }
                 
                }
            }

            labelPos.Text = _TraySet.MainList.Current.ToString();
            labelName.Text = $"{_TraySet.Id}_{WaferSize}" ;
          //  labelWaferSize.Text = WaferSize;
            labelIndex.Text = TrayCurInfo;
        }
        #endregion

        private void Tray_MouseMove(object sender, MouseEventArgs e)
        {
            if (_TraySet == null)
            {
                return;
            }

            this.Focus();

            if (!_leftButtonPress)
            {
                int column = (int)((e.X - _MarginX - _gridLeftTop.X) / (_Width + _MarginX));
                int row = (int)((e.Y - _MarginY - _gridLeftTop.Y) / (_Height + _MarginY));

                if (column < 0 || row < 0 || column >= _TraySet.Size.Width || row >= _TraySet.Size.Height)
                {
                    _SelectedLoc = null;
                    return;
                }

                var dest = new RectangleF(column * (_Width + _MarginX) + _MarginX + _gridLeftTop.X, row * (_Height + _MarginY) + _MarginY + _gridLeftTop.Y, _Width, _Height);

                if (!dest.Contains(e.Location))
                {
                    _SelectedLoc = null;
                    return;
                }

                _SelectedLoc = _TraySet[row, column];
                labelPos.Text = _SelectedLoc.ToString();
            }
            else
            {
                _SelectedLoc = null;
            }

            var offsetX = e.X - _mousePosition.X;
            var offsetY = e.Y - _mousePosition.Y;
            if (_leftButtonPress)
            {
                _gridLeftTop.X += offsetX;
                _gridLeftTop.Y += offsetY;

                _mousePosition.X = e.X;
                _mousePosition.Y = e.Y;

                this.Refresh();

                panel1.Invalidate();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePosition.X = e.X;
                _mousePosition.Y = e.Y;

                _leftButtonPress = true;
                this.Cursor = Cursors.Hand;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _leftButtonPress = false;
            this.Cursor = Cursors.Default;
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

            if(_TraySet.Size.Height > 20)
            {
                _Operation = OperationType.None;
            }

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

        private void 重置缩放比例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoom = 1;
            _gridLeftTop = new Point(0,0);
            _Operation = OperationType.None;
            panel1.Invalidate();
        }

        public void ResetToFit()
        {
            _zoom = 1;
            _gridLeftTop = new Point(0, 0);
            panel1.Invalidate();
        }

        private void 全部跳过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TraySet.SkipAll();
            panel1.Invalidate();
            _Operation = OperationType.PickCandinate;
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("重新把所有位置及状态恢复初始状态 ", "重置",
                     MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

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
