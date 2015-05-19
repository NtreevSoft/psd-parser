using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("SoLd")]
    class Reader_SoLd : ResourceReaderBase
    {
        public Reader_SoLd(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            reader.ValidateType("soLD", "SoLd ID");
            reader.ValidateInt32(4, "SoLd Version");
            value = new DescriptorStructure(reader, true);
        }
    }
}
