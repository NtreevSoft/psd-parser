using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("lsct")]
    class Reader_lsct : LayerResourceBase
    {
        public Reader_lsct(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props["SectionType"] = (SectionType)reader.ReadInt32();
            return props;
        }
    }
}
