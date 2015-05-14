using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class ImageResourceReader : ReadablePropertiesHost
    {
        //private readonly PsdReader reader;
        //private readonly long length;
        //private readonly long position;

        public ImageResourceReader(PsdReader reader)
            : base(reader, () => reader.ReadInt32())
        {
            //this.reader = reader;
            //this.length = reader.ReadInt32();
            //this.position = reader.Position;

            //reader.Position += length;
        }

        //private void Read(PsdReader reader)
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

        //        if (resourceSize == 0)
        //            continue;

        //        IProperties properties = null;
        //        switch (imageResourceID)
        //        {
        //            case 0x0421:
        //                properties = new VersionInfoReader(reader);
        //                break;
        //            case 0x0408:
        //                properties = new GridAndGuidesReader(reader);
        //                break;
        //            case 0x3ed:
        //                properties = new ResolutionInfoReader(reader);
        //                break;
        //            case 0x041a:
        //                properties = new SliceInfoReader(reader);
        //                break;
        //        }

        //        reader.Position += resourceSize;

        //        if (properties != null)
        //        {
        //            this.Add(imageResourceID.ToString(), properties);
        //        }

        //        if ((resourceSize % 2) != 0)
        //        {
        //            reader.Position += 1L;
        //        }
        //    }
        //}

        protected override IDictionary<string, object> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            //int size = reader.ReadInt32();
            long position = reader.Position;

            while ((reader.Position - position) < this.Length)
            {
                string signature = reader.ReadAscii(4);
                if (signature != "8BIM")
                {
                    continue;
                }
                short imageResourceID = reader.ReadInt16();
                string name = reader.ReadPascalString(2);
                int resourceSize = reader.ReadInt32();

                if (resourceSize == 0)
                    continue;

                IProperties properties = null;
                switch (imageResourceID)
                {
                    case 0x0421:
                        properties = new VersionInfoReader(reader);
                        break;
                    case 0x0408:
                        properties = new GridAndGuidesReader(reader);
                        break;
                    case 0x3ed:
                        properties = new ResolutionInfoReader(reader);
                        break;
                    case 0x041a:
                        properties = new SliceInfoReader(reader);
                        break;
                }

                reader.Position += resourceSize;

                if (properties != null)
                {
                    props[imageResourceID.ToString()] =  properties;
                }

                if ((resourceSize % 2) != 0)
                {
                    reader.Position += 1L;
                }
            }
            return props;
        }
    }
}
