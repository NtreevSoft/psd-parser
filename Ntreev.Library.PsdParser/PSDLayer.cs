using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSDLayer : IProperties
    {
        private int left, top, right, bottom;
        private int id;
        private string name;
        private SectionType sectionType;
        private byte opacity;
        private BlendMode blendMode;
        private bool clipping;
        private LayerFlags flags;
        private int filter;
        private Properties props = new Properties();
        private byte[] buffers;
        
        public void load(PSDReader reader, int bpp)
        {
            this.top = reader.ReadInt32();
            this.left = reader.ReadInt32();
            this.bottom = reader.ReadInt32();
            this.right = reader.ReadInt32();

            if ((this.Width > 0x3000) || (this.Height > 0x3000))
            {
                throw new SystemException(string.Format("Too big image (width:{0} height{1})", this.Width, this.Height));
            }
            ushort channelCount = reader.ReadUInt16();
            if (channelCount > 0x38)
            {
                throw new SystemException(string.Format("Too many channels {0}", channelCount));
            }
            this.channels = new ChannelInfo[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                ChannelInfo info = new ChannelInfo 
                {
                    width = this.Width,
                    height = this.Height,
                };
                info.loadHeader(reader);
                this.channels[i] = info;
            }
            string str = reader.ReadAscii(4);
            if ("8BIM" != str)
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }
            //reader.ReadInt32();
            this.blendMode = this.ToBlendMode(reader.ReadAscii(4));
            this.opacity = reader.ReadByte();
            this.clipping = reader.ReadBoolean();
            this.flags = (LayerFlags)reader.ReadByte(); // Flags
            this.filter = reader.ReadByte(); // Filter

            long extraSize = reader.ReadUInt32(); // Length of the extra data field ( = the total length of the next five fields).

            long position = reader.Position;
            uint num7 = reader.ReadUInt32();
            reader.Position += num7;
            extraSize -= reader.Position - position;

            position = reader.Position;
            new LayerBlendingRanges(reader);
            extraSize -= reader.Position - position;

            position = reader.Position;
            this.name = reader.ReadPascalString(4);
            extraSize -= reader.Position - position;

            LayerResource resource = new LayerResource();
            
            while (extraSize > 7L)
            {
                position = reader.Position;
                resource.Load(reader);
                extraSize -= reader.Position - position;
            }

            this.props.Add("Resources", resource);

            if (extraSize > 0L)
            {
                reader.Position += extraSize;
            }

            this.id = resource.ToInt32("lyid.ID");
            if (resource.ContainsProperty("luni.Name") == true)
                this.name = resource.ToString("luni.Name");
            this.drop = resource.drop;
            if (resource.ContainsProperty("lsct.SectionType") == true)
                this.sectionType = (SectionType)resource.GetProperty("lsct.SectionType");

            if ((this.Width == 0) && (this.sectionType == 0))
            {
                int qwer = 0;
                //this.area = resource.typeToolObj2.area;
            }

            this.props.Add("ID", this.id);
            this.props.Add("Name", this.name);
            this.props.Add("SectionType", this.sectionType);
            this.props.Add("Left", this.left);
            this.props.Add("Top", this.top);
            this.props.Add("Right", this.right);
            this.props.Add("Bottom", this.bottom);
            this.props.Add("Width", this.Width);
            this.props.Add("Height", this.Height);
            this.props.Add("Bounds", string.Format("{0}, {1}, {2}, {3}", this.left, this.top, this.right, this.bottom));
        }

        public byte[] mergeChannels()
        {
            if (!this.isImageLayer)
            {
                return null;
            }

            if (this.buffers == null)
            {
                int length = this.channels.Length;
                int num2 = this.channels[0].data.Length;
                byte[] buffer = new byte[(this.Width * this.Height) * length];
                int num3 = 0;
                for (int i = 0; i < num2; i++)
                {
                    switch (length)
                    {
                        case 4:
                            buffer[num3++] = this.channels[3].data[i];
                            buffer[num3++] = this.channels[2].data[i];
                            buffer[num3++] = this.channels[1].data[i];
                            buffer[num3++] = this.channels[0].data[i];
                            break;

                        case 3:
                            buffer[num3++] = this.channels[2].data[i];
                            buffer[num3++] = this.channels[1].data[i];
                            buffer[num3++] = this.channels[0].data[i];
                            break;
                    }
                }

                this.buffers = buffer;
            }
            return this.buffers;
        }

        //public PSDRect area { get; internal set; }

        public ChannelInfo[] channels { get; internal set; }

        public bool drop { get; internal set; }

        public SectionType SectionType
        {
            get { return this.sectionType; }
        }

        public int ID { get { return this.id; } }

        public bool isImageLayer
        {
            get
            {
                return (((!this.drop && (this.Width > 0)) && (this.Height > 0)) && (this.channels.Length > 0));
            }
        }

        public bool isTextLayer
        {
            get
            {
                return this.props.ContainsProperty("TypeToolInfo");
            }
        }

        public string Name
        {
            get { return this.name; }
        }

        public float Opacity
        {
            get
            {
                return (((float) this.opacity) / 255f);
            }
        }

        public int pitch
        {
            get
            {
                return (this.Width * this.channels.Length);
            }
        }

        public string text
        {
            get
            {
                if (this.isTextLayer == true)
                    return this.props["TypeToolInfo.Text.Txt"] as string;
                return null;
            }
        }

        public int Left
        {
            get { return this.left; }
        }

        public int Top
        {
            get { return this.top; }
        }

        public int Right
        {
            get { return this.right; }
        }

        public int Bottom
        {
            get { return this.bottom; }
            }

        public int Width
        {
            get { return this.right - this.left; }
        }

        public int Height
        {
            get { return this.bottom - this.top; }
        }

        private BlendMode ToBlendMode(string text)
        {
            switch (text)
            {
                case "pass":
                    return BlendMode.PassThrough;
                case "norm":
                    return BlendMode.Normal;
                case "diss":
                    return BlendMode.Dissolve;
                case "dark":
                    return BlendMode.Darken;
                case "mul":
                    return BlendMode.Multiply;
                case "idiv":
                    return BlendMode.ColorBurn;
                case "lbrn":
                    return BlendMode.LinearBurn;
                case "dkCl":
                    return BlendMode.DarkerColor;
                case "lite":
                    return BlendMode.Lighten;
                case "scrn":
                    return BlendMode.Screen;
                case "div":
                    return BlendMode.ColorDodge;
                case "lddg":
                    return BlendMode.LinearDodge;
                case "lgCl":
                    return BlendMode.LighterColor;
                case "over":
                    return BlendMode.Overlay;
                case "sLit":
                    return BlendMode.SoftLight;
                case "hLit":
                    return BlendMode.HardLight;
                case "vLit":
                    return BlendMode.VividLight;
                case "lLit":
                    return BlendMode.LinearLight;
                case "pLit":
                    return BlendMode.PinLight;
                case "hMix":
                    return BlendMode.HardMix;
                case "diff":
                    return BlendMode.Difference;
                case "smud":
                    return BlendMode.Exclusion;
                case "fsub":
                    return BlendMode.Subtract;
                case "fdiv":
                    return BlendMode.Divide;
                case "hue":
                    return BlendMode.Hue;
                case "sat":
                    return BlendMode.Saturation;
                case "colr":
                    return BlendMode.Color;
                case "lum":
                    return BlendMode.Luminosity;
            }
            return BlendMode.Normal;
        }

        #region IProperties

        bool IProperties.Contains(string property)
        {
            return this.props.ContainsProperty(property);
        }

        object IProperties.this[string property]
        {
            get { return this.props[property]; }
        }

        int IProperties.Count
        {
            get { return this.props.Count; }
        }

        System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, object>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.props.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.props.GetEnumerator();
        }

        #endregion
    }
}

