using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class Channel
    {
        private byte[] data;
        private readonly ChannelType type;
        private int height;
        private int width;
        private int[] rlePackLengths;
        private float opacity = 1.0f;

        internal Channel(ChannelType type, int width, int height)
        {
            this.type = type;
            this.width = width;
            this.height = height;
        }

        public byte[] Data
        {
            get { return this.data; }
        }

        public ChannelType Type
        {
            get { return this.type; }
        }

        internal void LoadHeader(PSDReader reader, CompressionType compressionType)
        {
            if (compressionType != CompressionType.RLE)
                return;

            this.rlePackLengths = new int[this.height];
            if (reader.Version == 1)
            {
                for (int i = 0; i < this.height; i++)
                {
                    rlePackLengths[i] = reader.ReadInt16();
                }
            }
            else
            {
                for (int i = 0; i < this.height; i++)
                {
                    rlePackLengths[i] = reader.ReadInt32();
                }
            }
        }

        internal void Load(PSDReader reader, int bpp, CompressionType compressionType)
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
                    {
                        int qwre = 0;
                    }
                    break;
            }
        }

        internal int Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
            }
        }

        internal int Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
            }
        }

        internal float Opacity
        {
            get { return this.opacity; }
            set { this.opacity = value; }
        }

        private void ReadData(PSDReader reader, int bps, CompressionType compressionType, int[] rlePackLengths)
        {
            int length = PSDUtil.DepthToPitch(bps, this.width);
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
                        PSDUtil.decodeRLE(buffer, dst, rlePackLengths[i], length);
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

