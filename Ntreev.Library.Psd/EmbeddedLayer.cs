using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class EmbeddedLayer : ILinkedLayer
    {
        private readonly Guid id;
        private readonly PsdResolver resolver;
        private readonly Uri absoluteUri;
        private PsdDocument document;
        
        public EmbeddedLayer(Guid id, PsdResolver resolver, Uri absoluteUri)
        {
            this.id = id;
            this.resolver = resolver;
            this.absoluteUri = absoluteUri;
        }

        public PsdDocument Document
        {
            get
            {
                if (this.document == null)
                {
                    this.document = this.resolver.GetDocument(this.absoluteUri);
                }
                return this.document;
            }
        }

        public Uri AbsoluteUri
        {
            get { return this.absoluteUri; }
        }

        public bool HasDocument
        {
            get { return File.Exists(this.absoluteUri.LocalPath); }
        }

        public Guid ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.absoluteUri.LocalPath; }
        }
    }
}
