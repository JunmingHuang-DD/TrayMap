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
    public partial class ucAxisState : UserControl
    {
        public event AxisSelectHandler SelectStateChanged;

        //public bool _USE = true; 

        private IAxis _Axis;

        public IAxis Axis
        {

            set
            {
                if (value == null)
                {
                    if (_Axis != null)
                    {
                        _Axis.PositionUpdate -= new EventHandler(_Axis_PositionUpdate);
                    }
                }
                else
                {
                    _Axis = value;
                    rbAxisName.Text = _Axis.Setting.DisplayName;
                    tbPosition.Text = _Axis.Position.ToString("F3");
                    _Axis.PositionUpdate += new EventHandler(_Axis_PositionUpdate);
                    //_Axis.StatusUpdate += _Axis_StatusUpdate;
                }
            }
        }

        //public bool Checked
        //{
        //    get { return rbAxisName.Checked; }
        //    set { rbAxisName.Checked = value; }
        //}


        public ucAxisState(bool use = true)
        {
            //_USE = use;
            InitializeComponent();
            
            //this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        //private void rbAxisName_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbAxisName.Checked && SelectStateChanged != null && _Axis != null)
        //    {
        //        SelectStateChanged(this, new AxisStateEventArgs()
        //        {
        //            AxisName = _Axis.Name,
        //            Checked = rbAxisName.Checked
        //        });
        //    }
        //}

        //void _Axis_StatusUpdate(object sender, EventArgs e)
        //{
        //    if (labelNel.InvokeRequired)
        //    {
        //        this.Invoke(new MethodInvoker(UpdateState));
        //    }
        //    else
        //    {
        //        UpdateState();
        //    }
        //}

        void _Axis_PositionUpdate(object sender, EventArgs e)
        {
            

            if (tbPosition != null && tbPosition.InvokeRequired)
            {
                if (!this.IsHandleCreated || this.IsDisposed)
                {
                    return;
                }

                try
                {
                    this.BeginInvoke(new MethodInvoker(UpdatePosition));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("update position fail: {0}, {1}, {2}", this.IsHandleCreated, this.IsDisposed, ex.Message);
                }
            }
            else
            {
                UpdatePosition();
            }
        }

        //void UpdateState()
        //{
        //    lblOperate.BackColor = (_Axis.State == AxisState.Moving ? Color.Lime : SystemColors.Control);
        //    labelNel.BackColor = _Axis.NegativeEL ? Color.Red : SystemColors.Control;
        //    labelPel.BackColor = _Axis.PositiveEL ? Color.Red : SystemColors.Control;
        //    labelOrigin.BackColor = _Axis.HomeSensor ? Color.Lime : SystemColors.Control;
        //}

        void UpdatePosition()
        {
            tbPosition.Text = _Axis.Position.ToString("F3");
        }

        private void rbAxisName_Click(object sender, EventArgs e)
        {
            if (SelectStateChanged != null && _Axis != null)
            {
                SelectStateChanged(this, new AxisStateEventArgs()
                {
                    AxisName = _Axis.Name,
                    //Checked = rbAxisName.Checked
                });
            }
        }
    }

    public delegate void AxisSelectHandler(object sender, AxisStateEventArgs e);
    public class AxisStateEventArgs : EventArgs
    {
        public string AxisName { get; set; }

        public bool Checked { get; set; }
    }
}
