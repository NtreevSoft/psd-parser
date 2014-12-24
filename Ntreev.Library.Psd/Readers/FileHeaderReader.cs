using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class FileHeaderReader : FileHeader
    {
        private readonly string signature;
        private readonly short version;
        private readonly byte[] reserved;
        private readonly short depth;
        private readonly int channels;
        private readonly ColorMode colorMode;
        private readonly int height;
        private readonly int width;

        public FileHeaderReader(PsdReader reader)
        {
            this.signature = reader.ReadAscii(4);
            this.version = reader.ReadInt16();
            this.reserved = reader.ReadBytes(6);
            if ((this.signature != "8BPS") || (this.version != 1 && this.version != 2))
            {
                throw new InvalidFormatException();
            }
            this.channels = reader.ReadInt16();
            this.height = reader.ReadInt32();
            this.width = reader.ReadInt32();
            this.depth = reader.ReadInt16();
            this.colorMode = (ColorMode)reader.ReadInt16();

            reader.Version = this.version;
            if (this.depth != 8)
            {
                throw new SystemException("For now, only Support 8 Bit Per Channel");
            }
        }

        public override int Depth
        {
            get { return this.depth; }
        }

        public override int NumberOfChannels
        {
            get { return this.channels; }
        }

        public override ColorMode ColorMode
        {
            get { return this.colorMode; }
        }

        public override int Height
        {
            get { return this.height; }
        }

        public override int Width
        {
            get { return this.width; }
        }

        public override int Version
        {
            get { return this.version; }
        }
    }
}
