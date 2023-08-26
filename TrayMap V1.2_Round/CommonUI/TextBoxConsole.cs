using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CommonUI
{
    /// <summary>
    /// 一个将输出写入到文本框的TextWriter
    /// </summary>
    public class TextBoxConsole : TextWriter
    {
        private static NLog.ILogger _Logger = NLog.LogManager.GetLogger("Console Info");

        private int _MaxLines = 2000;
        private TextBoxBase _Output;
        private AutoResetEvent _WaitEvent = new AutoResetEvent(true);


        /// <summary>
        /// 将Console的输出绑定到TextBoxConsole上
        /// </summary>
        /// <param name="tbc">需要输出的TextBoxConsole</param>
        public static void SetConsoleOut(TextBoxConsole tbc, bool listenDebug = true)
        {
            Console.SetOut(tbc);

            //也可以将Debug的输出到Console中，然后Console输出到TextBoxConsole，
            //这样无论写Debug输出还是Console输出，都可以直接输出到TextBox中
            if (listenDebug)
            {
                System.Diagnostics.TextWriterTraceListener myWriter = new
                        System.Diagnostics.TextWriterTraceListener(Console.Out);
                System.Diagnostics.Debug.Listeners.Add(myWriter);
            }
        }

        /// <summary>
        /// 将Console的输出绑定到TextBoxConsole上
        /// </summary>
        /// <param name="tb">需要输出的TextBoxBase object, TextBox or RichTextBox </param>
        public static void SetConsoleOut(TextBoxBase tb, bool listenDebug = true)
        {
            TextBoxConsole tbc = new TextBoxConsole(tb);

            Console.SetOut(tbc);

            //也可以将Debug的输出到Console中，然后Console输出到TextBoxConsole，
            //这样无论写Debug输出还是Console输出，都可以直接输出到TextBox中
            if (listenDebug)
            {
                System.Diagnostics.TextWriterTraceListener myWriter = new
                        System.Diagnostics.TextWriterTraceListener(Console.Out);
                System.Diagnostics.Debug.Listeners.Add(myWriter);
            }
        }


        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public TextBoxConsole(TextBoxBase output)
        {
            _Output = output;
        }

        //本来可以只用重载这一个函数就可以的
        //public override void Write(char value)
        //{
        //    _Output.BeginInvoke(new Action(() =>
        //    {
        //        _Output.AppendText(value.ToString());

        //        ClearText();
        //    }));
        //}


        void ClearText()
        {
            return;

            //_WaitEvent.WaitOne(500);

            if (_Output.Lines.Length > _MaxLines)
            {
                //clear first 1000 lines
                var first1000Lines = _Output.Lines.Skip(_MaxLines / 2);
                _Output.Lines = first1000Lines.ToArray();
            }

            //_WaitEvent.Set();
        }

        public override void Write(string format, params object[] arg)
        {
            _Output.BeginInvoke(new Action(() =>
            {
                _WaitEvent.WaitOne(150);

                _Output.AppendText(string.Format(format, arg));
                _Logger.Info(format, arg);

                ClearText();

                _WaitEvent.Set();
            }));
        }

        public override void Write(string value)
        {
            if (!_Output.IsHandleCreated)
            {
                return;
            }

            _Output.BeginInvoke(new Action(() =>
            {
                _WaitEvent.WaitOne(150);

                if (!_Output.IsHandleCreated || _Output.IsDisposed)
                {
                    return;
                }

                _Output.AppendText(value);
                _Logger.Info(value);

                ClearText();

                _WaitEvent.Set();
            }));
        }

        public override void WriteLine(string format, params object[] arg)
        {
            _Output.BeginInvoke(new Action(() =>
            {
                _WaitEvent.WaitOne(150);

                
                _Output.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + string.Format(format, arg) + Environment.NewLine);
                _Logger.Info(format, arg);

                ClearText();

                _WaitEvent.Set();
            }));
        }

        public override void WriteLine(string value)
        {
            _Output.BeginInvoke(new Action(() =>
            {
                _WaitEvent.WaitOne(150);

                _Output.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + value + Environment.NewLine);
                _Logger.Info("Texbox line {0}, {1}", _Output.Lines.Length, value);

                ClearText();

                _WaitEvent.Set();
            }));
        }

    }

}
