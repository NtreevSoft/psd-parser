using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class ChannelsReader : LazyReadableValue<Channel[]>
    {
        private readonly IPsdLayer layer;
        private readonly LayerRecords layerRecords;
        private readonly LayerMask layerMask;
        private readonly int depth;

        public ChannelsReader(PsdReader reader, IPsdLayer layer, LayerRecords layerRecords, LayerMask layerMask, int depth)
            : base(reader)
        {
            this.layer = layer;
            this.layerRecords = layerRecords;
            this.layerMask = layerMask;
            this.depth = depth;
        }

        protected override void ReadValue(PsdReader reader, out Channel[] value)
        {
            int count = this.layerRecords.ChannelCount;
            List<Channel> channels = new List<Channel>(count);

            for (int i = 0; i < count; i++)
            {
                ChannelType channelType = this.layerRecords.ChannelTypes[i];
                long channelSize = this.layerRecords.ChannelSizes[i];
                CompressionType compressionType = reader.ReadCompressionType();

                int width, height;
                if(channelType == ChannelType.Mask)
                {
                    width = this.layerRecords.Width;
                    height = this.layerRecords.Height;
                }
                else
                {
                    width = this.layerMask.Width;
                    height = this.layerMask.Height;
                }

                Channel channel = new Channel(channelType, width, height, channelSize);

                if (channelType == ChannelType.Alpha && this.layer.Resources.Contains("iOpa") == true)
                {
                    byte opa = this.layer.Resources.ToByte("iOpa", "Opacity");
                    channel.Opacity = opa / 255.0f;
                }

                channel.ReadHeader(reader, compressionType);
                channel.Read(reader, this.depth, compressionType);
                channels.Add(channel);
            }
            
            value = channels.ToArray();
        }
    }
}
