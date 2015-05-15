using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.ImageResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageResourceReader : ReadablePropertiesHost
    {
        private static readonly IDictionary<string, Type> readers = ReaderCollector.Collect(typeof(ImageResourceBase));
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

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
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
                string resourceID = reader.ReadInt16().ToString();
                string name = reader.ReadPascalString(2);
                int resourceSize = reader.ReadInt32();

                if (resourceSize == 0)
                    continue;

                Type readerType = null;
                if (readers.ContainsKey(resourceID) == true)
                {
                    readerType = readers[resourceID];
                }

                if (readerType != null)
                {
                    string displayName = ReaderCollector.GetDisplayName(readerType);
                    object instance = TypeDescriptor.CreateInstance(null, readerType, new Type[] { typeof(PsdReader), }, new object[] { reader, });
                    props[displayName] = instance;
                }

                //IProperties properties = null;
                //switch (imageResourceID)
                //{
                //    case 0x0421:
                //        properties = new VersionInfoReader(reader);
                //        break;
                //    case 0x0408:
                //        properties = new GridAndGuidesReader(reader);
                //        break;
                //    case 0x3ed:
                //        properties = new ResolutionInfoReader(reader);
                //        break;
                //    case 0x041a:
                //        properties = new SliceInfoReader(reader);
                //        break;
                //}

                reader.Position += resourceSize;

                //if (properties != null)
                //{
                //    props[imageResourceID.ToString()] =  properties;
                //}

                if ((resourceSize % 2) != 0)
                {
                    reader.Position += 1L;
                }
            }
            return props;
        }
    }
}
