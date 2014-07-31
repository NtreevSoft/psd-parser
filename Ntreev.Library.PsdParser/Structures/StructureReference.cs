using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Structures
{
    class StructureReference : BaseStructure
    {
        public StructureReference(PSDReader reader, string key)
            : base(reader, key)
        {
        }
    }
}
