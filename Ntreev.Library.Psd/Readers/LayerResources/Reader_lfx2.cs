using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lfx2")]
    class Reader_lfx2 : ResourceReaderBase
    {
        public Reader_lfx2(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            reader.ValidateInt32(0, "lfx2 Version");
            value = new DescriptorStructure(reader, true);
        }
    }
}
