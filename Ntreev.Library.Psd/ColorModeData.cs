using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class ColorModeData
    {
        private int size;
        private byte[] data;

        internal ColorModeData(PSDReader reader)
        {
            this.size = reader.ReadInt32();
            if (this.size > 0)
            {
                this.data = new byte[this.size];
                this.data = reader.ReadBytes(this.size);
            }
        }

        public byte[] Data
        {
            get { return this.data; }
        }
    }
}

