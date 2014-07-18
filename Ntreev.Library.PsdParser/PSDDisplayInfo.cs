namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;

    public sealed class PSDDisplayInfo
    {
        public short[] color = new short[4];
        public short colorSpace;
        public bool kind;
        public short opacity;
        public byte padding;

        public void load(BinaryReader br)
        {
            this.colorSpace = EndianReverser.getInt16(br);
            if (this.colorSpace != 0)
            {
                throw new SystemException("Color space support RGB only");
            }
            for (int i = 0; i < this.color.Length; i++)
            {
                this.color[i] = EndianReverser.getInt16(br);
            }
            this.opacity = EndianReverser.getInt16(br);
            if ((this.opacity < 0) || (this.opacity > 100))
            {
                this.opacity = 100;
            }
            this.kind = br.ReadByte() != 0;
            this.padding = br.ReadByte();
        }

        public void save(BinaryWriter bw)
        {
            bw.Write(EndianReverser.convert(this.colorSpace));
            for (int i = 0; i < this.color.Length; i++)
            {
                bw.Write(EndianReverser.convert(this.color[i]));
            }
            bw.Write(EndianReverser.convert(this.opacity));
            bw.Write(this.kind ? 1 : 0);
            bw.Write(this.padding);
        }

        private enum PSDColorSpace
        {
            PSD_CS_ANPA = 0xbb8,
            PSD_CS_CMYK = 2,
            PSD_CS_DIC = 11,
            PSD_CS_FOCOLTONE = 4,
            PSD_CS_GRAYSCALE = 8,
            PSD_CS_HKS = 10,
            PSD_CS_HSB = 1,
            PSD_CS_LAB = 7,
            PSD_CS_PANTONE = 3,
            PSD_CS_RGB = 0,
            PSD_CS_TOYO = 6,
            PSD_CS_TRUMATCH = 5
        }
    }
}

