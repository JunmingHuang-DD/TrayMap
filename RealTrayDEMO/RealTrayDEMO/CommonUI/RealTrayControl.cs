using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Incube.Vision;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using CommonUI;
using Incube.Motion;

namespace ProControl
{
    public partial class RealTrayControl : UserControl
    {
        public StringEvnetHandler SelectTrayEventHandle;

        RealTraySet _TraySet;

        /// <summary>
        /// 框选产品时，第一个Mark点
        /// </summary>
        Point Mark1=new Point(99999,99999);
        /// <summary>
        /// 框选产品时，第二个Mark点
        /// </summary>
        Point Mark2=new Point(99999,99999);
        /// <summary>
        /// 第一个Mark点在Map图的像素坐标
        /// </summary>
        SizeF MarkszSub1;
        /// <summary>
        /// 第二个Mark点在Map图的像素坐标
        /// </summary>
        SizeF MarkszSub2;
        /// <summary>
        /// ctrl键按下的标识，用来确定是否要框选产品
        /// </summary>
        bool IsCtrlDown = false;
        /// <summary>
        /// 选中的产品要设置的状态
        /// </summary>
        ProductState ChangeStateCheck=ProductState.NextTest;
        public Bitmap ThisMap
        {
            get { return _TraySet.Map; }
        }
        void _TraySet_StateChanged(object sender, EventArgs e)
        {
            _TraySet=(RealTraySet)sender;
            m_bmp = ThisMap;

            Tray_Window.Invalidate();
        }

        #region PictureWindowParam
        Bitmap m_bmp;               //画布中的图像
        Point m_ptCanvas;           //画布原点在设备上的坐标
        Point m_ptCanvasBuf;        //重置画布坐标计算时用的临时变量
        Point m_ptBmp;              //图像位于画布坐标系中的坐标
        float m_nScale = 1.0F;      //缩放比例
        SizeF szSub;

        Point m_ptMouseDown;        //鼠标点下是在设备坐标上的坐标

        string m_strMousePt;        //鼠标当前位置对应的坐标

        /// <summary>
        /// 标识是否只更新产品状态
        /// </summary>
        /// 
        bool IsUpDataState;
        /// <summary>
        /// 更新产品状态用的临时坐标偏移变量
        /// </summary>
        Point TempCanvas;
        /// <summary>
        /// 更新产品状态用的临时缩放比
        /// </summary>
        float Tempm_nScale;
        /// <summary>
        /// 更新产品状态用的临时图像位于画布坐标系中的坐标
        /// </summary>
        Point Tempm_m_ptBmp;
        /// <summary>
        /// 更新产品状态用的临时鼠标当前位置对应的坐标
        /// </summary>
        string Tempm_strMousePt;
        #endregion

        public RealTrayControl()
        {
            InitializeComponent();
            this.Tray_Window.MouseWheel += new MouseEventHandler(Tray_Window_MouseWheel);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            m_ptCanvas = new Point(-133, -133);
          //  m_nScale = (float)(Tray_Window.Width) / (float)(m_bmp.Width);

            m_nScale = 0.6f;
        }

        public void SetTray(RealTraySet trayset)
        {
            _TraySet = trayset;
            if (_TraySet != null)
            {
                _TraySet.StateChanged += new EventHandler(_TraySet_StateChanged);
            }
        }

