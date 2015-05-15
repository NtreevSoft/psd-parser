using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("lnsr")]
    class Reader_lnsr : LayerResourceBase
    {
        public Reader_lnsr(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props["Name"] = reader.ReadAscii(4);
            return props;
        }
    }
}
