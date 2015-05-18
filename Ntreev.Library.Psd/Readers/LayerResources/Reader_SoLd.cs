using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("SoLd")]
    class Reader_SoLd : LayerResourceBase
    {
        public Reader_SoLd(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            reader.ValidateType("soLD", "SoLd ID");
            reader.ValidateInt32(4, "SoLd Version");
            value = new DescriptorStructure(reader, true);
        }
    }
}
