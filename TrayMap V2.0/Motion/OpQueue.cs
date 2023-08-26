// ===============================================================================
// Project Name        :    Motion
// Project Description :    
// ===============================================================================
// Class Name          :    OpQueue
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/21 13:52:26
// Update Time         :    2014/10/21 13:52:26
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Incube.Motion
{
    public class OpNode : Operation //两个同时执行
    {
        private delegate void ExcuteDel();

        List<Operation> _Ops = new List<Operation>();
        private int _Count = 0;
        private int _Fail = 0;

        private string _Error = "";

        public OpNode(string name)
            : base(name)
        {

        }

        public OpNode(IEnumerable<Operation> operations)
            : base("Path Operation")
        {
            foreach (var op in operations)
            {
                _Ops.Add(op);
            }
        }

        public void Add(Operation op)
        {
            _Ops.Add(op);
        }

        protected override bool _Execute()
        {
            foreach (var op in _Ops)
            {
                if (op == null)
                {
                    _Count++;
                    continue;
                }

                ExcuteDel del = new ExcuteDel(op.Execute);
                del.BeginInvoke(new AsyncCallback(OperateCheck), op);//先是模组X，在是相机X，在是物料盘Y    调用异步线程
                Thread.Sleep(20);      //执行异步线程
            }

            while (_Count < _Ops.Count)
            {
                Thread.Sleep(10);
            }

            if (_Fail > 0)
            {
                _Logger.Error(_Error, "Path operaton failed: {0}", _Error);
                throw new ExceptionFormat("Path operaton failed: {0}", _Error);    //丢出事件
            }

            return true;
        }

        void OperateCheck(IAsyncResult ar)
        {
            Operation op = ar.AsyncState as Operation;
            _Count++;

            if (op.Failed)
            {
                _Fail++;
                _Error += string.Format("{0} failed: {1}\r\n", op.Name, op.Failure.Message);
            }
        }

        public override void Abort()
        {
            base.Abort();

            foreach (var op in _Ops)
            {
                if (op == null)
                {
                    _Count++;
                    continue;
                }
                op.Abort();
            }
        }
    }

    public class OpPath : Operation//两个，先执行一个，要是失败了，另一个就不会执行
    {
        protected List<Operation> _Ops = new List<Operation>();
        protected Operation _CurOp = null;

        public OpPath(string name) : base(name)
        {

        }

        public void Add(Operation op)
        {
            _Ops.Add(op);
        }

        protected override bool _Execute()
        {
            foreach (var op in _Ops)
            {
                _CurOp = op;
                op.Execute();

                if (op.Failed || _Aborted)
                {
                    this.Failure = op.Failure;
                    throw new ExceptionFormat("Path Operation {0} failed as operation {1}, Error: {2}",
                                               Name, op.Name, op.Failure.Message);
                }
            }

            return true;
        }

        public override void Abort()
        {
            base.Abort();

            if (_CurOp != null)
            {
                _CurOp.Abort();
            }
        }
    }

    public class DelayOp : Operation
    {
        private int _Delay;

        public DelayOp(int minisecond): base("Delay")
        {
            _Delay = minisecond;
        }

        protected override bool _Execute()
        {
            Thread.Sleep(_Delay);

            return true;
        }
    }

    /// <summary>
    /// 队列   一个任务执行完再执行下一任务，当前任务发生错误，下一任务取消
    /// </summary>
    public class OpQueue 
    {
        public enum QueueState  //队列状态
        {
            Idle, 
            Running, 
            Error, 
            OperateDone,
        }

        private Queue<Operation> _Ops;
        private Thread _Process;
        private Operation _Operate; 
        private QueueState _State;
        private string _Message;

        public event EventHandler StateChanged; 
        public event EventHandler<StringEventArgs> MessageUpdate;

        public OpQueue()
        {
            _Ops = new Queue<Operation>();

            _Process = new Thread(new ThreadStart(Processor));
            _Process.IsBackground = true;

            _Process.Start();
        }

        public Operation CurrentOperate 
        {
            get { return _Operate; }
        }

        public QueueState State 
        {
            get { return _State; }
            set
            {
                if (_State == value)
                {
                    return;
                }

                _State = value;
                if (StateChanged != null)
                {
                    StateChanged(this, new EventArgs());
                }
            }
        }

        public string Message 
        {
            get { return _Message; }
            set
            {
                _Message = value;

                if (MessageUpdate != null)
                {
                    MessageUpdate(this, new StringEventArgs(_Message));
                }
            }
        }

        public string LastError { get; set; } 

        public bool _bFatalError { get; set; } 

        private void Processor()
        {
            do
            {
                if (_Ops.Count > 0 && !_bFatalError)  
                {
                    _Operate = _Ops.Dequeue();

                    State = QueueState.Running;

                    if (_Operate != null)
                    {
                        _Operate.Execute();

                        if (_Operate != null && _Operate.Failed)
                        {
                            Message = _Operate.Name + " was done failed";

                            LastError = _Operate.Failure == null ? "" : _Operate.Failure.Message; 
                            State = QueueState.Error;

                            _Ops.Clear();  //Clear the unRun queue
                        }
                        else if (_Operate != null)
                        {
                            State = QueueState.OperateDone; 
                            Message = _Operate.Name + " was done successfully";
                            LastError = "";
                        }

                    }
                    
                    _Operate = null;
                }
                else
                {
                    State = QueueState.Idle;
                    _Operate = null;
                    Thread.Sleep(10);
                }

            } while (true);
        }

        public void Add(Operation op)
        {
            if (_bFatalError)
                return;

            _Ops.Enqueue(op);
        }

        public void Abort()
        {
            if (_Operate != null)
            {              
                _Operate.Abort();
                
            }
            //清除止所有在停止状态下，加入的队列
            if (_Ops.Count > 0)
            {
                _Ops.Clear();
            }

            State = QueueState.Idle;

        }

        public void Start()//启动线程
        {
            if (_Process.ThreadState == System.Threading.ThreadState.Aborted ||
                _Process.ThreadState == System.Threading.ThreadState.Stopped)
            {
                _Process = new Thread(new ThreadStart(Processor));
                _Process.IsBackground = true;
                _Process.Start();
            }
        }

    }
}
