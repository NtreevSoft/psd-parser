using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ReadableValue<T>
    {
        private readonly PsdReader reader;
        private readonly long position;
        private T value;

        protected ReadableValue(PsdReader reader)
            : this(reader, false)
        {

        }

        protected ReadableValue(PsdReader reader, bool readSize)
        {
            long size = 0;
            if (readSize == true)
            {
                size = reader.ReadLength();
            }

            this.reader = reader;
            this.position = reader.Position;

            reader.Position += size;
        }

        public T Value
        {
            get
            {
                if (this.value == null)
                {
                    this.reader.Position = this.position;
                    this.value = this.ReadValue(this.reader);
                }
                return this.value;
            }
        }

        protected abstract T ReadValue(PsdReader reader);
    }
}