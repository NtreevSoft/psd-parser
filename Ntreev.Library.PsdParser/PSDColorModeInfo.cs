namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;

    public sealed class PSDColorModeInfo
    {
        public byte[] data;
        public int size;

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
    }
}

