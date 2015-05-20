using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd
{
    class Channel : IChannel
    {
        private byte[] data;
        private readonly ChannelType type;
        private int height;
        private int width;
        private int[] rlePackLengths;
        private float opacity = 1.0f;
        private long size;

        public Channel(ChannelType type, int width, int height, long size)
        {
            this.type = type;
            this.width = width;
            this.height = height;
            this.size = size;
        }

        public byte[] Data
        {
            get { return this.data; }
        }

        public ChannelType Type
        {
            get { return this.type; }
        }

        public void ReadHeader(PsdReader reader, CompressionType compressionType)
        {
            if (compressionType != CompressionType.RLE)
                return;

            this.rlePackLengths = new int[this.height];
            if (reader.Version == 1)
            {
                for (int i = 0; i < this.height; i++)
                {
                    this.rlePackLengths[i] = reader.ReadInt16();
                }
            }
            else
            {
                for (int i = 0; i < this.height; i++)
                {
                    this.rlePackLengths[i] = reader.ReadInt32();
                }
            }
        }

        public void Read(PsdReader reader, int bpp, CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Raw:
                    this.ReadData(reader, bpp, compressionType, null);
                    return;

                case CompressionType.RLE:
                    this.ReadData(reader, bpp, compressionType, this.rlePackLengths);
                    return;

                default:
                    break;
            }
        }

        public int Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
            }
        }

        public float Opacity
        {
            get { return this.opacity; }
            set { this.opacity = value; }
        }

        public long Size
        {
            get { return this.size; }
        }

        private void ReadData(PsdReader reader, int bps, CompressionType compressionType, int[] rlePackLengths)
        {
            int length = PsdUtility.DepthToPitch(bps, this.width);
            this.data = new byte[length * this.height];
            switch (compressionType)
            {
                case CompressionType.Raw:
                    reader.Read(this.data, 0, this.data.Length);
                    break;

                case CompressionType.RLE:
                    for (int i = 0; i < this.height; i++)
                    {
                        byte[] buffer = new byte[rlePackLengths[i]];
                        byte[] dst = new byte[length];
                        reader.Read(buffer, 0, rlePackLengths[i]);
                        PsdUtility.DecodeRLE(buffer, dst, rlePackLengths[i], length);
                        for (int j = 0; j < length; j++)
                        {
                            this.data[(i * length) + j] = (byte)(dst[j] * this.opacity);
                        }
                    }
                    break;
            }
        }
    }
}

