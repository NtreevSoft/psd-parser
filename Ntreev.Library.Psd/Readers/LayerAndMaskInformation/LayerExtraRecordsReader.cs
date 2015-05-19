using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerExtraRecordsReader : ValueReader<LayerExtraRecords>
    {
        public LayerExtraRecordsReader(PsdReader reader)
            : base(reader, true, null)
        {
            
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadUInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerExtraRecords value)
        {
            LayerMaskReader layerMask = new LayerMaskReader(reader);
            LayerBlendingRangesReader blendingRanges = new LayerBlendingRangesReader(reader);
            string name = reader.ReadPascalString(4);
            LayerResourceReader resources = new LayerResourceReader(reader, this.EndPosition - reader.Position);

            value = new LayerExtraRecords(layerMask, blendingRanges, resources, name);
        }
    }
}
