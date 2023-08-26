using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    public interface IIOCard //IO卡
    {
        /// <summary>
        /// card type name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// IO card version information
        /// </summary>
        string Version { get; }

        /// <summary>
        /// card number, or card ID
        /// </summary>
        ushort CardID { get; }

        /// <summary>
        /// Port count of controller, greater than 0
        /// </summary>
        ushort PortCount { get; } //端口计算

        /// <summary>
        /// max line count
        /// </summary>
        int MaxLines { get; }

        /// <summary>
        /// indicate the controller connected or not
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Configuration file name
        /// </summary>
        string ConfigFile { get; }

        /// <summary>
        /// input lines
        /// </summary>
        Dictionary<string, IInputLine> Inputs { get; }

        /// <summary>
        /// output lines
        /// </summary>
        Dictionary<string, IOutputLine> Outputs { get; }

        IInputLine GenerateInput(string name, string display, ushort index, ushort port = 0, bool reversed = false);

        IOutputLine GenerateOutput(string name, string display, ushort index, ushort port = 0, bool reversed = false);

        /// <summary>
        /// connect with IO card
        /// </summary>
        bool Connect();

        /// <summary>
        /// disconnect with IO card
        /// </summary>
        bool Disconnect();

        /// <summary>
        /// update IO state
        /// </summary>
        void Update();


        /// <summary>
        /// stop IO change
        /// </summary>
        void Stop();

        void LoadConfig(string configFile);

        void Reset();
    }
}
