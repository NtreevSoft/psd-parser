using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("luni")]
    class Reader_luni : LayerResourceBase
    {
        public Reader_luni(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            props["Name"] = reader.ReadString();
            value = props;
        }
    }
}
