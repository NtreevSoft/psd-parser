using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureOffset : Properties
    {
        public StructureOffset(PsdReader reader)
        {
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());
            this.Add("Offset", reader.ReadInt32());
        }
    }
}
