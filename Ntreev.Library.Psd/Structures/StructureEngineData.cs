using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureEngineData : Properties
    {
        public StructureEngineData(PSDReader reader)
        {
            int length = reader.ReadInt32();

            //string ddd = string.Empty;
            //for (int i = 0; i < length; i++)
            //{
            //    char d = reader.ReadChar();
            //    if (d != '\0')
            //        ddd += d;
            //}
            
            reader.Skip('\n', 2);
            this.ReadProperties(reader, 0, this);
        }

        private void ReadProperties(PSDReader reader, int level, Properties props)
        {
            reader.Skip('\t', level);
            char c = reader.ReadChar();
            if (c == ']')
            {
                return;
            }
            else if (c == '<')
            {
                reader.Skip('<');
            }
            reader.Skip('\n');
            //Properties props = new Properties();
            while (true)
            {
                reader.Skip('\t', level);
                c = reader.ReadChar();
                if (c == '>')
                {
                    reader.Skip('>');
                    return;
                }
                else
                {
                    //assert c == 9;
                    c = reader.ReadChar();
                    //assert c == '/' : "unknown char: " + c + " on level: " + level;
                    string name = string.Empty;
                    while (true)
                    {
                        c = reader.ReadChar();
                        if (c == ' ' || c == 10)
                        {
                            break;
                        }
                        name += c;
                    }
                    if (c == 10)
                    {
                        Properties p = new Properties();
                        this.ReadProperties(reader, level + 1, p);
                        if(p.Count > 0)
                            props.Add(name, p);
                        reader.Skip('\n');
                    }
                    else if (c == ' ')
                    {
                        object value = this.ReadValue(reader, level + 1);
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
            char c = reader.ReadChar();
            if (c == ']')
            {
                return null;
            }
            else if (c == '(')
            {
                // unicode string
                string text = string.Empty;
                int stringSignature = reader.ReadInt16() & 0xFFFF;
                //assert stringSignature == 0xFEFF;
                while (true)
                {
                    char b1 = reader.ReadChar();
                    if (b1 == ')')
                    {
                        reader.Skip('\n');
                        return text;
                    }
                    char b2 = reader.ReadChar();
                    if (b2 == '\\')
                    {
                        b2 = reader.ReadChar();
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
                c = reader.ReadChar();
                while (true)
                {
                    if (c == ' ')
                    {
                        object val = this.ReadValue(reader, level);
                        if (val == null)
                        {
                            reader.Skip('\n');
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
                        this.ReadProperties(reader, level, p);
                        reader.Skip('\n');
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
                string value = string.Empty;
                do
                {
                    value += c;
                    c = reader.ReadChar();
                }
                while (c != 10 && c != ' ');

                {
                    int f;
                    if (int.TryParse(value, out f) == true)
                        return f;
                }
                {
                    float f;
                    if (float.TryParse(value, out f) == true)
                        return f;
                }
                {
                    bool f;
                    if (bool.TryParse(value, out f) == true)
                        return f;
                }
                
                return value;
            }
        }

        //private void SkipTabs(PSDReader reader, int count)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        byte tabCh = reader.ReadByte();
        //        //assert tabCh == 9 : "must be tab: " + tabCh;
        //    }
        //}
    }
}
