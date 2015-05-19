using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class FileHeaderSectionReader : ValueReader<FileHeaderSection>
    {
        public FileHeaderSectionReader(PsdReader reader)
            : base(reader, false, null)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out FileHeaderSection value)
        {
            value = new FileHeaderSection();
           
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
