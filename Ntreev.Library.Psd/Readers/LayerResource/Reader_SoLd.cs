using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("SoLd")]
    class Reader_SoLd : LayerResourceBase
    {
        public Reader_SoLd(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            reader.ValidateType("soLD", "SoLd ID");
            reader.ValidateInt32(4, "SoLd Version");
            return new DescriptorStructure(reader, true);
        }
    }
}
