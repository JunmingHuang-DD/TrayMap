using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using FastColoredTextBoxNS;

namespace CommonUI
{
    public enum PresetColor
    {
        Yellow,
        Green,
        Red,
        White
    }

    /// <summary>
    /// 使用一个FastColoredTextBox <see cref="https://github.com/PavelTorgashov/FastColoredTextBox"/>
    /// 来输出信息，可以使用彩色文本了
    /// </summary>
    /// 

    public enum LogStype
    {
        WarningStyle = 0,
        InfoStyle = 1,
        ErrorStyle = 2,
        DefayltStyle = 3,
    }
    public static class ColoredOutputF
    {
        private static FastColoredTextBox fctb;

        public static void AttachOutput(FastColoredTextBox textBox)
        {
            fctb = textBox;
            fctb.Text = "";
        }

        public static void WriteLine(string text, LogStype logStype = LogStype.DefayltStyle)
        {
            Style style = new TextStyle(Brushes.White, null, FontStyle.Regular);
            switch (logStype)
            {
                case LogStype.DefayltStyle:
                    style = new TextStyle(Brushes.White, null, FontStyle.Regular);
                    break;
                case LogStype.WarningStyle:
                    style = new TextStyle(Brushes.Yellow, null, FontStyle.Regular);
                    break;
                case LogStype.InfoStyle:
                    style = new TextStyle(Brushes.Green, null, FontStyle.Regular);
                    break;
                case LogStype.ErrorStyle:
                    style = new TextStyle(Brushes.Red, null, FontStyle.Regular);
                    break;

            }     
                 
            //some stuffs for best performance
            fctb.BeginUpdate();

            fctb.Selection.BeginUpdate();
            //remember user selection
            var userSelection = fctb.Selection.Clone();
            //add text with predefined style
            fctb.TextSource.CurrentTB = fctb;

            fctb.AppendText(DateTime.Now + $" {text}\r\n", style);

            if (!userSelection.IsEmpty || userSelection.Start.iLine < fctb.LinesCount - 2)
            {
                fctb.Selection.Start = userSelection.Start;
                fctb.Selection.End = userSelection.End;
            }
            else
                fctb.GoEnd();//scroll to end of the text

            fctb.GoEnd();//scroll to end of the text
            
            fctb.EndUpdate();
        }

    }
    public static class ColoredOutput
    {
        private static ILogger _logger = LogManager.GetLogger("LogUI");

        private static TextBox _coloredTextBox;
        //private static readonly TextStyle _higlightStyle = new TextStyle(Brushes.Yellow, null, FontStyle.Regular);
        //private static readonly TextStyle _infoStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        //private static readonly TextStyle _errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        //private static readonly TextStyle _defaultStyle = new TextStyle(Brushes.White, null, FontStyle.Regular);

        public static int MaxLines = 1000;

        public static bool IsAutoUpdata { get; set; } = true;
        public static void AttachOutput(TextBox textBox)
        {
            _coloredTextBox = textBox;
        }

        /// <summary>
        /// 正常颜色显示
        /// </summary>
        /// <param name="formatString"></param>
        /// <param name="args"></param>
        public static void WriteLine(string formatString, params object[] args)
        {
            var data = string.Format(formatString, args);

            _logger.Info(data);

            _coloredTextBox?.BeginInvoke(new Action(() =>
            {
                AutoClear();

                _coloredTextBox.AppendText(data + Environment.NewLine);
 
                if (_coloredTextBox.Lines.Length > MaxLines)
                {
                    var rm = new List<int>();
                    for (int i = 0; i < MaxLines / 2; i++)
                    {
                        rm.Add(i);
                    }
                    
                    _coloredTextBox.ScrollToCaret();
                }

            }));
        }

        public static void AutoClear()
        {
            int line = _coloredTextBox.TextLength;
            //  if (line >= 1024 * 1024 * 10)
            if (line >= 2000)
            {
                _coloredTextBox.Clear();
                line = 0;
            }
        }

        /// <summary>
        /// 可变颜色显示
        /// </summary>
        /// <param name="color">red, yellow, white, green</param>
        /// <param name="formatString"></param>
        /// <param name="args"></param>
        public static void WriteLine(PresetColor color, string formatString, params object[] args)
        {
            try
            {
                var data = string.Format(formatString, args);
                _logger.Info(data);

                Color style;
                switch (color)
                {
                    case PresetColor.Yellow:
                        style = Color.Yellow;
                        break;
                    case PresetColor.Green:
                        style = Color.Green;
                        break;
                    case PresetColor.Red:
                        style = Color.Red;
                        break;
                    case PresetColor.White:
                        style = Color.White;
                        break;
                    default:
                        style = Color.White;
                        break;
                }

                if (_coloredTextBox.InvokeRequired && IsAutoUpdata)
                {
                    if (_coloredTextBox.IsDisposed)
                    {
                        return;
                    }

                    _coloredTextBox?.BeginInvoke(new Action(() =>
                    {                      
                        AutoClear();
                      //  _coloredTextBox.SelectionColor = style;
                        _coloredTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss-fff  ") + data + "\r\n");

                        _coloredTextBox.ScrollToCaret();
                    }));
                }

               

            }
            catch(Exception ex)
            {

            }
        }
    }
}
