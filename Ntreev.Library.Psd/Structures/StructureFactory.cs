using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureFactory
    {
        public const string ALIAS = "alis";
        public const string BOOLEAN = "bool";
        public const string CLASS = "Clss";
        public const string CLASS1 = "type";
        public const string CLASS2 = "GlbC";
        public const string DESCRIPTOR = "Objc";
        public const string DOUBLE = "doub";
        public const string ENUMERATED = "enum";
        public const string ENUMERATED_REFERENCE = "Enmr";
        public const string GLOBAL_OBJECT = "GlbO";
        public const string IDENTIFIER = "Idnt";
        public const string INDEX = "indx";
        public const string INTEGER = "long";
        public const string LIST = "VlLs";
        public const string NAME = "name";
        public const string OFFSET = "rele";
        public const string PROPERTY = "prop";
        public const string RAW_DATA = "tdta";
        public const string REFERENCE = "obj ";
        public const string STRING = "TEXT";
        public const string UNIT_FLOAT = "UntF";

        private static object decode_ALIAS(PSDReader reader, string key)
        {
            return new StructureAlias(reader);
        }

        private static object decode_BOOLEAN(PSDReader reader, string key)
        {
            return reader.ReadBoolean();
        }

        private static object decode_CLASS(PSDReader reader, string key)
        {
            return new StructureClass(reader);
        }

        private static object decode_CLASS1(PSDReader reader, string key)
        {
            return new StructureClass(reader);
        }

        private static object decode_CLASS2(PSDReader reader, string key)
        {
            return new StructureClass(reader);
        }

        private static object decode_DESCRIPTOR(PSDReader reader, string key)
        {
            return new DescriptorStructure(reader);
        }

        private static object decode_DOUBLE(PSDReader reader, string key)
        {
            return reader.ReadDouble();
        }

        private static object decode_ENUMERATED(PSDReader reader, string key)
        {
            return new StructureEnumerate(reader);
        }

        private static object decode_ENUMERATED_REFERENCE(PSDReader reader, string key)
        {
            return new StructureEnumerateReference(reader);
        }

        private static object decode_GLOBAL_OBJECT(PSDReader reader, string key)
        {
            return new DescriptorStructure(reader);
        }

        private static object decode_IDENTIFIER(PSDReader reader, string key)
        {
            return new StructureUnknownOSType("Cannot decode IDENTIFIER data");
        }

        private static object decode_INDEX(PSDReader reader, string key)
        {
            return new StructureUnknownOSType("Cannot decode INDEX data");
        }

        private static object decode_INTEGER(PSDReader reader, string key)
        {
            return reader.ReadInt32();
        }

        private static object decode_LIST(PSDReader reader, string key)
        {
            return new StructureList(reader, key);
        }

        private static object decode_NAME(PSDReader reader, string key)
        {
            return new StructureUnknownOSType("Cannot decode NAME data");
        }

        private static object docode_OBJECTARRAY(PSDReader reader, string key)
        {
            return new StructureObjectArray(reader, key);
        }

        

        private static object decode_OFFSET(PSDReader reader, string key)
        {
            return new StructureOffset(reader);
        }

        private static object decode_PROPERTY(PSDReader reader, string key)
        {
            return new StructureProperty(reader);
        }

        private static object decode_RAW_DATA(PSDReader reader, string key)
        {
            if (key == "EngineData")
            {
                return new StructureEngineData(reader);
            }
            return new StructureUnknownOSType("Cannot decode RAW_DATA data");
        }

        private static object decode_REFERENCE(PSDReader reader, string key)
        {
            return new StructureReference(reader, key);
        }

        private static object decode_STRING(PSDReader reader, string key)
        {
            return reader.ReadUnicodeString2();
        }

        private static object decode_UNIT_FLOAT(PSDReader reader, string key)
        {
            return new StructureUnitFloat(reader);
        }

        public static DecodeFunc GetDecoder(string ostype)
        {
            switch (ostype)
            {
                case "obj ":
                    return new DecodeFunc(StructureFactory.decode_REFERENCE);

                case "Objc":
                    return new DecodeFunc(StructureFactory.decode_DESCRIPTOR);

                case "VlLs":
                    return new DecodeFunc(StructureFactory.decode_LIST);

                case "doub":
                    return new DecodeFunc(StructureFactory.decode_DOUBLE);

                case "UntF":
                    return new DecodeFunc(StructureFactory.decode_UNIT_FLOAT);

                case "TEXT":
                    return new DecodeFunc(StructureFactory.decode_STRING);

                case "enum":
                    return new DecodeFunc(StructureFactory.decode_ENUMERATED);

                case "long":
                    return new DecodeFunc(StructureFactory.decode_INTEGER);

                case "bool":
                    return new DecodeFunc(StructureFactory.decode_BOOLEAN);

                case "GlbO":
                    return new DecodeFunc(StructureFactory.decode_GLOBAL_OBJECT);

                case "type":
                    return new DecodeFunc(StructureFactory.decode_CLASS1);

                case "GlbC":
                    return new DecodeFunc(StructureFactory.decode_CLASS2);

                case "alis":
                    return new DecodeFunc(StructureFactory.decode_ALIAS);

                case "tdta":
                    return new DecodeFunc(StructureFactory.decode_RAW_DATA);

                case "prop":
                    return new DecodeFunc(StructureFactory.decode_PROPERTY);

                case "Clss":
                    return new DecodeFunc(StructureFactory.decode_CLASS);

                case "Enmr":
                    return new DecodeFunc(StructureFactory.decode_ENUMERATED);

                case "rele":
                    return new DecodeFunc(StructureFactory.decode_OFFSET);

                case "Idnt":
                    return new DecodeFunc(StructureFactory.decode_IDENTIFIER);

                case "indx":
                    return new DecodeFunc(StructureFactory.decode_INDEX);

                case "name":
                    return new DecodeFunc(StructureFactory.decode_NAME);

                case "ObAr":
                    return new DecodeFunc(StructureFactory.docode_OBJECTARRAY);
            }
            return null;
        }

        //public static string readStringOrKey(PSDReader reader)
        //{
        //    int length = reader.ReadInt32();
        //    length = (length > 0) ? length : 4;
        //    return reader.ReadAscii(length);
        //}

        //public static string readUnicodeString(PSDReader reader)
        //{
        //    int num = reader.ReadInt32();
        //    if (num == 0)
        //    {
        //        return "";
        //    }
        //    byte[] bytes = reader.ReadBytes(num * 2);
        //    for (int i = 0; i < num; i++)
        //    {
        //        int index = i * 2;
        //        byte num4 = bytes[index];
        //        bytes[index] = bytes[index + 1];
        //        bytes[index + 1] = num4;
        //    }
        //    return Encoding.Unicode.GetString(bytes);
        //}

        public delegate object DecodeFunc(PSDReader reader, string key);
    }
}
