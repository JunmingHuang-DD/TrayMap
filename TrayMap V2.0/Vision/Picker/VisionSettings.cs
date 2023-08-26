using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Incube.Vision.Picker
{
    public partial class VisionSettings : Form
    {
        VisionConfig _config;
        VisionConfig _ThisConfig;

        ComponentResourceManager resources;

        public event EventHandler UpdataVisionConfig;
        public VisionSettings(VisionConfig config)
        {
            InitializeComponent();

            resources = new ComponentResourceManager(typeof(VisionSettings));

            if (config != null)
            {
                _ThisConfig = config.Clone() as VisionConfig;
                _config = config;
            }
        }

        private void VisionSettings_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = _ThisConfig;
            toolStripTextBox1.Text = _ThisConfig.CurrentFileName;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                _ThisConfig.Save(_config.CurrentFileName);
                _config = _ThisConfig.Clone() as VisionConfig;
                MessageBox.Show(resources.GetString("SaveSuccess"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(resources.GetString("SaveFail"));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
