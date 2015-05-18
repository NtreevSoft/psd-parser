using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("PlLd")]
    class Reader_PlLd : LayerResourceBase
    {
        public Reader_PlLd(PsdReader reader)
            : base(reader)
        {

        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();

            reader.ValidateType("plcL", "LayerResource PlLd");
            props["Version"] = reader.ReadInt32();
            props["UniqueID"] = reader.ReadPascalString(1);
            props["PageNumbers"] = reader.ReadInt32();
            props["Pages"] = reader.ReadInt32();
            props["AntiAlias"] = reader.ReadInt32();
            props["LayerType"] = reader.ReadInt32();
            props["Transformation"] = reader.ReadDoubles(8);
            reader.ValidateInt32(0, "WarpVersion");
            props["Warp"] = new DescriptorStructure(reader);

            value = props;
        }
    }
}
