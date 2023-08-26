using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Configuration;

namespace Incube.Motion
{
    public class MotionFactory : IDisposable  //
    {
        private NLog.ILogger _Logger = NLog.LogManager.GetLogger("Motion Factory");

        //private IAxis _Axis;
        private static object _Lock = new object();
        protected static MotionFactory _Motion;

        private Thread _Monitor;

        protected Dictionary<string, IMotionCard> _MotionCards = new Dictionary<string, IMotionCard>();
        protected Dictionary<string, IIOCard> _IOCards = new Dictionary<string, IIOCard>();
        protected Dictionary<string, IInputLine> _Inputs = new Dictionary<string, IInputLine>();
        protected Dictionary<string, IOutputLine> _Outputs = new Dictionary<string, IOutputLine>();
        protected Dictionary<string, IAxis> _Axes = new Dictionary<string, IAxis>();
        protected List<IAxis> _PauseIgnoredAxes = new List<IAxis>();//不能暂停的轴
        //protected IAxis _Y;

        protected Dictionary<string, CardElement> _AvailableCardTypes = new Dictionary<string, CardElement>();//雷塞
        protected List<string> _AvailableCardTypes2 = new List<string>();//固高

        //public IAxis Y { get { return _Y; } }
        public List<IAxis> PauseIgnoredAxes { get { return _PauseIgnoredAxes; } }

        public Dictionary<string, IMotionCard> MotionCards 
        { 
            get { return _MotionCards; } 
        }

        public Dictionary<string, IIOCard> IOCards //IO卡
        { 
            get { return _IOCards; } 
        }

        public Dictionary<string, IInputLine> Inputs//输入
        {
            get { return _Inputs; }
        }

        public Dictionary<string, IOutputLine> Outputs//输出
        {
            get { return _Outputs; }
        }

        public Dictionary<string, IAxis> Axes 
        {
            get { return _Axes; }
        }

        /// <summary>
        /// singleton object of motionfactory
        /// </summary>
        public static MotionFactory Instance 
        { 
            get 
            {
                lock (_Lock)
                {
                    if (_Motion == null)
                    {
                        
                        _Motion = new MotionFactory();//固高和雷塞卡不一样,需要注意
                                      
                    }
                }

                return _Motion; 
            } 
        }

        /// <summary>
        /// indicated the controller has been connected
        /// </summary>
        public bool Connected { get; protected set; }

        /// <summary>
        /// whether the machine is at emergency stop state
        /// </summary>
        public bool EmergencyStopped { get; protected set; }

        /// <summary>
        /// used for change all axes speed when debug machine
        /// </summary>
        public double SpeedScale { get; set; }//速度比例

        /// <summary>
        /// the delay to update all motion cards state, unit is ms
        /// </summary>
        public int UpdateDelay { get; set; }


        /// <summary>
        /// constructor
        /// </summary>
        protected MotionFactory()
        {
            Init();

            _Monitor = new Thread(new ThreadStart(Update));
            _Monitor.IsBackground = true;
            _Monitor.Priority = ThreadPriority.AboveNormal;
            _Monitor.Start();

            SpeedScale = 1.0;
            EmergencyStopped = false;
            UpdateDelay = 50;
        }

        public void ResetAxisError()
        {
            EmergencyStopped = false;
        }

        public void LoadTypesInfo(IDictionary<string, CardElement> info)
        {
            foreach (var item in info)
            {
                _AvailableCardTypes.Add(item.Key, item.Value);
            }
        }

        public virtual IInputLine GetInput(string name)
        {
            foreach (var card in _IOCards)
            {
                foreach (var line in card.Value.Inputs)
                {
                    if (line.Value.Name == name)
                    {
                        return line.Value;
                    }
                }
            }

            foreach (var card in _MotionCards)
            {
                foreach (var line in card.Value.Inputs)
                {
                    if (line.Value.Name == name)
                    {
                        return line.Value;
                    }
                }
            }

            return null;
        }

        public virtual IOutputLine GetOutput(string name)
        {
            foreach (var card in _IOCards)
            {
                foreach (var line in card.Value.Outputs)
                {
                    if (line.Value.Name == name)
                    {
                        return line.Value;
                    }
                }
            }

            foreach (var card in _MotionCards)
            {
                foreach (var line in card.Value.Outputs)
                {
                    if (line.Value.Name == name)
                    {
                        return line.Value;
                    }
                }
            }

            return null;
        }

