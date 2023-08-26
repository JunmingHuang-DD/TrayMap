using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    public class ExceptionFormat : Exception
    {
        public ExceptionFormat(string format, params object[] objects)
            : base(string.Format(format, objects))
        {

        }
    }
}
