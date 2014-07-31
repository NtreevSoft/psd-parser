using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Structures
{
    class StructureEngineData : Properties
    {
        public StructureEngineData(PSDReader reader)
        {
            int count = reader.ReadInt32();

            for (int i = 0; i < 2; i++)
            {
                byte ch = reader.ReadByte();
                //assert ch == 10;
            }
            this.ReadProperties(reader, 0, this);
        }

        public StructureEngineData(PSDReader reader, int index)
        {
            this.ReadProperties(reader, 0, this);
        }

        //private void Trace(string name, object props)
        //{
        //    System.Diagnostics.Trace.Indent();
        //    if (props is Dictionary<string, object> == true)
        //    {
        //        System.Diagnostics.Trace.WriteLine(name);
        //        foreach (var item in props as Dictionary<string, object>)
        //        {
        //            this.Trace(item.Key, item.Value);
        //        }
        //    }
        //    else if (props is ArrayList == true)
        //    {
        //        ArrayList list = props as ArrayList;
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            //System.Diagnostics.Trace.Write(string.Format("[{0}] :", i));
        //            this.Trace(string.Format("{0}[{1}]", name, i), list[i]);
        //            //System.Diagnostics.Trace.WriteLine(string.Empty);
        //        }
        //    }
        //    else
        //    {
        //        System.Diagnostics.Trace.WriteLine(string.Format("{0} : {1}", name, props));
        //    }
        //    System.Diagnostics.Trace.Unindent();
        //}

        private void ReadProperties(PSDReader reader, int level, Properties props)
        {
            SkipTabs(reader, level);
            char c = (char)reader.ReadByte();
            if (c == ']')
            {
                return;
            }
            else if (c == '<')
            {
                SkipString(reader, "<");
            }
            SkipEndLine(reader);
            //Properties props = new Properties();
            while (true)
            {
                SkipTabs(reader, level);
                c = (char)reader.ReadByte();
                if (c == '>')
                {
                    SkipString(reader, ">");
                    return;
                }
                else
                {
                    //assert c == 9;
                    c = (char)reader.ReadByte();
                    //assert c == '/' : "unknown char: " + c + " on level: " + level;
                    string name = string.Empty;
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
                        Properties p = new Properties();
                        ReadProperties(reader, level + 1, p);
                        if(p.Count > 0)
                            props.Add(name, p);
                        SkipEndLine(reader);
                    }
                    else if (c == ' ')
                    {
                        object value = ReadValue(reader, level + 1);
                        props.Add(name, value);
                    }
                    else
                    {
                        //assert false;
                    }
                }
            }
        }

        private object ReadValue(PSDReader reader, int level)
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
                        Properties p = new Properties();
                        ReadProperties(reader, level, p);
                        SkipEndLine(reader);
                        if (p.Count == 0)
                        {
                            return list;
                        }
                        else
                        {
                            list.Add(p);
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

        private void SkipTabs(PSDReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                byte tabCh = reader.ReadByte();
                //assert tabCh == 9 : "must be tab: " + tabCh;
            }
        }

        private void SkipEndLine(PSDReader reader)
        {
            byte newLineCh = reader.ReadByte();
            //assert newLineCh == 10;
        }

        private void SkipString(PSDReader reader, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char streamCh = (char)reader.ReadByte();
                //assert streamCh == string.charAt(i) : "char " + streamCh + " mustBe " + string.charAt(i);
            }
        }
    }
}
