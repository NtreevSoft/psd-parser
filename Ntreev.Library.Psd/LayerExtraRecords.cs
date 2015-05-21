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
    }
}
