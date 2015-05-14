using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureEnumerate : Properties
    {
        public StructureEnumerate()
            : base(2)
        {

        }

        public StructureEnumerate(PsdReader reader)
        {
            this.Add("Type", reader.ReadKey());
            this.Add("Value", reader.ReadKey());
        }
    }
}
