using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;

namespace Incube.Motion
{
    [Serializable]
    public class AxisParam //轴参数
    {
        [Serializable]
        public struct HomeParam //回零参数
        {
            /// <summary>
            /// home mode, 0: ORG; 1: EL; 2: EZ 回零模式
            /// </summary>
            public int Mode { get; set; }

            /// <summary>
            /// home direction, 0: positive direction; 1: negative direction 回零方向
            /// </summary>
            public int Direction { get; set; }

            /// <summary>
            /// home curve factor, 0~1, 0:T-curve; 1:S-curve, default is 0.5 //回零曲线要素,可以理解为平滑时间
            /// </summary>
            public float CurveFactor { get; set; }

            /// <summary>
            /// homing acceleration & deceleration //回零速度模式，加速回零 & 减速回零
            /// </summary>
            public double Acc { get; set; }


            public double StartSpeed { get; set; }//启动速度


            public double Speed { get; set; }//速度

            /// <summary>
            /// offset from home stop sensor
            /// </summary>
            public double Offset { get; set; }

            /// <summary>
            /// EZ alignment enabled, default is false. don't know what's this???
            /// </summary>
         //   public bool EZAlignEnabled { get; set; }

            /// <summary>
            /// speed when leaving homing sensor
            /// </summary>
            public double LeaveHomeSpeed { get; set; }

            /// <summary>
            /// position when homed
            /// </summary>
            public double HomePosition { get; set; }

            /// <summary>
            /// 回原点类型：-1,该轴不回零或者自动清零,并且这些轴不设置慢速快速 ；0  原点 + 负限位；1 只有原点；2 IO回零 (电缸);
            /// </summary>
            public int HomeStyle { get; set; }

            /// <summary>
            /// IO回零端口名称(电缸IO回零模式)
            /// </summary>
            public string HomeIO { get; set; }
        }

        /// <summary>
        /// name of axis
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// name for display
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// card type name
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// based motion card index
        /// </summary>
        public int CardID { get; set; }


        /// <summary>
        /// axis index in the controller
        /// </summary>
        public int Index { get; set; }


        public HomeParam HomeSetting { get; set; }


        /// <summary>
        /// Axis position direction //轴的直接位置
        /// </summary>
        public int Direct { get; set; }


        /// <summary>
        /// Axis max move distance in positive direction//正限位
        /// </summary>
        public double PositiveLimit { get; set; } 

        /// <summary>
        /// Axis max move distance in negative dirction //负限位
        /// </summary>
        public double NegativeLimit { get; set; }

        /// <summary>
        /// axis resolution, pulse count per mm //脉冲计数 单位MM毫米
        /// </summary>
        public double CountPerMM { get; set; }
 
        /// <summary>
        /// whether read encoder position as the motion position
        /// </summary>
        public bool UseEncoder { get; set; } //编码器

        /// <summary>
        /// 是否读取电机定位完成信号
        /// </summary>
        public bool ReadINP { get; set; }
        }


    public enum AxisState //轴声明
    {
        Idle,
        Moving,
        MoveDone,
        MotionError,
        AxisError
    }

    public interface IAxis : ISerializable
    {

        /// <summary>
        /// 
        /// </summary>
        event EventHandler StatusUpdate;//状态更新

        /// <summary>
        /// 
        /// </summary>
        event EventHandler PositionUpdate;//位置更新

        /// <summary>
        /// name of axis
        /// </summary>
        string Name { get; }

        /// <summary>
        /// axis index in the controller
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Axis position direction //轴的位置方向
        /// </summary>
        int Direct { get; }

        AxisParam Setting { get; } //设置


        /// <summary>
        /// Axis command position or feedback position in mm
        /// </summary>
        double Position { get; } //位置

        /// <summary>
        /// ratio of current speed, it can be used for debug motion, easily change all speed to slow or full speed
        /// </summary>
        //double SpeedScale { get; set; }

        /// <summary>
        /// 起步速度  雷塞有起步速度；固高没有起步速度
        /// </summary>
        double StartSpeed { get; set; }

        /// <summary>
        /// motion speed
        /// </summary>
        double Speed { get; set; } //速度

