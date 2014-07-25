using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class ColorModeData
    {
        private int size;
        private byte[] data;

        public void load(BinaryReader br)
        {
            this.size = EndianReverser.getInt32(br);
            if (this.size > 0)
            {
                this.data = new byte[this.size];
                this.data = br.ReadBytes(this.size);
            }
        }

        public void save(BinaryWriter bw)
        {
            bw.Write(EndianReverser.convert(this.size));
            if (this.size > 0)
            {
                bw.Write(this.data);
            }
        }

        public byte[] Data
        {
            get { return this.data; }
        }
    }
}

