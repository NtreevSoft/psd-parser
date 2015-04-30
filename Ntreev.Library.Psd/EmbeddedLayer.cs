using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class EmbeddedLayer : LinkedLayer
    {
        private PsdDocument document;
        private Uri absoluteUri;
        private readonly PsdReader reader;
        private bool hasDocument;

        public EmbeddedLayer(PsdReader reader, Uri baseUri)
            : base(reader, baseUri)
        {
            this.reader = reader;
        }

        protected override void Validate(string type, int version)
        {
            if (type != "liFE" || version < 5)
                throw new InvalidFormatException();
        }

        public override Uri AbsoluteUri
        {
            get { return this.absoluteUri; }
        }

        public override PsdDocument Document
        {
            get 
            {
                if (this.HasDocument == false)
                    return null;

                if (this.document == null)
                {
                    this.document = this.reader.Resolver.GetDocument(this.absoluteUri);
                }
                return this.document; 
            }
        }

        public override string Name
        {
            get { return this.absoluteUri.LocalPath; }
        }

        public override bool HasDocument
        {
            get { return File.Exists(this.absoluteUri.LocalPath); }
        }

        protected override void OnDocumentRead(PsdReader reader, long length)
        {
            IProperties desc = new DescriptorStructure(reader);
            if (desc.Contains("fullPath") == true)
            {
                this.absoluteUri = new Uri(desc["fullPath"] as string);

                if (File.Exists(this.absoluteUri.LocalPath) == true)
                    return;
            }

            if (desc.Contains("relPath") == true)
            {
                string relativePath = desc["relPath"] as string;
                this.absoluteUri = reader.Resolver.ResolveUri(this.BaseUri, relativePath);
                if (File.Exists(this.absoluteUri.LocalPath) == true)
                    return;
            }

            if (desc.Contains("Nm") == true)
            {
                string name = desc["Nm"] as string;
                this.absoluteUri = reader.Resolver.ResolveUri(this.BaseUri, name);
                if (File.Exists(this.absoluteUri.LocalPath) == true)
                    return;
            }

            //throw new FileNotFoundException(string.Format("{0} 파일을 찾을 수 없습니다.", this.absoluteUri.LocalPath), this.absoluteUri.LocalPath);
        }
    }
}
