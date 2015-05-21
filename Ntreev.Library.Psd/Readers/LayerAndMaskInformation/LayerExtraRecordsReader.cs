using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerExtraRecordsReader : ValueReader<LayerRecords>
    {
        private LayerExtraRecordsReader(PsdReader reader, LayerRecords records)
            : base(reader, true, records)
        {

        }

        public static LayerRecords Read(PsdReader reader, LayerRecords records)
        {
            LayerExtraRecordsReader instance = new LayerExtraRecordsReader(reader, records);
            return instance.Value;
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadUInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerRecords value)
        {
            LayerRecords records = userData as LayerRecords;
            LayerMask mask = LayerMaskReader.Read(reader);
            LayerBlendingRanges blendingRanges = LayerBlendingRangesReader.Read(reader);
            string name = reader.ReadPascalString(4);
            IProperties resources = new LayerResourceReader(reader, this.EndPosition - reader.Position);

            records.SetExtraRecords(mask, blendingRanges, resources, name);

            value = records;
        }
    }
}
