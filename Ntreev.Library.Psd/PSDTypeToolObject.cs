//using System;
//using System.IO;

//namespace Ntreev.Library.Psd
//{
//    public sealed class PSDTypeToolObject
//    {
//        public Color color = new Color();
//        public Font font = new Font();
//        public Style style = new Style();
//        public Text text = new Text();
//        public double[] transforms = new double[6];
//        public short version;

//        public void load(PSDReader reader)
//        {
//            this.version = reader.ReadInt16();
//            for (int i = 0; i < this.transforms.Length; i++)
//            {
//                this.transforms[i] = reader.ReadDouble();
//            }
//            this.font.load(reader);
//            this.style.load(reader);
//            this.text.load(reader);
//            this.color.load(reader);
//        }

//        public sealed class Color
//        {
//            public byte antiAlias;
//            public short[] components = new short[4];
//            public short space;

//            public void load(PSDReader reader)
//            {
//                this.space = reader.ReadInt16();
//                for (int i = 0; i < this.components.Length; i++)
//                {
//                    this.components[i] = reader.ReadInt16();
//                }
//                this.antiAlias = reader.ReadByte();
//            }
//        }

//        public sealed class Font
//        {
//            public short faceCount;
//            public Face[] faces;
//            public short version;

//            public void load(PSDReader reader)
//            {
//                this.version = reader.ReadInt16();
//                this.faceCount = reader.ReadInt16();
//                this.faces = new Face[this.faceCount];
//                for (int i = 0; i < this.faceCount; i++)
//                {
//                    this.faces[i] = new Face();
//                    this.faces[i].load(reader);
//                }
//            }

//            public sealed class Face
//            {
//                public int designAxes;
//                public int designVector;
//                public string familyName;
//                public short mark;
//                public string name;
//                public short script;
//                public string styleName;
//                public int type;

//                public void load(PSDReader reader)
//                {
//                    this.mark = reader.ReadInt16();
//                    this.type = reader.ReadInt32();
//                    this.name = reader.ReadPascalString(4);
//                    this.familyName = reader.ReadPascalString(4);
//                    this.styleName = reader.ReadPascalString(4);
//                    this.script = reader.ReadInt16();
//                    this.designAxes = reader.ReadInt32();
//                    this.designVector = reader.ReadInt32();
//                }
//            }
//        }

//        public sealed class Style
//        {
//            public short infoCount;
//            public Info[] infos;

//            public void load(PSDReader reader)
//            {
//                this.infoCount = reader.ReadInt16();
//                this.infos = new Info[this.infoCount];
//                for (int i = 0; i < this.infoCount; i++)
//                {
//                    this.infos[i] = new Info();
//                    this.infos[i].load(reader);
//                }
//            }

//            public sealed class Info
//            {
//                public byte autoKern;
//                public int baseShift;
//                public short faceMark;
//                public int kerning;
//                public int leading;
//                public short mark;
//                public byte rotateDirection;
//                public int size;
//                public int tracking;
//                public byte ver1_5specific;

//                public void load(PSDReader reader)
//                {
//                    this.mark = reader.ReadInt16();
//                    this.faceMark = reader.ReadInt16();
//                    this.size = reader.ReadInt32();
//                    this.tracking = reader.ReadInt32();
//                    this.kerning = reader.ReadInt32();
//                    this.leading = reader.ReadInt32();
//                    this.baseShift = reader.ReadInt32();
//                    this.autoKern = reader.ReadByte();
//                    this.ver1_5specific = reader.ReadByte();
//                    this.rotateDirection = reader.ReadByte();
//                }
//            }
//        }

//        public sealed class Text
//        {
//            public int count;
//            public int horzPlacement;
//            public short lineCount;
//            public Line[] lines;
//            public int scaling;
//            public int selEnd;
//            public int selStart;
//            public short type;
//            public int vertPlacement;

//            public void load(PSDReader reader)
//            {
//                this.type = reader.ReadInt16();
//                this.scaling = reader.ReadInt32();
//                this.count = reader.ReadInt32();
//                this.horzPlacement = reader.ReadInt32();
//                this.vertPlacement = reader.ReadInt32();
//                this.selStart = reader.ReadInt32();
//                this.selEnd = reader.ReadInt32();
//                this.lineCount = reader.ReadInt16();
//                this.lines = new Line[this.lineCount];
//                for (int i = 0; i < this.lineCount; i++)
//                {
//                    this.lines[i] = new Line();
//                    this.lines[i].load(reader);
//                }
//            }

//            public sealed class Line
//            {
//                public short align;
//                public short ch;
//                public int count;
//                public short orientation;
//                public short style;

//                public void load(PSDReader reader)
//                {
//                    this.count = reader.ReadInt32();
//                    this.orientation = reader.ReadInt16();
//                    this.align = reader.ReadInt16();
//                    this.ch = reader.ReadInt16();
//                    this.style = reader.ReadInt16();
//                }
//            }
//        }
//    }
//}

