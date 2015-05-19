using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("fxrp")]
    class Reader_fxrp : ResourceReaderBase
    {
        public Reader_fxrp(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["RefernecePoint"] = reader.ReadDoubles(2);
            value = props;
        }
    }
}
