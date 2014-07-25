using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class DisplayInfo
    {
        public readonly short[] color = new short[4];
        public readonly short colorSpace;
        public readonly bool kind;
        public readonly short opacity;
        public readonly byte padding;

        public DisplayInfo(BinaryReader br)
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
    }
}

