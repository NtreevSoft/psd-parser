using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureEnumerate : Properties
    {
        public StructureEnumerate(PSDReader reader)
        {
            this.Add("Type", reader.ReadStringOrKey());
            this.Add("EnumName", reader.ReadStringOrKey());
        }
    }
}
