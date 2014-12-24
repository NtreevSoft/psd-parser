using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureEnumerateReference : Properties
    {
        public StructureEnumerateReference(PsdReader reader)
        {
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());
            this.Add("TypeID", reader.ReadKey());
            this.Add("EnumID", reader.ReadKey());
        }
    }
}
