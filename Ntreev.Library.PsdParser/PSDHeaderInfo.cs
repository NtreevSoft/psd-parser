namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class PSDHeaderInfo
    {
        public short bpp;
        public short channels;
        public short colorMode;
        public int height;
        public int width;

        public void load(BinaryReader br)
        {
            string str = PSDUtil.readAscii(br, 4);
            short num = EndianReverser.getInt16(br);
            br.ReadBytes(6);
            if ((str != "8BPS") || (num != 1))
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

        private enum PSDColorMode
        {
            PSD_BITMAP = 0,
            PSD_CMYK = 4,
            PSD_DUOTONE = 8,
            PSD_GRAYSCALE = 1,
            PSD_INDEXED = 2,
            PSD_LAB = 9,
            PSD_MULTICHANNEL = 7,
            PSD_RGB = 3
        }
    }
}

