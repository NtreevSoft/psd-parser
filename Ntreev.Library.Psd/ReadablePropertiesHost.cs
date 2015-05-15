using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ReadablePropertiesHost : PropertiesHost
    {
        private readonly PsdReader reader;
        private readonly long position;
        private readonly long length;

        protected ReadablePropertiesHost(PsdReader reader)
            : this(reader, false)
        {
            
        }

        protected ReadablePropertiesHost(PsdReader reader, bool readSize)
        {
            if (readSize == true)
            {
                this.length = reader.ReadLength();
            }

            this.reader = reader;
            this.position = reader.Position;

            reader.Position += this.length;
        }

        protected ReadablePropertiesHost(PsdReader reader, Func<long> readFunc)
        {
            this.length = readFunc();

            this.reader = reader;
            this.position = reader.Position;

            reader.Position += this.length;
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