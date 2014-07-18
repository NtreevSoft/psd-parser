namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;

    public sealed class PSDLayerInfo
    {
        public long channelDataEndPosition;
        public long channelDataStartPosition;
        public PSDLayer[] layers;

        public void loadData(BinaryReader br, int bpp)
        {
            br.BaseStream.Position = this.channelDataStartPosition;
            for (int i = 0; i < this.layers.Length; i++)
            {
                PSDLayer layer = this.layers[i];
                int length = layer.channels.Length;
                bool flag = !layer.drop && layer.isImageLayer;
                for (int j = 0; j < length; j++)
                {
                    PSDChannelInfo info = layer.channels[j];
                    if (flag)
                    {
                        info.loadData(br, bpp);
                    }
                    else
                    {
                        Stream baseStream = br.BaseStream;
                        baseStream.Position += info.size;
                    }
                }
            }
            br.BaseStream.Position = this.channelDataEndPosition;
        }

        public void loadHeader(BinaryReader br, int bpp)
        {
            int num = (int) EndianReverser.getUInt32(br);
            long num2 = br.BaseStream.Position + num;
            EndianReverser.getInt32(br);
            int num3 = EndianReverser.getInt16(br);
            if (num3 == 0)
            {
                br.BaseStream.Position = num2;
            }
            else
            {
                if (num3 < 0)
                {
                    num3 = -num3;
                }
                this.layers = new PSDLayer[num3];
                for (int i = 0; i < num3; i++)
                {
                    PSDLayer layer = new PSDLayer();
                    layer.load(br, bpp);
                    this.layers[i] = layer;
                }
                this.channelDataStartPosition = br.BaseStream.Position;
                this.channelDataEndPosition = num2;
                for (int j = 0; j < this.layers.Length; j++)
                {
                    PSDLayer layer2 = this.layers[j];
                    int length = layer2.channels.Length;
                    for (int k = 0; k < length; k++)
                    {
                        PSDChannelInfo info = layer2.channels[k];
                        info.dataStartPosition = br.BaseStream.Position;
                        Stream baseStream = br.BaseStream;
                        baseStream.Position += info.size;
                    }
                }
                br.BaseStream.Position = num2;
            }
        }
    }
}

