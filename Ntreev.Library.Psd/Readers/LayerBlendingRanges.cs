using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class LayerBlendingRangesReader
    {
        public LayerBlendingRangesReader(PsdReader reader)
        {
            int size = reader.ReadInt32();

            reader.Position += size;
        }
    }
}
