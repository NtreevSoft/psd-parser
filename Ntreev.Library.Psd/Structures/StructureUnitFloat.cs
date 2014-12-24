using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureUnitFloat : Properties
    {
        public StructureUnitFloat(PsdReader reader)
        {
            string type = reader.ReadType();
          
            this.Add("Type", PsdUtility.ToUnitType(type));
            this.Add("Value", reader.ReadDouble());
        }
    }
}
