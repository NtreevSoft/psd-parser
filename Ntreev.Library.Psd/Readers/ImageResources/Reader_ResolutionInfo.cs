using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd.Readers.ImageResources
{
    [ResourceID("1005", DisplayName = "Resolution")]
    class Reader_ResolutionInfo : ResourceReaderBase
    {
        public Reader_ResolutionInfo(PsdReader reader, long length)
            : base(reader, length)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties(6);

            props["HorizontalRes"] = reader.ReadInt16();
            props["HorizontalResUnit"] = reader.ReadInt32();
            props["WidthUnit"] = reader.ReadInt16();
            props["VerticalRes"] = reader.ReadInt16();
            props["VerticalResUnit"] = reader.ReadInt32();
            props["HeightUnit"] = reader.ReadInt16();

            value = props;
        }
    }
}
