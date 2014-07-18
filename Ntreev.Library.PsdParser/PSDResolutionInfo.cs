namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;

    public sealed class PSDResolutionInfo
    {
        public short heightUnit;
        public short horizontalRes;
        public int horizontalResUnit;
        public short verticalRes;
        public int verticalResUnit;
        public short widthUnit;

        public void load(BinaryReader br)
        {
            this.horizontalRes = EndianReverser.getInt16(br);
            this.horizontalResUnit = EndianReverser.getInt32(br);
            this.widthUnit = EndianReverser.getInt16(br);
            this.verticalRes = EndianReverser.getInt16(br);
            this.verticalResUnit = EndianReverser.getInt32(br);
            this.heightUnit = EndianReverser.getInt16(br);
        }

        public void save(BinaryWriter bw)
        {
            bw.Write(EndianReverser.convert(this.horizontalRes));
            bw.Write(EndianReverser.convert(this.horizontalResUnit));
            bw.Write(EndianReverser.convert(this.widthUnit));
            bw.Write(EndianReverser.convert(this.verticalRes));
            bw.Write(EndianReverser.convert(this.verticalResUnit));
            bw.Write(EndianReverser.convert(this.heightUnit));
        }
    }
}

