using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Incube.Motion;

namespace CommonUI
{
    public partial class LineControl : UserControl
    {
        private ILine _Line;
        private bool _IsInput;
        //private Color[] _Color = { Color.Lime, Color.Red };
        private Color[] _Color = { Color.Lime, Color.LightGray};

        public LineControl()
        {
            InitializeComponent();
        }

        public LineControl(ILine line) : this()
        {
            _Line = line;

            lblLineName.Text = _Line.DisplayName;
            if (_Line is IInputLine)
            {
                btnState.Enabled = false;
                _IsInput = true;
            }
            else
            {
                btnState.Enabled = true;
                _IsInput = false;
            }

            SetState();
            _Line.Transition += new EventHandler(_Line_Transition);
        }

        void _Line_Transition(object sender, EventArgs e)
        {
            if (btnState.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(SetState));
            }
            else
            {
                SetState();
            }
        }

        private void SetState()
        {
            if (_IsInput)
            {
                btnState.BackColor = (_Line as IInputLine).State ? _Color[0] : _Color[1];
            }
            else
            {
                btnState.BackColor = (_Line as IOutputLine).State ? _Color[0] : _Color[1];
            }
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            if (!(_Line is IOutputLine))
            {
                return;
            }

            if (!(_Line as IOutputLine).State && _Line.Name.Contains("_Home"))
            {
                //if (DialogResult.OK != MessageBox.Show("Are you sure to Home this Axis?", "Danger",
                if (DialogResult.OK != MessageBox.Show("确定要此轴做回原点动作?", "危险",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    return;
                }
            }

            (_Line as IOutputLine).State = !(_Line as IOutputLine).State;
        }
    }
}
