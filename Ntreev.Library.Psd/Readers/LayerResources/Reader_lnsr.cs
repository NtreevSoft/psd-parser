using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lnsr")]
    class Reader_lnsr : LayerResourceBase
    {
        public Reader_lnsr(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            props["Name"] = reader.ReadAscii(4);
            value = props;
        }
    }
}
