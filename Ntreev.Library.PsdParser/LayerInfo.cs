using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    sealed class LayerInfo
    {
        //private int length;
        //public long channelDataEndPosition;
        //public long channelDataStartPosition;
        //private PSDLayer[] layers;

        //internal void loadData(PSDReader reader, int bpp)
        //{
        //    return;
        //    reader.Position = this.channelDataStartPosition;
        //    for (int i = 0; i < this.layers.Length; i++)
        //    {
        //        PSDLayer layer = this.layers[i];
        //        int length = layer.channels.Length;
        //        bool flag = !layer.drop && layer.isImageLayer;
        //        for (int j = 0; j < length; j++)
        //        {
        //            ChannelInfo info = layer.channels[j];
        //            if (flag)
        //            {
        //                info.LoadImage(reader, bpp);
        //            }
        //            else
        //            {
        //                reader.Position += info.Size;
        //            }
        //        }
        //    }
        //    reader.Position = this.channelDataEndPosition;
        //}

        public static Layer[] LoadLayers(PSDReader reader, int bpp)
        {
            int num = (int)reader.ReadUInt32();
            long num2 = reader.Position + num;
            reader.ReadInt32();
            int layerCount = reader.ReadInt16();
            if (layerCount == 0)
            {
                reader.Position = num2;

                return new Layer[] { };
            }
            else
            {
                if (layerCount < 0)
                {
                    layerCount = -layerCount;
                }
                Layer[] layers = new Layer[layerCount];
                for (int i = 0; i < layerCount; i++)
                {
                    layers[i] = new Layer(reader, bpp);
                }
                //this.channelDataStartPosition = reader.Position;
                //this.channelDataEndPosition = num2;

                foreach(var item in layers)
                {
                    item.LoadChannels(reader, bpp);
                }
                reader.Position = num2;

                return layers;
            }
        }

        //public PSDLayer[] Layers
        //{
        //    get { return this.layers; }
        //}
    }
}

