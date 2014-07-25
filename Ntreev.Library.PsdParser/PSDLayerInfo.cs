using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSDLayerInfo
    {
        private int length;
        public long channelDataEndPosition;
        public long channelDataStartPosition;
        public PSDLayer[] layers;

        public void loadData(PSDReader reader, int bpp)
        {
            reader.Position = this.channelDataStartPosition;
            for (int i = 0; i < this.layers.Length; i++)
            {
                PSDLayer layer = this.layers[i];
                int length = layer.channels.Length;
                bool flag = !layer.drop && layer.isImageLayer;
                for (int j = 0; j < length; j++)
                {
                    ChannelInfo info = layer.channels[j];
                    if (flag)
                    {
                        info.loadData(reader, bpp);
                    }
                    else
                    {
                        reader.Position += info.size;
                    }
                }
            }
            reader.Position = this.channelDataEndPosition;
        }

        public void loadHeader(PSDReader reader, int bpp)
        {
            int num = (int) reader.ReadUInt32();
            long num2 = reader.Position + num;
            reader.ReadInt32();
            int layerCount = reader.ReadInt16();
            if (layerCount == 0)
            {
                reader.Position = num2;
            }
            else
            {
                if (layerCount < 0)
                {
                    layerCount = -layerCount;
                }
                this.layers = new PSDLayer[layerCount];
                for (int i = 0; i < layerCount; i++)
                {
                    PSDLayer layer = new PSDLayer();
                    layer.load(reader, bpp);
                    this.layers[i] = layer;
                }
                this.channelDataStartPosition = reader.Position;
                this.channelDataEndPosition = num2;
                for (int j = 0; j < this.layers.Length; j++)
                {
                    PSDLayer layer2 = this.layers[j];
                    int length = layer2.channels.Length;
                    for (int k = 0; k < length; k++)
                    {
                        ChannelInfo info = layer2.channels[k];
                        info.dataStartPosition = reader.Position;
                        reader.Position += info.size;
                    }
                }
                reader.Position = num2;
            }
        }
    }
}

