using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LayerMask
    {
        private int left, top, right, bottom;
        private long size;
        private byte color;
        private byte flag;

        public LayerMask(PsdReader reader)
        {
            this.size = reader.ReadInt32();
            long position = reader.Position;
            if (this.size == 0)
            {
                return;
            }

            this.top = reader.ReadInt32();
            this.left = reader.ReadInt32();
            this.bottom = reader.ReadInt32();
            this.right = reader.ReadInt32();

            this.color = reader.ReadByte();
            this.flag = reader.ReadByte();

            reader.Position = position + this.size;
        }

        public int Width
        {
            get { return this.right - this.left; }
        }

        public int Height
        {
            get { return this.bottom - this.top; }
        }

        public long Size
        {
            get { return this.size; }
        }
    }
}
