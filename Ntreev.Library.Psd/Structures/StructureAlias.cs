using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureAlias : Properties
    {
        public StructureAlias(PsdReader reader)
        {
            int length = reader.ReadInt32();
            this.Add("Alias", reader.ReadAscii(length));
        }
    }
}
