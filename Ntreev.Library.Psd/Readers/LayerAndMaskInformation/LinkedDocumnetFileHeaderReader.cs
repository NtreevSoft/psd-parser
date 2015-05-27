using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LinkedDocumnetFileHeaderReader : LazyValueReader<FileHeaderSection>
    {
        public LinkedDocumnetFileHeaderReader(PsdReader reader, long length)
            : base(reader, length, null)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out FileHeaderSection value)
        {
            if (this.IsDocument(reader) == true)
            {
                using (Stream stream = new RangeStream(reader.Stream, reader.Position, this.Length))
                using (PsdReader r = new PsdReader(stream, reader.Resolver, reader.Uri))
                {
                    r.ReadDocumentHeader();
                    value = FileHeaderSectionReader.Read(r);
                }
            }
            else
            {
                value = new FileHeaderSection();
            }
        }

        private bool IsDocument(PsdReader reader)
        {
            long position = reader.Position;
            try
            {
                return reader.ReadType() == "8BPS";
            }
            finally
            {
                reader.Position = position;
            }
        }
    }
}
