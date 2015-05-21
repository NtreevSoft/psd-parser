using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerBlendingRangesReader : ValueReader<LayerBlendingRanges>
    {
        private LayerBlendingRangesReader(PsdReader reader)
            : base(reader, true, null)
        {
            
        }

        public static LayerBlendingRanges Read(PsdReader reader)
        {
            LayerBlendingRangesReader instance = new LayerBlendingRangesReader(reader);
            return instance.Value;
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerBlendingRanges value)
        {
            value = new LayerBlendingRanges();
        }
    }
}
