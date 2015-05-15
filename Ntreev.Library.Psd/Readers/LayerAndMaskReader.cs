using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers
{
    class LayerAndMaskReader
    {
        private readonly PsdReader reader;
        private readonly long position;

        private LayerReader layerReader;
        private GlobalLayerMaskReader globalLayerMask;
        private LinkedLayerReader linkedLayerReader;

        public LayerAndMaskReader(PsdReader reader, PsdDocument document)
        {
            this.reader = reader;
            this.position = reader.Position;

            long length = reader.ReadLength();
            long end = reader.Position + length;

            this.layerReader = new LayerReader(reader, document);
            this.globalLayerMask = new GlobalLayerMaskReader(reader);
            this.linkedLayerReader = new LinkedLayerReader(reader, end, document.BaseUri);

            reader.Position = end;
        }

        public PsdLayer[] Layers
        {
            get { return this.layerReader.Value; }
        }

        public LinkedLayer[] LinkedLayers
        {
            get { return this.linkedLayerReader.Value; }
        }
    }
}

