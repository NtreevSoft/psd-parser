using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("fxrp")]
    class Reader_fxrp : LayerResourceBase
    {
        public Reader_fxrp(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props["RefernecePoint"] = reader.ReadDoubles(2);
            return props;
        }
    }
}
