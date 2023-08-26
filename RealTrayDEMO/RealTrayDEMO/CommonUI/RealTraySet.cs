using Incube.Vision;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonUI
{
    [Flags]
    /// <summary>
    /// Tray上产品的状态
    /// </summary>
    public enum ProductState : int
    {
        /// <summary>
        /// 空,代表当前已无产品
        /// </summary>
        Empty = 0,
        /// <summary>
        /// 未工作
        /// </summary>
        Untested = 1,
        /// <summary>
        /// 下一个工作
        /// </summary>
        NextTest = 2,
        /// <summary>
        /// 工作中
        /// </summary>
        InTesting = 3,
        /// <summary>
        /// 工作完成
        /// </summary>
        TestPass = 4,
        /// <summary>
        /// 工作失败
        /// </summary>
        TestFail = 5,
        /// <summary>
        /// 跳过
        /// </summary>
        SkipTest = 6,
    }
    public class ProductPixelPos
    {
        /// <summary>
        /// 中心点像素坐标
        /// </summary>
        public PointF CenterPos { get; set; }
        /// <summary>
        /// 左上点像素坐标
        /// </summary>
        public PointF LUPos { get; set; }
        /// <summary>
        /// 右上点像素坐标
        /// </summary>
        public PointF RUPos { get; set; }
        /// <summary>
        /// 左下点像素坐标
        /// </summary>
        public PointF LDPos { get; set; }
        /// <summary>
        /// 右下点像素坐标
        /// </summary>
        public PointF RDPos { get; set; }

        public ProductPixelPos Clone()
        {
            ProductPixelPos Pos = new ProductPixelPos();
            Pos.CenterPos = CenterPos;
            Pos.LUPos = LUPos;
            Pos.LDPos = LDPos;
            Pos.RUPos = RUPos;
            Pos.RDPos = RDPos;
            return Pos;
        }
    }


    /// <summary>
    /// Tray上产品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 工作序号，按照这个序号操作tray位置  从 1 开始
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 是否启用，有些位置不使用
        /// </summary>
        public bool Activated { get; set; }
        /// <summary>
        /// 状态，为Empty时表示Tray列表已工作完（tray最后一个状态一定为Empty）
        /// </summary>
        public ProductState State { get; set; }
        /// <summary>
        /// 在Tray控件中的像素坐标
        /// </summary>
        public ProductPixelPos PixelPos { get; set; }
        /// <summary>
        /// 在Tray上的绝对坐标(以Wafer中心为原点)
        /// </summary>
        public PointF PosInTray { get; set; }
        /// <summary>
        /// 在X/Y/R平台上的绝对坐标
        /// </summary>
        public Point3D PosInMachine { get; set; }

        public Product()
        {
            Index = - 1;
        }

    }

    /// <summary>
    /// 在XYR平台上的一个Tray，俯视视角平台运动方向为：X-左负右正 Y-上负下正 R-逆时针正顺时针负
    /// </summary>
    public class RealTraySet
    {
        #region  properties
        /// <summary>
        /// Tray的尺寸，单位寸
        /// </summary>
        protected double _TrayInches;

        /// <summary>
        /// Tray的拾取边界的直径
        /// </summary>
        protected double _TrayWorkZone;

        /// <summary>
        /// 在Tray上所有的产品
        /// </summary>
        protected List<Product> _Products;

        /// <summary>
        /// 下一个位置序号
        /// </summary>
        protected int _NextLoc;

        /// <summary>
        /// Tray上产品的生产顺序 0=行优先 1=列优先
        /// </summary>
        protected int _TestMode;

        /// <summary>
        /// Tray最大行列数
        /// </summary>
        protected Point _TraySize;

        /// <summary>
        /// Tray已经准备好的标识
        /// </summary>
        protected bool _IsTrayReadly=false;

        /// <summary>
        /// Tray名称
        /// </summary>
        protected string _Id;

        /// <summary>
        /// Tray中心在实际XYR平台的坐标
        /// </summary>
        protected PointF _TrayCenter;

        /// <summary>
        /// Tray上产品的尺寸
        /// </summary>
        protected Point4D _ProductInfo;

        /// <summary>
        /// Tray的Map图
        /// </summary>
        protected Bitmap _Map;

        /// <summary>
        /// 产品在Map中的轮廓长宽
        /// </summary>
        protected Point3D _ProductInfoAsPixel;

        /// <summary>
        /// 显示顺序角标
        /// </summary>
        public bool _VisibleIndex;

        /// <summary>
        /// 显示十字线
        /// </summary>
        public bool _VisibleReticle;

        /// <summary>
        /// 显示拾取边界
        /// </summary>
        public bool _VisibleBorder;
        /// <summary>
        /// 画面分辨率倍率
        /// </summary>
        public int WindowMag=10;

        /// <summary>
        /// Product状态显示的颜色
        /// </summary>
        public Dictionary<ProductState, Color> StateColor;

        /// <summary>
        /// tray 名称
        /// </summary>
        public string Id { get { return _Id; } }

        /// <summary>
        /// Tray的Map图
        /// </summary>
        public Bitmap Map { get {return _Map; } }

        /// <summary>
        /// Tray上产品尺寸
        /// </summary>
        public Point4D ProductInfo { get { return _ProductInfo; } }

        /// <summary>
        /// Tray中心在XYR平台实际坐标
        /// </summary>
        public PointF TrayCenter { get { return _TrayCenter; } }

        /// <summary>
        /// 在Tray上的所有产品
        /// </summary>
        public List<Product> Products { get { return _Products; } }

        /// <summary>
        /// 当前位置序号  从0开始
        /// </summary>
        public int CurIndex { get; set; }

        /// <summary>
        /// 当前工作位置
        /// </summary>
        public Product MainLoc
        {
            get
            {
                int index = 0;
                for (int i = CurIndex; i < Products.Count; i++)
                {
                    if (Products[i].State == ProductState.NextTest)
                    {
                        index = Products[i].Index;
                        break;
                    }
                }
                if(index == 0&&IsTrayReadly)
                {
                    bool HaveUnTest = false;
                    for (int i = CurIndex; i < Products.Count; i++)
                    {
                        if (Products[i].State == ProductState.Untested)
                        {
                            index = Products[i].Index;
                            HaveUnTest = true;
                            break;
                        }
                    }
                    if(HaveUnTest==false)
                    {
                        IsTrayReadly = false;
                        return new Product();
                    }
                }
                if (index == 0 && IsTrayReadly==false)
                {
                    return new Product();
                }
                CurIndex = index - 1;

                return Products[index-1];
            }
         }

        /// <summary>
        /// 下一个工作位置序号
        /// </summary>
        public int NextLoc
        {
            get
            {
                int loc = -1;
            
                for (int i = CurIndex +1 ;i< Products.Count;i++)
                {
                    if (Products[i].State == ProductState.NextTest || Products[i].State == ProductState.Untested)
                    {
                        loc = Products[i].Index;
                        break;
                    }
                }

                if(loc >= Products.Count+1)
                {
                    return -1;
                }

                return loc;
            }
        }
        /// <summary>
        /// Tray是否已经准备好的标识
        /// </summary>
        public bool IsTrayReadly { get { return _IsTrayReadly; } set { _IsTrayReadly = value; } }
        #endregion
        public event EventHandler StateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traymap">Tray配置文件路径</param>
        public RealTraySet(string traymap)
        {
            if (traymap != null && traymap.Length > 1)
            {
                LoadConfig(traymap);
            }

            CurIndex = 0;
        }

        /// <summary>
        /// 加载Tray配置文件
        /// </summary>
        /// <param name="Path">Tray配置文件路径</param>
        protected void LoadConfig(string Path)
        {
            _IsTrayReadly = false;
            _Products = new List<Product>();
            using (var file = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                var sr = new StreamReader(file);
                JObject setting = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());
                _Id = setting["ID"].ToString();
                _TrayInches = Convert.ToDouble(setting["WaferSetting"]["Size"].ToString());
                _TrayWorkZone = Convert.ToDouble(setting["WaferSetting"]["WorkZone"]);
                _TrayCenter =new PointF(
                    Convert.ToSingle(setting["WaferSetting"]["CenterX"])
                    , Convert.ToSingle(setting["WaferSetting"]["CenterY"]));
                _TraySize =new Point(
                    (int)setting["WaferSetting"]["MAXhang"]
                    ,(int)setting["WaferSetting"]["MAXLie"]);
                if ((int)setting["TestMode"] == 0)
                {
                    _TestMode = 0;
                }
                else
                {
                    _TestMode = 1;
                }
               _ProductInfo =new Point4D(
                   (double)setting["ProductInfo"]["ProductX"]
                   ,(double)setting["ProductInfo"]["ProductY"]);
                if ((string)setting["Other"]["JiaoBiao"] == "True")
                {
                    _VisibleIndex = true;
                }
                else
                {
                    _VisibleIndex = false;
                }
                if ((string)setting["Other"]["ShiZhi"] == "True")
                {
                    _VisibleReticle = true;
                }
                else
                {
                    _VisibleReticle = false;
                }
                if ((string)setting["Other"]["BianJie"] == "True")
                {
                    _VisibleBorder = true;
                }
                else
                {
                    _VisibleBorder = false;
                }
                StateColor = new Dictionary<ProductState, Color>();
                StateColor.Add(ProductState.Untested,System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["UnTest"]));
                StateColor.Add(ProductState.InTesting, System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["Testing"]));
                StateColor.Add(ProductState.NextTest, System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["NextTest"]));
                StateColor.Add(ProductState.SkipTest, System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["SkipPos"]));
                StateColor.Add(ProductState.TestPass, System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["OKPos"]));
                StateColor.Add(ProductState.TestFail, System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["NGPos"]));
                StateChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// 向Tray添加产品
        /// </summary>
        /// <param name="posS">产品实际坐标</param>
        public void AddProduct(List<Point3D> posS)
        {
            int RealPosCol = 0, RealPosRow = 0, w=0, h=0;
            GetResolving(ref w,ref h);
            Point3D[,] PosArray = new Point3D[_TraySize.Y, _TraySize.X];
            PosSort(posS, ref PosArray,ref RealPosCol,ref RealPosRow);
            int index = 1;
            Graphics g = Graphics.FromImage(_Map);
            SolidBrush MyBrush = new SolidBrush(Color.Black);
            ProductState state = ProductState.NextTest;
            Point3D MachinePos = new Point3D(0, 0, 0);
            _ProductInfoAsPixel = new Point3D(MillimetersToPixelsWidth(WindowMag*ProductInfo.X, 0),MillimetersToPixelsWidth(WindowMag*ProductInfo.Y, 1) /** ((double)h / ((double)_Map.Size.Width))*/, ProductInfo.R);
            #region
            if (_TestMode==0)
            {
                for(int m=0;m<RealPosCol;m++)
                {
                    for(int n=0;n<RealPosRow;n++)
                    {
                        if(PosArray[m,n].R!=9999)
                        {
                            MachinePos = PosArray[m,n];
                            PointF IsCenterPos = PosInMap(new Point3D(WindowMag * PosArray[m, n].X, WindowMag * PosArray[m, n].Y, PosArray[m, n].R));
                            PointF posintray = PosRelativeCenter(PosArray[m, n]);
                            ProductPixelPos pixelpos = new ProductPixelPos
                            {
                                CenterPos = IsCenterPos,
                                LUPos = GetNewPoint(-PosArray[m, n].R, new PointF(IsCenterPos.X, IsCenterPos.Y),
                                        new PointF((float)(IsCenterPos.X - (_ProductInfoAsPixel.X * WindowMag / 2)), (float)(IsCenterPos.Y - (_ProductInfoAsPixel.Y * WindowMag / 2)))),
                                RUPos = GetNewPoint(-PosArray[m, n].R, new PointF(IsCenterPos.X, IsCenterPos.Y),
                                        new PointF((float)(IsCenterPos.X + (_ProductInfoAsPixel.X * WindowMag / 2)), (float)(IsCenterPos.Y - (_ProductInfoAsPixel.Y * WindowMag / 2)))),
                                RDPos = GetNewPoint(-PosArray[m, n].R, new PointF(IsCenterPos.X, IsCenterPos.Y),
                                        new PointF((float)(IsCenterPos.X + (_ProductInfoAsPixel.X * WindowMag / 2)), (float)(IsCenterPos.Y + (_ProductInfoAsPixel.Y * WindowMag / 2)))),
                                LDPos = GetNewPoint(-PosArray[m, n].R, new PointF(IsCenterPos.X, IsCenterPos.Y),
                                        new PointF((float)(IsCenterPos.X - (_ProductInfoAsPixel.X * WindowMag / 2)), (float)(IsCenterPos.Y + (_ProductInfoAsPixel.Y * WindowMag / 2)))),

                                //LUPos = new PointF((float)(IsCenterPos.X - (_ProductInfoAsPixel.X / 2)), (float)(IsCenterPos.Y - (_ProductInfoAsPixel.Y / 2))),
                                //RUPos = new PointF((float)(IsCenterPos.X + (_ProductInfoAsPixel.X / 2)), (float)(IsCenterPos.Y - (_ProductInfoAsPixel.Y / 2))),
                                //RDPos = new PointF((float)(IsCenterPos.X + (_ProductInfoAsPixel.X / 2)), (float)(IsCenterPos.Y + (_ProductInfoAsPixel.Y / 2))),
                                //LDPos = new PointF((float)(IsCenterPos.X - (_ProductInfoAsPixel.X / 2)), (float)(IsCenterPos.Y + (_ProductInfoAsPixel.Y / 2))),  //旋转角度有问题
                            };

                            _Products.Add(new Product
                            {
                                Label = "Bar",
                                Index = index,
                                Activated = true,
                                State = state,
                                PosInMachine = MachinePos,
                                PosInTray =posintray,
                                PixelPos = pixelpos,
                            });

                            PointF[] poss = new PointF[]
                            {
                                _Products[index-1].PixelPos.LUPos,
                                _Products[index-1].PixelPos.RUPos,
                                _Products[index-1].PixelPos.RDPos,
                                _Products[index-1].PixelPos.LDPos,
                            };

                            MyBrush = new SolidBrush(StateColor[state]);
                            g.FillPolygon(MyBrush, poss);
                            if(_VisibleIndex)
                            {
                                Font f = new Font("Arail", WindowMag * (float)(3));
                                g.DrawString(Convert.ToString(index),f , Brushes.Red, pixelpos.RUPos);
                            }
                            state = ProductState.Untested;
                            index++;
                        }
                    }
                }
            }
            else if(_TestMode==1)
            {
                for (int n = 0; n < RealPosCol; n++)
                {
                    for (int m = 0; m < RealPosRow; m++)
                    {
                        if (PosArray[m, n].R != 9999)
                        {
                            MachinePos = PosArray[m, n];
                            PointF IsCenterPos = PosInMap(PosArray[m, n]);
                            PointF posintray = PosRelativeCenter(PosArray[m, n]);
                            ProductPixelPos pixelpos = new ProductPixelPos
                            {
                                CenterPos = IsCenterPos,
                                LUPos = GetNewPoint(-PosArray[m, n].R, new Point((int)IsCenterPos.X, (int)IsCenterPos.Y),
                                        new Point((int)(IsCenterPos.X - (_ProductInfoAsPixel.X / 2)), (int)(IsCenterPos.Y - (_ProductInfoAsPixel.Y / 2)))),
                                RUPos = GetNewPoint(-PosArray[m, n].R, new Point((int)IsCenterPos.X, (int)IsCenterPos.Y),
                                        new Point((int)(IsCenterPos.X + (_ProductInfoAsPixel.X / 2)), (int)(IsCenterPos.Y - (_ProductInfoAsPixel.Y / 2)))),
                                RDPos = GetNewPoint(-PosArray[m, n].R, new Point((int)IsCenterPos.X, (int)IsCenterPos.Y),
                                        new Point((int)(IsCenterPos.X + (_ProductInfoAsPixel.X / 2)), (int)(IsCenterPos.Y + (_ProductInfoAsPixel.Y / 2)))),
                                LDPos = GetNewPoint(-PosArray[m, n].R, new Point((int)IsCenterPos.X, (int)IsCenterPos.Y),
                                        new Point((int)(IsCenterPos.X - (_ProductInfoAsPixel.X / 2)), (int)(IsCenterPos.Y + (_ProductInfoAsPixel.Y / 2)))),
                            };
                            _Products.Add(new Product
                            {
                                Label = "Bar",
                                Index = index,
                                Activated = true,
                                State = state,
                                PosInMachine = MachinePos,
                                PosInTray = posintray,
                                PixelPos = pixelpos,
                            });
                            PointF[] poss = new PointF[]
                            {
                                _Products[index-1].PixelPos.LUPos,
                                _Products[index-1].PixelPos.RUPos,
                                _Products[index-1].PixelPos.RDPos,
                                _Products[index-1].PixelPos.LDPos,
                            };
                            MyBrush = new SolidBrush(StateColor[state]);
                            g.FillPolygon(MyBrush, poss);
                            if (_VisibleIndex)
                            {
                                Font f = new Font("Arail", WindowMag*(float)(3));
                                g.DrawString(Convert.ToString(index), f, Brushes.Red, pixelpos.RUPos);
                            }
                            state = ProductState.Untested;
                            index++;
                        }
                    }
                }
            }
            #endregion
            g.Dispose();
            _IsTrayReadly = true;
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 初始化Tray盘样式
        /// </summary>
        public void InitWindow()
        {
            _Products = new List<Product>();
            int TrayDiam =WindowMag*(int)MillimetersToPixelsWidth(_TrayInches*25.4,0);
            int ZoneDiam = WindowMag * (int)MillimetersToPixelsWidth(_TrayWorkZone * 10, 0);
            PointF StartPos = new PointF(TrayDiam +( WindowMag * 100 - WindowMag * 50), WindowMag * 50);
            PointF[] Poss = new PointF[] {
                new PointF(StartPos.X,StartPos.Y),
                new PointF(StartPos.X+WindowMag*10,StartPos.Y),
                new PointF(StartPos.X+WindowMag*10,StartPos.Y+WindowMag*30),
                new PointF(StartPos.X+WindowMag*20,StartPos.Y+WindowMag*30),
                new PointF(StartPos.X+WindowMag*5,StartPos.Y+WindowMag*70),
                new PointF(StartPos.X-WindowMag*10,StartPos.Y+WindowMag*30),
                new PointF(StartPos.X,StartPos.Y+WindowMag*30),
            };
            Bitmap bMap = new Bitmap(TrayDiam + WindowMag * 100, TrayDiam + WindowMag * 100);
            Graphics g = Graphics.FromImage(bMap);
            SolidBrush MyBrush = new SolidBrush(Color.White);

            Font f = new Font("本墨竞圆 - 常规", 100);
            g.DrawString("A", f, Brushes.White, new PointF(StartPos.X - WindowMag * 10, StartPos.Y + WindowMag * 60));
            g.DrawString("R", f, Brushes.White, new PointF(StartPos.X - WindowMag * 10, StartPos.Y + WindowMag * 120));

            g.FillEllipse(MyBrush, (TrayDiam + WindowMag * 100 - (TrayDiam)) / 2, (TrayDiam + WindowMag * 100 - (+TrayDiam)) / 2, TrayDiam, TrayDiam);
            if(_VisibleBorder)
            {
                Pen pen = new Pen(Color.Blue, 2);
                g.DrawEllipse(pen, (TrayDiam + WindowMag * 100 - (ZoneDiam)) / 2, (TrayDiam + WindowMag * 100 - (ZoneDiam)) / 2, ZoneDiam, ZoneDiam);
            }
            g.FillPolygon(MyBrush, Poss);
            _Map = new Bitmap(bMap);
            g.Dispose();
            bMap.Dispose();
            _IsTrayReadly = false;
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// 移动到下一个位置
        /// </summary>
        /// <param name="State">当前位置设置的状态</param>
        public void MoveNext(ProductState State)
        {
            if(IsTrayReadly==false)
            {
                return;
            }

            Graphics g = Graphics.FromImage(_Map);
            try
            {
                SolidBrush MyBrush = new SolidBrush(StateColor[State]);

                if (NextLoc == -1) //已到最后位置
                {
                    PointF[] poss1 = new PointF[]
                               {
                                MainLoc.PixelPos.LUPos,
                                MainLoc.PixelPos.RUPos,
                                MainLoc.PixelPos.RDPos,
                                MainLoc.PixelPos.LDPos,
                                };
                    g.FillPolygon(MyBrush, poss1);
                    MainLoc.State = State;
                    _IsTrayReadly = false;
                }
                else
                {
                    PointF[] poss1 = new PointF[]
                                 {
                                MainLoc.PixelPos.LUPos,
                                MainLoc.PixelPos.RUPos,
                                MainLoc.PixelPos.RDPos,
                                MainLoc.PixelPos.LDPos,
                                  };
                    g.FillPolygon(MyBrush, poss1); //改变当前状态

                    int index = NextLoc;
                    MainLoc.State = State;
                    if (index > 0)
                    {
                        Products[index - 1].State = ProductState.NextTest;
                        PointF[] poss2 = new PointF[]
                                {
                                _Products[index-1].PixelPos.LUPos,
                                _Products[index-1].PixelPos.RUPos,
                                _Products[index-1].PixelPos.RDPos,
                                _Products[index-1].PixelPos.LDPos,
                                };
                        MyBrush = new SolidBrush(StateColor[ProductState.NextTest]); //改变下一个位置状态
                        g.FillPolygon(MyBrush, poss2);

                        CurIndex++;
                    }

                    g.Dispose();
                }
            }
            catch(Exception e)
            {
                g.Dispose();
            }
            StateChanged?.Invoke(this, null);
        }
        /// <summary>
        /// 设置选择位置状态
        /// </summary>
        /// <param name="index">鼠标选中的产品</param>
        /// <param name="State">要设置的状态</param>
        public void SetState(int index, ProductState State)
        {
            if (IsTrayReadly == false)
            {
                return;
            }
            Graphics g = Graphics.FromImage(_Map);
            try
            {
                SolidBrush MyBrush = new SolidBrush(StateColor[State]);
                if (State == ProductState.NextTest)
                {
                    CurIndex = 0;
                    MyBrush = new SolidBrush(StateColor[ProductState.Untested]);
                    PointF[] poss = new PointF[]
                                {
                                MainLoc.PixelPos.LUPos,
                                MainLoc.PixelPos.RUPos,
                                MainLoc.PixelPos.RDPos,
                                MainLoc.PixelPos.LDPos,
                                };
                    g.FillPolygon(MyBrush, poss);
                    MainLoc.State = ProductState.Untested;
                    Products[index - 1].State = ProductState.NextTest;
                    MyBrush = new SolidBrush(StateColor[ProductState.NextTest]);
                    PointF[] poss2 = new PointF[]
                                {
                                Products[index - 1].PixelPos.LUPos,
                                Products[index - 1].PixelPos.RUPos,
                                Products[index - 1].PixelPos.RDPos,
                                Products[index - 1].PixelPos.LDPos,
                                };
                    g.FillPolygon(MyBrush, poss2);
                }
                else
                {
                    MyBrush = new SolidBrush(StateColor[State]);
                    PointF[] poss = new PointF[]
                               {
                                Products[index - 1].PixelPos.LUPos,
                                Products[index - 1].PixelPos.RUPos,
                                Products[index - 1].PixelPos.RDPos,
                                Products[index - 1].PixelPos.LDPos,
                                };
                    g.FillPolygon(MyBrush, poss);
                    Products[index - 1].State = State;
                }
                g.Dispose();
            }
            catch(Exception e)
            {
                g.Dispose();
            }
            StateChanged?.Invoke(this, null);
        }

        public void ResetAllState()
        {
            //foreach(var p in _Products)
            //{
            //    p.State = ProductState.NextTest;
            //}

            InitWindow();

            CurIndex = 0;

            StateChanged?.Invoke(this, null);
        }

        #region 计算
        /// <summary>
        /// 获取移动角度的新坐标
        /// </summary>
        /// <param name="Rate">旋转角度</param>
        /// <param name="CirPoint">圆心坐标</param>
        /// <param name="MovePoint">移动的坐标</param>
        /// <returns></returns>
        private PointF GetNewPoint(double Rate, PointF CirPoint, PointF MovePoint)
        {
            double Rage2 = Rate / 180 * Math.PI;
            //圆心坐标+计算坐标=新位置的坐标
            float newx = (float)( (MovePoint.X - CirPoint.X) * Math.Cos(Rage2) + (MovePoint.Y - CirPoint.Y) * Math.Sin(Rage2));
            float newy = (float)( -(MovePoint.X - CirPoint.X) * Math.Sin(Rage2) + (MovePoint.Y - CirPoint.Y) * Math.Cos(Rage2));
            PointF newpoint = new PointF(CirPoint.X + newx, CirPoint.Y + newy);

            return newpoint;
        }
        /// <summary>
        /// 将一组乱序的坐标按设备坐标系排序保存到二维数组
        /// </summary>
        /// <param name="posS">排序前的一组坐标</param>
        /// <param name="PosArray">排序后的二维数组</param>
        public void PosSort(List<Point3D> posS, ref Point3D[,] PosArrays, ref int RealPosCol, ref int RealPosRow)
        {
            #region 初始化二维数组，并填充假数据
            Point3D[,] PosArray = new Point3D[_TraySize.Y, _TraySize.X];//按阵列保存坐标的二维数组（X,Y）,（0,0）坐标为左上角产品
            Point3D FakerPos = new Point3D(9999, 9999, 9999);
            for (int y = 0; y < _TraySize.Y; y++)
            {
                for (int x = 0; x < _TraySize.X; x++)
                {
                    PosArray[y, x] = new Point3D(9999, 9999, 9999);
                }
            }
            #endregion

            bool IsFst = true;//是第一个坐标的标识

            #region 按列将坐标保存到二维数组
            foreach (Point3D pos in posS)
            {
                if (IsFst)//是第一个位置，阵列中的坐标始终是0,0
                {
                    PosArray[0, 0] = pos;
                    IsFst = false;
                }
                else
                {
                    bool IsHaveTL = false;//已加入同列
                    for (int col = 0; col < PosArray.GetLength(1); col++)//遍历二维数组中每一列，计算这颗产品是在哪一列
                    {
                        if (Math.Abs(pos.X - PosArray[0, col].X) < (ProductInfo.X / 2))//如果X坐标小于产品X边长/2，加入此列并冒泡排序
                        {
                            //PosArray[PosArray.GetLength(0), col] = pos;
                            for (int n = 0; n < _TraySize.Y; n++)
                            {
                                if (PosArray[n, col].R == FakerPos.R)
                                {
                                    PosArray[n, col] = pos;
                                    IsHaveTL = true;
                                    break;
                                }
                            }
                            Point3D Temp = new Point3D(0, 0, 0);
                            for (int i = 0; i < PosArray.GetLength(0); i++)
                            {
                                for (int j = 0; j < PosArray.GetLength(0) - i - 1; j++)
                                {
                                    if (PosArray[j, col].Y - PosArray[j + 1, col].Y > 0)
                                    {
                                        Temp = PosArray[j + 1, col];
                                        PosArray[j + 1, col] = PosArray[j, col];
                                        PosArray[j, col] = Temp;
                                    }
                                }
                            }
                        }
                    }
                    if (IsHaveTL == false)
                    {
                        for (int n = 0; n < _TraySize.X; n++)
                        {
                            if (PosArray[0, n].R == FakerPos.R)
                            {
                                PosArray[0, n] = pos;
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            #region 冒泡排序二维数组中整列
            Point3D Temp2 = new Point3D(0, 0, 0);
            for (int i = 0; i < PosArray.GetLength(1); i++)
            {
                for (int j = 0; j < PosArray.GetLength(1) - i - 1; j++)
                {
                    if (PosArray[0, j].X - PosArray[0, j + 1].X > 0)
                    {
                        for (int m = 0; m < PosArray.GetLength(0); m++)
                        {
                            Temp2 = PosArray[m, j + 1];
                            PosArray[m, j + 1] = PosArray[m, j];
                            PosArray[m, j] = Temp2;
                        }
                    }
                }
            }
            #endregion

            #region 计算二维数组中真实的坐标数量，去除假数据
            RealPosCol = 0;
            RealPosRow = 0;
            for (int m = 0; m < _TraySize.X; m++)//获取真实坐标的列数
            {
                if (PosArray[m, 0].R == FakerPos.R)
                {
                    RealPosCol = m;
                    break;
                }
                int TempRow = 0;
                for (int n = 0; n < _TraySize.Y; n++)//获取真实坐标最大行数
                {
                    if (PosArray[m, n].R != FakerPos.R)
                    {
                        TempRow++;
                    }
                }
                if (TempRow > RealPosRow)
                {
                    RealPosRow = TempRow;
                }
            }
            PosArrays = new Point3D[RealPosCol, RealPosRow];
            for (int m = 0; m < RealPosCol; m++)//写入数据到新数组
            {
                for (int n = 0; n < RealPosRow; n++)
                {
                    if (PosArray[m, n].R != FakerPos.R)
                    {
                        PosArrays[m, n] = PosArray[m, n];
                    }
                    else
                    {
                        PosArrays[m, n] = FakerPos;
                    }
                }
            }
            #endregion

        }

        /// <summary>
        /// 计算点相对Wafer中心的绝对坐标)
        /// </summary>
        /// <param name="pos">要计算的点</param>
        /// <param name="PosInTray">在tray上的绝对坐标</param>
        public PointF PosRelativeCenter(Point3D pos)
        {
            PointF PosInTray = new PointF((float)(pos.X - _TrayCenter.X), (float)(pos.Y - _TrayCenter.Y));
            return PosInTray;
        }
        /// <summary>
        /// 计算点在Map上的像素坐标
        /// </summary>
        /// <param name="pos">要计算的点</param>
        /// <param name="PosInTray">在tray上的绝对坐标</param>
        public PointF PosInMap(Point3D pos)
        {
            PointF PosInMap = new PointF(Convert.ToSingle(MillimetersToPixelsWidth(pos.X - TrayCenter.X, 0) + (_Map.Size.Width / 2)),
            Convert.ToSingle(MillimetersToPixelsWidth(pos.Y - TrayCenter.Y, 1) + (_Map.Size.Width / 2)));
            return PosInMap;
        }
        /// <summary>
        /// 计算点是否在产品上,如果在产品上，返回点处在的产品序号
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool PosOnProduct(PointF pos,ref int index)
        {
            bool state = false;
            index = -1;
            foreach(Product p in Products)
            {
                if((pos.X>p.PixelPos.LUPos.X&&pos.X<p.PixelPos.RUPos.X)&&(pos.Y>p.PixelPos.LUPos.Y&&pos.Y<p.PixelPos.LDPos.Y))
                {
                    index = p.Index;
                    state = true;
                }
            }
            return state;
        }

        /// <summary>
        /// 计算产品是否在一个区域内，如果在，返回所有区域内产品的序号
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool PosInZone(PointF pos1,PointF pos2, ref List<int> index)
        {
            bool state = false;
            foreach (Product p in Products)
            {
                if ((p.PixelPos.CenterPos.X>pos1.X&&p.PixelPos.CenterPos.X<pos2.X)
                    &&(p.PixelPos.CenterPos.Y>pos1.Y&&p.PixelPos.CenterPos.Y<pos2.Y))
                {
                    index.Add(p.Index);
                    state = true;
                }
            }
            return state;
        }

        #region 毫米-像素换算
        /// <summary>
        /// 毫米换算像素
        /// </summary>
        /// <param name="length">毫米长度</param>
        /// <param name="Mode">换算模式 0=X方向 1=Y方向</param>
        /// <returns></returns>
        protected double MillimetersToPixelsWidth(double length, int Mode)
        {
            float ScaleX = 0, ScaleY = 0;
            GetDPIScale(ref ScaleX, ref ScaleY);
            if (Mode == 0)
            {
                return ((double)length * 96.0 * ScaleX / 24);
            }
            else
            {
                return ((double)length * 96.0 * ScaleY / 24);
            }

        }

        int iH = Screen.PrimaryScreen.Bounds.Height;
        int iW = Screen.PrimaryScreen.Bounds.Width;

        #region Dll引用
        [DllImport("User32.dll", EntryPoint = "GetDC")]
        private extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "ReleaseDC")]
        private extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("User32.dll")]
        public static extern int GetSystemMetrics(int hWnd);

        const int DESKTOPVERTRES = 117;
        const int DESKTOPHORZRES = 118;

        const int SM_CXSCREEN = 0;
        const int SM_CYSCREEN = 1;

        #endregion

        /// <summary>
        /// 获取DPI缩放比例
        /// </summary>
        /// <param name="dpiscalex"></param>
        /// <param name="dpiscaley"></param>
        protected static void GetDPIScale(ref float dpiscalex, ref float dpiscaley)
        {
            int x = GetSystemMetrics(SM_CXSCREEN);
            int y = GetSystemMetrics(SM_CYSCREEN);
            IntPtr hdc = GetDC(IntPtr.Zero);
            int w = GetDeviceCaps(hdc, DESKTOPHORZRES);
            int h = GetDeviceCaps(hdc, DESKTOPVERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            dpiscalex = (float)w / x;
            dpiscaley = (float)h / y;
        }
        /// <summary>
        /// 获取分辨率
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        protected static void GetResolving(ref int width, ref int height)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            width = GetDeviceCaps(hdc, DESKTOPHORZRES);
            height = GetDeviceCaps(hdc, DESKTOPVERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
        }
        #endregion
        #endregion
    }
}
