using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lsct")]
    class Reader_lsct : ResourceReaderBase
    {
        public Reader_lsct(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            props["SectionType"] = (SectionType)reader.ReadInt32();
            value = props;
        }
    }
}
