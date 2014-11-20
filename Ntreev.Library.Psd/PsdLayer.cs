using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Ntreev.Library.Psd
{
    sealed class PsdLayer : IPsdLayer
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
        private List<PsdLayer> childs = new List<PsdLayer>();
        private List<IPsdLayer> childArray;
        private PsdLayer parent;
        private Guid placedID;
        private int depth;
        private int index;
        private PsdDocument document;

        public PsdLayer(PSDReader reader, PsdDocument document, int bpp, int index)
        {
            this.document = document;
            this.index = index;
            this.depth = bpp;
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
            this.Channels = new Channel[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                ChannelType type = (ChannelType)reader.ReadInt16();
                long size = reader.ReadLength();
                //    this.size = reader.ReadLength();
                this.Channels[i] = new Channel(type, this.Width, this.Height);
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
            long end = reader.Position + extraSize;

            long position = reader.Position;
            LayerMask layerMask = new LayerMask(reader);

            if (layerMask.Size > 0)
            {
                var mask = this.Channels.Where(item => item.Type == ChannelType.Mask).First();
                mask.Width = layerMask.Width;
                mask.Height = layerMask.Height;
            }

            new LayerBlendingRanges(reader); // Layer blending ranges
            this.name = reader.ReadPascalString(4); // Layer name: Pascal string, padded to a multiple of 4 bytes.

            LayerResource resource = new LayerResource();

            while (reader.Position < end)
            {
                resource.Load(reader, index);
            }

            this.props.Add("Resources", resource);

            this.id = resource.ToInt32("lyid.ID");
            if (resource.ContainsProperty("luni.Name") == true)
                this.name = resource.ToString("luni.Name");
            if (resource.ContainsProperty("lsct.SectionType") == true)
                this.sectionType = (SectionType)resource.GetProperty("lsct.SectionType");
            if (resource.ContainsProperty("SoLd.Descriptor.Idnt") == true)
                this.placedID = new Guid(resource.GetProperty("SoLd.Descriptor.Idnt") as string);
            if (resource.ContainsProperty("iOpa") == true)
            {
                byte opa = (byte)resource.GetProperty("iOpa.Opacity");
                var alphaChannel = this.Channels.Where(item => item.Type == ChannelType.Alpha).FirstOrDefault();
                if (alphaChannel != null)
                {
                    alphaChannel.Opacity = opa / 255.0f;
                }
            }

            this.props.Add("ID", this.id);
            this.props.Add("Name", this.name);
            this.props.Add("SectionType", this.sectionType);
            this.props.Add("BlendMode", this.blendMode);
            this.props.Add("Opacity", this.opacity);
            this.props.Add("Left", this.left);
            this.props.Add("Top", this.top);
            this.props.Add("Right", this.right);
            this.props.Add("Bottom", this.bottom);
            this.props.Add("Width", this.Width);
            this.props.Add("Height", this.Height);
            this.props.Add("Bounds", string.Format("{0}, {1}, {2}, {3}", this.left, this.top, this.right, this.bottom));
            this.props.Add("Clipping", this.clipping);
        }

        public Channel[] Channels { get; internal set; }

        public SectionType SectionType
        {
            get { return this.sectionType; }
        }

        public int ID { get { return this.id; } }

        public string Name
        {
            get { return this.name; }
        }

        public float Opacity
        {
            get
            {
                return (((float)this.opacity) / 255f);
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

        public int Depth
        {
            get { return this.depth; }
        }

        public int Pitch
        {
            get
            {
                return (this.Width * this.Channels.Length);
            }
        }

        public bool IsClipping
        {
            get { return this.clipping; }
        }

        public BlendMode BlendMode
        {
            get { return this.blendMode; }
        }

        public PsdLayer Parent
        {
            get { return this.parent; }
        }

        public IEnumerable<PsdLayer> Childs
        {
            get
            {
                return this.childs;
            }
        }

        public IProperties Properties
        {
            get { return this.props; }
        }

        public Guid PlacedID
        {
            get { return this.placedID; }
        }

        public PsdDocument Document
        {
            get { return this.document; }
        }

        public LinkedLayer LinkedLayer
        {
            get;
            internal set;
        }

        public bool HasImage
        {
            get
            {
                if (this.sectionType != SectionType.Normal)
                    return false;
                if (this.Width == 0 || this.Height == 0)
                    return false;
                return true;
            }
        }

        public void LoadChannels(PSDReader reader, int bpp)
        {
            foreach (var item in this.Channels)
            {
                CompressionType compressionType = (CompressionType)reader.ReadInt16();
                item.LoadHeader(reader, compressionType);
                item.Load(reader, bpp, compressionType);
            }
        }

        public void ComputeBounds()
        {
            if (this.sectionType != SectionType.Opend && this.sectionType != SectionType.Closed)
                return;

            int left = int.MaxValue;
            int top = int.MaxValue;
            int right = int.MinValue;
            int bottom = int.MinValue;

            bool isSet = false;

            foreach (var item in this.All())
            {
                if (item == this || item.HasImage == false)
                    continue;

                // 일반 레이어인데 비어 있을때
                if (item.sectionType == SectionType.Normal && item.HasImage == false)
                {
                    double[] transforms = (double[])item.Properties["PlLd.Transformation"];
                    double[] xx = new double[] { transforms[0], transforms[2], transforms[4], transforms[6], };
                    double[] yy = new double[] { transforms[1], transforms[3], transforms[5], transforms[7], };

                    int l = (int)Math.Ceiling(xx.Min());
                    int r = (int)Math.Ceiling(xx.Max());
                    int t = (int)Math.Ceiling(yy.Min());
                    int b = (int)Math.Ceiling(yy.Max());
                    left = Math.Min(l, left);
                    top = Math.Min(t, top);
                    right = Math.Max(r, right);
                    bottom = Math.Max(b, bottom);
                }
                else
                {
                    left = Math.Min(item.Left, left);
                    top = Math.Min(item.Top, top);
                    right = Math.Max(item.Right, right);
                    bottom = Math.Max(item.Bottom, bottom);
                }
                isSet = true;
            }

            if (isSet == false)
                return;

            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;

            this.props["Left"] = this.left;
            this.props["Top"] = this.top;
            this.props["Right"] = this.right;
            this.props["Bottom"] = this.bottom;
            this.props["Width"] = this.Width;
            this.props["Height"] = this.Height;
        }

        public static PsdLayer[] Initialize(PsdLayer parent, PsdLayer[] layers)
        {
            Stack<PsdLayer> stack = new Stack<PsdLayer>();
            List<PsdLayer> rootLayers = new List<PsdLayer>();

            foreach (var item in layers.Reverse())
            {
                if (item.SectionType == SectionType.Divider == true)
                {
                    parent = stack.Pop();
                    continue;
                }

                if (parent != null)
                {
                    parent.childs.Insert(0, item);
                    item.parent = parent;
                }
                else
                {
                    rootLayers.Insert(0, item);
                }

                if (item.sectionType == SectionType.Opend || item.sectionType == SectionType.Closed)
                {
                    stack.Push(parent);
                    parent = item;
                }
            }

            return rootLayers.ToArray();
        }

        #region IPsdLayer

        IEnumerable<IPsdLayer> IPsdLayer.Childs
        {
            get 
            {
                if (this.childArray == null)
                {
                    this.childArray = new List<IPsdLayer>(this.childs.Count);
                    foreach (var item in this.childs)
                    {
                        this.childArray.Add(item);
                    }
                }
                return this.childArray; 
            }
        }

        IPsdLayer IPsdLayer.Parent
        {
            get { return this.parent; }
        }

        IPsdLayer IPsdLayer.LinkedLayer
        {
            get
            {
                if (this.LinkedLayer == null)
                    return null;
                return this.LinkedLayer.PSD;
            }
        }

        #endregion
    }
}

