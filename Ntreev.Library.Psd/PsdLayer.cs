using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using Ntreev.Library.Psd.Readers;

namespace Ntreev.Library.Psd
{
    class PsdLayer : IPsdLayer
    {
        private readonly PsdDocument document;
        private readonly LayerRecordsReader records;
        private int left, top, right, bottom;
        private int id;
        private string name;
        private SectionType sectionType;
        private byte opacity;
        private BlendMode blendMode;
        private bool clipping;
        private LayerFlags flags;
        private int filter;
        private List<PsdLayer> childs = new List<PsdLayer>();
        private List<IPsdLayer> childArray;
        private PsdLayer parent;
        private Guid placedID;
        
        private Channel[] channels;
        private PsdReader channelReader;
        private long channelPosition;
        private ILinkedLayer linkedLayer;

        private LayerMaskReader layerMask;
        private LayerBlendingRangesReader blendingRanges;
        private LayerResourceReader resources;
        
        public PsdLayer(PsdReader reader, PsdDocument document)
        {
            this.document = document;
            this.records = new LayerRecordsReader(reader);
            this.top = reader.ReadInt32();
            this.left = reader.ReadInt32();
            this.bottom = reader.ReadInt32();
            this.right = reader.ReadInt32();
            this.ValidateSize(this.Width, this.Height);

            ushort channelCount = reader.ReadUInt16();
            this.ValidateChannelCount(channelCount);

            this.channels = new Channel[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                this.channels[i] = new Channel(reader, this.Width, this.Height);
            }

            reader.ValidateSignature();

            //reader.ReadInt32();
            this.blendMode = reader.ReadBlendMode();
            this.opacity = reader.ReadByte();
            this.clipping = reader.ReadBoolean();
            this.flags = reader.ReadLayerFlags();
            this.filter = reader.ReadByte();

            long extraSize = reader.ReadUInt32();
            long end = reader.Position + extraSize;

            long position = reader.Position;
            this.layerMask = new LayerMaskReader(reader);

            if (this.layerMask.Size > 0)
            {
                var mask = this.Channels.Where(item => item.Type == ChannelType.Mask).First();
                mask.Width = this.layerMask.Width;
                mask.Height = this.layerMask.Height;
            }

            this.blendingRanges = new LayerBlendingRangesReader(reader);
            this.name = reader.ReadPascalString(4);

            this.resources = new LayerResourceReader(reader, end);

            this.id = this.resources.ToInt32("lyid.ID");
            this.resources.TryGetValue<string>(ref this.name, "luni.Name");
            this.resources.TryGetValue<SectionType>(ref this.sectionType, "lsct.SectionType");

            if (this.resources.Contains("SoLd.Idnt") == true)
                this.placedID = this.resources.ToGuid("SoLd.Idnt");
            else if (this.resources.Contains("SoLE.Idnt") == true)
                this.placedID = this.resources.ToGuid("SoLE.Idnt");
            if (this.resources.Contains("iOpa") == true)
            {
                byte opa = this.resources.ToByte("iOpa.Opacity");
                var alphaChannel = this.Channels.Where(item => item.Type == ChannelType.Alpha).FirstOrDefault();
                if (alphaChannel != null)
                {
                    alphaChannel.Opacity = opa / 255.0f;
                }
            }

            reader.Position = end;
        }

        public Channel[] Channels
        {
            get { return this.channels; }
        }

        public SectionType SectionType
        {
            get { return this.sectionType; }
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public float Opacity
        {
            get { return (((float)this.opacity) / 255f); }
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
            get { return this.document.FileHeader.Depth; }
        }

        public int Pitch
        {
            get { return (this.Width * this.Channels.Length); }
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
            get { return this.childs; }
        }

        public IProperties Resources
        {
            get { return this.resources; }
        }

        public Guid PlacedID
        {
            get { return this.placedID; }
        }

        public PsdDocument Document
        {
            get { return this.document; }
        }

        public ILinkedLayer LinkedLayer
        {
            get
            {
                if (this.placedID == Guid.Empty)
                    return null;

                if (this.linkedLayer == null)
                {
                    this.linkedLayer = this.document.LinkedLayers.Where(i => i.ID == this.placedID && i.HasDocument).FirstOrDefault();
                }
                return this.linkedLayer;
            }
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

        public void ReadChannels(PsdReader reader)
        {
            this.channelReader = reader;
            this.channelPosition = reader.Position;
            foreach (var item in this.channels)
            {
                reader.Position += item.Size;
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
                if (item.Resources.Contains("PlLd.Transformation"))
                {
                    double[] transforms = (double[])item.Resources["PlLd.Transformation"];
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

            //this.props["Left"] = this.left;
            //this.props["Top"] = this.top;
            //this.props["Right"] = this.right;
            //this.props["Bottom"] = this.bottom;
            //this.props["Width"] = this.Width;
            //this.props["Height"] = this.Height;
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

        private void ValidateSize(int width, int height)
        {
            if ((width > 0x3000) || (height > 0x3000))
            {
                throw new Exception(string.Format("Invalidated size ({0}, {1})", width, height));
            }
        }

        private void ValidateChannelCount(int channelCount)
        {
            if (channelCount > 0x38)
            {
                throw new Exception(string.Format("Too many channels : {0}", channelCount));
            }
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

        ILinkedLayer IPsdLayer.LinkedLayer
        {
            get { return this.LinkedLayer; }
        }

        IChannel[] IImageSource.Channels
        {
            get
            {
                if (this.channelReader != null)
                {
                    this.channelReader.Position = this.channelPosition;
                    foreach (var item in this.channels)
                    {
                        CompressionType compressionType = (CompressionType)this.channelReader.ReadInt16();
                        item.ReadHeader(this.channelReader, compressionType);
                        item.Read(this.channelReader, this.Depth, compressionType);
                    }
                    this.channelReader = null;
                }

                return this.channels;
            }
        }

        #endregion
    }
}

