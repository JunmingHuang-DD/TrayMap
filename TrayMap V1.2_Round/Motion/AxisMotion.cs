using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Incube.Motion
{
    public class HomeAxis : Operation
    {
        private IAxis _Axis;

        public HomeAxis(IAxis axis)
            : base("HomeAxis " + axis.Setting.DisplayName)
        {
            _Axis = axis;
        }

        public HomeAxis(string axisName)
            : base("HomeAxis " + axisName)
        {
            _Axis = Motion.Axes[axisName];
        }

        protected override bool _Execute()
        {
            _Axis.Home();

            if (_Axis.Homed)
            {
                return true;
            }
            else
            {
                string msg = string.Format("Home axis {0} failed, as no home sensored detected",
                    _Axis.Setting.DisplayName);

                _Logger.Error(msg);

                throw new Exception(msg);
            }
        }
    }


    public class HomeAxes : Operation
    {
        private string _Error;
        List<IAxis> _Axes = new List<IAxis>();

        public HomeAxes(params IAxis[] axes)
            : base("Home Axes")
        {
            foreach (var axis in axes)
            {
                _Axes.Add(axis);
            }
        }

        protected override bool _Execute()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var axis in _Axes)
            {
                sb.AppendFormat("{0}  ", axis.Setting.DisplayName);
            }

            int homed = 0;
            _Error = "";
            foreach (var axis in _Axes)
            {
                Operation op = new HomeAxis(axis.Name);
                Action h = new Action(op.Execute);
                h.BeginInvoke(new AsyncCallback(ir =>
                {
                    Operation p = ir.AsyncState as Operation;
                    if (p.Succeed)
                    {
                        homed++;
                    }
                    else
                    {
                        _Error += p.Failure.Message;
                    }

                }), op);
            }

            DateTime timeout = DateTime.Now + TimeSpan.FromSeconds(50);
            while (homed < _Axes.Count && timeout > DateTime.Now)
            {
                Thread.Sleep(10);
            }

            if (homed < _Axes.Count)
            {
                //MotionMgr.Instance.BeepError(true);

                _Logger.Warn("Home Axes {0} , time out, {1} ", sb.ToString(), _Error);
                throw new ExceptionFormat("Home all axes {0}  failed", sb.ToString());
            }


            return true;
        }
    }


    public class AxisMove : Operation
    {
        private IAxis _Axis;
        private bool _Relative;
        private double _startSpeed,_Speed, _Acc, _Dec,_smoothTime;
        private double _Pos;

        public AxisMove(IAxis axis, double pos,double startSpeed, double speed, double acc, double dec,double smoothTime, bool relative = false)
            : base("AxisMove " + axis.Name)
        {
            _Axis = axis;

            _Pos = pos;
            _startSpeed = startSpeed;
            _Speed = speed;
            _Acc = acc;
            _Dec = dec;
            _smoothTime = smoothTime;
            _Relative = relative;
        }

        public AxisMove(IAxis axis, TeachItem item, bool relative = false)
            : this(axis, item[axis.Name].Value, item[axis.Name].StartSpeed, item[axis.Name].Speed, item[axis.Name].Acc, item[axis.Name].Dec, item[axis.Name].SmoothTime, relative)
        {

        }

        public AxisMove(IAxis axis, KeyValue item, bool relative = false)
            : this(axis, item.Value,item.StartSpeed, item.Speed, item.Acc, item.Dec,item.SmoothTime, relative)
        {

        }

        public AxisMove(IAxis axis, double pos, KeyValue item, bool relative = false)
            : this(axis, pos,item.StartSpeed, item.Speed, item.Acc, item.Dec,item.SmoothTime, relative)
        {

        }

        public AxisMove(IAxis axis, double pos, TeachItem item, bool relative = false)
                : this(axis, pos, item[axis.Name].StartSpeed, item[axis.Name].Speed, item[axis.Name].Acc, item[axis.Name].Dec, item[axis.Name].SmoothTime, relative)
        {

        }


        protected override bool _Execute()
        {
            _Axis.SetSpeed(_startSpeed,_Speed, _Acc, _Dec,_smoothTime);

            int m = 0;
            if (_Relative)
            {
                m = _Axis.MoveRelative(_Pos);
            }
            else
            {
                m = _Axis.MoveTo(_Pos);
            }

            if (m != 0)
            {
                throw new ExceptionFormat("{0} move to destine {1:F3} failed, returned {2}", _Axis.Name, _Pos, m);   //抛出来一个异常处理
            }

            return true;
        }
    }

    public class AxesMove : OpNode
    {
        public AxesMove(IAxis[] axes, double[] pos, TeachItem speed)
            : base("Axes Move")
        {
            for (int i = 0; i < axes.Length; i++)
            {
                Add(new AxisMove(axes[i], pos[i], speed));
            }
        }

        public AxesMove(IAxis[] axes, TeachItem speed)
            : base("Axes Move")
        {
            for (int i = 0; i < axes.Length; i++)
            {
                Add(new AxisMove(axes[i], speed));
            }
        }
    }

    public class AxisMoveTo : Operation
    {
        private IAxis _axis;
        public double Pos;


        public AxisMoveTo(string axisName, double pos)
            : base("HomeAxis " + axisName)
        {
            _axis = Motion.Axes[axisName];
            Pos = pos;
        }

        protected override bool _Execute()
        {
            if (Math.Abs(Pos) > 40)
            {
                return false;
            }

            int rtn = _axis.MoveRelative(Pos);
            if (rtn == -1)
            {          
                throw new ExceptionFormat("[Initialization] 检测到轴报警异常,请检查！");
                //return false;
            }
            _axis.ClearPos();
            return true;
        }
    }

}
