using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd.Readers.ImageResource
{
    [ResourceID("1005", DisplayName = "Resolution")]
    class Reader_ResolutionInfo : ImageResourceBase
    {
        public Reader_ResolutionInfo(PsdReader reader)
            : base(reader)
        {
            
        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
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
