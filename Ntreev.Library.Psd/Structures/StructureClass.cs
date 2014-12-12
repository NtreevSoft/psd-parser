using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureClass : Properties
    {
        public StructureClass(PSDReader reader)
        {
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());
        }
    }
}