        public virtual IAxis GetAxis(string name)
        {
            foreach (var card in _MotionCards)
            {
                foreach (var axis in card.Value.Axes)
                {
                    if (axis.Value.Name == name)
                    {
                        return axis.Value;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// create axis object
        /// </summary>
        /// <param name="name">axis name</param>
        /// <param name="index">axis index, could also contain the card number and card index</param>
        /// <param name="param">other parameters maybe used</param>
        /// <returns>new Axis object</returns>
        public IAxis CreateAxis(AxisParam param)
        {
            IAxis axis = _CreateAxis(param);

            if (axis != null)
            {
                _Axes.Add(param.Name, axis);
            }

            return axis;
        }

        protected virtual IAxis _CreateAxis(AxisParam param)
        {
            IAxis axis = null;

            foreach (var card in _MotionCards)
            {
                if (param.CardID == card.Value.CardID && param.CardType == card.Value.Name)
                {
                    axis = card.Value.AddAxis(param);

                    break;
                }
            }

            if (axis == null)
            {
                IMotionCard card = null;

                //这里想使用依赖注入，但还没有学会
                foreach (var item in _AvailableCardTypes)////////////////////////固高的卡没有这样写
                {
                    if (item.Key.Contains(param.CardType))
                    {
                        //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        //Assembly.LoadFile(Path.Combine(path,  item.Value.Assembly + ".dll"));
                        string typeName = string.Format("{0}, {1}", item.Value.CardNameSpace, item.Value.Assembly);
                        Type cardType = Type.GetType(typeName);

                        card = (IMotionCard)Activator.CreateInstance(cardType, param.CardType, param.CardID);
                        _MotionCards.Add(string.Format("{0}-{1}", card.Name, card.CardID), card);
                        break;
                    }
                }
                if (card != null)
                {
                    axis = card.AddAxis(param);
                }
                
            }
            return axis;
        }

        /// <summary>
        /// create IO input line object
        /// </summary>
        /// <param name="name">line name</param>
        /// <param name="card">host card id</param>
        /// <param name="cardtype">host card type name</param>
        /// <param name="port">line port</param>
        /// <param name="index">line index</param>
        /// <returns>new Line object</returns>
        public IInputLine CreateInputLine(string lineName, string display, int card, string cardtype, int port, int index, bool reversed = false)
        {
            IInputLine line = _CreateInputLine(lineName, display, card, cardtype, port, index, reversed);

            if (line != null)
            {
                _Inputs.Add(lineName, line);
            }
            

            return line;
        }


        protected virtual IInputLine _CreateInputLine(string lineName, string display, int card, string cardtype, int port, int index, bool reversed)
        {
            IInputLine line = null;
            foreach (var mCard in _MotionCards)
            {
                if (mCard.Value.CardID == card && mCard.Value.Name == cardtype)
                {
                    line = mCard.Value.GenerateInput(lineName, display, (ushort)index, (ushort)port, reversed);

                    return line;
                }
            }

            foreach (var iod in _IOCards)
            {
                if (iod.Value.Name == cardtype && iod.Value.CardID == card)
                {
                    line = iod.Value.GenerateInput(lineName, display, (ushort)index, (ushort)port, reversed);

                    return line;
                }
            }

            IIOCard iocard = null;

            //这里想使用依赖注入，但还没有学会
            foreach (var item in _AvailableCardTypes)////////////////////////固高的卡没有这样写
            {
                if (item.Key.Contains(cardtype))
                {
                    string typeName = string.Format("{0}, {1}", item.Value.CardNameSpace, item.Value.Assembly);
                    Type cardType = Type.GetType(typeName);


                    iocard = (IIOCard)Activator.CreateInstance(cardType, cardtype, card);//

                    _IOCards.Add(cardtype + "-" + card, iocard);
                    break;
                }
            }
            if (iocard != null)
            {
                line = iocard.GenerateInput(lineName, display, (ushort)index, (ushort)port, reversed);
            }

            return line;
        }

        /// <summary>
        /// create IO output line object
        /// </summary>
        /// <param name="lineName">line name</param>
        /// <param name="card">host card id</param>
        /// <param name="cardtype">host card type name</param>
        /// <param name="port">line port</param>
        /// <param name="index">line index</param>
        /// <returns>new Line object</returns>
        public IOutputLine CreateOutputLine(string lineName, string display, int card, string cardtype, int port, int index, bool reversed = false)
        {
            IOutputLine line = _CreateOutputLine(lineName, display, card, cardtype, port, index,reversed);

            if (line != null)
            {
                _Outputs.Add(lineName, line);
            }

            return line;
        }


        protected virtual IOutputLine _CreateOutputLine(string lineName, string display, int card, string cardtype, int port, int index, bool reversed)//输出取反
        {
            IOutputLine line = null;
            foreach (var mCard in _MotionCards)
            {
                if (mCard.Value.CardID == card && mCard.Value.Name == cardtype)
                {
                    line = mCard.Value.GenerateOutput(lineName, display, (ushort)index, (ushort)port, reversed);

                    return line;
                }
            }

            foreach (var iod in _IOCards)
            {
                if (iod.Value.Name == cardtype && iod.Value.CardID == card)
                {
                    line = iod.Value.GenerateOutput(lineName, display, (ushort)index, (ushort)port, reversed);

                    return line;
                }
            }

            IIOCard iocard = null;              //?????????????

            //这里想使用依赖注入，但还没有学会
            foreach (var item in _AvailableCardTypes)////////////////////////固高的卡没有这样写
            {
                if (item.Key.Contains(cardtype))
                {
                    string typeName = string.Format("{0}, {1}", item.Value.CardNameSpace, item.Value.Assembly);
                    Type cardType = Type.GetType(typeName);

                    iocard = (IIOCard)Activator.CreateInstance(cardType, cardtype, card);

                    _IOCards.Add(cardtype + "-" + card, iocard);
                    break;
                }
            }
            if (iocard != null)
            {
                line = iocard.GenerateOutput(lineName, display, (ushort)index, (ushort)port, reversed);
            }

            return line;
        }
        
        /// <summary>
        /// connect with all hardware controller, including motion card and IO card
        /// </summary>
        public virtual void Connect() //连接
        {
            if (Connected)
            {
                return;
            }

            Connected = false;


            foreach (var card in _MotionCards)
            {
                bool con = card.Value.Connect();
              
                if (!con)
                {
                    return;
                }
            }


            Connected = true;
        }

        /// <summary>
        /// enumerate all hardware devices
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// disconnect all controllers
        /// </summary>
        protected virtual void Close()
        {
            foreach (var card in _MotionCards)
            {
                card.Value.Disconnect();
            }
        }

        /// <summary>
        /// get update of motion card and IO card status, 
        /// including motion position and IO state
        /// </summary>
        protected virtual void Update() //更新
        {
            
            while (true)
            {
                if (!Connected)
                {
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    //update IO cards
                    foreach (var card in _IOCards)
                    {
                        card.Value.Update();
                    }

                    //update motion cards
                    foreach (var card in _MotionCards)
                    {
                        card.Value.Update();
                    }

                    Thread.Sleep(UpdateDelay);

                }
                catch (Exception ex)
                {
                    _Logger.Error(ex, "Update IO/axes status failed");
                }
                
            }
            
        }


        public virtual void EmergencyStop()//紧急停止
        {
            EmergencyStopped = true;

            foreach (var card in _MotionCards)
            {
                card.Value.Stop();
            }

            foreach (var card in _IOCards)
            {
                card.Value.Stop();
            }
        }

        public virtual void Dispose()
        {
            Connected = false;

            _Monitor.Abort();//终止线程
            _Monitor.Join(200); //等待线程终止的毫秒数。

            Close();

        }

        /// <summary>
        /// pause current motion
        /// </summary>
        public virtual void Pause() //暂停
        {
            foreach (var axis in _Axes)
            {
                if (_PauseIgnoredAxes.Contains(axis.Value))
                {
                    continue;
                }

                axis.Value.Pause();
            }
        }


 

        /// <summary>
        /// resume paused motion
        /// </summary>
        public virtual void Resume()
        {
            foreach (var axis in _Axes)
            {
                if (_PauseIgnoredAxes.Contains(axis.Value))
                {
                    continue;
                }

                axis.Value.Resume();
            }
        }

        //清除轴报警
        public virtual void CleraAlam()
        {
            foreach (var axis in _Axes)
            {
                axis.Value.ClearError();
                Thread.Sleep(5);
            }
        }



        //保存加载设置表的XML文件
        #region save & load settings from XML file
        public void LoadSetting(string file)
        {
            XDocument doc = XDocument.Load(file);
            //string path = Path.GetFullPath(file);

            var axes = doc.Descendants("Axis");
            foreach (var eAxis in axes)
            {
                XElement ehome = eAxis.Element("homeParam");
                CreateAxis(new AxisParam()
                {
                    Name = eAxis.Attribute("Name").Value,
                    DisplayName = eAxis.Attribute("Display").Value,
                    Index = int.Parse(eAxis.Attribute("Index").Value),
                    CardType = eAxis.Attribute("CardType").Value,
                    CardID = int.Parse(eAxis.Attribute("CardID").Value),
                    Direct = int.Parse(eAxis.Attribute("Direct").Value),
                    PositiveLimit = double.Parse(eAxis.Attribute("Positive").Value),
                    NegativeLimit = double.Parse(eAxis.Attribute("Negative").Value),

                    CountPerMM = double.Parse(eAxis.Attribute("CountPerMM").Value),
                    UseEncoder = bool.Parse(eAxis.Attribute("UseEncoder").Value),
                    ReadINP = bool.Parse(eAxis.Attribute("ReadINP").Value),

                    HomeSetting = new AxisParam.HomeParam()
                    {
                        Mode = int.Parse(ehome.Attribute("Mode").Value),
                        Direction = int.Parse(ehome.Attribute("Direction").Value),
                        CurveFactor = float.Parse(ehome.Attribute("CurveFactor").Value),
                        Acc = double.Parse(ehome.Attribute("Acc").Value),
                        StartSpeed = double.Parse(ehome.Attribute("StartSpeed").Value),
                        Speed = double.Parse(ehome.Attribute("Speed").Value),
                        Offset = double.Parse(ehome.Attribute("Offset").Value),
                        LeaveHomeSpeed = double.Parse(ehome.Attribute("LeaveHomeSpeed").Value),
                        HomePosition = double.Parse(ehome.Attribute("HomePosition").Value),
                        HomeStyle = int.Parse(ehome.Attribute("HomeStyle").Value),
                        HomeIO = ehome.Attribute("HomeIO").Value
                    },
                });
            }
            System.Diagnostics.Debug.WriteLine("loaded all aexes objects");

            var inputs = doc.Descendants("Input");
            foreach (var line in inputs)
            {
                CreateInputLine(line.Attribute("name").Value,
                    line.Attribute("display").Value,
                    int.Parse(line.Attribute("cardid").Value),
                    line.Attribute("cardtype").Value,
                    int.Parse(line.Attribute("port").Value),
                    int.Parse(line.Attribute("index").Value),
                    bool.Parse(line.Attribute("reversed").Value)
                    );
            }
            System.Diagnostics.Debug.WriteLine("loaded all input objects");

            var outputs = doc.Descendants("Output");
            foreach (var line in outputs)
            {
                CreateOutputLine(line.Attribute("name").Value,
                    line.Attribute("display").Value,
                    int.Parse(line.Attribute("cardid").Value),
                    line.Attribute("cardtype").Value,
                    int.Parse(line.Attribute("port").Value),
                    int.Parse(line.Attribute("index").Value),
                    bool.Parse(line.Attribute("reversed").Value)
                    );
            }
            System.Diagnostics.Debug.WriteLine("loaded all output objects");
        }

        /// <summary>
        /// save motion related param to file
        /// </summary>
        /// <param name="file">file path</param>
        public void SaveSetting(string file)
        {
            if (File.Exists(file))
            {
                string path = Path.GetDirectoryName(file);
                string name = Path.GetFileName(file);
                path = Path.Combine(path, "backup");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, name + DateTime.Now.ToString(".yyyyMMddHHmmss") + ".bak");
                File.Move(file, path);
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement root = new XElement("MotionSetting");
            doc.Add(root);

            //add motion card setting
            XElement motion = new XElement("MotionAxes");
            root.Add(motion);

            foreach (var axis in _Axes)
            {
                XElement eAxis = new XElement("Axis");
                motion.Add(eAxis);

                Type cType = typeof(AxisParam);
                PropertyInfo[] typeInfo = cType.GetProperties();

                foreach (var info in typeInfo)
                {
                    if (info.PropertyType == typeof(AxisParam.HomeParam))
                    {
                        XElement home = new XElement("homeParam");
                        PropertyInfo[] homeInfo = typeof(AxisParam.HomeParam).GetProperties();
                        foreach (var h in homeInfo)
                        {
                            object obj = h.GetValue(axis.Value.Setting.HomeSetting, null);
                            home.Add(new XAttribute(h.Name, obj));
                        }

                        eAxis.Add(home);
                    }
                    else
                    {
                        object obj = info.GetValue(axis.Value.Setting, null);
                        eAxis.Add(new XAttribute(info.Name, obj));
                    }
                }
            }


            //save IO lines
            XElement inputs = new XElement("InputIO");
            root.Add(inputs);

            //motion card inputs
            foreach (var line in _Inputs)
            {
                XElement eLine = new XElement("Input",
                    new XAttribute("name", line.Value.Name),
                    new XAttribute("index", line.Value.Index),
                    new XAttribute("port", line.Value.Port),
                    new XAttribute("cardid", line.Value.Card),
                    new XAttribute("cardtype", line.Value.CardType));

                inputs.Add(eLine);
            }



            XElement outputs = new XElement("InputIO");
            root.Add(outputs);
            foreach (var line in _Outputs)
            {
                XElement eLine = new XElement("Output",
                    new XAttribute("name", line.Value.Name),
                    new XAttribute("index", line.Value.Index),
                    new XAttribute("port", line.Value.Port),
                    new XAttribute("cardid", line.Value.Card),
                    new XAttribute("cardtype", line.Value.CardType));

                outputs.Add(eLine);
            }

            

            doc.Save(file);
        }
        #endregion
    }
}
