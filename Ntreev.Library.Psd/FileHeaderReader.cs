using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class FileHeaderReader
    {
        private readonly string signature;
        private readonly short version;
        private readonly byte[] reserved;
        private readonly short depth;
        private readonly int channels;
        private readonly ColorMode colorMode;
        private readonly int height;
        private readonly int width;

        private readonly FileHeader value;

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
                throw new NotSupportedException("only support 8 Bit Channel");
            }

            this.value.Depth = this.depth;
            this.value.NumberOfChannels = this.channels;
            this.value.ColorMode = this.colorMode;
            this.value.Height = this.height;
            this.value.Width = this.width;
            this.value.Version = this.version;
        }

        public FileHeader Value
        {
            get { return this.value; }
        }
    }
}
