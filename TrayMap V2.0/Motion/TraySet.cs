using Motion;
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
        Empty = 0, Untested = 1, InTesting = 2, TestPass = 3, TestFail = 4, SkipTest = 5, NextPick = 6, Glue = 7
    }

    [Serializable]
    public class TrayLocation 
    {
        public static TrayLocation Empty = new TrayLocation(null, false) { Index = -1, Loc = new Point(-1, -1), Pos = new PointF(), Angle = 0 };

        private TrayState _State;
        private TraySet _Tray;

        /// <summary>
        /// 行 列位置， X代表列，Y代表行
        /// </summary>
        public Point Loc { get; set; }

        /// <summary>
        /// 相对于第0个物料，在tray盘上的位置，单位mm
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
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于保存当前产品标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 工作序号，按照这个序号操作tray位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否启用，有些位置不使用
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

        public int CurrentIndex { get { return _current; } }

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

        public void MoveNext()
        {
            int start = _current + 1;
            _current = -1;

            for (int i = start; i < _locList.Count; i++)
            {
                if (_locList[i].Status == TrayState.Untested || _locList[i].Status == TrayState.InTesting)
                {
                    _current = i;
                    break;
                }
            }

            //对于状态已经为intesting的情况，不设置其状态
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

                item.Label = "";
            }

            _locList[_current].Status = TrayState.NextPick;
        }

        public void GetFirstStart()
        {
            for (int i = 0; i < _locList.Count; i++)
            {
                if (_locList[i].Status == TrayState.Untested || _locList[i].Status == TrayState.InTesting)
                {
                    _current = i;
                    _locList[_current].Status = TrayState.NextPick;
                    break;
                }
            }
        }

        public void SetAllSkip()
        {
            foreach (var item in _locList)
            {
                item.Status = TrayState.SkipTest;
            }

            _current = -1; //jm 2019-04-20
        }

        /// <summary>
        /// 获取 TestFail 的位置信息
        /// </summary>
        /// <returns></returns>
        public List<TrayLocation> GetFialLocation()
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
        /// 获取名称为 name的第一个位置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TrayLocation GetLocationByName(string name)
        {
            TrayLocation loc = TrayLocation.Empty;

            foreach (var pos in _locList)
            {
                if (pos.Name == name && (pos.Status == TrayState.Untested || pos.Status == TrayState.InTesting) )
                {
                    loc = pos;   //获取 TestFail 位置
                    break;
                }
            }

            return loc;
        }

        public List<string> GetLocationLabel()
        {
            List<string> list = new List<string>();

            foreach (var pos in _locList)
            {
                list.Add(pos.Label);
            }

            return list;
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
        /// 将整个需要操作的位置按照tray位置的序号排序，这样后续的操作将不再考虑顺序
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

        public bool IsShowMode_Down2Up { get; set; }

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

        public virtual void SetCurrentLabel(string label)
        {
            _workLocList[0].Current.Label = label;
        }

        public List<string> GetLocationLabel()
        {
            List<string> list = new List<string>();

            foreach (var worklist in _workLocList)
            {
                List<string> wlist = worklist.GetLocationLabel();
                foreach(var v in wlist)
                {
                    list.Add(v);
                }
            }

            return list;
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
        /// 获取所以失败的位置
        /// </summary>
        /// <returns></returns>
        public List<TrayLocation> GetAllFail()
        {
            List<TrayLocation> loc = new List<TrayLocation>();

            foreach (var worklist in _workLocList)
            {
                List<TrayLocation> loc1 = new List<TrayLocation>();

                loc1 = worklist.GetFialLocation();

                if(loc1.Count >0)
                {
                    loc.AddRange(loc1);
                }
            }

            return loc;
        }

        public TrayLocation GetPosByName(string name)
        {
            TrayLocation loc = TrayLocation.Empty;

            foreach (var worklist in _workLocList)
            {
                var pos = worklist.GetLocationByName(name);
                if(pos != TrayLocation.Empty)
                {
                    loc = pos;break;
                }
            }

            return loc;
        }
        #endregion

        public virtual void SetAsCurrent(int row, int col)
        {
            _workLocList[0].ChangeCurrent(row, col);
        }

        public virtual void MoveNext()
        {
            _workLocList[0].MoveNext();
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
                    _trayset[i, j].Label = "";
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

                var setting = JsonConvert.DeserializeObject<jsonFile>(sr.ReadToEnd());

                if(setting.totalCol == 0 && setting.totalRow == 0)
                {
                    return;
                }

                //初始化TraySet的大小
                Size = new Size(setting.totalCol, setting.totalRow);
                _trayset = new TrayLocation[setting.totalRow, setting.totalCol]; 

                foreach (var e in setting.total)
                {
                    TrayLocation block = new TrayLocation(this, true);
                    block.Index = e.Index;
                    block.Loc = new Point(e.col, e.row);
                    _trayset[e.row, e.col] = block;

                    _trayset[e.row, e.col].Activated = e.Active == 1;
                    _trayset[e.row, e.col].Angle = e.R;
                    _trayset[e.row, e.col].Name = e.Name;
                    _trayset[e.row, e.col].Pos = new PointF((float)e.X, (float)e.Y);

                    _trayset[e.row, e.col].PitchXY = new PointF((float)setting.pitch_x, (float)setting.pitch_y);
                }
            }

        }


        #endregion

        #region save and read data
        /// <summary>
        /// 反序列化，获取数据
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

        public override void MoveNext()
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
    /// 适用于双吸嘴 隔N个点贴装，比如说隔1给点;
    /// 列数多的时候，S走位，可能不理想
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class IntervalPointsDualTray : TraySet
    {
        private int _nextLoc, _previewNext;
        private int _intervalPoints;

        /// <summary>
        /// 下一个位置；MainLoc 是第一个位置（理解为左吸嘴），NextLoc是下一个位置（可以理解为右吸嘴）；
        /// </summary>
        public TrayLocation NextLoc { get { return _nextLoc < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_nextLoc]; } }

        /// <summary>
        /// 下一个轮的第一个位置；用于判断当前Tray是否为空，或者相机提前移动的下一个位置；
        /// </summary>
        public TrayLocation PreviewNextLoc { get { return _previewNext < 0 ? TrayLocation.Empty : _workLocList[0].LocList[_previewNext]; } }

        /// <summary>
        /// 构造函数，指定加载文件路径及间隔个数
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
            { SetLocationState(NextLoc.Loc.Y, NextLoc.Loc.X, TrayState.Untested); }

            int start = _workLocList[0].CurrentIndex;
            _nextLoc = FindNext(start);
            base.SetAsCurrent(row, col);

            SetLocationState(NextLoc.Loc.Y, NextLoc.Loc.X, TrayState.NextPick);
        }

        public override void MoveNext()
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
        /// 还有点缺陷，目前只能成双成对，如果中途Skip一个，顺序就没有那么美好了
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
}
