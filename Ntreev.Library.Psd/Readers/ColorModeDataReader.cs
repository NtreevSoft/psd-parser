using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ColorModeDataReader : ColorModeData
    {
        private byte[] data;

        internal ColorModeDataReader(PsdReader reader)
        {
            int size = reader.ReadInt32();
            if (size > 0)
            {
                this.data = new byte[size];
                this.data = reader.ReadBytes(size);
            }
        }

        public override byte[] Data
        {
            get { return this.data; }
        }
    }
}
