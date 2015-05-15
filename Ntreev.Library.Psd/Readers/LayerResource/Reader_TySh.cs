using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("TySh")]
    class Reader_TySh : LayerResourceBase
    {
        public Reader_TySh(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>(7);

            reader.ValidateInt16(1, "Typetool Version");
            props["Transforms"] = reader.ReadDoubles(6);
            props["TextVersion"] = reader.ReadInt16();
            props["Text"] = new DescriptorStructure(reader);
            props["WarpVersion"] = reader.ReadInt16();
            props["Warp"] = new DescriptorStructure(reader);
            props["Bounds"] = reader.ReadDoubles(2);

            return props;
        }
    }
}
