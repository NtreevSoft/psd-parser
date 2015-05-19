using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("iOpa")]
    class Reader_iOpa : ResourceReaderBase
    {
        public Reader_iOpa(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["Opacity"] = reader.ReadByte();
            value = props;
        }
    }
}
