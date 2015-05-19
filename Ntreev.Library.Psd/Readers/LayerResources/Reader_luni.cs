using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("luni")]
    class Reader_luni : ResourceReaderBase
    {
        public Reader_luni(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["Name"] = reader.ReadString();
            value = props;
        }
    }
}
