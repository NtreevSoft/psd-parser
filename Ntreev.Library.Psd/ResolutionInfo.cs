using System;
using System.IO;

namespace Ntreev.Library.Psd
{
    public sealed class ResolutionInfo
    {
        private readonly int heightUnit;
        private readonly int horizontalRes;
        private readonly int horizontalResUnit;
        private readonly int verticalRes;
        private readonly int verticalResUnit;
        private readonly int widthUnit;

        internal ResolutionInfo(PSDReader reader)
        {
            this.horizontalRes = reader.ReadInt16();
            this.horizontalResUnit = reader.ReadInt32();
            this.widthUnit = reader.ReadInt16();
            this.verticalRes = reader.ReadInt16();
            this.verticalResUnit = reader.ReadInt32();
            this.heightUnit = reader.ReadInt16();
        }

        public int HeightUnit
        {
            get { return this.heightUnit; }
        }

        public int HorizontalRes
        {
            get { return this.horizontalRes; }
        }

        public int HorizontalResUnit
        {
            get { return this.horizontalResUnit; }
        }

        public int VerticalRes
        {
            get { return this.verticalRes; }
        }

        public int VerticalResUnit
        {
            get { return this.verticalResUnit; }
        }

        public int WidthUnit
        {
            get { return this.widthUnit; }
        }
    }
}

