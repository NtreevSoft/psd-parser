using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;

namespace Ntreev.Library.Psd
{
    class PsdLayer : IPsdLayer
    {
        private readonly PsdDocument document;
        private readonly LayerRecordsReader records;
        private readonly LayerExtraRecordsReader extraRecords;
        //private string name;
        //private SectionType sectionType;

        private PsdLayer[] childs;
        private PsdLayer parent;
        //private Guid placedID;
        private ILinkedLayer linkedLayer;

        //private LayerMaskReader layerMask;
        //private LayerBlendingRangesReader blendingRanges;
        //private LayerResourceReader resources;
        private ChannelsReader channels;

        private static PsdLayer[] emptyChilds = new PsdLayer[] { };
        
        public PsdLayer(PsdReader reader, PsdDocument document)
        {
            this.document = document;
            this.records = new LayerRecordsReader(reader);
            this.extraRecords = new LayerExtraRecordsReader(reader);

            //long length = reader.ReadUInt32();
            //long end = reader.Position + length;

            //long position = reader.Position;
            //this.layerMask = new LayerMaskReader(reader);
            //this.blendingRanges = new LayerBlendingRangesReader(reader);
            //this.name = reader.ReadPascalString(4);

            //this.resources = new LayerResourceReader(reader, end - reader.Position);
            //this.resources.TryGetValue<string>(ref this.name, "luni.Name");
            //this.resources.TryGetValue<SectionType>(ref this.sectionType, "lsct.SectionType");

            //if (this.resources.Contains("SoLd.Idnt") == true)
            //    this.placedID = this.resources.ToGuid("SoLd.Idnt");
            //else if (this.resources.Contains("SoLE.Idnt") == true)
            //    this.placedID = this.resources.ToGuid("SoLE.Idnt");

            //reader.Position = end;
        }

        public Channel[] Channels
        {
            get { return this.channels.Value; }
        }

        public SectionType SectionType
        {
            get { return this.extraRecords.Value.SectionType; }
        }

        public string Name
        {
            get { return this.extraRecords.Value.Name; }
        }

        public float Opacity
        {
            get { return (((float)this.records.Value.Opacity) / 255f); }
        }

        public int Left
        {
            get { return this.records.Value.Left; }
        }

        public int Top
        {
            get { return this.records.Value.Top; }
        }

        public int Right
        {
            get { return this.records.Value.Right; }
        }

        public int Bottom
        {
            get { return this.records.Value.Bottom; }
        }

        public int Width
        {
            get { return this.records.Value.Width; }
        }

        public int Height
        {
            get { return this.records.Value.Height; }
        }

        public int Depth
        {
            get { return this.document.FileHeaderSection.Depth; }
        }

        public bool IsClipping
        {
            get { return this.records.Value.Clipping; }
        }

        public BlendMode BlendMode
        {
            get { return this.records.Value.BlendMode; }
        }

        public PsdLayer Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public PsdLayer[] Childs
        {
            get 
            {
                if (this.childs == null)
                    return emptyChilds;
                return this.childs; 
            }
            set { this.childs = value; }
        }

        public IProperties Resources
        {
            get { return this.extraRecords.Value.Resources; }
        }

        public PsdDocument Document
        {
            get { return this.document; }
        }

        public LayerRecords Records
        {
            get { return this.records.Value; }
        }

        public LayerExtraRecords ExtraRecords
        {
            get { return this.extraRecords.Value; }
        }

        public ILinkedLayer LinkedLayer
        {
            get
            {
                Guid placeID = this.extraRecords.Value.PlacedID;

                if (placeID == Guid.Empty)
                    return null;

                if (this.linkedLayer == null)
                {
                    this.linkedLayer = this.document.LinkedLayers.Where(i => i.ID == placeID && i.HasDocument).FirstOrDefault();
                }
                return this.linkedLayer;
            }
        }

        public bool HasImage
        {
            get
            {
                if (this.extraRecords.Value.SectionType != SectionType.Normal)
                    return false;
                if (this.Width == 0 || this.Height == 0)
                    return false;
                return true;
            }
        }

        public void ReadChannels(PsdReader reader)
        {
            
            this.channels = new ChannelsReader(reader, this.records.Value.ChannelSize, this);
        }

        public void ComputeBounds()
        {
            SectionType sectionType = this.extraRecords.Value.SectionType;
            if (sectionType != SectionType.Opend && sectionType != SectionType.Closed)
                return;

            int left = int.MaxValue;
            int top = int.MaxValue;
            int right = int.MinValue;
            int bottom = int.MinValue;

            bool isSet = false;

            foreach (var item in this.Descendants())
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

            //this.records.Value.Left = left;

            this.records.Value.Left = left;
            this.records.Value.Top = top;
            this.records.Value.Right = right;
            this.records.Value.Bottom = bottom;

            //this.props["Left"] = this.left;
            //this.props["Top"] = this.top;
            //this.props["Right"] = this.right;
            //this.props["Bottom"] = this.bottom;
            //this.props["Width"] = this.Width;
            //this.props["Height"] = this.Height;
        }

        //public static PsdLayer[] Initialize(PsdLayer parent, PsdLayer[] layers)
        //{
        //    Stack<PsdLayer> stack = new Stack<PsdLayer>();
        //    List<PsdLayer> rootLayers = new List<PsdLayer>();

        //    foreach (var item in layers.Reverse())
        //    {
        //        if (item.SectionType == SectionType.Divider)
        //        {
        //            parent = stack.Pop();
        //            continue;
        //        }

        //        if (parent != null)
        //        {
        //            parent.childs.Insert(0, item);
        //            item.parent = parent;
        //        }
        //        else
        //        {
        //            rootLayers.Insert(0, item);
        //        }

        //        if (item.sectionType == SectionType.Opend || item.sectionType == SectionType.Closed)
        //        {
        //            stack.Push(parent);
        //            parent = item;
        //        }
        //    }

        //    return rootLayers.ToArray();
        //}

        //private void ValidateSize(int width, int height)
        //{
        //    if ((width > 0x3000) || (height > 0x3000))
        //    {
        //        throw new Exception(string.Format("Invalidated size ({0}, {1})", width, height));
        //    }
        //}

        //private void ValidateChannelCount(int channelCount)
        //{
        //    if (channelCount > 0x38)
        //    {
        //        throw new Exception(string.Format("Too many channels : {0}", channelCount));
        //    }
        //}

        #region IPsdLayer

        IPsdLayer IPsdLayer.Parent
        {
            get 
            {
                if (this.parent == null)
                    return this.document;
                return this.parent; 
            }
        }

        //ILinkedLayer IPsdLayer.LinkedLayer
        //{
        //    get { return this.LinkedLayer; }
        //}

        IChannel[] IImageSource.Channels
        {
            get { return this.channels.Value; }
        }

        IPsdLayer[] IPsdLayer.Childs
        {
            get { return this.Childs; }
        }

        #endregion


        
    }
}

