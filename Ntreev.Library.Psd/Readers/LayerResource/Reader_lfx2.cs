using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("lfx2")]
    class Reader_lfx2 : LayerResourceBase
    {
        public Reader_lfx2(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            reader.ValidateInt32(0, "lfx2 Version");
            return new DescriptorStructure(reader, true);
        }
    }
}
