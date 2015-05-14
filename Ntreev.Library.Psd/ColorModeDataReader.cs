using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class ColorModeDataReader
    {
        private readonly PsdReader reader;
        private readonly long position;

        private byte[] data;

        public ColorModeDataReader(PsdReader reader)
        {
            this.reader = reader;
            this.position = reader.Position;
            int size = reader.ReadInt32();
            reader.Position += size;
        }

        public byte[] Data
        {
            get 
            {
                if (this.data == null)
                {
                    this.reader.Position = this.position;
                    this.data = Read(this.reader);
                }
                return this.data; 
            }
        }

        private static byte[] Read(PsdReader reader)
        {
            int size = reader.ReadInt32();
            if (size > 0)
            {
                return reader.ReadBytes(size);
            }
            return new byte[] { };
        }
    }
}
