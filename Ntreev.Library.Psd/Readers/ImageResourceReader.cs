using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageResourceReader : Properties
    {
        //public ImageResourceReader(PsdReader reader)
        //{
        //    int size = reader.ReadInt32();
        //    long position = reader.Position;
        //    while ((reader.Position - position) < size)
        //    {
        //        string signature = reader.ReadAscii(4);
        //        if (signature != "8BIM")
        //        {
        //            continue;
        //        }
        //        short imageResourceID = reader.ReadInt16();
        //        string name = reader.ReadPascalString(2);
        //        int resourceSize = reader.ReadInt32();
        //        if (resourceSize > 0)
        //        {
        //            switch (imageResourceID)
        //            {
        //                case 0x0408:
        //                    this.gridAndGuidesInfo = new GridAndGuidesInfo(reader);
        //                    break;
        //                case 0x3ed:
        //                    this.resolutionInfo = new ResolutionInfo(reader);
        //                    break;
        //                case 0x041a:
        //                    {
        //                        long ppp = reader.Position;
        //                        var version = reader.ReadInt32();
        //                        if (version == 6)
        //                        {
        //                            var r1 = reader.ReadInt32();
        //                            var r2 = reader.ReadInt32();
        //                            var r3 = reader.ReadInt32();
        //                            var r4 = reader.ReadInt32();
        //                            string text = reader.ReadString();
        //                            var count = reader.ReadInt32();

        //                            List<SliceInfo> slices = new List<SliceInfo>(count);
        //                            for (int i = 0; i < count; i++)
        //                            {
        //                                slices.Add(new SliceInfo(reader));
        //                            }

        //                            this.slices = slices.ToArray();

        //                            int descVer = reader.ReadInt32();
        //                            this.props.Add(imageResourceID.ToString(), new DescriptorStructure(reader));
        //                        }
        //                        else
        //                        {
        //                            int descVer = reader.ReadInt32();
        //                            var v = new DescriptorStructure(reader) as IProperties;
        //                            this.props.Add(imageResourceID.ToString(), v);

        //                            var items = v["slices.Items[0]"] as object[];
        //                            List<SliceInfo> slices = new List<SliceInfo>(items.Length);
        //                            foreach (var item in items)
        //                            {
        //                                slices.Add(new SliceInfo(item as IProperties));
        //                            }
        //                            this.slices = slices.ToArray();
        //                        }
        //                    }
        //                    break;

        //                default:
        //                    {
        //                        reader.Position += resourceSize;
        //                        this.props.Add(imageResourceID.ToString(), null);
        //                        break;
        //                    }
        //            }
        //            if ((resourceSize % 2) != 0)
        //            {
        //                reader.Position += 1L;
        //            }
        //        }
        //    }
        //}
    }
}
