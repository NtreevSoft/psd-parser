using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ColorModeDataSectionReader : LazyValueReader<byte[]>
    {
        public ColorModeDataSectionReader(PsdReader reader)
            : base(reader, null)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out byte[] value)
        {
            if (this.Length > 0)
            {
                value = reader.ReadBytes((int)this.Length);
            }
            else
            {
                value = new byte[] { };
            }
        }
    }
}
