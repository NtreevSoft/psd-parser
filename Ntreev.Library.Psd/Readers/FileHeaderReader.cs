using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class FileHeaderReader : ReadableValue<FileHeader>
    {
        public FileHeaderReader(PsdReader reader)
            : base(reader)
        {
            
        }

        protected override void ReadValue(PsdReader reader, out FileHeader value)
        {
            value = new FileHeader();

            reader.ValidateDocumentSignature();
            reader.Version = reader.ReadInt16();
            reader.Skip(6);
            
            value.NumberOfChannels = reader.ReadInt16();
            value.Height = reader.ReadInt32();
            value.Width = reader.ReadInt32();
            value.Depth = reader.ReadInt16();
            value.ColorMode = reader.ReadColorMode();

            if (value.Depth != 8)
            {
                throw new NotSupportedException("only support 8 Bit Channel");
            }
        }
    }
}
