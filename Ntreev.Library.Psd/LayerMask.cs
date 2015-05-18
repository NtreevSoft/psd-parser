using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    struct LayerMask
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public long Size { get; set; }

        public byte Color { get; set; }

        public byte Flag { get; set; }

        public int Width
        {
            get { return this.Right - this.Left; }
        }

        public int Height
        {
            get { return this.Bottom - this.Top; }
        }
    }
}
