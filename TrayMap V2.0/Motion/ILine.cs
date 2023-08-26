using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    public interface ILine
    {
        /// <summary>
        /// event for Line state change
        /// </summary>
        event EventHandler Transition;


        /// <summary>
        /// line name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// line name for display
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// port number
        /// </summary>
        ushort Port { get; }

        /// <summary>
        /// IO Index
        /// </summary>
        ushort Index { get; }

        /// <summary>
        /// IO/Motion card ID
        /// </summary>
        ushort Card { get; }

        /// <summary>
        /// IO card or motion card type
        /// </summary>
        string CardType { get; }

        /// <summary>
        /// 信号取反来使用
        /// </summary>
        bool Reversed { get; }

        /// <summary>
        /// set the line's state, true or false
        /// </summary>
        /// <param name="state">line state</param>
        void SetState(bool state);

        

    }

    public interface IInputLine : ILine
    {

        /// <summary>
        /// line state  
        /// </summary>
        bool State { get; }
    }

    public interface IOutputLine : ILine
    {
        /// <summary>
        /// ignore Output state set
        /// </summary>
        bool IgnoreState { get; set; }

        /// <summary>
        /// line state  
        /// </summary>
        bool State { get; set; }

        /// <summary>
        /// To operation the IO with PWM way
        /// </summary>
        /// <param name="cycles">the repeat cycles, if -1 means not stop until reset it's status to false</param>
        /// <param name="frequency">the frequency of PWM</param>
        void PWM(int cycles, double frequency); //圈数，频率
    }

    /// <summary>
    /// compare line output type
    /// </summary>
    public enum CompareOutType : short 
    {
        /// <summary>
        /// 
        /// </summary>
        Pulse = 0,  // 表示输出脉冲，脉冲宽度由time参数设定，

        /// <summary>
        /// 
        /// </summary>
        Level = 1 // 表示输出电平。
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CompareSource : short
    {
        /// <summary>
        ///
        /// </summary>
        Ctrl = 0,//内部脉冲计数器

        /// <summary>
        /// 
        /// </summary>
        Encoder = 1 //外部编码器
    }

    public interface ICompareLine : IOutputLine
    {
        

        /// <summary>
        /// 生成脉冲宽度，时间长度，单位根据具体控制卡而定
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <param name="relativePos">相对位置</param>
        int Compare(IAxis axis, double[] pos, bool relativePos);

        /// <summary>
        /// 
        /// </summary>
        void StopCompare();
    }
}
