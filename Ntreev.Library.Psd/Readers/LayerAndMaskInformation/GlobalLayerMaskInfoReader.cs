using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class GlobalLayerMaskInfoReader : ValueReader<object>
    {
        public GlobalLayerMaskInfoReader(PsdReader reader)
            : base(reader, true, null)
        {
            
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out object value)
        {
            value = new object();
        }
    }
}
