using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSDTypeToolObject
    {
        public Color color = new Color();
        public Font font = new Font();
        public Style style = new Style();
        public Text text = new Text();
        public double[] transforms = new double[6];
        public short version;

        public void load(BinaryReader br)
        {
            this.version = EndianReverser.getInt16(br);
            for (int i = 0; i < this.transforms.Length; i++)
            {
                this.transforms[i] = EndianReverser.getDouble(br);
            }
            this.font.load(br);
            this.style.load(br);
            this.text.load(br);
            this.color.load(br);
        }

        public sealed class Color
        {
            public byte antiAlias;
            public short[] components = new short[4];
            public short space;

            public void load(BinaryReader br)
            {
                this.space = EndianReverser.getInt16(br);
                for (int i = 0; i < this.components.Length; i++)
                {
                    this.components[i] = EndianReverser.getInt16(br);
                }
                this.antiAlias = br.ReadByte();
            }
        }

        public sealed class Font
        {
            public short faceCount;
            public Face[] faces;
            public short version;

            public void load(BinaryReader br)
            {
                this.version = EndianReverser.getInt16(br);
                this.faceCount = EndianReverser.getInt16(br);
                this.faces = new Face[this.faceCount];
                for (int i = 0; i < this.faceCount; i++)
                {
                    this.faces[i] = new Face();
                    this.faces[i].load(br);
                }
            }

            public sealed class Face
            {
                public int designAxes;
                public int designVector;
                public string familyName;
                public short mark;
                public string name;
                public short script;
                public string styleName;
                public int type;

                public void load(BinaryReader br)
                {
                    this.mark = EndianReverser.getInt16(br);
                    this.type = EndianReverser.getInt32(br);
                    this.name = PSDUtil.readPascalString(br, 4);
                    this.familyName = PSDUtil.readPascalString(br, 4);
                    this.styleName = PSDUtil.readPascalString(br, 4);
                    this.script = EndianReverser.getInt16(br);
                    this.designAxes = EndianReverser.getInt32(br);
                    this.designVector = EndianReverser.getInt32(br);
                }
            }
        }

        public sealed class Style
        {
            public short infoCount;
            public Info[] infos;

            public void load(BinaryReader br)
            {
                this.infoCount = EndianReverser.getInt16(br);
                this.infos = new Info[this.infoCount];
                for (int i = 0; i < this.infoCount; i++)
                {
                    this.infos[i] = new Info();
                    this.infos[i].load(br);
                }
            }

            public sealed class Info
            {
                public byte autoKern;
                public int baseShift;
                public short faceMark;
                public int kerning;
                public int leading;
                public short mark;
                public byte rotateDirection;
                public int size;
                public int tracking;
                public byte ver1_5specific;

                public void load(BinaryReader br)
                {
                    this.mark = EndianReverser.getInt16(br);
                    this.faceMark = EndianReverser.getInt16(br);
                    this.size = EndianReverser.getInt32(br);
                    this.tracking = EndianReverser.getInt32(br);
                    this.kerning = EndianReverser.getInt32(br);
                    this.leading = EndianReverser.getInt32(br);
                    this.baseShift = EndianReverser.getInt32(br);
                    this.autoKern = br.ReadByte();
                    this.ver1_5specific = br.ReadByte();
                    this.rotateDirection = br.ReadByte();
                }
            }
        }

        public sealed class Text
        {
            public int count;
            public int horzPlacement;
            public short lineCount;
            public Line[] lines;
            public int scaling;
            public int selEnd;
            public int selStart;
            public short type;
            public int vertPlacement;

            public void load(BinaryReader br)
            {
                this.type = EndianReverser.getInt16(br);
                this.scaling = EndianReverser.getInt32(br);
                this.count = EndianReverser.getInt32(br);
                this.horzPlacement = EndianReverser.getInt32(br);
                this.vertPlacement = EndianReverser.getInt32(br);
                this.selStart = EndianReverser.getInt32(br);
                this.selEnd = EndianReverser.getInt32(br);
                this.lineCount = EndianReverser.getInt16(br);
                this.lines = new Line[this.lineCount];
                for (int i = 0; i < this.lineCount; i++)
                {
                    this.lines[i] = new Line();
                    this.lines[i].load(br);
                }
            }

            public sealed class Line
            {
                public short align;
                public short ch;
                public int count;
                public short orientation;
                public short style;

                public void load(BinaryReader br)
                {
                    this.count = EndianReverser.getInt32(br);
                    this.orientation = EndianReverser.getInt16(br);
                    this.align = EndianReverser.getInt16(br);
                    this.ch = EndianReverser.getInt16(br);
                    this.style = EndianReverser.getInt16(br);
                }
            }
        }
    }
}

