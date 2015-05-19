using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LayerExtraRecords
    {
        private readonly LayerMaskReader layerMask;
        private readonly LayerBlendingRangesReader blendingRanges;
        private readonly LayerResourceReader resources;
        private readonly string name;
        private SectionType sectionType;
        private Guid placedID;

        public LayerExtraRecords(LayerMaskReader layerMask, LayerBlendingRangesReader blendingRanges, LayerResourceReader resources, string name)
        {
            this.layerMask = layerMask;
            this.blendingRanges = blendingRanges;
            this.resources = resources;
            this.name = name;

            this.resources.TryGetValue<string>(ref this.name, "luni.Name");
            this.resources.TryGetValue<SectionType>(ref this.sectionType, "lsct.SectionType");

            if (this.resources.Contains("SoLd.Idnt") == true)
                this.placedID = this.resources.ToGuid("SoLd.Idnt");
            else if (this.resources.Contains("SoLE.Idnt") == true)
                this.placedID = this.resources.ToGuid("SoLE.Idnt");
        }

        public SectionType SectionType
        {
            get { return this.sectionType; }
        }

        public Guid PlacedID
        {
            get { return this.placedID; }
        }

        //private LayerMaskReader layerMask;
        //private LayerBlendingRangesReader blendingRanges;
        //private LayerResourceReader resources;

        //private int channelCount;

        //public void ValidateSize()
        //{
        //    int width = this.Right - Left;
        //    int height = this.Bottom - this.Top;

        //    if ((width > 0x3000) || (height > 0x3000))
        //    {
        //        throw new NotSupportedException(string.Format("Invalidated size ({0}, {1})", width, height));
        //    }
        //}

        //public int Left { get; set; }

        //public int Top { get; set; }

        //public int Right { get; set; }

        //public int Bottom { get; set; }

        //public int Width
        //{
        //    get { return this.Right - this.Left; }
        //}

        //public int Height
        //{
        //    get { return this.Bottom - this.Top; }
        //}

        //public int ChannelCount
        //{
        //    get { return this.channelCount; }
        //    set
        //    {
        //        if (value > 0x38)
        //        {
        //            throw new Exception(string.Format("Too many channels : {0}", channelCount));
        //        }
        //        this.channelCount = value;
        //    }
        //}

        //public ChannelType[] ChannelTypes { get; set; }

        //public long[] ChannelSizes { get; set; }

        //public BlendMode BlendMode { get; set; }

        //public byte Opacity { get; set; }

        //public bool Clipping { get; set; }

        //public LayerFlags Flags { get; set; }

        //public int Filter { get; set; }

        public string Name
        {
            get { return this.name; }
        }

        public LayerMask Mask
        {
            get { return this.layerMask.Value; }
        }

        public object BlendingRanges
        {
            get { return this.blendingRanges.Value; }
        }

        public IProperties Resources
        {
            get { return this.resources.Value; }
        }

        //public LayerMask Mask
        //{
        //    get { return this.layerMask.Value; }
        //}

        //public object BlendingRanges
        //{
        //    get { return this.blendingRanges.Value; }
        //}

        //public IProperties Resources
        //{
        //    get { return this.resources.Value; }
        //}

        //public void ReadVariable(PsdReader reader)
        //{
        //    long extraSize = reader.ReadUInt32();
        //    long end = reader.Position + extraSize;

        //    long position = reader.Position;
        //    this.layerMask = new LayerMaskReader(reader);
        //    this.blendingRanges = new LayerBlendingRangesReader(reader);
        //    this.Name = reader.ReadPascalString(4);
        //    this.resources = new LayerResourceReader(reader, end - reader.Position);
        //}
    }
}
