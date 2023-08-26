using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Incube.Motion;

namespace CommonUI
{
    public partial class MotionSetting : Form
    {
        string _FileName;

        public MotionSetting(string fileName)
        {
            InitializeComponent();

            _FileName = fileName;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            MotionFactory.Instance.SaveSetting(_FileName);
        }

        private void MotionSetting_Load(object sender, EventArgs e)
        {
            InitUI();
        }

        private void InitUI()
        {
            pgAxes.SelectedObject = MotionFactory.Instance.Axes;
            pgInput.SelectedObject = MotionFactory.Instance.Inputs;
            pgOutput.SelectedObject = MotionFactory.Instance.Outputs;
        }
    }
}
