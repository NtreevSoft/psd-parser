using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class ResolutionInfo
    {
        public readonly short heightUnit;
        public readonly short horizontalRes;
        public readonly int horizontalResUnit;
        public readonly short verticalRes;
        public readonly int verticalResUnit;
        public readonly short widthUnit;

        public ResolutionInfo(BinaryReader br)
        {
            this.horizontalRes = EndianReverser.getInt16(br);
            this.horizontalResUnit = EndianReverser.getInt32(br);
            this.widthUnit = EndianReverser.getInt16(br);
            this.verticalRes = EndianReverser.getInt16(br);
            this.verticalResUnit = EndianReverser.getInt32(br);
            this.heightUnit = EndianReverser.getInt16(br);
        }
    }
}

