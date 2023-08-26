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
    public partial class IOContainer : UserControl
    {
        public IList<ILine> LineList
        {
            set
            {
                flpContainer.Controls.Clear();

                foreach (var line in value)
                {
                    LineControl ctl = new LineControl(line);
                    ctl.Margin = new Padding(10, 3, 10, 3);
                    flpContainer.Controls.Add(ctl);
                }
            }
        }

        public IOContainer()
        {
            InitializeComponent();
        }
    }
}
