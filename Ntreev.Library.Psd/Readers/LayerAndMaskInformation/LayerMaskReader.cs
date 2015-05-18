using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerMaskReader : ReadableValue<LayerMask>
    {
        public LayerMaskReader(PsdReader reader)
            : base(reader, true)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, out LayerMask value)
        {
            value = new LayerMask();

            if (this.Length == 0)
                return;

            value.Top = reader.ReadInt32();
            value.Left = reader.ReadInt32();
            value.Bottom = reader.ReadInt32();
            value.Right = reader.ReadInt32();
            value.Size = this.Length;
            value.Color = reader.ReadByte();
            value.Flag = reader.ReadByte();
        }
    }
}
