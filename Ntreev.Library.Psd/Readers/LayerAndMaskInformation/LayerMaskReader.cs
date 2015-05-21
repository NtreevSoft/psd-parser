using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerMaskReader : ValueReader<LayerMask>
    {
        private LayerMaskReader(PsdReader reader)
            : base(reader, true, null)
        {

        }

        public static LayerMask Read(PsdReader reader)
        {
            LayerMaskReader instance = new LayerMaskReader(reader);
            return instance.Value;
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerMask value)
        {
            LayerMask mask = new LayerMask();
            mask.Top = reader.ReadInt32();
            mask.Left = reader.ReadInt32();
            mask.Bottom = reader.ReadInt32();
            mask.Right = reader.ReadInt32();
            mask.Color = reader.ReadByte();
            mask.Flag = reader.ReadByte();

            value = mask;
        }
    }
}
