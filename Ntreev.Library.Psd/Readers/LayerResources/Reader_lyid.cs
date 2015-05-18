using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lyid")]
    class Reader_lyid : LayerResourceBase
    {
        public Reader_lyid(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            props["ID"] = reader.ReadInt32();
            value = props;
        }
    }
}
