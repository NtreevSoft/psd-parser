using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageDataSectionReader : LazyValueReader<Channel[]>
    {
        private readonly int channelCount;
        private readonly int width;
        private readonly int height;
        private readonly int depth;

        public ImageDataSectionReader(PsdReader reader, PsdDocument document)
            : base(reader, null)
        {
            this.channelCount = document.FileHeaderSection.NumberOfChannels;
            this.width = document.Width;
            this.height = document.Height;
            this.depth = document.FileHeaderSection.Depth;
        }

        protected override void ReadValue(PsdReader reader, object userData, out Channel[] value)
        {
            CompressionType compressionType = (CompressionType)reader.ReadInt16();

            ChannelType[] types = new ChannelType[] { ChannelType.Red, ChannelType.Green, ChannelType.Blue, ChannelType.Alpha, };
            Channel[] channels = new Channel[this.channelCount];

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = new Channel(types[i], this.width, this.height, 0);
                channels[i].ReadHeader(reader, compressionType);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i].Read(reader, this.depth, compressionType);
            }

            if (channels.Length == 4)
            {
                for (int i = 0; i < channels[3].Data.Length; i++)
                {
                    float a = channels[3].Data[i] / 255.0f;

                    for (int j = 0; j < 3; j++)
                    {
                        float r = channels[j].Data[i] / 255.0f;
                        float r1 = (((a + r) - 1f) * 1f) / a;
                        channels[j].Data[i] = (byte)(r1 * 255.0f);
                    }
                }
            }

            value = channels.OrderBy(item => item.Type).ToArray();
        }
    }
}
