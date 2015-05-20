using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LinkedDocumentReader : LazyValueReader<PsdDocument>
    {
        public LinkedDocumentReader(PsdReader reader, long length)
            : base(reader, length, null)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out PsdDocument value)
        {
            if (this.IsDocument(reader) == true)
            {
                using (Stream stream = new RangeStream(reader.Stream, reader.Position, this.Length))
                {
                    PsdDocument document = new InternalDocument();
                    document.Read(stream, reader.Resolver, reader.Uri);
                    value = document;
                }
            }
            else
            {
                value = null;
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
