using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("luni")]
    class Reader_luni : LayerResourceBase
    {
        public Reader_luni(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props["Name"] = reader.ReadString();
            return props;
        }
    }
}
