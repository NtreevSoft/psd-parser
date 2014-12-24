using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureProperty : Properties
    {
        public StructureProperty(PsdReader reader)
        {
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());
            this.Add("KeyID", reader.ReadKey());
        }
    }
}
