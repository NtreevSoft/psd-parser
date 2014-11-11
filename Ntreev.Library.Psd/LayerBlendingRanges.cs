using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
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
