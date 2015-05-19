using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lrFX")]
    class Reader_lrFX : ResourceReaderBase
    {
        public Reader_lrFX(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            value = new Properties();

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
        }
    }
}
