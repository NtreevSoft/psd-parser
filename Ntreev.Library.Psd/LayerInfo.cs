using System;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    sealed class LayerInfo
    {
        public static PsdLayer[] ReadLayers(PSDReader reader, PsdDocument psd, int bpp)
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
                layers[i] = new PsdLayer(reader, psd, bpp, i);
            }

            foreach (var item in layers)
            {
                item.LoadChannels(reader, bpp);
            }

            reader.Position = start + length;

            return PsdLayer.Initialize(null, layers);
        }

        public static void ComputeBounds(PsdLayer[] layers)
        {
            foreach (var item in layers.SelectMany(item => item.Descendants()).Reverse())
            {
                item.ComputeBounds();
            }
        }
    }
}

