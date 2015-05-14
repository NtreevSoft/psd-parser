using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureClass : Properties
    {
        public StructureClass()
            : base(2)
        {

        }

        public StructureClass(PsdReader reader)
        {
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());
        }
    }
}
