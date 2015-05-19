using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerMaskReader : LazyValueReader<LayerMask>
    {
        public LayerMaskReader(PsdReader reader)
            : base(reader, null)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerMask value)
        {
            LayerMask mask = new LayerMask();

            if (this.Length > 0)
            {
                mask.Top = reader.ReadInt32();
                mask.Left = reader.ReadInt32();
                mask.Bottom = reader.ReadInt32();
                mask.Right = reader.ReadInt32();
                mask.Size = this.Length;
                mask.Color = reader.ReadByte();
                mask.Flag = reader.ReadByte();
            }

            value = mask;
        }
    }
}
