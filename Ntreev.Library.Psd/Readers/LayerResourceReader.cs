using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.LayerResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ntreev.Library.Psd.Readers
{
    class LayerResourceReader : ReadableLazyProperties
    {
        private readonly long end;

        private static readonly IDictionary<string, Type> readers = ReaderCollector.Collect(typeof(LayerResourceBase));

        public LayerResourceReader(PsdReader reader, long end)
            : base(reader)
        {
            this.end = end;
            reader.Position = end;
        }

        private void Read(PsdReader reader, IDictionary<string, object> properties)
        {
            reader.ValidateSignature();
            string resourceID = reader.ReadAscii(4);

            int length = reader.ReadInt32();

            long position = reader.Position;

            Type readerType = null;
            if (readers.ContainsKey(resourceID) == true)
            {
                readerType = readers[resourceID];
            }

            if (readerType != null)
            {
                properties.Add(resourceID, TypeDescriptor.CreateInstance(null, readerType, new Type[] { typeof(PsdReader), }, new object[] { reader, }));
            }
            IProperties p = null;
            //switch (resourceID)
            //{
            //    case "lrFX":
            //        {
            //            p = new ReaderlrFX(reader);
            //        }
            //        break;
            //    case "SoLE":
            //        {
            //            Properties props = new Properties();
            //            string id = reader.ReadAscii(4);
            //            if (id != "soLD")
            //                throw new InvalidFormatException();
            //            props.Add("Descriptor", new DescriptorStructure(reader, true));
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "SoLd":
            //        {
            //            Properties props = new Properties();
            //            string id = reader.ReadAscii(4);
            //            if (id != "soLD")
            //                throw new InvalidFormatException();
            //            props.Add("Descriptor", new DescriptorStructure(reader, true));
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "lfx2":
            //        {
            //            Properties props = new Properties();
            //            props.Add("Descriptor", new DescriptorStructure(reader, true));
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "PlLd":
            //        {
            //            Properties props = new Properties();
            //            string id = reader.ReadAscii(4);
            //            if (id != "plcL")
            //                throw new InvalidFormatException();
            //            props.Add("Version", reader.ReadInt32());
            //            props.Add("UniqueID", reader.ReadPascalString(1));
            //            props.Add("PageNumbers", reader.ReadInt32());
            //            props.Add("Pages", reader.ReadInt32());
            //            props.Add("AntiAlias", reader.ReadInt32());
            //            props.Add("LayerType", reader.ReadInt32());
            //            props.Add("Transformation", reader.ReadDoubles(8));
            //            props.Add("WarpDescriptor", new DescriptorStructure(reader, true));

            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "lnsr":
            //        {
            //            this.Add(resourceID, reader.ReadAscii(4));
            //        }
            //        break;
            //    case "fxrp":
            //        {
            //            Properties props = new Properties();
            //            props.Add("RefernecePoint", reader.ReadDoubles(2));
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "TySh":
            //        {
            //            this.Add(resourceID, new TypeToolInfo(reader));
            //        }
            //        break;
            //    case "lyid":
            //        {
            //            Properties props = new Properties();
            //            props.Add("ID", reader.ReadInt32());
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "luni":
            //        {
            //            Properties props = new Properties();
            //            props.Add("Name", reader.ReadString());
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "lsct":
            //        {
            //            Properties props = new Properties();
            //            props.Add("SectionType", (SectionType)reader.ReadInt32());
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "iOpa":
            //        {
            //            Properties props = new Properties();
            //            props.Add("Opacity", reader.ReadByte());
            //            this.Add(resourceID, props);
            //        }
            //        break;
            //    case "shmd":
            //        {
            //            int count = reader.ReadInt32();

            //            List<DescriptorStructure> dss = new List<DescriptorStructure>();

            //            for (int i = 0; i < count; i++)
            //            {
            //                string s = reader.ReadAscii(4);
            //                string k = reader.ReadAscii(4);
            //                var c = reader.ReadByte();
            //                var p = reader.ReadBytes(3);
            //                var l = reader.ReadInt32();
            //                var p2 = reader.Position;
            //                var ds = new DescriptorStructure(reader);
            //                dss.Add(ds);
            //                reader.Position = p2 + l;
            //            }

            //            Properties props = new Properties();
            //            props.Add("Items", dss);
            //            this.Add(resourceID, props);
            //        }
            //        break;

            //}

            //properties.Add(resourceID, p);

            if (reader.Position > position + length)
            {
                throw new InvalidFormatException();
            }

            reader.Position = position + length;

            //if (this.Contains(resourceID) == false)
            //{
            //    //this.Add(resourceID, "Unparsed");
            //}
        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            while (reader.Position < this.end)
            {
                this.Read(reader, props);
            }
            return props;
        }
    }
}

