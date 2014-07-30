using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public static class Extension
    {
        public static byte[] MergeChannels(this IImageSource imageSource)
        {
            Channel[] channels = imageSource.Channels;
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
    }
}
