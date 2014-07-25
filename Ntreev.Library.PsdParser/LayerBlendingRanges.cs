using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser
{
    class LayerBlendingRanges
    {
        public LayerBlendingRanges(PSDReader reader)
        {
            int size = reader.ReadInt32();


            reader.Position += size;
        }
    }
}
