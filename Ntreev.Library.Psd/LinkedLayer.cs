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
        private readonly LinkedDocumnetFileHeaderReader fileHeaderReader;

        public LinkedLayer(string name, Guid id, LinkedDocumentReader documentReader, LinkedDocumnetFileHeaderReader fileHeaderReader)
        {
            this.name = name;
            this.id = id;
            this.documentReader = documentReader;
            this.fileHeaderReader = fileHeaderReader;
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

        public int Width
        {
            get { return this.fileHeaderReader.Value.Width; }
        }

        public int Height
        {
            get { return this.fileHeaderReader.Value.Height; }
        }
    }
}
