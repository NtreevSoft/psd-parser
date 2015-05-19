using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LayerAndMaskInformationSection
    {
        private readonly LayerInfoReader layerInfo;
        private readonly GlobalLayerMaskInfoReader globalLayerMask;
        private readonly AdditionalLayerInformationReader additionalLayerInformation;

        public LayerAndMaskInformationSection(LayerInfoReader layerInfo, GlobalLayerMaskInfoReader globalLayerMask, AdditionalLayerInformationReader additionalLayerInformation)
        {
            this.layerInfo = layerInfo;
            this.globalLayerMask = globalLayerMask;
            this.additionalLayerInformation = additionalLayerInformation;
        }

        public PsdLayer[] Layers
        {
            get { return this.layerInfo.Value; }
        }

        public LinkedLayer[] LinkedLayers
        {
            get { return this.additionalLayerInformation.Value.LinkedLayers; }
        }
    }
}
