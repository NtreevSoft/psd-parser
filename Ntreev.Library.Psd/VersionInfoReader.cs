using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class VersionInfoReader : ReadablePropertiesHost
    {
        public VersionInfoReader(PsdReader reader)
            : base(reader)
        {

        }

        protected override IDictionary<string, object> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>(5);

            props["Version"] = reader.ReadInt32();
            props["HasCompatibilityImage"] = reader.ReadBoolean();
            props["WriterName"] = reader.ReadString();
            props["ReaderName"] = reader.ReadString();
            props["FileVersion"] = reader.ReadInt32();

            return props;
        }
    }
}
