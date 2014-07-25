using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderFactory
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
            return new DecoderAlias(reader);
        }

        private static object decode_BOOLEAN(PSDReader reader, string key)
        {
            return reader.ReadBoolean();
        }

        private static object decode_CLASS(PSDReader reader, string key)
        {
            return new DecoderClass(reader);
        }

        private static object decode_CLASS1(PSDReader reader, string key)
        {
            return new DecoderClass(reader);
        }

        private static object decode_CLASS2(PSDReader reader, string key)
        {
            return new DecoderClass(reader);
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
            return new DecoderEnumerate(reader);
        }

        private static object decode_ENUMERATED_REFERENCE(PSDReader reader, string key)
        {
            return new DecoderEnumerateReference(reader);
        }

        private static object decode_GLOBAL_OBJECT(PSDReader reader, string key)
        {
            return new DescriptorStructure(reader);
        }

        private static object decode_IDENTIFIER(PSDReader reader, string key)
        {
            return new DecoderUnknownOSType("Cannot decode IDENTIFIER data");
        }

        private static object decode_INDEX(PSDReader reader, string key)
        {
            return new DecoderUnknownOSType("Cannot decode INDEX data");
        }

        private static object decode_INTEGER(PSDReader reader, string key)
        {
            return reader.ReadInt32();
        }

        private static object decode_LIST(PSDReader reader, string key)
        {
            return new DecoderList(reader, key);
        }

        private static object decode_NAME(PSDReader reader, string key)
        {
            return new DecoderUnknownOSType("Cannot decode NAME data");
        }

        private static object decode_OFFSET(PSDReader reader, string key)
        {
            return new DecoderOffset(reader);
        }

        private static object decode_PROPERTY(PSDReader reader, string key)
        {
            return new DecoderProperty(reader);
        }

        private static object decode_RAW_DATA(PSDReader reader, string key)
        {
            if (key == "EngineData")
            {
                return new DecoderEngineData(reader);
            }
            return new DecoderUnknownOSType("Cannot decode RAW_DATA data");
        }

        private static object decode_REFERENCE(PSDReader reader, string key)
        {
            return new DecoderReference(reader, key);
        }

        private static object decode_STRING(PSDReader reader, string key)
        {
            return reader.ReadUnicodeString2();
        }

        private static object decode_UNIT_FLOAT(PSDReader reader, string key)
        {
            return new DecoderUnitFloat(reader);
        }

        public static DecodeFunc GetDecoder(string ostype)
        {
            switch (ostype)
            {
                case "obj ":
                    return new DecodeFunc(DecoderFactory.decode_REFERENCE);

                case "Objc":
                    return new DecodeFunc(DecoderFactory.decode_DESCRIPTOR);

                case "VlLs":
                    return new DecodeFunc(DecoderFactory.decode_LIST);

                case "doub":
                    return new DecodeFunc(DecoderFactory.decode_DOUBLE);

                case "UntF":
                    return new DecodeFunc(DecoderFactory.decode_UNIT_FLOAT);

                case "TEXT":
                    return new DecodeFunc(DecoderFactory.decode_STRING);

                case "enum":
                    return new DecodeFunc(DecoderFactory.decode_ENUMERATED);

                case "long":
                    return new DecodeFunc(DecoderFactory.decode_INTEGER);

                case "bool":
                    return new DecodeFunc(DecoderFactory.decode_BOOLEAN);

                case "GlbO":
                    return new DecodeFunc(DecoderFactory.decode_GLOBAL_OBJECT);

                case "type":
                    return new DecodeFunc(DecoderFactory.decode_CLASS1);

                case "GlbC":
                    return new DecodeFunc(DecoderFactory.decode_CLASS2);

                case "alis":
                    return new DecodeFunc(DecoderFactory.decode_ALIAS);

                case "tdta":
                    return new DecodeFunc(DecoderFactory.decode_RAW_DATA);

                case "prop":
                    return new DecodeFunc(DecoderFactory.decode_PROPERTY);

                case "Clss":
                    return new DecodeFunc(DecoderFactory.decode_CLASS);

                case "Enmr":
                    return new DecodeFunc(DecoderFactory.decode_ENUMERATED);

                case "rele":
                    return new DecodeFunc(DecoderFactory.decode_OFFSET);

                case "Idnt":
                    return new DecodeFunc(DecoderFactory.decode_IDENTIFIER);

                case "indx":
                    return new DecodeFunc(DecoderFactory.decode_INDEX);

                case "name":
                    return new DecodeFunc(DecoderFactory.decode_NAME);
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
