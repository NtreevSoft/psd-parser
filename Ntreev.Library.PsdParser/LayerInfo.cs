using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    sealed class LayerInfo
    {
        public static Layer[] ReadLayers(PSDReader reader, PSD psd, int bpp)
        {
            int length = reader.ReadLength();
            long start = reader.Position;
            int layerCount = reader.ReadInt16();
            if (layerCount == 0)
            {
                return new Layer[] { };
            }

            if (layerCount < 0)
            {
                layerCount = -layerCount;
            }
            Layer[] layers = new Layer[layerCount];
            for (int i = 0; i < layerCount; i++)
            {
                layers[i] = new Layer(reader, psd, bpp, i);
            }

            foreach (var item in layers)
            {
                item.LoadChannels(reader, bpp);
            }

            reader.Position = start + length;

            return Layer.Initialize(null, layers);
        }
    }
}

