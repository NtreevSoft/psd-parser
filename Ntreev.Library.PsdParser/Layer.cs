using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Ntreev.Library.PsdParser
{
    public sealed class Layer : IProperties
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
        
        internal Layer(PSDReader reader, int bpp)
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
            this.Channels = new ChannelInfo[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                this.Channels[i] = new ChannelInfo(reader, this.Width, this.Height);
            }
            string str = reader.ReadAscii(4);
            if ("8BIM" != str)
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }
            //reader.ReadInt32();
            this.blendMode = PSDUtil.ToBlendMode(reader.ReadAscii(4));
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

        internal void LoadChannels(PSDReader reader, int bpp)
        {
            foreach (var item in this.Channels)
            {
                item.LoadImage(reader, bpp);
            }
        }

        public byte[] MergedChannels
        {
            get
            {
                if (!this.isImageLayer)
                {
                    return null;
                }

                if (this.buffers == null)
                {
                    int length = this.Channels.Length;
                    int num2 = this.Channels[0].Data.Length;
                    byte[] buffer = new byte[(this.Width * this.Height) * length];
                    int num3 = 0;
                    for (int i = 0; i < num2; i++)
                    {
                        switch (length)
                        {
                            case 4:
                                buffer[num3++] = this.Channels[3].Data[i];
                                buffer[num3++] = this.Channels[2].Data[i];
                                buffer[num3++] = this.Channels[1].Data[i];
                                buffer[num3++] = this.Channels[0].Data[i];
                                break;

                            case 3:
                                buffer[num3++] = this.Channels[2].Data[i];
                                buffer[num3++] = this.Channels[1].Data[i];
                                buffer[num3++] = this.Channels[0].Data[i];
                                break;
                        }
                    }

                    this.buffers = buffer;
                }
                return this.buffers;
            }
        }

        public ChannelInfo[] Channels { get; internal set; }

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
                return (((!this.drop && (this.Width > 0)) && (this.Height > 0)) && (this.Channels.Length > 0));
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

        public int Pitch
        {
            get
            {
                return (this.Width * this.Channels.Length);
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

        public BlendMode BlendMode
        {
            get { return this.blendMode; }
        }

        public Layer[] Childs
        {
            get { return null; }
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

