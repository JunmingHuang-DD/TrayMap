using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Incube.Motion
{

    [Flags]
    public enum TrayState : int
    {
        Empty = 0, Untested = 1, InTesting = 2, TestPass = 3, TestFail = 4, SkipTest = 5, NextPick = 6, Rotate180 = 7,
    }

    [Serializable]
    public class TrayLocation 
    {
        public static TrayLocation Empty = new TrayLocation(null, false) { Index = -1, Loc = new Point(-1, -1), Pos = new PointF(), Angle = 0 };

        private TrayState _State;
        private TraySet _Tray;

        /// <summary>
        /// 行 列位置, X代表列,Y代表行
        /// </summary>
        public Point Loc { get; set; }

        /// <summary>
        /// 相对于第0个物料,在tray盘上的位置,单位mm
        /// </summary>
        public PointF Pos { get; set; }

        /// <summary>
        /// 产品间距 宽度
        /// </summary>
        public PointF PitchXY { get; set; }

        /// <summary>
        /// 取料后旋转的角度
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 产品长度
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作序号,按照这个序号操作tray位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否启用,有些位置不使用
        /// </summary>
        public bool Activated { get; set; }


        public TrayState Status
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;

                    StateChanged?.Invoke(this, new EventArgs());
                }
            }
        }


        public event EventHandler StateChanged;

        public TrayLocation(TraySet parent, bool activated)
        {
            _Tray = parent;

            Angle = 0;

            Length = 0;

            Activated = activated;

            _State = TrayState.Untested;
        }

        public override string ToString()
        {
            return string.Format("Row:{0} Col:{1}", Loc.Y, Loc.X);
        }

        public static bool operator ==(TrayLocation a, TrayLocation b)
        {
            if ((object)a == null && (object)b == null)
            {
                return true;
            }
            else if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return (a.Loc.X == b.Loc.X) && (a.Loc.Y == b.Loc.Y);
        }

        public static bool operator !=(TrayLocation a, TrayLocation b)
        {
            return !(a == b);
        }

    }

    public class WorkList
    {
        private int _current;
        private List<TrayLocation> _locList;
        public int CurrentIndex { get { return _current; } set { _current = value; } }
        public TrayLocation Current { get { return _current < 0 ? TrayLocation.Empty : _locList[_current]; } }
        public List<TrayLocation> LocList { get { return _locList; } }

        public WorkList(int count = 100)
        {
            _locList = new List<TrayLocation>(count);
            _current = 0;
        }

        public void ChangeCurrent(int row, int col)
        {
            if(_current >= 0)
               _locList[_current].Status = TrayState.Untested;  //jm 2019-04-20

            for (int i = 0; i < _locList.Count; i++)
            {
                if (_locList[i].Loc.X == col && _locList[i].Loc.Y == row)
                {
                    _current = i;
                    break;
                }
            }

            _locList[_current].Status = TrayState.NextPick;
        }

        public void MoveNext(bool getMaxLength = false)
        {
            int start = _current;
            _current = -1;

            for (int i = start; i < _locList.Count; i++)
            {
                if (_locList[i].Status == TrayState.Untested || _locList[i].Status == TrayState.InTesting || _locList[i].Status == TrayState.Rotate180 || _locList[i].Status == TrayState.NextPick)
                {
                    _current = i;
                    break;
                }
            }

            //对于状态已经为intesting的情况,不设置其状态
            if (_current < 0 || _locList[_current].Status == TrayState.InTesting)
            {
                return;
            }

            _locList[_current].Status = TrayState.NextPick;
        }

        public void ResetAllState()
        {
            _current = 0;

            foreach (var item in _locList)
            {
                item.Status = TrayState.Untested;
            }

            _locList[_current].Status = TrayState.NextPick;
        }

        public bool ExitedRotate180()
        {
            for (int i = 0; i < _locList.Count; i++)
            {
                if (_locList[i].Status == TrayState.Rotate180)
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetFirstStart()
        {
            for (int i = 0; i < _locList.Count; i++)
            {
                if (_locList[i].Status == TrayState.Untested || _locList[i].Status == TrayState.InTesting || _locList[i].Status == TrayState.Rotate180)
                {
                    _current = i;
                    _locList[_current].Status = TrayState.NextPick;
                    return true;
                }
            }

            return false;
        }

        public void SetAllSkip()
        {
            foreach (var item in _locList)
            {
                item.Status = TrayState.SkipTest;
            }

            _current = -1; //jm 2019-04-20
        }

        public List<TrayLocation> GetFailLocation()
        {
            List<TrayLocation> loc = new List<TrayLocation>();

            foreach (var pos in _locList)
            {
                if(pos.Status == TrayState.TestFail)
                {
                    loc.Add(pos);   //获取 TestFail 位置
                }
            }

            return loc;
        }

        /// <summary>
        /// 查找名字  name 是否没有 test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetNameNotTest(string name)
        {
            int start = _current;
            for (int i = start; i < _locList.Count && i >=0; i++)
            {
                if (_locList[i].Status == TrayState.Untested || _locList[i].Status == TrayState.InTesting || _locList[i].Status == TrayState.NextPick)
                {
                    if (_locList[i].Name == name) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 添加位置
        /// </summary>
        /// <param name="loc"></param>
        internal void AddItem(TrayLocation loc)
        {
            _locList.Add(loc);
        }

        /// <summary>
        /// 将整个需要操作的位置按照tray位置的序号排序,这样后续的操作将不再考虑顺序
        /// </summary>
        internal void Sort()
        {
            //_locList.Sort(new Comparison<TrayLocation>((t1, t2) => { return t1.Index > t2.Index ? 1 : -1; }));
            _locList.Sort((t1, t2) => { return t1.Index == t2.Index ? 0 : t1.Index > t2.Index ? 1 : -1; });

            _locList[0].Status = TrayState.NextPick;
        }
    }

    [Serializable]
    public class TraySet : ISerializable
    {
        protected bool _activated;
        protected TrayLocation[,] _trayset;
        protected List<WorkList> _workLocList = new List<WorkList>();
        protected bool _isBaseTraySet;

        /// <summary>
        /// tray 名称
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tray盘总共的行列数
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// 单个Tray盘的行列数
        /// </summary>
        public Size TraySize { get; set; }

        /// <summary>
        /// 第一个工作位置列表
        /// </summary>
        public WorkList MainList { get { return _workLocList[0]; } }


        public TrayLocation[,] Trayset { get { return _trayset; }set { _trayset = value; } }

        public bool IsBaseTraySet
        {
            get { return _isBaseTraySet; }
            set { _isBaseTraySet = value; }
        }

        /// <summary>
        /// 当前工作位置
        /// </summary>
        public TrayLocation MainLoc { get { return _workLocList[0].Current; } }

        public TrayLocation this[int row, int col]
        {
            get { return _trayset[row, col]; }
        }

        public void SetPos(int row,int col,double x,double y)
        {
            if (row > Size.Height || col > Size.Width) return;

            _trayset[row, col].Pos = new PointF((float)x, (float)y);

            _trayset[row, col].Status = TrayState.Untested;
        }

        public void SetPos(int row, int col, double x, double y, double r,double l)
        {
            if (row > Size.Height || col > Size.Width) return;

            _trayset[row, col].Pos = new PointF((float)x, (float)y);

            _trayset[row, col].Angle = r;

            _trayset[row, col].Length = l;

            _trayset[row, col].Status = TrayState.Untested;
        }

        public bool Activated
        {
            get { return _activated; }
            set
            {
                _activated = value;
                StateChanged?.Invoke(this, null);
            }
        }

        public event EventHandler StateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traymap">tray设置的json文件</param>
        public TraySet(string traymap,bool baseTraySet= true)
        {
            _activated = true;
            _isBaseTraySet = true;

            if (traymap != null && traymap.Length > 1)
            {
                LoadConfig(traymap);
                GenerateWorkList();
            }
            
        }

        #region TrayLocation状态设置

        public virtual void SetCurrentState(TrayState state)
        {
            _workLocList[0].Current.Status = state;
        }

        public void SetLocationState(int row, int col, TrayState state)
        {
            if (_trayset[row, col].Activated)
            {
                _trayset[row, col].Status = state;

             //   _workLocList[0].CurrentIndex = _trayset[row, col].Index;
            }
        }

        /// <summary>
        /// 将所有位置重置为untested
        /// </summary>
        public virtual void ResetAllState()
        {
            foreach (var worklist in _workLocList)
            {
                worklist.ResetAllState();
            }
        }

        public void SkipAll()
        {
            foreach (var worklist in _workLocList)
             {
                worklist.SetAllSkip();
             }
         }

        /// <summary>
        /// 是否存在需要旋转180度的产品
        /// </summary>
        /// <returns></returns>
        public bool GetAllListExitRotate180()
        {
            foreach (var worklist in _workLocList)
            {
                if (worklist.ExitedRotate180()) return true;
            }

            return false;
        }

        public virtual bool GetFirstStart(bool getMaxLength = false)
        {
            foreach (var worklist in _workLocList)
            {
                if (worklist.GetFirstStart()) return true;
            }

            return false;
        }


        /// <summary>
        /// 获取所以失败的位置
        /// </summary>
        /// <returns></returns>
        public List<TrayLocation> GetAllFail()
        {
            List<TrayLocation> loc = new List<TrayLocation>();

            foreach (var worklist in _workLocList)
            {
                List<TrayLocation> loc1 = new List<TrayLocation>();

                loc1 = worklist.GetFailLocation();

                if(loc1.Count >0)
                {
                    loc.AddRange(loc1);
                }
            }

            return loc;
        }

        /// <summary>
        /// 获取是否有 name的产品没有 test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetProductNotTest(string name)
        {
            foreach (var worklist in _workLocList)
            {
                if (worklist.GetNameNotTest(name)) return true;
            }

            return false;
        }
        #endregion

        public virtual void SetAsCurrent(int row, int col)
        {
            _workLocList[0].ChangeCurrent(row, col);
        }

        public virtual void MoveNext(bool getMaxLength = false)
        {
            _workLocList[0].MoveNext(getMaxLength);
        }

        protected virtual void GenerateWorkList()
        {
            _workLocList.Clear();

            var workList = new WorkList();
            _workLocList.Add(workList);

            for (int i = 0; i < Size.Height; i++)
            {
                for (int j = 0; j < Size.Width; j++)
                {
                    if (_trayset[i, j].Activated)
                    {
                        workList.AddItem(_trayset[i, j]);
                    }
                }
            }

            workList.Sort();
        }

        #region 加载trapmap
        public void ReloadTray(string traymap)
        {
            LoadConfig(traymap);
            GenerateWorkList();
        }

        private void LoadConfig(string traymap)
        {
            using (var file = new FileStream(traymap, FileMode.Open, FileAccess.Read))
            {
                var sr = new StreamReader(file);

                JObject setting = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());

                Id = setting["traymap"]["id"].ToString();

                #region 加载tray参数
                int row = (int)setting["traymap"]["row"];
                int col = (int)setting["traymap"]["col"];
                bool sloop = (bool)setting["traymap"]["s_loop"];
                bool trayLeftToRight = (bool)setting["traymap"]["left_to_right"];


                //tray pitch
                bool samePitch = (bool)setting["traymap"]["pitch"]["same_distance"];
                var trayPitchX = new List<float>();
                var trayPitchY = new List<float>();
                JArray trayPitch = (JArray)setting["traymap"]["pitch"]["pitchX"];
                for (int i = 0; i < trayPitch.Count; i++)
                {
                    trayPitchX.Add((float)trayPitch[i]);
                }
                trayPitch = (JArray)setting["traymap"]["pitch"]["pitchY"];
                for (int i = 0; i < trayPitch.Count; i++)
                {
                    trayPitchY.Add((float)trayPitch[i]);
                }

                //roation
                bool trayRotated = (bool)setting["traymap"]["rotation"]["apply"];
                bool rotatedByRow = (bool)setting["traymap"]["rotation"]["byRow"];
                bool singleRotation = (bool)setting["traymap"]["rotation"]["single"];
                JArray rotateAngles = (JArray)setting["traymap"]["rotation"]["angles"];
                var trayAngles = new List<float>();
                for (int i = 0; i < rotateAngles.Count; i++)
                {
                    trayAngles.Add((float)rotateAngles[i]);
                }

                //name
                bool nameByColumn = (bool)setting["traymap"]["NameID"]["bycolumn"];
                bool nameByRow = (bool)setting["traymap"]["NameID"]["byRow"];
                bool nameSame = (bool)setting["traymap"]["NameID"]["sameName"];
                JArray names = (JArray)setting["traymap"]["NameID"]["names"];
                var nameIDS = new List<string>();
                for (int i = 0; i < names.Count; i++)
                {
                    nameIDS.Add((string)names[i]);
                }

                //array setting
                bool hasTrayArray = (bool)setting["array"]["apply"];
                bool trayArrayRowFirst = (bool)setting["array"]["row_first"];
                int trayArrayRow = (int)setting["array"]["rows"];
                int trayArrayCol = (int)setting["array"]["cols"];
                float arrayGapX = (float)setting["array"]["gap"]["x"]; //这里的间隙(Gap)是指两个Tray之间,同一行或者同一列,最后一个物料和下一个tray第一个物料,最近的距离
                float arrayGapY = (float)setting["array"]["gap"]["y"];


                //activation
                bool allActivated = (bool)setting["traymap"]["activation"]["all"];
                var validInfo = (JArray)setting["traymap"]["activation"]["blocks"];

                #endregion

                TraySize = new Size(col, row);
                Size = new Size(col * trayArrayCol, row * trayArrayRow);
                _trayset = new TrayLocation[row * trayArrayRow, col * trayArrayCol];

                #region Generate Tray
                if (trayArrayRowFirst)
                {
                    for (int i = 0; i < trayArrayRow; i++)
                    {
                        for (int j = 0; j < trayArrayCol; j++)
                        {
                            int index = (i * trayArrayCol + j) * row * col;
                            GenerateTray(row, col, sloop, i, j, index, trayLeftToRight);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < trayArrayCol; j++)
                    {
                        for (int i = 0; i < trayArrayRow; i++)
                        {
                            int index = (j * trayArrayRow + i) * row * col;
                            GenerateTray(row, col, sloop, i, j, index, trayLeftToRight);
                        }
                    }
                }
                #endregion

                CaleTrayPos(singleRotation,trayRotated, rotatedByRow, trayAngles,
                            row, col, trayLeftToRight,
                            samePitch, trayPitchX, trayPitchY,
                            arrayGapX, arrayGapY,
                            nameSame, nameByColumn,nameByRow,nameIDS);


                //加载取消激活的部分
                if (allActivated)
                {
                    return;
                }

                for (int i = 0; i < validInfo.Count; i++)
                {
                    string data = (string)validInfo[i];
                    for (int j = 0; j < data.Length; j++)
                    {
                        _trayset[i, j].Activated = data[j] == '1';
                    }
                }
            }

        }

        private void GenerateTray(int row, int col, bool sLoop, int arrayRow, int arrayCol, 
                                  int startIndex,bool trayLeftToRight)
        {
            for (int m = 0; m < row; m++)
            {

                for (int n = 0; n < col; n++)
                {
                    TrayLocation block = new TrayLocation(this, true);
                    #region 计算tray loc 的index序号
                    if (trayLeftToRight)
                    {
                        if (sLoop)
                        {
                            if (m % 2 == 0)
                            {
                                block.Index = startIndex + m * col + n;
                            }
                            else
                            {
                                block.Index = startIndex + (m + 1) * col - n - 1;
                            }
                        }
                        else
                        {
                            block.Index = startIndex + m * col + n;
                        }
                    }
                    else
                    {
                        if (sLoop)
                        {
                            if (m % 2 == 1)
                            {
                                block.Index = startIndex + m * col + n;
                            }
                            else
                            {
                                block.Index = startIndex + (m + 1) * col - n - 1;
                            }
                        }
                        else
                        {
                            block.Index = startIndex + (m + 1) * col - n - 1;
                        }
                    }
                    #endregion

                    block.Index = block.Index + 1;

                    block.Loc = new Point(
                        x: arrayCol * col + n,
                        y: arrayRow * row + m
                        );

                    _trayset[block.Loc.Y, block.Loc.X] = block;
                }
            }
        }

        private void CaleTrayPos(bool single,bool rotate, bool rotateByRow, List<float> rotateAngles,
            int trayRow, int trayCol, bool trayLeftToRight,
            bool samePitch, List<float> pitchX, List<float> pitchY,
            float arrayGapX, float arrayGapY,
            bool sameName,bool nameByColumn, bool nameByRow,List<string>nameList)
        {

            for (int i = 0; i < Size.Height; i++)
            {
                for (int j = 0; j < Size.Width; j++)
                {
                    if (rotate||single)
                    {
                        if (rotateByRow)
                        {
                            if(i%2 == 0)
                            {
                                _trayset[i, j].Angle = rotateAngles[0];
                            }else 
                            {
                                _trayset[i, j].Angle = rotateAngles[1];
                             }
                        }
                        else
                        {
                            _trayset[i, j].Angle = rotateAngles[j];
                        }
                     }

                    //name
                    if(sameName)
                    {
                        _trayset[i, j].Name = nameList[0];
                    }
                    else
                    {
                       if(nameByRow)
                        {
                            _trayset[i, j].Name = nameList[i];
                        }
                       else
                        {
                            _trayset[i, j].Name = nameList[j];
                        }
                    }

                    //计算tray loc的位置
                    float x = 0, y = 0;
                    int row = i, col = j;
                    if (!trayLeftToRight)
                    {
                        col = trayCol - j - 1; //当从右到左的时候,将右上角作为基准位置
                    }

                    if (samePitch)
                    {
                        x = pitchX[0] * j + (j / trayCol == 0 ? 0 : j / trayCol * arrayGapX - pitchX[0] * j);
                        float ac = Convert.ToInt32(Size.Height) / trayRow + 1;
                        if (i == 0)
                        {
                            y = 0;
                        }
                        else if (i <= trayRow - 1)
                        {
                            y = pitchY[0] * i;
                        }
                        else
                        {
                            for (int z = 0; z < ac; z++)
                            {
                                if (i >= (z + 1) * trayRow && i < (z + 2) * trayRow)
                                    y = pitchY[0] * i - ((pitchY[0] - arrayGapY) * (z + 1));
                            }
                        }
                    }
                    else
                    {
                        for (int m = 1; m < j; m++) //第0列X间距为0
                        {
                            if (m % trayCol == 0)
                            {
                                x += arrayGapX;
                            }
                            else
                            {
                                x += pitchX[m % trayCol];
                            }
                        }

                        for (int m = 1; m < i; m++) //第0行Y间距为0
                        {
                            if (m % trayRow == 0)
                            {
                                y += arrayGapY;
                            }
                            else
                            {
                                y += pitchY[m % trayRow];
                            }
                        }
                    }

                    _trayset[i, j].Pos = new PointF(x, y);

                    _trayset[i, j].PitchXY = new PointF(pitchX[0], pitchY[0]);
                }

            }
        }

        #endregion

        #region save and read data
        /// <summary>
        /// 反序列化,获取数据
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public TraySet(SerializationInfo info, StreamingContext context)
        {
            _trayset = (TrayLocation[,])info.GetValue("trayset", typeof(TrayLocation[,]));
        }

        /// <summary>
        /// 序列化数据到文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("trayset", _trayset);
        }
        #endregion

        public override string ToString()
        {
            return "Tray " + Id;
        }
    }

    /// <summary>
    /// 适用于双吸嘴顺序贴装
    /// </summary>
    public class SequentialDualTray : TraySet
    {
        private int _nextLoc, _preview;

        public TrayLocation NextLoc { get { return _nextLoc < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_nextLoc]; } }

        public TrayLocation PreviewLoc { get { return _preview < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_preview]; } }

        public SequentialDualTray(string traymap) : base(traymap,false)
        {
            _nextLoc = 1;
        }

        public override void ResetAllState()
        {
            base.ResetAllState();

            _nextLoc = 1;
            _preview = 2;            
        }

        public override void SetAsCurrent(int row, int col)
        {
            base.SetAsCurrent(row, col);

            int start = _workLocList[0].CurrentIndex + 1;

            _nextLoc = FindNext(start);   //   5-21 Imp 这里选择当前位置下一个物料位置应该在当前选择的位置加1
        }

        public void SetDoubleState(int index)
        {
            _workLocList[index + 3].Current.Status = TrayState.TestPass; //测试
        }

        public override void MoveNext(bool getMaxLength = false)
        {
            if (MainLoc.Status != TrayState.InTesting && MainLoc.Status != TrayState.Untested)
            {
                _workLocList[0].MoveNext();
            }

            _nextLoc = FindNext(_nextLoc + 1);

            _preview = FindNext(_nextLoc + 1);
        }

        private int FindNext(int start)
        {
            int loc = -1;

            for (int i = start; i < _workLocList[0].LocList.Count; i++)
            {
                if (_workLocList[0].LocList[i].Status == TrayState.Untested ||
                    _workLocList[0].LocList[i].Status == TrayState.InTesting)
                {
                    loc = i;
                    break;
                }
            }

            return loc;
        }

        public void SetNextLocState(TrayState state)
        {
            if (_nextLoc < 0)
            {
                return;
            }

            NextLoc.Status = state;
        }
    }

    /// <summary>
    /// 适用于左右吸嘴分开贴一半的情况
    /// </summary>
    public class SplitDualTray : TraySet
    {
        /// <summary>
        /// 第2个工作位置列表
        /// </summary>
        public WorkList SecondList { get { return _workLocList[1]; } }

        /// <summary>
        /// 当前工作位置
        /// </summary>
        public TrayLocation SecondLoc { get { return _workLocList[1].Current; } }

        public SplitDualTray(string traymap) : base(traymap)
        {
        }

        protected override void GenerateWorkList()
        {
            _workLocList.Clear();

            var workList = new WorkList();
            _workLocList.Add(workList);
            workList = new WorkList();
            _workLocList.Add(workList);

            bool evenTray = Size.Width % 2 == 0;

            for (int i = 0; i < Size.Height; i++)
            {
                for (int j = 0; j < Size.Width; j++)
                {
                    if (_trayset[i, j].Activated)
                    {
                        if (evenTray)
                        {
                            _workLocList[2 * j / Size.Width].AddItem(_trayset[i, j]);
                        }
                        else
                        {
                            //偶数行多一个
                            if (j == Size.Width - 1)
                            {
                                _workLocList[1].AddItem(_trayset[i, j]);
                            }
                            else
                            {
                                _workLocList[2 * (j + i % 2) / Size.Width].AddItem(_trayset[i, j]);
                            }

                        }

                    }
                }
            }

            foreach (var loclist in _workLocList)
            {
                loclist.Sort();
            }
        }
    }

    /// <summary>
    /// 适用于双吸嘴 隔N个点贴装,比如说隔1给点;
    /// 列数多的时候,S走位,可能不理想
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class IntervalPointsDualTray : TraySet
    {
        private int _nextLoc, _previewNext;
        private int _intervalPoints;

        /// <summary>
        /// 下一个位置;MainLoc 是第一个位置（理解为左吸嘴）,NextLoc是下一个位置（可以理解为右吸嘴）;
        /// </summary>
        public TrayLocation NextLoc { get { return _nextLoc < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_nextLoc]; } }

        /// <summary>
        /// 下一个轮的第一个位置;用于判断当前Tray是否为空,或者相机提前移动的下一个位置;
        /// </summary>
        public TrayLocation PreviewNextLoc { get { return _previewNext < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_previewNext]; } }

        /// <summary>
        /// 构造函数,指定加载文件路径及间隔个数
        /// </summary>
        /// <param name="traymap"></param>
        /// <param name="intervalPoints"></param>
        public IntervalPointsDualTray(string traymap, int intervalPoints = 1) : base(traymap)
        {
            _nextLoc = 1;
            _intervalPoints = intervalPoints;
        }

        public override void ResetAllState()
        {
            base.ResetAllState();
            _nextLoc = 2;
            _previewNext = 1;
            SetLocationState(0, _nextLoc, TrayState.NextPick);
        }

        public override void SetAsCurrent(int row, int col)
        {
            SetLocationState(row, col, TrayState.Untested);
            if (NextLoc.Status == TrayState.NextPick)
            {
                SetLocationState(NextLoc.Loc.Y, NextLoc.Loc.X, TrayState.Untested);
            }

            int start = _workLocList[0].CurrentIndex;
            _nextLoc = FindNext(start);
            base.SetAsCurrent(row, col);

            SetLocationState(NextLoc.Loc.Y, NextLoc.Loc.X, TrayState.NextPick);
        }

        public override void MoveNext(bool getMaxLength = false)
        {
            if (MainLoc.Status != TrayState.InTesting && MainLoc.Status != TrayState.Untested)
            {
                _workLocList[0].MoveNext();
            }

            SetLocationState(_workLocList[0].Current.Loc.Y, _workLocList[0].Current.Loc.X, TrayState.Untested);
            _nextLoc = FindNext(_workLocList[0].CurrentIndex - 1);
            SetLocationState(_workLocList[0].Current.Loc.Y, _workLocList[0].Current.Loc.X, TrayState.NextPick);

            if (NextLoc.Loc.X >= 0 && NextLoc.Loc.Y >= 0)
            {
                SetLocationState(NextLoc.Loc.Y, NextLoc.Loc.X, TrayState.NextPick);
            }

            _previewNext = FindNext(_nextLoc + 1);
        }

        /// <summary>
        /// 还有点缺陷,目前只能成双成对,如果中途Skip一个,顺序就没有那么美好了
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private int FindNext(int start)
        {
            int loc = -1;
            int UntestedCounter = 0;

            for (int i = start; i < _workLocList[0].LocList.Count; i++)
            {
                if (_workLocList[0].LocList[i].Status == TrayState.Untested ||
                    _workLocList[0].LocList[i].Status == TrayState.InTesting)
                {
                    UntestedCounter++; //连续 没有测试Untested的个数

                    if (UntestedCounter > _intervalPoints + 1)//连续intervalPoints +1个 没有测试
                    {
                        if (_workLocList[0].LocList[i].Status == TrayState.Untested ||
                            _workLocList[0].LocList[i].Status == TrayState.InTesting)
                        {
                            loc = i;
                            break;
                        }
                    }
                    else if (i == _workLocList[0].LocList.Count - 1)
                    {
                        loc = i;
                        break;
                    }
                }
            }

            return loc;
        }

        public void SetNextLocState(TrayState state)
        {
            if (_nextLoc < 0)
            {
                return;
            }
            NextLoc.Status = state;
        }
    }

    /// <summary>
    /// 拾取不同长度的条
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DifferenceLenghtTray : TraySet
    {
        private List<double> _columnAverageLength = new List<double>();

        private int _preColumn;

        /// <summary>
        /// 构造函数,指定加载文件路径及间隔个数
        /// </summary>
        /// <param name="traymap"></param>
        /// <param name="intervalPoints"></param>
        public DifferenceLenghtTray(string traymap) : base(traymap)
        {
            
        }

        /// <summary>
        /// 获取每一列产品长度的平均值
        /// </summary>
        private void GetColumnAverageLenght()
        {
            _columnAverageLength.Clear();

            for(int col = 0; col < Size.Width; col++)
            {
                double total = 0;
                int points = 0;
                for(int row = 0;row < Size.Height;row ++)
                {
                    if(_trayset[row, col].Status == TrayState.Untested 
                        || _trayset[row, col].Status == TrayState.InTesting 
                        || _trayset[row, col].Status == TrayState.Rotate180
                        || _trayset[row, col].Status == TrayState.NextPick)
                    {
                        total += _trayset[row, col].Length;
                        points++;
                    }                  
                }

                double average = total / points;
                _columnAverageLength.Add(average);
            }
        }

        public override bool GetFirstStart(bool getMaxLength = false)
        {
            GetColumnAverageLenght();

            bool success = base.GetFirstStart(getMaxLength);

            if(getMaxLength)
            {
                ReCaculateFirstPos(_columnAverageLength);
            }
            else
            {
                _preColumn = MainList.Current.Loc.X;
            }

            return success;
        }

        public override void MoveNext(bool getMaxLength = false)
        {
            if(getMaxLength == false)
            {
                _workLocList[0].MoveNext(getMaxLength);
                return;
            }
            else
            {
                _workLocList[0].MoveNext(getMaxLength);

                if(MainList.Current.Loc.X != _preColumn)
                {
                    //重新计算起始位置
                    SetCurrentState(TrayState.Untested);

                    ReCaculateFirstPos(_columnAverageLength);
                }
                else
                {
                    _preColumn = MainList.Current.Loc.X;
                }
            
            }
        }

        /// <summary>
        /// 重新计算起始位置
        /// </summary>
        private void ReCaculateFirstPos(List<double> columnAverage)
        {
            List<double> av = new List<double>();
            foreach(var d in columnAverage)
            {
                av.Add(d);
            }
            av.Sort();
            av.Reverse();

            for (int i = 0;i< av.Count;i++)
            {
                for(int col = 0; col < _columnAverageLength.Count; col++)
                {
                    if(Math.Abs(av[i] - _columnAverageLength[col]) < 0.2)
                    {
                        for(int row = 0; row < Size.Height; row++)
                        {
                            if (_trayset[row, col].Status == TrayState.Untested
                              || _trayset[row, col].Status == TrayState.InTesting
                              || _trayset[row, col].Status == TrayState.Rotate180
                              || _trayset[row, col].Status == TrayState.NextPick)
                            {
                                _preColumn = col;
                                SetLocationState(row, col, TrayState.NextPick);      //找到新的一列
                                SetAsCurrent(row, col);
                                return;
                            }
                        }
                    }
                }
            }

         }


    }
}
