using Ntreev.Library.PsdParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ntreev.Library.PsdViewer
{
    static class LayerExtension
    {
        public static BitmapSource GetBitmap(this IImageSource imageSource)
        {
            if (imageSource.Width == 0 || imageSource.Height == 0)
                return null;

            byte[] data = imageSource.MergeChannels();
            var channelCount = imageSource.Channels.Length;
            var pitch = imageSource.Width * imageSource.Channels.Length;
            var w = imageSource.Width;
            var h = imageSource.Height;

            //var format = channelCount == 3 ? TextureFormat.RGB24 : TextureFormat.ARGB32;
            //var tex = new Texture2D(w, h, format, false);
            var colors = new Color[data.Length / channelCount];


            var k = 0;
            for (var y = h - 1; y >= 0; --y)
            {
                for (var x = 0; x < pitch; x += channelCount)
                {
                    var n = x + y * pitch;

                    var c = Color.FromArgb(1, 1, 1, 1);
                    if (channelCount == 4)
                    {
                        c.B = data[n++];
                        c.G = data[n++];
                        c.R = data[n++];
                        c.A = (byte)System.Math.Round(data[n++] / 255f * imageSource.Opacity * 255f);
                    }
                    else
                    {
                        c.B = data[n++];
                        c.G = data[n++];
                        c.R = data[n++];
                        c.A = (byte)System.Math.Round(imageSource.Opacity * 255f);
                    }
                    colors[k++] = c;
                }
            }
            if (channelCount == 4)
                return BitmapSource.Create(imageSource.Width, imageSource.Height, 96, 96, PixelFormats.Bgra32, null, data, pitch);
            return BitmapSource.Create(imageSource.Width, imageSource.Height, 96, 96, PixelFormats.Bgr24, null, data, pitch);
        }
    }
}
