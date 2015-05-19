using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.ImageResources
{
    [ResourceID("1032", DisplayName = "GridAndGuides")]
    class Reader_GridAndGuides : ResourceReaderBase
    {
        public Reader_GridAndGuides(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();

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

            value = props;
        }
    }
}