        #region 画布缩放及移动
        /// <summary>
        /// 重新绘制时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tray_Window_Paint(object sender, PaintEventArgs e)
        {
            if (m_bmp != null)
            {
                Graphics g = e.Graphics;
                g.TranslateTransform(m_ptCanvas.X, m_ptCanvas.Y);       //设置坐标偏移
                g.ScaleTransform(m_nScale, m_nScale);                   //设置缩放比
                g.DrawImage(m_bmp, m_ptBmp);                            //绘制图像
                g.ResetTransform();                                     //重置坐标系
                Size szTemp = Tray_Window.Size - (Size)m_ptCanvas;
                PointF ptCanvasOnShowRectLT = new PointF(-m_ptCanvas.X / m_nScale, -m_ptCanvas.Y / m_nScale);
                PointF ptCanvasOnShowRectRB = new PointF(szTemp.Width / m_nScale, szTemp.Height / m_nScale);
                Bitmap bMap = new Bitmap(Tray_Window.Width, Tray_Window.Height);
                g = Graphics.FromImage(bMap);
                g.SmoothingMode = SmoothingMode.HighQuality; //高质量
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移
                using (Graphics tg = g)
                {
                    //this.Refresh();
                    tg.DrawImage(bMap, 0, 0);  //把画布贴到画面上
                    tg.Dispose();
                }
                g.Dispose();
            }
            if (IsUpDataState)
            {
                Graphics g = e.Graphics;
                g.TranslateTransform(TempCanvas.X, TempCanvas.Y);       //设置坐标偏移
                g.ScaleTransform(Tempm_nScale, Tempm_nScale);                   //设置缩放比
                g.DrawImage(m_bmp, Tempm_m_ptBmp);                            //绘制图像
                g.ResetTransform();                                     //重置坐标系
                Size szTemp = Tray_Window.Size - (Size)TempCanvas;
                PointF ptCanvasOnShowRectLT = new PointF(-TempCanvas.X / Tempm_nScale, -TempCanvas.Y / Tempm_nScale);
                PointF ptCanvasOnShowRectRB = new PointF(szTemp.Width / Tempm_nScale, szTemp.Height / Tempm_nScale);
                Bitmap bMap = new Bitmap(Tray_Window.Width, Tray_Window.Height);
                g = Graphics.FromImage(bMap);
                g.SmoothingMode = SmoothingMode.HighQuality; //高质量
                g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移
                using (Graphics tg = g)
                {
                    //this.Refresh();
                    tg.DrawImage(bMap, 0, 0);  //把画布贴到画面上
                    tg.Dispose();
                }
                g.Dispose();
                m_ptCanvas = TempCanvas;
                m_nScale = Tempm_nScale;
                m_ptBmp = Tempm_m_ptBmp;
                m_strMousePt = Tempm_strMousePt;
                IsUpDataState = false;
            }

        }
        /// <summary>
        /// 鼠标按下拖动画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tray_Window_MouseDown(object sender, MouseEventArgs e)
        {
                if(Control.ModifierKeys == Keys.Control&& e.Button == MouseButtons.Left)
                {
                 Mark1 = new Point(e.X, e.Y);
                MarkszSub1 = (Size)e.Location - (Size)m_ptCanvas;  //计算鼠标当前点对应画布中的坐标
                MarkszSub1.Width /= m_nScale;
                MarkszSub1.Height /= m_nScale;
                 }
                else if (e.Button == MouseButtons.Left && !IsCtrlDown)
                {      //如果中键点下    初始化计算要用的临时数据
                    m_ptMouseDown = e.Location;
                    m_ptCanvasBuf = m_ptCanvas;
                }

                Tray_Window.Focus();
        }
        /// <summary>
        /// 滑动滑轮缩放画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tray_Window_MouseWheel(object sender, MouseEventArgs e)
        {
            if (m_nScale <= 0.3 && e.Delta <= 0) return;        //缩小下线
            if (m_nScale >= 4.9 && e.Delta >= 0) return;        //放大上线
            //获取 当前点到画布坐标原点的距离
            SizeF szSub = (Size)m_ptCanvas - (Size)e.Location;
            //当前的距离差除以缩放比还原到未缩放长度
            float tempX = szSub.Width / m_nScale;           //这里
            float tempY = szSub.Height / m_nScale;          //将画布比例
            //还原上一次的偏移                               //按照当前缩放比还原到
            m_ptCanvas.X -= (int)(szSub.Width - tempX);     //没有缩放
            m_ptCanvas.Y -= (int)(szSub.Height - tempY);    //的状态
            //重置距离差为  未缩放状态                       
            szSub.Width = tempX;
            szSub.Height = tempY;
            m_nScale += e.Delta > 0 ? 0.2F : -0.2F;
            //重新计算 缩放并 重置画布原点坐标
            m_ptCanvas.X += (int)(szSub.Width * m_nScale - szSub.Width);
            m_ptCanvas.Y += (int)(szSub.Height * m_nScale - szSub.Height);
            Tray_Window.Invalidate();
        }

        private void Tray_Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if ((Mark1.X != 99999 && Mark1.Y != 99999))
                {
                    ((PictureBox)sender).Refresh();
                    Graphics g = ((PictureBox)sender).CreateGraphics();
                    Pen penss = new Pen(Color.Red, 1);
                    PointF[] poss = new PointF[]{
                        new PointF(Mark1.X,Mark1.Y),
                        new PointF(Mark1.X, e.Y  ),
                        new PointF(e.X, e.Y),
                        new PointF(e.X, Mark1.Y)
                    };
                    Mark2 = new Point(e.X, e.Y);
                    g.DrawPolygon(penss, poss);
                    g.Dispose();
                    MarkszSub2 = (Size)e.Location - (Size)m_ptCanvas;  //计算鼠标当前点对应画布中的坐标
                    MarkszSub2.Width /= m_nScale;
                    MarkszSub2.Height /= m_nScale;
                    return;
                }
            }

