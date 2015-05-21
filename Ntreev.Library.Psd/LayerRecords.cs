using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LayerRecords
    {
        private Channel[] channels;

        private LayerMask layerMask;
        private LayerBlendingRanges blendingRanges;
        private IProperties resources;
        private string name;
        private SectionType sectionType;
        private Guid placedID;

        public void SetExtraRecords(LayerMask layerMask, LayerBlendingRanges blendingRanges, IProperties resources, string name)
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


            foreach (var item in this.channels)
            {
                switch (item.Type)
                {
                    case ChannelType.Mask:
                        {
                            if (this.layerMask != null)
                            {
                                item.Width = this.layerMask.Width;
                                item.Height = this.layerMask.Height;
                            }
                        }
                        break;
                    case ChannelType.Alpha:
                        {
                            if (this.resources.Contains("iOpa") == true)
                            {
                                byte opa = this.resources.ToByte("iOpa", "Opacity");
                                item.Opacity = opa / 255.0f;
                            }
                        }
                        break;
                }
            }
        }

        public void ValidateSize()
        {
            int width = this.Right - Left;
            int height = this.Bottom - this.Top;

            if ((width > 0x3000) || (height > 0x3000))
            {
                throw new NotSupportedException(string.Format("Invalidated size ({0}, {1})", width, height));
            }
        }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public int Width
        {
            get { return this.Right - this.Left; }
        }

        public int Height
        {
            get { return this.Bottom - this.Top; }
        }

        public int ChannelCount
        {
            get 
            {
                if (this.channels == null)
                    return 0;
                return this.channels.Length; 
            }
            set
            {
                if (value > 0x38)
                {
                    throw new Exception(string.Format("Too many channels : {0}", value));
                }

                this.channels = new Channel[value];
                for (int i = 0; i < value; i++)
                {
                    this.channels[i] = new Channel();
                }
            }
        }

        public Channel[] Channels
        {
            get { return this.channels; }
        }

        public BlendMode BlendMode { get; set; }

        public byte Opacity { get; set; }

        public bool Clipping { get; set; }

        public LayerFlags Flags { get; set; }

        public int Filter { get; set; }

        public long ChannelSize
        {
            get { return this.channels.Select(item => item.Size).Aggregate((v, n) => v + n); }
        }

        public SectionType SectionType
        {
            get { return this.sectionType; }
        }

        public Guid PlacedID
        {
            get { return this.placedID; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public LayerMask Mask
        {
            get { return this.layerMask; }
        }

        public object BlendingRanges
        {
            get { return this.blendingRanges; }
        }

        public IProperties Resources
        {
            get { return this.resources; }
        }
    }
}
