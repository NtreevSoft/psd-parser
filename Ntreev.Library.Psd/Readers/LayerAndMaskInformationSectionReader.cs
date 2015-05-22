using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers
{
    class LayerAndMaskInformationSectionReader : LazyValueReader<LayerAndMaskInformationSection>
    {
        public LayerAndMaskInformationSectionReader(PsdReader reader, PsdDocument document)
            : base(reader, document)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out LayerAndMaskInformationSection value)
        {
            PsdDocument document = userData as PsdDocument;

            LayerInfoReader layerInfo = new LayerInfoReader(reader, document);

            if (reader.Position + 4 >= this.EndPosition)
            {
                value = new LayerAndMaskInformationSection(layerInfo, null, new Properties());
            }
            else
            {
                GlobalLayerMaskInfoReader globalLayerMask = new GlobalLayerMaskInfoReader(reader);
                DocumentResourceReader documentResource = new DocumentResourceReader(reader, this.EndPosition - reader.Position);

                value = new LayerAndMaskInformationSection(layerInfo, globalLayerMask, documentResource);
            }
        }
    }
}

