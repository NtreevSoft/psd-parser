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
            this.Add("Type", reader.ReadKey());
            this.Add("EnumName", reader.ReadKey());
        }
    }
}
