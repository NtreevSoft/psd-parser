using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class InvalidFormatException : Exception
    {
        public InvalidFormatException()
            : base("Invalid PSD file")
        {

        }
    }
}
