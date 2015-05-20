using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LinkedLayer : ILinkedLayer
    {
        private readonly string name;
        private readonly Guid id;
        private readonly LinkedDocumentReader documentReader;

        public LinkedLayer(string name, Guid id, LinkedDocumentReader documentReader)
        {
            this.name = name;
            this.id = id;
            this.documentReader = documentReader;
        }

        public PsdDocument Document
        {
            get
            {
                if (this.documentReader == null)
                    return null;
                return this.documentReader.Value; 
            }
        }

        public Uri AbsoluteUri
        {
            get { return null; }
        }

        public bool HasDocument
        {
            get { return this.documentReader != null; }
        }

        public Guid ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
