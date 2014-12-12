using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureUnitFloat : Properties
    {
        public StructureUnitFloat(PSDReader reader)
        {
            string type = reader.ReadType();
          
            this.Add("Type", PSDUtil.ToUnitType(type));
            this.Add("Value", reader.ReadDouble());
        }
    }
}
