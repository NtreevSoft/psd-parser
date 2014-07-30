using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public class Channel
    {
        //private CompressionType compressionType;
        private byte[] data;
        private readonly ChannelType type;
        private int height;
        private int width;
        private int[] rlePackLengths;

        internal Channel(ChannelType type, int width, int height)
        {
            //this.compressionType = compressionType;
            this.type = type;
            this.width = width;
            this.height = height;
            //this.size = 100;
        }

        public byte[] Data
        {
            get { return this.data; }
        }

        public ChannelType Type
        {
            get { return this.type; }
        }

        //public int Size
        //{
        //    get { return this.size; }
        //}

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

                    if (rlePackLengths[i] < 0)
                    {
                        int qwer = 0;
                    }
                }

            }

        }

        internal void Load(PSDReader reader, int bpp, CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.Raw:
                    this.readData2(reader, bpp, compressionType, null);
                    return;

                case CompressionType.RLE:
                    this.readData2(reader, bpp, compressionType, this.rlePackLengths);
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

        //internal void LoadImage(PSDReader reader, int bpp)
        //{
        //    long position = reader.Position;
        //    if (this.size > 2)
        //    {
        //        //reader.Position = this.dataStartPosition;
        //        this.compressionType = (CompressionType)reader.ReadInt16();
        //        switch (this.compressionType)
        //        {
        //            case CompressionType.Raw:
        //                this.readData(reader, bpp, this.compressionType, null);
        //                return;

        //            case CompressionType.RLE:
        //                {
        //                    if (reader.Version == 1)
        //                    {
        //                        short[] rlePackLengths = new short[this.height * 2];
        //                        for (int i = 0; i < this.height; i++)
        //                        {
        //                            rlePackLengths[i] = reader.ReadInt16();
        //                        }
        //                        this.readData(reader, bpp, this.compressionType, rlePackLengths);
        //                    }
        //                    else
        //                    {
        //                        int[] rlePackLengths = new int[this.height * 2];
        //                        for (int i = 0; i < this.height; i++)
        //                        {
        //                            rlePackLengths[i] = reader.ReadInt32();
        //                        }
        //                        this.readData2(reader, bpp, this.compressionType, rlePackLengths);
        //                    }
        //                    return;
        //                }
        //        }
        //        throw new SystemException(string.Format("Unsupport compression type {0}", this.compressionType));
        //    }

        //    reader.Position = position + this.size;
        //}

        //private void readData(PSDReader reader, int bps, CompressionType compressionType, short[] rlePackLengths)
        //{
        //    int unpackedLength = 0;
        //    if (bps == 1)
        //    {
        //        unpackedLength = (this.width + 7) >> 3;
        //    }
        //    else
        //    {
        //        unpackedLength = (this.width * bps) >> 3;
        //    }
        //    this.data = new byte[unpackedLength * this.height];
        //    switch (compressionType)
        //    {
        //        case CompressionType.Raw:
        //            reader.Read(this.data, 0, this.data.Length);
        //            break;

        //        case CompressionType.RLE:
        //            for (int i = 0; i < this.height; i++)
        //            {
        //                byte[] buffer = new byte[rlePackLengths[i]];
        //                byte[] dst = new byte[unpackedLength];
        //                reader.Read(buffer, 0, rlePackLengths[i]);
        //                PSDUtil.decodeRLE(buffer, dst, rlePackLengths[i], unpackedLength);
        //                //dst = PSDUtil.decodeRLE(buffer);
        //                for (int j = 0; j < unpackedLength; j++)
        //                {
        //                    this.data[(i * unpackedLength) + j] = dst[j];
        //                }
        //            }
        //            break;
        //    }
        //    int num14 = bps;
        //    switch (num14)
        //    {
        //        case 1:
        //            {
        //                byte[] data = this.data;
        //                byte[] buffer6 = new byte[this.width * this.height];
        //                int height = this.height;
        //                int width = this.width;
        //                uint num7 = 0;
        //                int index = 0;
        //                int num11 = 0;
        //                for (int k = 0; k < (height * ((width + 7) >> 3)); k++)
        //                {
        //                    byte num12 = 0x80;
        //                    for (int m = 0; (m < 8) && (num7 < width); m++)
        //                    {
        //                        buffer6[num11++] = ((data[index] & num12) != 0) ? ((byte)0) : ((byte)1);
        //                        num12 = (byte)(num12 >> 1);
        //                        num7++;
        //                    }
        //                    if (num7 >= width)
        //                    {
        //                        num7 = 0;
        //                    }
        //                    index++;
        //                }
        //                this.data = buffer6;
        //                break;
        //            }
        //        case 8:
        //            break;

        //        default:
        //            if (num14 == 0x10)
        //            {
        //                byte[] buffer3 = this.data;
        //                byte[] buffer4 = new byte[this.width * this.height];
        //                for (int n = 0; n < this.data.Length; n++)
        //                {
        //                    this.data[n] = buffer3[n * 2];
        //                }
        //                this.data = buffer4;
        //            }
        //            return;
        //    }
        //}

        private void readData2(PSDReader reader, int bps, CompressionType compressionType, int[] rlePackLengths)
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
                            this.data[(i * length) + j] = dst[j];
                        }
                    }
                    break;
            }
        }
    }
}

