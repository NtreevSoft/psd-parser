using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    [ResourceID("lrFX")]
    class Reader_lrFX : LayerResourceBase
    {
        public Reader_lrFX(PsdReader reader)
            : base(reader)
        {

        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();

            short version = reader.ReadInt16();
            int count = reader.ReadInt16();

            for (int i = 0; i < count; i++)
            {
                string _8bim = reader.ReadAscii(4);
                string effectType = reader.ReadAscii(4);
                int size = reader.ReadInt32();
                long p = reader.Position;

                switch (effectType)
                {
                    case "dsdw":
                        {
                            //ShadowInfo.Parse(reader);
                        }
                        break;
                    case "sofi":
                        {
                            //this.solidFillInfo = SolidFillInfo.Parse(reader);
                        }
                        break;
                }

                reader.Position = p + size;
            }

            return props;
        }
    }
}
