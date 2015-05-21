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
            LayerRecords records = layer.Records;

            //int count = records.ChannelCount;
            //List<Channel> channels = new List<Channel>(count);

            foreach(var item in records.Channels)
            {
                //Channel channel = records.Channels[i];
                CompressionType compressionType = reader.ReadCompressionType();

                //long p = reader.Position;
                //int width = records.Width;
                //int height = records.Height;

                //if (channelType == ChannelType.Mask && layer.ExtraRecords.Mask != null)
                //{
                //    LayerMask layerMask = layer.ExtraRecords.Mask;
                //    width = layerMask.Width;
                //    height = layerMask.Height;
                //}

                //Channel channel = new Channel(channelType, width, height, channelSize);

                //if (channelType == ChannelType.Alpha && layer.Resources.Contains("iOpa") == true)
                //{
                //    byte opa = layer.Resources.ToByte("iOpa", "Opacity");
                //    channel.Opacity = opa / 255.0f;
                //}

                item.ReadHeader(reader, compressionType);
                item.Read(reader, layer.Depth, compressionType);
                //channels.Add(channel);
            }

            value = records.Channels;
        }

        private void ReadChannel(PsdReader reader, LayerRecords records, LayerExtraRecords extraRecords)
        {

        }
    }
}
