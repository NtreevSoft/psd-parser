using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ColorModeDataReader : ReadableLazyValue<byte[]>
    {
        public ColorModeDataReader(PsdReader reader)
            : base(reader, true)
        {
            
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override byte[] ReadValue(PsdReader reader)
        {
            int size = reader.ReadInt32();
            if (size > 0)
            {
                return reader.ReadBytes(size);
            }
            return new byte[] { };
        }
    }
}
