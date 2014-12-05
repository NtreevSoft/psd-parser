using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public static class Extension
    {
        public static byte[] MergeChannels(this IImageSource imageSource)
        {
            IChannel[] channels = imageSource.Channels;
            int length = channels.Length;
            int num2 = channels[0].Data.Length;

            byte[] buffer = new byte[(imageSource.Width * imageSource.Height) * length];
            int num3 = 0;
            for (int i = 0; i < num2; i++)
            {
                for (int j = channels.Length - 1; j >= 0; j--)
                {
                    buffer[num3++] = channels[j].Data[i];
                }
            }
            return buffer;
        }

        public static IEnumerable<IPsdLayer> Descendants(this IPsdLayer layer)
        {
            return Descendants(layer, item => true);
        }

        public static IEnumerable<IPsdLayer> Descendants(this IPsdLayer layer, Func<IPsdLayer, bool> filter)
        {
            foreach (var item in layer.Childs)
            {
                if (filter(item) == false)
                    continue;

                yield return item;

                foreach (var child in item.Descendants())
                {
                    yield return child;
                }
            }
        }

        internal static IEnumerable<PsdLayer> All(this PsdLayer layer)
        {
            yield return layer;

            foreach (var item in layer.Childs)
            {
                foreach (var child in item.All())
                {
                    yield return child;
                }
            }
        }
    }
}
