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
        private LayerInfoReader layerInfo;
        private GlobalLayerMaskInfoReader globalLayerMask;
        private AdditionalLayerInformationReader additionalLayerInformation;

        public LayerAndMaskInformationSection(PsdReader reader,  PsdDocument document, long end)
        {
            this.layerInfo = new LayerInfoReader(reader, document);
            this.globalLayerMask = new GlobalLayerMaskInfoReader(reader);
            this.additionalLayerInformation = new AdditionalLayerInformationReader(reader, end - reader.Position, document.BaseUri);
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
