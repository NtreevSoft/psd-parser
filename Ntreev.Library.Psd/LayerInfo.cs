using System;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    class LayerInfo
    {
        public static PsdLayer[] ReadLayers(PsdReader reader, PsdDocument document)
        {
            int length = reader.ReadLength();
            long start = reader.Position;
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
                layers[i] = new PsdLayer(reader, document, i);
            }

            foreach (var item in layers)
            {
                item.LoadChannels(reader);
            }

            reader.Position = start + length;

            return PsdLayer.Initialize(null, layers);
        }

        public static void ComputeBounds(PsdLayer[] layers)
        {
            foreach (var item in layers.SelectMany(item => item.All()).Reverse())
            {
                item.ComputeBounds();
            }
        }
    }
}

