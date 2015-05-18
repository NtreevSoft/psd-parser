using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("iOpa")]
    class Reader_iOpa : LayerResourceBase
    {
        public Reader_iOpa(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            props["Opacity"] = reader.ReadByte();
            value = props;
        }
    }
}
