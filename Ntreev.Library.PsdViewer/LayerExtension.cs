//Released under the MIT License.
//
//Copyright (c) 2015 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Ntreev.Library.Psd;
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
            if (imageSource.HasImage == false)
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
