using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Structures
{
    class StructureUnknownOSType : Properties
    {
        private readonly string value;

        public StructureUnknownOSType(string value)
        {
            this.value = value;
            this.Add("Value", this.value);
        }
    }
}
