// ===============================================================================
// Project Name        :    Motion
// Project Description :    
// ===============================================================================
// Class Name          :    Operation
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/10 13:51:50
// Update Time         :    2014/10/10 13:51:50
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Drawing;

namespace Incube.Motion
{
    /// <summary>
    /// 操作状态
    /// </summary>
    public enum OperationState //
    {
        Initial, 
        Processing, 
        Succeed, 
        Failed, 
        Aborted
    }

    public delegate void ActionDelegate();


    public abstract class Operation         //抽象类
    {
        protected static Logger _Logger = LogManager.GetLogger("Operation");

        protected OperationState _State;
        protected bool _Aborted;
        protected MotionFactory Motion; 

        #region properties
        public string Name { get; set; }

        public bool Aborted
        {
            get 
            {
                return _Aborted;
            }
        }

        public Exception Failure { get; set; } //失败

        public OperationState State //声明
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;

                    //Fire.Asynchronous(Transition, this, new EventArgs());
                    if (Transition != null)
                    {
                        Transition(this, new EventArgs());
                    }
                }
            }
        }

        public TimeSpan Duration { get; set; } 

        public bool Failed 
        {
            get { return _State == OperationState.Failed; }// || _State == OperationState.Aborted
        }

        public bool Succeed 
        {
            get { return _State == OperationState.Succeed; }
        }

        public bool Processed 
        {
            get { return _State != OperationState.Processing; }
        }
        #endregion

        public event EventHandler Transition; 

        public Operation(string name)
        {
            Name = name;
            Motion = MotionFactory.Instance; 
            _State = OperationState.Initial; 
        }

        //duration
        public void Execute() 
        {
            try
            {
                State = OperationState.Processing;
                DateTime startTime = DateTime.Now;

                bool bResult = _Execute();

                Duration = DateTime.Now - startTime;

                _Logger.Info("Operaiton, {0}, Done, Duration, {1:F2}, s", Name, Duration.TotalSeconds);
           
                if (_Aborted)
                {
                    State = OperationState.Aborted;
                }
                else
                {
                    State = bResult ? OperationState.Succeed : OperationState.Failed;
                }
            }
            catch (Exception ex)
            {
                Failure = ex;
                Duration = TimeSpan.FromSeconds(0);

                _Logger.Info(ex, "Operation, {0}, failed, the exception is : {1}", Name, ex.Message);

                State = OperationState.Failed;
            }
        }



        protected abstract bool _Execute();

        public virtual void Abort()
        {
            _Aborted = true;
            Failure = new Exception("Aborted");

            _Logger.Info("Operation is aborted now");
        }
    }
}
