using System;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers
{
    class LayerReader : ReadableValue<PsdLayer[]>
    {
        private readonly PsdDocument document;

        public LayerReader(PsdReader reader, PsdDocument document)
            : base(reader, true)
        {
            this.document = document;
        }

        protected override PsdLayer[] ReadValue(PsdReader reader)
        {
            int layerCount = reader.ReadInt16();
            if (layerCount == 0)
            {
                return new PsdLayer[] { };
            }

            if (layerCount < 0)
            {
                layerCount = -layerCount;
            }
            PsdLayer[] layers = new PsdLayer[layerCount];
            for (int i = 0; i < layerCount; i++)
            {
                layers[i] = new PsdLayer(reader, this.document, i);
            }

            foreach (var item in layers)
            {
                item.ReadChannels(reader);
            }

            layers = PsdLayer.Initialize(null, layers);

            foreach (var item in layers.SelectMany(item => item.All()).Reverse())
            {
                item.ComputeBounds();
            }

            return layers;
        }
    }
}

