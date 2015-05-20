using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LinkedLayerReader : ValueReader<LinkedLayer>
    {
        public LinkedLayerReader(PsdReader reader)
            : base(reader, true, null)
        {
            
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return (reader.ReadInt64() + 3) & (~3);
        }

        protected override void ReadValue(PsdReader reader, object userData, out LinkedLayer value)
        {
            reader.ValidateSignature("liFD");
            int version = reader.ReadInt32();

            Guid id = new Guid(reader.ReadPascalString(1));
            string name = reader.ReadString();
            string type = reader.ReadType();
            string creator = reader.ReadType();
            long length = reader.ReadInt64();
            IProperties properties = reader.ReadBoolean() == true ? new DescriptorStructure(reader) : null;

            bool isDocument = this.IsDocument(reader);
            LinkedDocumentReader documentReader = null;
            if(length > 0 && isDocument == true)
            {
                documentReader = new LinkedDocumentReader(reader, length);
            }

            value = new LinkedLayer(name, id, documentReader);
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