            if(Control.ModifierKeys != Keys.Control&&( Mark1.X != 99999 && Mark1.Y != 99999)&&( Mark1.X != 99999 && Mark1.Y != 99999))
            {
                List<int> Indexs = new List<int>();
                Point Pos1 = new Point((int)MarkszSub1.Width, (int)MarkszSub1.Height);
                Point Pos2 = new Point((int)MarkszSub2.Width, (int)MarkszSub2.Height);
                bool state = _TraySet.PosInZone(Pos1, Pos2, ref Indexs);
                Mark1= new Point(99999, 99999);
                Mark2= new Point(99999, 99999);
                IsUpDataState = true;
                TempCanvas = m_ptCanvas;
                Tempm_nScale = m_nScale;
                Tempm_m_ptBmp = m_ptBmp;
                Tempm_strMousePt = m_strMousePt;
                if(ChangeStateCheck==ProductState.NextTest&&Indexs.Count>1)
                {
                    MessageBox.Show("禁止将框选的多个产品状态设置为下一个工作位");
                    return;
                }
                foreach(int index in Indexs)
                {
                    _TraySet.SetState(index, ChangeStateCheck);
                }
            }

            if (e.Button == MouseButtons.Left)
            {      //移动过程中 中键点下 重置画布坐标系
                m_ptCanvas = (Point)((Size)m_ptCanvasBuf + ((Size)e.Location - (Size)m_ptMouseDown));
                Tray_Window.Invalidate();
            }

            //计算 右上角显示的坐标信息
            szSub = (Size)e.Location - (Size)m_ptCanvas;  //计算鼠标当前点对应画布中的坐标
            szSub.Width /= m_nScale;
            szSub.Height /= m_nScale;
            Size sz = TextRenderer.MeasureText(m_strMousePt, this.Font);    //获取上一次的区域并重绘
            Tray_Window.Invalidate(new Rectangle(Tray_Window.Width - sz.Width, 0, sz.Width, sz.Height));
            m_strMousePt = e.Location.ToString() + "\n" + ((Point)(szSub.ToSize())).ToString();
            sz = TextRenderer.MeasureText(m_strMousePt, this.Font);         //绘制新的区域
            Tray_Window.Invalidate(new Rectangle(Tray_Window.Width - sz.Width, 0, sz.Width, sz.Height));

            ((PictureBox)sender).Refresh();
            if(_TraySet!=null)
            {
                if (_TraySet._VisibleReticle)
                {
                    Graphics g = ((PictureBox)sender).CreateGraphics();
                    Pen penss = new Pen(Color.Red, 1);
                    g.DrawLine(penss, e.X, 0, e.X, Tray_Window.Height);
                    g.DrawLine(penss, 0, e.Y, Tray_Window.Width, e.Y);
                    g.Dispose();
                }
            }
        }
        #endregion

        private void 设置为下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStateCheck = ProductState.NextTest;
        }

        private void 设置为跳过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStateCheck = ProductState.SkipTest;
        }
        private void 设置为启用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStateCheck = ProductState.Untested;
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeStateCheck = ProductState.NextTest;
        }
        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes != MessageBox.Show("请确认是否重置?", "重置", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;

            _TraySet.ResetAllState();
        }

        private void Tray_Window_Click(object sender, EventArgs e)
        {
            PointF pos = new PointF(szSub.Width, szSub.Height);
            int index = -1;
            bool state = _TraySet.PosOnProduct(pos, ref index);
            if (state)
            {
                IsUpDataState = true;
                TempCanvas = m_ptCanvas;
                Tempm_nScale = m_nScale;
                Tempm_m_ptBmp = m_ptBmp;
                Tempm_strMousePt = m_strMousePt;
                _TraySet.SetState(index, ChangeStateCheck);
                SelectTrayEventHandle?.BeginInvoke(this, new StringEventArgs(index.ToString()), null, null);
            }
        }

        private void RealTrayControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                IsCtrlDown = true;
            }
        }

        private void RealTrayControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                
                IsCtrlDown = false;
            }
        }
    }
}
