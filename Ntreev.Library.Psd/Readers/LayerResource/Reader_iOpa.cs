using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("iOpa")]
    class Reader_iOpa : LayerResourceBase
    {
        public Reader_iOpa(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props["Opacity"] = reader.ReadByte();
            return props;
        }
    }
}
