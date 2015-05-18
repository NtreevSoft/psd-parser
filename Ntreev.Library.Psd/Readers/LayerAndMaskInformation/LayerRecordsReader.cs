using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerRecordsReader : ReadableValue<LayerRecords>
    {
        public LayerRecordsReader(PsdReader reader)
            : base(reader)
        {
            
        }

        protected override void ReadValue(PsdReader reader, out LayerRecords value)
        {
            value = new LayerRecords();

            value.Top = reader.ReadInt32();
            value.Left = reader.ReadInt32();
            value.Bottom = reader.ReadInt32();
            value.Right = reader.ReadInt32();
            value.ValidateSize();

            int channelCount = reader.ReadUInt16();

            value.ChannelCount = channelCount;
            value.ChannelTypes = new ChannelType[channelCount];
            value.ChannelSizes = new long[channelCount];

            for (int i = 0; i < channelCount; i++)
            {
                value.ChannelTypes[i] = reader.ReadChannelType();
                value.ChannelSizes[i] = reader.ReadLength();
            }

            reader.ValidateSignature();

            value.BlendMode = reader.ReadBlendMode();
            value.Opacity = reader.ReadByte();
            value.Clipping = reader.ReadBoolean();
            value.Flags = reader.ReadLayerFlags();
            value.Filter = reader.ReadByte();


            //long extraSize = reader.ReadUInt32();
            //long end = reader.Position + extraSize;

            //long position = reader.Position;
            //this.layerMask = new LayerMaskReader(reader);
            //this.blendingRanges = new LayerBlendingRangesReader(reader);
            //value.Name = reader.ReadPascalString(4);
        }
    }
}
