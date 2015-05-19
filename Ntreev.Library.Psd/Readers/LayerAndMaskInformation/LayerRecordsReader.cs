using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerRecordsReader : ValueReader<LayerRecords>
    {
        public LayerRecordsReader(PsdReader reader)
            : base(reader, false, null)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerRecords value)
        {
            LayerRecords records = new LayerRecords();

            records.Top = reader.ReadInt32();
            records.Left = reader.ReadInt32();
            records.Bottom = reader.ReadInt32();
            records.Right = reader.ReadInt32();
            records.ValidateSize();

            int channelCount = reader.ReadUInt16();

            records.ChannelCount = channelCount;
            records.ChannelTypes = new ChannelType[channelCount];
            records.ChannelSizes = new long[channelCount];

            for (int i = 0; i < channelCount; i++)
            {
                records.ChannelTypes[i] = reader.ReadChannelType();
                records.ChannelSizes[i] = reader.ReadLength();
            }

            reader.ValidateSignature();

            records.BlendMode = reader.ReadBlendMode();
            records.Opacity = reader.ReadByte();
            records.Clipping = reader.ReadBoolean();
            records.Flags = reader.ReadLayerFlags();
            records.Filter = reader.ReadByte();

            value = records;
        }
    }
}
