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
        private string fileName;

        public EmbeddedLayer(PsdReader reader)
            : base(reader)
        {

        }

        protected override void Validate(string type, int version)
        {
            if (type != "liFE" || version < 5)
                throw new InvalidFormatException();
        }

        public override bool IsEmbedded
        {
            get { return true; }
        }

        public override PsdDocument Document
        {
            get 
            {
                if (this.document == null)
                {
                    this.document = new PsdDocument();
                    this.document.Read(this.fileName);
                }
                return this.document; 
            }
        }

        public override string FileName
        {
            get { return this.fileName; }
        }

        public override bool HasDocument
        {
            get { return true; }
        }

        protected override void OnDocumentRead(PsdReader reader, long length)
        {
            IProperties desc = new DescriptorStructure(reader);
            if (desc.Contains("fullPath") == true)
            {
                Uri uri = new Uri(desc["fullPath"] as string);
                this.fileName = uri.LocalPath;
            }
            else if (desc.Contains("relPath") == true)
            {
                string relativePath = desc["relPath"] as string;
                this.fileName = reader.Resolver.GetPath(relativePath);
            }
            else
            {
                throw new Exception();
            }

            if (File.Exists(this.fileName) == false)
                throw new FileNotFoundException("찾을 수 없습니다.", this.fileName);
        }
    }
}
