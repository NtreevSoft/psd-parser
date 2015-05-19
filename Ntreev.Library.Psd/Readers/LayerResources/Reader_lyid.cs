using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lyid")]
    class Reader_lyid : ResourceReaderBase
    {
        public Reader_lyid(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["ID"] = reader.ReadInt32();
            value = props;
        }
    }
}
