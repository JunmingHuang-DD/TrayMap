// ===============================================================================
// Project Name        :    Motion
// Project Description :    
// ===============================================================================
// Class Name          :    StringEventArgs
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/23 11:11:07
// Update Time         :    2014/10/23 11:11:07
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    public class StringEventArgs : EventArgs
    {
        public string Message { get; set; }

        public StringEventArgs()
        {

        }

        public StringEventArgs(string message)
        {
            Message = message;
        }

    }
    public delegate void StringEvnetHandler(object sender, StringEventArgs e);

}
