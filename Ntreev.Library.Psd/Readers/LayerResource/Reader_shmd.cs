using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("shmd")]
    class Reader_shmd : LayerResourceBase
    {
        public Reader_shmd(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();

            int count = reader.ReadInt32();

            List<DescriptorStructure> dss = new List<DescriptorStructure>();

            for (int i = 0; i < count; i++)
            {
                string s = reader.ReadAscii(4);
                string k = reader.ReadAscii(4);
                var c = reader.ReadByte();
                var p = reader.ReadBytes(3);
                var l = reader.ReadInt32();
                var p2 = reader.Position;
                var ds = new DescriptorStructure(reader);
                dss.Add(ds);
                reader.Position = p2 + l;
            }

            props["Items"] = dss;

            return props;
        }
    }
}
