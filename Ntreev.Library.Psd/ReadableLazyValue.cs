using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ReadableLazyValue<T>
    {
        private readonly PsdReader reader;
        private readonly long position;
        private readonly long length;
        private T value;

        protected ReadableLazyValue(PsdReader reader)
            : this(reader, false)
        {

        }

        protected ReadableLazyValue(PsdReader reader, bool hasLength)
        {
            if (hasLength == true)
            {
                this.length = this.OnLengthGet(reader);
            }

            this.reader = reader;
            this.position = reader.Position;

            reader.Position += this.length;
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

        protected virtual long OnLengthGet(PsdReader reader)
        {
            return reader.ReadLength();
        }

        protected abstract T ReadValue(PsdReader reader);

        protected long Length
        {
            get { return this.length; }
        }
    }
}