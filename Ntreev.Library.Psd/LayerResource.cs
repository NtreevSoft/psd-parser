using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd
{
    class LayerResource : Properties
    {
        internal void Load(PsdReader reader, int index)
        {
            string str = reader.ReadAscii(4);
            string str2 = reader.ReadAscii(4);

            int num = reader.ReadInt32();
            if (str != "8BIM")
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }

            long position = reader.Position;
            switch (str2)
            {
                case "lrFX":
                    {
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
                    break;
                case "SoLE":
                    {
                        Properties props = new Properties();
                        string id = reader.ReadAscii(4);
                        if (id != "soLD")
                            throw new InvalidFormatException();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("Descriptor", new DescriptorStructure(reader));
                        this.Add(str2, props);
                    }
                    break;
                case "SoLd":
                    {
                        Properties props = new Properties();
                        string id = reader.ReadAscii(4);
                        if (id != "soLD")
                            throw new InvalidFormatException();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("Descriptor", new DescriptorStructure(reader));
                        this.Add(str2, props);
                    }
                    break;
                case "lfx2":
                    {
                        Properties props = new Properties();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("Descriptor", new DescriptorStructure(reader));
                        this.Add(str2, props);
                    }
                    break;
                case "PlLd":
                    {
                        Properties props = new Properties();
                        string id = reader.ReadAscii(4);
                        if (id != "plcL")
                            throw new InvalidFormatException();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("UniqueID", reader.ReadPascalString(1));
                        props.Add("PageNumbers", reader.ReadInt32());
                        props.Add("Pages", reader.ReadInt32());
                        props.Add("AntiAlias", reader.ReadInt32());
                        props.Add("LayerType", reader.ReadInt32());
                        props.Add("Transformation", reader.ReadDoubles(8));
                        props.Add("WarpVersion ", reader.ReadInt32());
                        props.Add("WarpDescriptor", new DescriptorStructure(reader));

                        this.Add(str2, props);
                    }
                    break;
                case "lnsr":
                    {
                        this.Add(str2, reader.ReadAscii(4));
                    }
                    break;
                case "fxrp":
                    {
                        Properties props = new Properties();
                        props.Add("RefernecePoint", reader.ReadDoubles(2));
                        this.Add(str2, props);
                    }
                    break;
                case "TySh":
                    {
                        this.Add(str2, new TypeToolInfo(reader));
                    }
                    break;
                case "lyid":
                    {
                        Properties props = new Properties();
                        props.Add("ID", reader.ReadInt32());
                        this.Add(str2, props);
                    }
                    break;
                case "luni":
                    {
                        Properties props = new Properties();
                        props.Add("Name", reader.ReadString());
                        this.Add(str2, props);
                    }
                    break;
                case "lsct":
                    {
                        Properties props = new Properties();
                        props.Add("SectionType", (SectionType)reader.ReadInt32());
                        this.Add(str2, props);
                    }
                    break;
                case "iOpa":
                    {
                        Properties props = new Properties();
                        props.Add("Opacity", reader.ReadByte());
                        this.Add(str2, props);
                    }
                    break;
                case "shmd":
                    {
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

                        Properties props = new Properties();
                        props.Add("Items", dss);
                        this.Add(str2, props);
                    }
                    break;

            }

            if (reader.Position > position + num)
            {
                throw new InvalidFormatException();
            }

            reader.Position = position + num;

            if (this.ContainsKey(str2) == false)
            {
                this.Add(str2, "Unparsed");
            }
        }
    }
}

