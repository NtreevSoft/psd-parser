using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd
{
    class ResolutionInfoReader : ReadablePropertiesHost
    {
        public ResolutionInfoReader(PsdReader reader)
            : base(reader)
        {
            
        }

        protected override IDictionary<string, object> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>(6);

            props["HorizontalRes"] = reader.ReadInt16();
            props["HorizontalResUnit"] = reader.ReadInt32();
            props["WidthUnit"] = reader.ReadInt16();
            props["VerticalRes"] = reader.ReadInt16();
            props["VerticalResUnit"] = reader.ReadInt32();
            props["HeightUnit"] = reader.ReadInt16();

            return props;
        }
    }
}
