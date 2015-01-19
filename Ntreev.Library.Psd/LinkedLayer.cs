using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LinkedLayer : ILinkedLayer
    {
        private readonly Guid id;
        private readonly string fileName;
        private PsdDocument document;
        private DescriptorStructure descriptor;

        public LinkedLayer(PsdReader reader)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            string type = reader.ReadType();
            int version = reader.ReadInt32();

            if (type == "liFE")
            {
                int qwer = 0;
            }

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

        public PsdDocument Document
        {
            get { return this.document; }
        }

        public Guid ID
        {
            get { return this.id; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public IProperties Properties
        {
            get { return this.descriptor; }
        }

        public virtual bool IsEmbedded
        {
            get { return false; }
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

        private void ReadDocument(PsdReader reader, ref string fileName)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            bool fod = reader.ReadBoolean();
            if (fod == true)
            {
                var version = reader.ReadInt32();
                this.descriptor = new DescriptorStructure(reader);
            }

            if (length > 0)
            {
                byte[] bytes = reader.ReadBytes((int)length);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    if (this.IsDocument(bytes) == true)
                    {
                        PsdDocument psb = new PsdDocument();
                        psb.Read(stream, reader.Resolver);
                        this.document = psb;
                    }
                    else
                    {
                        //throw new NotSupportedException();
                    }
                }
            }
            else
            {
                int version = reader.ReadInt32();
                IProperties desc = new DescriptorStructure(reader);
                if (desc.Contains("relPath") == true)
                {
                    string relativePath = desc["relPath"] as string;
                    fileName = relativePath;
                    this.document = reader.Resolver.GetDocument(relativePath);
                }
                else
                {
                    string fullPath = desc["fullPath"] as string;
                    fileName = fullPath;
                    this.document = reader.Resolver.GetDocument(fullPath);
                }
            }
        }

        private bool IsDocument(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            using (PsdReader reader = new PsdReader(stream, null))
            {
                string key = reader.ReadType();
                return key == "8BPS";
            }
        }
    }
}
