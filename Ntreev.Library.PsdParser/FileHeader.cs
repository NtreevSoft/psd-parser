using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public sealed class FileHeader
    {
        private string signature;
        private short version;
        private byte[] reserved;
        public short bpp;
        public short channels;
        public short colorMode;
        public int height;
        public int width;

        public void load(BinaryReader br)
        {
            this.signature = PSDUtil.readAscii(br, 4);
            this.version = EndianReverser.getInt16(br);
            this.reserved = br.ReadBytes(6);
            if ((this.signature != "8BPS") || (this.version != 1))
            {
                throw new Exception("Invalid PSD file");
            }
            this.channels = EndianReverser.getInt16(br);
            this.height = EndianReverser.getInt32(br);
            this.width = EndianReverser.getInt32(br);
            this.bpp = EndianReverser.getInt16(br);
            this.colorMode = EndianReverser.getInt16(br);
        }

        public void save(BinaryWriter bw)
        {
            bw.Write(Encoding.ASCII.GetBytes("8BPS"));
            bw.Write(EndianReverser.convert((short) 1));
            bw.Write(new byte[6]);
            bw.Write(EndianReverser.convert(this.channels));
            bw.Write(EndianReverser.convert(this.height));
            bw.Write(EndianReverser.convert(this.width));
            bw.Write(EndianReverser.convert(this.bpp));
            bw.Write(EndianReverser.convert(this.colorMode));
        }
    }
}

