using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class GridAndGuidesReader : ReadablePropertiesHost
    {
        public GridAndGuidesReader(PsdReader reader)
            : base(reader)
        {
            
        }

        protected override IDictionary<string, object> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();

            int version = reader.ReadInt32();

            if (version != 1)
                throw new InvalidFormatException();

            props["HorizontalGrid"] = reader.ReadInt32();
            props["VerticalGrid"] = reader.ReadInt32();

            int guideCount = reader.ReadInt32();

            List<int> hg = new List<int>();
            List<int> vg = new List<int>();

            for (int i = 0; i < guideCount; i++)
            {
                int n = reader.ReadInt32();
                byte t = reader.ReadByte();

                if (t == 0)
                    vg.Add(n);
                else
                    hg.Add(n);
            }

            props["HorizontalGuides"] = hg.ToArray();
            props["VerticalGuides"] = vg.ToArray();

            return props;
        }
    }
}
