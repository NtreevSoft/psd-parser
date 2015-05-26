using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageDataSectionReader : LazyValueReader<Channel[]>
    {
        public ImageDataSectionReader(PsdReader reader, PsdDocument document)
            : base(reader, document)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.Length - reader.Position;
        }

        protected override void ReadValue(PsdReader reader, object userData, out Channel[] value)
        {
            //using (MemoryStream stream = new MemoryStream(reader.ReadBytes((int)this.Length)))
            //using (PsdReader r = new PsdReader(stream, reader.Resolver, reader.Uri))
            //{
            //    r.Version = reader.Version;
            //    value = ReadValue(r, userData as PsdDocument);
            //}
            value = ReadValue(reader, userData as PsdDocument);
        }

        private static Channel[] ReadValue(PsdReader reader, PsdDocument document)
        {
            int channelCount = document.FileHeaderSection.NumberOfChannels;
            int width = document.Width;
            int height = document.Height;
            int depth = document.FileHeaderSection.Depth;

            CompressionType compressionType = (CompressionType)reader.ReadInt16();

            ChannelType[] types = new ChannelType[] { ChannelType.Red, ChannelType.Green, ChannelType.Blue, ChannelType.Alpha };
            Channel[] channels = new Channel[channelCount];

            for (int i = 0; i < channels.Length; i++)
            {
                ChannelType type = i < types.Length ? types[i] : ChannelType.Mask;
                channels[i] = new Channel(type, width, height, 0);
                channels[i].ReadHeader(reader, compressionType);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i].Read(reader, depth, compressionType);
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

            return channels.OrderBy(item => item.Type).ToArray();
        }
    }
}