        /// <summary>
        /// motion acceleration
        /// </summary>
        double Acc { get; set; } //加速度

        /// <summary>
        /// motion deceleration
        /// </summary>
        double Dec { get; set; } //减速度

        /// <summary>
        /// 平滑时间；S段时间
        /// </summary>
        double SmoothTime { get; set; }

        /// <summary>
        /// whether it is servo on
        /// </summary>
        bool IsServoOn { get; } //伺服使能

        /// <summary>
        /// 等待运动结束
        /// </summary>
        void WaitForRunFinish();

        /// <summary>
        /// whether axis is homed
        /// </summary>
        bool Homed { get; set; } //回原

        /// <summary>
        /// Axis status
        /// </summary>
        AxisState State { get; }

        /// <summary>
        /// Axis Error message
        /// </summary>
        string ErrorMessage { get; } //异常消息


        /// <summary>
        /// positive end limit triggered
        /// </summary>
        bool PositiveEL { get; } //正限位

        /// <summary>
        /// negative end limit triggered
        /// </summary>
        bool NegativeEL { get; } //负限位

        /// <summary>
        /// home sensor triggered
        /// </summary>
        bool HomeSensor { get; } 

        /// <summary>
        /// INP 信号  伺服到位信号  读取板卡IO专用口
        /// </summary>
        bool INPSensor { get; }

        IMotionCard Card { get; }

        /// <summary>
        /// axis home function
        /// </summary>
        void Home();


        /// <summary>
        /// Servo on/off the motor
        /// </summary>
        /// <param name="onOff">true: on; false: off</param>
        void ServoOn(bool onOff);//伺服使能

        /// <summary>
        /// sync mode motion function, move to destination
        /// 0:successful  not zero: error
        /// </summary>
        /// <param name="destine">destination in mm</param>
        int MoveTo(double destine); //绝对运动


        /// <summary>
        /// sync mode motion, move a distance relative to current axis' position
        /// 0:successful  not zero: error
        /// </summary>
        /// <param name="distance">distance in mm</param>
        int MoveRelative(double distance);//相对运动

        /// <summary>
        /// async mode motion, move to destination
        /// for async usage, pls see <see cref="http://msdn.microsoft.com/zh-cn/library/system.iasyncresult(v=vs.110).aspx"/>
        /// and <seealso cref="http://msdn.microsoft.com/zh-cn/library/system.iasyncresult.asyncstate(v=vs.110).aspx"/>
        /// </summary>
        /// <param name="destine"></param>
        /// <param name="callback"></param>
        IAsyncResult MoveToAsync(double destine, AsyncCallback callback);

        /// <summary>
        /// jog start or stop
        /// </summary>
        /// <param name="start">true: start or false: stop</param>
        void Jog(bool start, int dirction);

        /// <summary>
        /// step jog
        /// </summary>
        /// <param name="step"></param>
        void Jog(double step);

        /// <summary>
        /// update axis state
        /// </summary>
        void Update();//更新

        /// <summary>
        /// stop the movement immediately
        /// </summary>
        void Stop();//停止

        /// <summary>
        /// stop current move, but can be resumed later
        /// it usually stop the axis, save the target position and send the command again later
        /// </summary>
        void Pause(); //暂停

        /// <summary>
        /// resume motion paused, it usually resend the motion command
        /// </summary>
        void Resume();

        /// <summary>
        /// Set Axis moving speed
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        void SetSpeed(double startSpeed,double speed, double acc, double dec,double smoothTime);//设置轴的速度模式


        /// <summary>
        /// usually for change the motion target before the motion completed
        /// most card has this feature
        /// </summary>
        /// <param name="destine"></param>
        void ResetTarget(double destine);

        /// <summary>
        /// 清除轴报警,对有些轴有用
        /// </summary>
        void ClearError();


        /// <summary>
        /// 清除轴位置
        /// </summary>
        /// <returns></returns>
        int ClearPos();

        /// <summary>
        /// 把当前值 pos 设置为轴的位置，  单位 脉冲
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        int SetPostion(int pos);

    }
}
