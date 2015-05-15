using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LayerRecords
    {
        private int channelCount;

        public void ValidateSize()
        {
            int width = this.Right - Left;
            int height = this.Bottom - this.Top;

            if ((width > 0x3000) || (height > 0x3000))
            {
                throw new NotSupportedException(string.Format("Invalidated size ({0}, {1})", width, height));
            }
        }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public int ChannelCount
        {
            get { return this.channelCount; }
            set
            {
                if (value > 0x38)
                {
                    throw new Exception(string.Format("Too many channels : {0}", channelCount));
                }
                this.channelCount = value;
            }
        }

        public ChannelType[] ChannelTypes { get; set; }

        public long[] ChannelSizes { get; set; }

        public BlendMode BlendMode { get; set; }

        public byte Opacity { get; set; }

        public bool Clipping { get; set; }

        public LayerFlags Flags { get; set; }

        public int Filter { get; set; }
    }
}
