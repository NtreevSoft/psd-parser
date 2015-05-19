using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerBlendingRangesReader : LazyValueReader<object>
    {
        public LayerBlendingRangesReader(PsdReader reader)
            : base(reader, null)
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
