using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lnsr")]
    class Reader_lnsr : ResourceReaderBase
    {
        public Reader_lnsr(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["Name"] = reader.ReadAscii(4);
            value = props;
        }
    }
}
