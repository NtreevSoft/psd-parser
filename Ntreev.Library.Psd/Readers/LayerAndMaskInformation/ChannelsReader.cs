using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class ChannelsReader : LazyValueReader<Channel[]>
    {
        public ChannelsReader(PsdReader reader, long length, PsdLayer layer)
            : base(reader, length, layer)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out Channel[] value)
        {
            PsdLayer layer = userData as PsdLayer;

            int count = layer.Records.ChannelCount;
            List<Channel> channels = new List<Channel>(count);

            for (int i = 0; i < count; i++)
            {
                ChannelType channelType = layer.Records.ChannelTypes[i];
                long channelSize = layer.Records.ChannelSizes[i];

                CompressionType compressionType = reader.ReadCompressionType();

                int width, height;
                if (channelType != ChannelType.Mask)
                {
                    width = layer.Width;
                    height = layer.Height;
                }
                else
                {
                    LayerMask layerMask = layer.ExtraRecords.Mask;
                    width = layerMask.Width;
                    height = layerMask.Height;
                }

                Channel channel = new Channel(channelType, width, height, channelSize);

                if (channelType == ChannelType.Alpha && layer.Resources.Contains("iOpa") == true)
                {
                    byte opa = layer.Resources.ToByte("iOpa", "Opacity");
                    channel.Opacity = opa / 255.0f;
                }

                channel.ReadHeader(reader, compressionType);
                channel.Read(reader, layer.Depth, compressionType);
                channels.Add(channel);
            }

            value = channels.ToArray();
        }
    }
}
