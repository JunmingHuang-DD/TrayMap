using Incube.Motion;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Incube.Motion
{
    public abstract class Stage 
    {
        protected static Logger _Logger = LogManager.GetLogger("Stage");

        protected List<IAxis> _Axes = new List<IAxis>();
        protected List<Stage> _SubStages = new List<Stage>();
        protected Stage _Holder; 
        protected bool _Initialized;
        protected bool _IsBusy;
        protected OpQueue _Process;
        protected string[] _IgnoreInitAxes;//ignored initialization axes' names
        protected bool _OpAbort;

        public event EventHandler StateChanged; 

        /// <summary>
        /// stage id
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// whether the stage is initialized
        /// </summary>
        public virtual bool Initialized 
        {
            get { return _Initialized; }
            set
            {
                _Initialized = value;
                StateChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// indicate stage is busy
        /// </summary>
        public bool IsBusy
        {
            get { return _IsBusy; }
            protected set
            {
                _IsBusy = value;
                StateChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool OpAbort
        {
            get { return _OpAbort; }
            set { _OpAbort = value; }
        }

        /// <summary>
        /// get stage axes
        /// </summary>
        public List<IAxis> Axes { get { return _Axes; } }

        /// <summary>
        /// get child stages
        /// </summary>
        public List<Stage> SubStages { get { return _SubStages; } } 

        /// <summary>
        /// get parent stages
        /// </summary>
        public Stage Holder { get { return _Holder; } } 


        /// <summary>
        /// running process
        /// </summary>
        public OpQueue Process { get { return _Process; } } 

        /// <summary>
        /// whether auto run started
        /// </summary>
        public bool AutoStarted { get; set; }

        /// <summary>
        /// stage running state
        /// </summary>
        public OpQueue.QueueState State { get { return _Process.State; } }


        protected Stage(string id, Stage holder = null, bool hasProcess = false)
        {
            ID = id;
            _Holder = holder;
            _Holder?.SubStages.Add(this);
            if (hasProcess)
            {
                _Process = new OpQueue();
            }
        }


        public abstract Operation Initialize();

        /// <summary>
        /// started auto run
        /// </summary>
        public virtual void StartAuto()
        {
            if (AutoStarted)
            {
                return;
            }

            foreach (Stage stage in _SubStages)
            {
                stage.StartAuto();
            }

            AutoStarted = true;
        }

        /// <summary>
        /// stop auto run and abort all current operations
        /// </summary>
        public virtual void Abort()
        {
            AutoStarted = false;

            foreach (var stage in _SubStages)
            {
                stage.Abort();
            }

            _OpAbort = true;

            _Process?.Abort();
        }

        protected void FireStateEvent()
        {
            StateChanged?.Invoke(this, new EventArgs());
        }

        protected IAxis AddAxis(string axisName)
        {
            IAxis axis = MotionFactory.Instance.Axes[axisName];

            _Axes.Add(axis);

            return axis;
        }
    }

}
