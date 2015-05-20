using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.ImageResources
{
    [ResourceID("1050", DisplayName = "Slices")]
    class Reader_SlicesInfo : ResourceReaderBase
    {
        public Reader_SlicesInfo(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();

            int version = reader.ReadInt32();
            if (version == 6)
            {
                var r1 = reader.ReadInt32();
                var r2 = reader.ReadInt32();
                var r3 = reader.ReadInt32();
                var r4 = reader.ReadInt32();
                string text = reader.ReadString();
                var count = reader.ReadInt32();

                List<IProperties> slices = new List<IProperties>(count);
                for (int i = 0; i < count; i++)
                {
                    slices.Add(ReadSliceInfo(reader));
                }
            }
            {
                var descriptor = new DescriptorStructure(reader) as IProperties;

                var items = descriptor["slices.Items[0]"] as object[];
                List<IProperties> slices = new List<IProperties>(items.Length);
                foreach (var item in items)
                {
                    slices.Add(ReadSliceInfo(item as IProperties));
                }
                props["Items"] = slices.ToArray();
            }

            value = props;
        }

        private static Properties ReadSliceInfo(PsdReader reader)
        {
            Properties props = new Properties();
            props["ID"] = reader.ReadInt32();
            props["GroupID"] = reader.ReadInt32();
            int origin = reader.ReadInt32();
            if (origin == 1)
            {
                int asso = reader.ReadInt32();
            }
            props["Name"] = reader.ReadString();
            int type = reader.ReadInt32();

            props["Left"] = reader.ReadInt32();
            props["Top"] = reader.ReadInt32();
            props["Right"] = reader.ReadInt32();
            props["Bottom"] = reader.ReadInt32();

            props["Url"] = reader.ReadString();
            props["Target"] = reader.ReadString();
            props["Message"] = reader.ReadString();
            props["AltTag"] = reader.ReadString();

            bool b = reader.ReadBoolean();

            string cellText = reader.ReadString();

            props["HorzAlign"] = reader.ReadInt32();
            props["VertAlign"] = reader.ReadInt32();

            props["Alpha"] = reader.ReadByte();
            props["Red"] = reader.ReadByte();
            props["Green"] = reader.ReadByte();
            props["Blue"] = reader.ReadByte();

            return props;
        }

        private static Properties ReadSliceInfo(IProperties properties)
        {
            Properties props = new Properties();
            props["ID"] = (int)properties["sliceID"];
            props["GroupID"] = (int)properties["groupID"];
            if (properties.Contains("Nm") == true)
                props["Name"] = properties["Nm"] as string;

            props["Left"] = (int)properties["bounds.Left"];
            props["Top"] = (int)properties["bounds.Top"];
            props["Right"] = (int)properties["bounds.Rght"];
            props["Bottom"] = (int)properties["bounds.Btom"];

            props["Url"] = properties["url"] as string;
            props["Target"] = properties["null"] as string;
            props["Message"] = properties["Msge"] as string;
            props["AltTag"] = properties["altTag"] as string;
            //this.horzAlign;
            //this.vertAlign;

            if (properties.Contains("bgColor") == true)
            {
                props["Alpha"] = (byte)(int)properties["bgColor.alpha"];
                props["Red"] = (byte)(int)properties["bgColor.Rd"];
                props["Green"] = (byte)(int)properties["bgColor.Grn"];
                props["Blue"] = (byte)(int)properties["bgColor.Bl"];
            }

            return props;
        }
    }
}
