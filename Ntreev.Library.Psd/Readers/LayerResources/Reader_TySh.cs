using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("TySh")]
    class Reader_TySh : ResourceReaderBase
    {
        public Reader_TySh(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties(7);

            reader.ValidateInt16(1, "Typetool Version");
            props["Transforms"] = reader.ReadDoubles(6);
            props["TextVersion"] = reader.ReadInt16();
            props["Text"] = new DescriptorStructure(reader);
            props["WarpVersion"] = reader.ReadInt16();
            props["Warp"] = new DescriptorStructure(reader);
            props["Bounds"] = reader.ReadDoubles(2);

            value = props;
        }
    }
}
