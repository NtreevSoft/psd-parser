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

        protected ReadablePropertiesHost(PsdReader reader)
        {
            this.reader = reader;
            this.position = reader.Position;
        }

        protected sealed override IDictionary<string, object> CreateProperties()
        {
            this.reader.Position = this.position;
            return this.CreateProperties(this.reader);
        }

        protected abstract IDictionary<string, object> CreateProperties(PsdReader reader);
    }
}