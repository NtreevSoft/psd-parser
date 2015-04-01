using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LinkedLayer : ILinkedLayer
    {
        private readonly Uri baseUri;
        private readonly Guid id;
        private readonly string fileName;
        private PsdDocument document;
        private DescriptorStructure descriptor;
        private bool hasDocument;

        public LinkedLayer(PsdReader reader, Uri baseUri)
        {
            this.baseUri = baseUri;
            long length = reader.ReadInt64();
            long position = reader.Position;

            string type = reader.ReadType();
            int version = reader.ReadInt32();

            //if (type != "liFE")
            //{
            //    throw new Exception();
            //}

            this.Validate(type, version);
            this.id = new Guid(reader.ReadPascalString(1));
            this.fileName = reader.ReadString();

            string fileType = reader.ReadType();
            string fileCreator = reader.ReadType();

            this.ReadDocument(reader, ref this.fileName);

            reader.Position = position + length;
            if (reader.Position % 2 != 0)
                reader.Position++;

            reader.Position += ((reader.Position - position) % 4);
        }

        public virtual PsdDocument Document
        {
            get { return this.document; }
        }

        public Guid ID
        {
            get { return this.id; }
        }

        public virtual string Name
        {
            get { return this.fileName; }
        }

        public IProperties Properties
        {
            get { return this.descriptor; }
        }

        public virtual Uri AbsoluteUri
        {
            get { return null; }
        }

        public virtual bool HasDocument
        {
            get { return this.hasDocument; }
        }

        public Uri BaseUri
        {
            get { return this.baseUri; }
        }

        protected virtual void Validate(string type, int version)
        {
            if (type != "liFD" || version < 2)
                throw new InvalidFormatException();
        }

        protected virtual string Path
        {
            get { return string.Empty; }
        }

        protected virtual void OnDocumentRead(PsdReader reader, long length)
        {
            if (length > 0)
            {
                if (this.IsDocument(reader) == true)
                {
                    using (Stream stream = new RangeStream(reader.Stream, reader.Position, length))
                    {
                        PsdDocument psb = new InternalDocument(this.baseUri);
                        psb.Read(stream, reader.Resolver);
                        this.document = psb;
                    }
                    this.hasDocument = true;
                }
            }
        }

        private void ReadDocument(PsdReader reader, ref string fileName)
        {
            long length = reader.ReadInt64();

            if (reader.ReadBoolean() == true)
            {
                this.descriptor = new DescriptorStructure(reader);
            }

            this.OnDocumentRead(reader, length);
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
