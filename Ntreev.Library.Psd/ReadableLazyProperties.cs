using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ReadableLazyProperties : LazyProperties
    {
        private readonly PsdReader reader;
        private readonly long position;
        private readonly long length;

        protected ReadableLazyProperties(PsdReader reader)
            : this(reader, false)
        {
            
        }

        protected ReadableLazyProperties(PsdReader reader, bool hasLength)
        {
            if (hasLength == true)
            {
                this.length = this.OnLengthGet(reader);
            }

            this.reader = reader;
            this.position = reader.Position;

            reader.Position += this.length;
        }

        protected virtual long OnLengthGet(PsdReader reader)
        {
            return reader.ReadLength();
        }

        protected long Length
        {
            get { return this.length; }
        }

        protected sealed override IEnumerable<KeyValuePair<string, object>> CreateProperties()
        {
            this.reader.Position = this.position;
            return this.CreateProperties(this.reader);
        }

        protected abstract IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader);
    }
}