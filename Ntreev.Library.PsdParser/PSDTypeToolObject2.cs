using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    sealed class PSDTypeToolObject2 : Properties
    {
        public PSDTypeToolObject2(BinaryReader br)
        {
            this.Add("Version", EndianReverser.getInt16(br));
            this.Add("Transforms", EndianReverser.getDouble(br, 6));
            this.Add("TextVersion", EndianReverser.getInt16(br));
            this.Add("TextDescVersion", EndianReverser.getInt32(br));
            this.Add("Text", new DescriptorStructure(br));
            this.Add("WarpVersion", EndianReverser.getInt16(br));
            this.Add("WarpDescVersion", EndianReverser.getInt32(br));
            this.Add("Warp", new DescriptorStructure(br));
            this.Add("Bounds", EndianReverser.getDouble(br, 4));
        }

        public PSDRect area
        {
            get
            {
                PSDRect rect;
                double[] transforms = this["Transforms"] as double[];
                return new PSDRect { left = rect.right = (int)transforms[4], top = rect.bottom = (int)transforms[5] };
            }
        }

        //public string text
        //{
        //    get
        //    {
        //        IProperties props = this as IProperties;
        //        if (props.Contains("Text.Txt") == false)
        //            return null;
        //        return props["Text.Txt"] as string;
        //        //if (..descCount > 0)
        //        //{
        //        //    foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //        //    {
        //        //        if (description.ostype == "TEXT")
        //        //        {
        //        //            return (description.value as string);
        //        //        }
        //        //    }
        //        //}
        //        //return null;
        //    }
        //}

        //public Color textFillColor
        //{
        //    get
        //    {
        //        if (this.textDescriptor.ContainsProperty("EngineDict") == false)
        //            return Color.Empty;
        //        float a = (float)this.textDescriptor.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FillColor.Values[0]");
        //        float r = (float)this.textDescriptor.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FillColor.Values[1]");
        //        float g = (float)this.textDescriptor.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FillColor.Values[2]");
        //        float b = (float)this.textDescriptor.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FillColor.Values[3]");
        //        return Color.FromArgb((int)(a * 255.0f), (int)(r * 255.0f), (int)(g * 255.0f), (int)(b * 255.0f));
        //    }
        //}

        //public string textStrokeColor
        //{
        //    get
        //    {
        //        if (this.textDescriptor.descCount > 0)
        //        {
        //            foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //            {
        //                if (description.ostype == Decoder.RAW_DATA)
        //                {
        //                    Decoder_EngineData rawData = description.value as Decoder_EngineData;
        //                    object a = rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.StrokeColor.Values[0]");
        //                    object r = rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.StrokeColor.Values[1]");
        //                    object g = rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.StrokeColor.Values[2]");
        //                    object b = rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.StrokeColor.Values[3]");
        //                    return string.Format("{0},{1},{2},{3}", a, r, g, b);
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //}

        //public string fontName
        //{
        //    get
        //    {
        //        if (this.textDescriptor.descCount > 0)
        //        {
        //            foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //            {
        //                if (description.ostype == Decoder.RAW_DATA)
        //                {
        //                    Decoder_EngineData rawData = description.value as Decoder_EngineData;
        //                    return rawData.GetProperty("ResourceDict.FontSet[0].Name").ToString();
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //}

        //public float fontSize
        //{
        //    get
        //    {
        //        if (this.textDescriptor.descCount > 0)
        //        {
        //            foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //            {
        //                if (description.ostype == Decoder.RAW_DATA)
        //                {
        //                    Decoder_EngineData rawData = description.value as Decoder_EngineData;
        //                    return float.Parse(rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FontSize").ToString());
        //                }
        //            }
        //        }
        //        return 0.0f;
        //    }
        //}

        //public bool fontBold
        //{
        //    get
        //    {
        //        if (this.textDescriptor.descCount > 0)
        //        {
        //            foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //            {
        //                if (description.ostype == Decoder.RAW_DATA)
        //                {
        //                    Decoder_EngineData rawData = description.value as Decoder_EngineData;
        //                    return (bool)rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FauxBold");
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //}

        //public bool fontItalic
        //{
        //    get
        //    {
        //        if (this.textDescriptor.descCount > 0)
        //        {
        //            foreach (DescriptorStructure.Description description in this.textDescriptor.descs)
        //            {
        //                if (description.ostype == Decoder.RAW_DATA)
        //                {
        //                    Decoder_EngineData rawData = description.value as Decoder_EngineData;
        //                    return (bool)rawData.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FauxItalic");
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //}

        internal class BaseDecoder
        {
            public List<object> items = new List<object>();

            public BaseDecoder(BinaryReader br, string key)
            {
                int num = EndianReverser.getInt32(br);
                while (num-- > 0)
                {
                    PSDTypeToolObject2.Decoder.DecodeFunc func = PSDTypeToolObject2.Decoder.getDecoder(PSDUtil.readAscii(br, 4));
                    if (func != null)
                    {
                        object item = func(br, key);
                        if (item != null)
                        {
                            this.items.Add(item);
                        }
                    }
                }
            }
        }

        internal class Decoder
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

            private static object decode_ALIAS(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Alias(br);
            }

            private static object decode_BOOLEAN(BinaryReader br, string key)
            {
                return EndianReverser.getBoolean(br);
            }

            private static object decode_CLASS(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Class(br);
            }

            private static object decode_CLASS1(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Class(br);
            }

            private static object decode_CLASS2(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Class(br);
            }

            private static object decode_DESCRIPTOR(BinaryReader br, string key)
            {
                return new DescriptorStructure(br);
            }

            private static object decode_DOUBLE(BinaryReader br, string key)
            {
                return EndianReverser.getDouble(br);
            }

            private static object decode_ENUMERATED(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Enumerate(br);
            }

            private static object decode_ENUMERATED_REFERENCE(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_EnumerateReference(br);
            }

            private static object decode_GLOBAL_OBJECT(BinaryReader br, string key)
            {
                return new DescriptorStructure(br);
            }

            private static object decode_IDENTIFIER(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_UnknownOSType("Cannot decode IDENTIFIER data");
            }

            private static object decode_INDEX(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_UnknownOSType("Cannot decode INDEX data");
            }

            private static object decode_INTEGER(BinaryReader br, string key)
            {
                return EndianReverser.getInt32(br);
            }

            private static object decode_LIST(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_List(br, key);
            }

            private static object decode_NAME(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_UnknownOSType("Cannot decode NAME data");
            }

            private static object decode_OFFSET(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Offset(br);
            }

            private static object decode_PROPERTY(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Property(br);
            }

            private static object decode_RAW_DATA(BinaryReader br, string key)
            {
                if (key == "EngineData")
                {
                    return new PSDTypeToolObject2.Decoder_EngineData(br).properties;
                }
                return new PSDTypeToolObject2.Decoder_UnknownOSType("Cannot decode RAW_DATA data");
            }

            private static object decode_REFERENCE(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_Reference(br, key);
            }

            private static object decode_STRING(BinaryReader br, string key)
            {
                return readUnicodeString(br);
            }

            private static object decode_UNIT_FLOAT(BinaryReader br, string key)
            {
                return new PSDTypeToolObject2.Decoder_UnitFloat(br);
            }

            public static DecodeFunc getDecoder(string ostype)
            {
                switch (ostype)
                {
                    case "obj ":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_REFERENCE);

                    case "Objc":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_DESCRIPTOR);

                    case "VlLs":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_LIST);

                    case "doub":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_DOUBLE);

                    case "UntF":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_UNIT_FLOAT);

                    case "TEXT":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_STRING);

                    case "enum":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_ENUMERATED);

                    case "long":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_INTEGER);

                    case "bool":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_BOOLEAN);

                    case "GlbO":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_GLOBAL_OBJECT);

                    case "type":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_CLASS1);

                    case "GlbC":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_CLASS2);

                    case "alis":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_ALIAS);

                    case "tdta":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_RAW_DATA);

                    case "prop":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_PROPERTY);

                    case "Clss":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_CLASS);

                    case "Enmr":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_ENUMERATED);

                    case "rele":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_OFFSET);

                    case "Idnt":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_IDENTIFIER);

                    case "indx":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_INDEX);

                    case "name":
                        return new DecodeFunc(PSDTypeToolObject2.Decoder.decode_NAME);
                }
                return null;
            }

            public static string readStringOrKey(BinaryReader br)
            {
                int length = EndianReverser.getInt32(br);
                length = (length > 0) ? length : 4;
                return PSDUtil.readAscii(br, length);
            }

            public static string readUnicodeString(BinaryReader br)
            {
                int num = EndianReverser.getInt32(br);
                if (num == 0)
                {
                    return "";
                }
                byte[] bytes = br.ReadBytes(num * 2);
                for (int i = 0; i < num; i++)
                {
                    int index = i * 2;
                    byte num4 = bytes[index];
                    bytes[index] = bytes[index + 1];
                    bytes[index + 1] = num4;
                }
                return Encoding.Unicode.GetString(bytes);
            }

            public delegate object DecodeFunc(BinaryReader br, string key);
        }

        internal class Decoder_Alias
        {
            public string alias;

            public Decoder_Alias(BinaryReader br)
            {
                int length = EndianReverser.getInt32(br);
                this.alias = PSDUtil.readAscii(br, length);
            }
        }

        internal class Decoder_Class
        {
            public string classId;
            public string name;

            public Decoder_Class(BinaryReader br)
            {
                this.name = PSDTypeToolObject2.Decoder.readUnicodeString(br);
                this.classId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
            }
        }

        internal class Decoder_EngineData
        {
            public Properties properties;

            public Decoder_EngineData(BinaryReader br)
            {
                int count = EndianReverser.getInt32(br);

                for (int i = 0; i < 2; i++)
                {
                    byte ch = br.ReadByte();
                    //assert ch == 10;
                }
                this.properties = this.ReadProperties(br, 0);
                //this.Trace("Root", this.properties);
                //var ddd = this.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FillColor.Values[0]");
                //var ddd = this.GetProperty("EngineDict.StyleRun.RunArray[0].StyleSheet.StyleSheetData.FontSize");
                //var ddd1 = this.GetProperty("EngineDict.Editor.Text");
                //if (ddd1.ToString() == "팀관리\n")
                //{
                //    int qwer = 0;
                //}

            }

            private void Trace(string name, object props)
            {
                System.Diagnostics.Trace.Indent();
                if (props is Dictionary<string, object> == true)
                {
                    System.Diagnostics.Trace.WriteLine(name);
                    foreach (var item in props as Dictionary<string, object>)
                    {
                        this.Trace(item.Key, item.Value);
                    }
                }
                else if (props is ArrayList == true)
                {
                    ArrayList list = props as ArrayList;
                    for(int i=0 ; i<list.Count ; i++)
                    {
                        //System.Diagnostics.Trace.Write(string.Format("[{0}] :", i));
                        this.Trace(string.Format("{0}[{1}]", name, i), list[i]);
                        //System.Diagnostics.Trace.WriteLine(string.Empty);
                    }
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0} : {1}", name, props));
                }
                System.Diagnostics.Trace.Unindent();
            }

            public object GetProperty(string property)
            {
                string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

                object value = this.properties;

                foreach (var item in ss)
                {
                    if (value is ArrayList == true)
                    {
                        ArrayList arrayList = value as ArrayList;
                        value = arrayList[int.Parse(item)];
                    }
                    else if (value is Dictionary<string, object> == true)
                    {
                        Dictionary<string, object> props = value as Dictionary<string, object>;
                        if (props.ContainsKey(item) == false)
                            throw new ArgumentException(string.Format("Wrong property {0}", item));

                        value = props[item];
                    }

                }
                return value;
            }


            private Properties ReadProperties(BinaryReader reader, int level)
            {
                SkipTabs(reader, level);
                char c = (char)reader.ReadByte();
                if (c == ']')
                {
                    return null;
                }
                else if (c == '<')
                {
                    SkipString(reader, "<");
                }
                SkipEndLine(reader);
                Properties props = new Properties();
                while (true)
                {
                    SkipTabs(reader, level);
                    c = (char)reader.ReadByte();
                    if (c == '>')
                    {
                        SkipString(reader, ">");
                        return props;
                    }
                    else
                    {
                        //assert c == 9;
                        c = (char)reader.ReadByte();
                        //assert c == '/' : "unknown char: " + c + " on level: " + level;
                        string name = "";
                        while (true)
                        {
                            c = (char)reader.ReadByte();
                            if (c == ' ' || c == 10)
                            {
                                break;
                            }
                            name += c;
                        }
                        if (c == 10)
                        {
                            props.Add(name, ReadProperties(reader, level + 1));
                            SkipEndLine(reader);
                        }
                        else if (c == ' ')
                        {
                            props.Add(name, ReadValue(reader, level + 1));
                        }
                        else
                        {
                            //assert false;
                        }
                    }
                }
            }

            private object ReadValue(BinaryReader reader, int level)
            {
                char c = (char)reader.ReadByte();
                if (c == ']')
                {
                    return null;
                }
                else if (c == '(')
                {
                    // unicode string
                    string text = "";
                    int stringSignature = reader.ReadInt16() & 0xFFFF;
                    //assert stringSignature == 0xFEFF;
                    while (true)
                    {
                        byte b1 = reader.ReadByte();
                        if (b1 == ')')
                        {
                            SkipEndLine(reader);
                            return text;
                        }
                        byte b2 = reader.ReadByte();
                        if (b2 == '\\')
                        {
                            b2 = reader.ReadByte();
                        }
                        if (b2 == 13)
                        {
                            text += '\n';
                        }
                        else
                        {
                            text += (char)((b1 << 8) | b2);
                        }
                    }
                }
                else if (c == '[')
                {
                    ArrayList list = new ArrayList();
                    // array
                    c = (char)reader.ReadByte();
                    while (true)
                    {
                        if (c == ' ')
                        {
                            object val = ReadValue(reader, level);
                            if (val == null)
                            {
                                SkipEndLine(reader);
                                return list;
                            }
                            else
                            {
                                list.Add(val);
                            }
                        }
                        else if (c == 10)
                        {
                            object val = ReadProperties(reader, level);
                            SkipEndLine(reader);
                            if (val == null)
                            {
                                return list;
                            }
                            else
                            {
                                list.Add(val);
                            }
                        }
                        else
                        {
                            //assert false;
                        }
                    }
                }
                else
                {
                    string val = "";
                    do
                    {
                        val += c;
                        c = (char)reader.ReadByte();
                    }
                    while (c != 10 && c != ' ')
                        ;

                    {
                        int f;
                        if (int.TryParse(val, out f) == true)
                            return f;
                    }
                    {
                        float f;
                        if (float.TryParse(val, out f) == true)
                            return f;
                    }
                    {
                        bool f;
                        if (bool.TryParse(val, out f) == true)
                            return f;
                    }
                    
                    return val;
                    //if (val.Equals("true") || val.Equals("false"))
                    //{
                    //    return bool.Parse(val);
                    //}
                    //else
                    //{
                    //    return val;
                    //    //return false;
                    //    //return bool.Parse(val);
                    //}
                }
            }

            private void SkipTabs(BinaryReader reader, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    byte tabCh = reader.ReadByte();
                    //assert tabCh == 9 : "must be tab: " + tabCh;
                }
            }

            private void SkipEndLine(BinaryReader reader)
            {
                byte newLineCh = reader.ReadByte();
                //assert newLineCh == 10;
            }

            private void SkipString(BinaryReader reader, string text)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    char streamCh = (char)reader.ReadByte();
                    //assert streamCh == string.charAt(i) : "char " + streamCh + " mustBe " + string.charAt(i);
                }
            }
        }

        internal class Decoder_Enumerate
        {
            public string enumName;
            public string type;

            public Decoder_Enumerate(BinaryReader br)
            {
                this.type = PSDTypeToolObject2.Decoder.readStringOrKey(br);
                this.enumName = PSDTypeToolObject2.Decoder.readStringOrKey(br);
            }
        }

        internal class Decoder_EnumerateReference
        {
            public string classId;
            public string enumId;
            public string name;
            public string typeId;

            public Decoder_EnumerateReference(BinaryReader br)
            {
                this.name = PSDTypeToolObject2.Decoder.readUnicodeString(br);
                this.classId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
                this.typeId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
                this.enumId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
            }
        }

        internal class Decoder_List : PSDTypeToolObject2.BaseDecoder
        {
            public Decoder_List(BinaryReader br, string key)
                : base(br, key)
            {
            }
        }

        internal class Decoder_Offset
        {
            public string classId;
            public string name;
            public int offset;

            public Decoder_Offset(BinaryReader br)
            {
                this.name = PSDTypeToolObject2.Decoder.readUnicodeString(br);
                this.classId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
                this.offset = EndianReverser.getInt32(br);
            }
        }

        internal class Decoder_Property
        {
            public string classId;
            public string keyId;
            public string name;

            public Decoder_Property(BinaryReader br)
            {
                this.name = PSDTypeToolObject2.Decoder.readUnicodeString(br);
                this.classId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
                this.keyId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
            }
        }

        internal class Decoder_Reference : PSDTypeToolObject2.BaseDecoder
        {
            public Decoder_Reference(BinaryReader br, string key)
                : base(br, key)
            {
            }
        }

        internal class Decoder_UnitFloat
        {
            private const string ANGLE = "#Ang";
            private const string DENSITY = "#Rsl";
            private const string DISTANCE = "#Rlt";
            public string key;
            private const string NONE = "#Nne";
            private const string PERCENT = "#Prc";
            private const string PIXELS = "#Pxl";
            public double value;

            public Decoder_UnitFloat(BinaryReader br)
            {
                this.key = PSDUtil.readAscii(br, 4);
                this.value = EndianReverser.getDouble(br);
            }
        }

        internal class Decoder_UnknownOSType
        {
            public string message;

            public Decoder_UnknownOSType(string message)
            {
                this.message = message;
            }
        }

        
    }
}

