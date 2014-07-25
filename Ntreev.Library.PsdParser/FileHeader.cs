using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public sealed class FileHeader
    {
        private readonly string signature;
        private readonly short version;
        private readonly byte[] reserved;
        private readonly short bpp;
        private readonly int channels;
        private readonly ColorMode colorMode;
        private readonly int height;
        private readonly int width;

        internal FileHeader(PSDReader reader)
        {
            this.signature = reader.ReadAscii(4);
            this.version = reader.ReadInt16();
            this.reserved = reader.ReadBytes(6);
            if ((this.signature != "8BPS") || (this.version != 1))
            {
                throw new Exception("Invalid PSD file");
            }
            this.channels = reader.ReadInt16();
            this.height = reader.ReadInt32();
            this.width = reader.ReadInt32();
            this.bpp = reader.ReadInt16();
            this.colorMode = (ColorMode)reader.ReadInt16();
        }

        public int BPP
        {
            get { return this.bpp; }
        }

        public int NumberOfChannels
        {
            get { return this.channels; }
        }

        public ColorMode ColorMode
        {
            get { return this.colorMode; }
        }

        public int Height
        {
            get { return this.height; }
        }

        public int Width
        {
            get { return this.width; }
        }


        //public void save(BinaryWriter bw)
        //{
        //    bw.Write(Encoding.ASCII.GetBytes("8BPS"));
        //    bw.Write(reader.convert((short) 1));
        //    bw.Write(new byte[6]);
        //    bw.Write(reader.convert(this.channels));
        //    bw.Write(reader.convert(this.height));
        //    bw.Write(reader.convert(this.width));
        //    bw.Write(reader.convert(this.bpp));
        //    bw.Write(reader.convert(this.colorMode));
        //}
    }
}

