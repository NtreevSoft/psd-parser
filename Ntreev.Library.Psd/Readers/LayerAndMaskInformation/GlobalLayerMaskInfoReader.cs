using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class GlobalLayerMaskInfoReader
    {
        internal GlobalLayerMaskInfoReader(PsdReader reader)
        {
            int length = reader.ReadInt32();
            long position = reader.Position;
            if (length == 0)
                return;

            reader.Position = position + length;
        }
    }
}
