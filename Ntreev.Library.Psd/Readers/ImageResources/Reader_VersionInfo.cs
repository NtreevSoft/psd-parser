using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.ImageResources
{
    [ResourceID("1057", DisplayName = "Version")]
    class Reader_VersionInfo : ResourceReaderBase
    {
        public Reader_VersionInfo(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties(5);

            props["Version"] = reader.ReadInt32();
            props["HasCompatibilityImage"] = reader.ReadBoolean();
            props["WriterName"] = reader.ReadString();
            props["ReaderName"] = reader.ReadString();
            props["FileVersion"] = reader.ReadInt32();

            value = props;
        }
    }
}
