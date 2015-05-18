using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lsct")]
    class Reader_lsct : LayerResourceBase
    {
        public Reader_lsct(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            props["SectionType"] = (SectionType)reader.ReadInt32();
            value = props;
        }
    }
}
