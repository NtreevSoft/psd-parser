using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ColorModeDataSectionReader : LazyReadableValue<byte[]>
    {
        public ColorModeDataSectionReader(PsdReader reader)
            : base(reader, true)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, out byte[] value)
        {
            int size = reader.ReadInt32();
            if (size > 0)
            {
                value = reader.ReadBytes(size);
            }
            else
            {
                value = new byte[] { };
            }
        }
    }
}
