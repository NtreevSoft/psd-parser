using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers
{
    class LayerAndMaskInformationSectionReader : LazyReadableValue<LayerAndMaskInformationSection>
    {
        private readonly PsdDocument document;

        public LayerAndMaskInformationSectionReader(PsdReader reader, PsdDocument document)
            : base(reader, true)
        {
            this.document = document;
        }

        protected override void ReadValue(PsdReader reader, out LayerAndMaskInformationSection value)
        {
            value = new LayerAndMaskInformationSection(reader, this.document, this.Position + this.Length);
        }
    }
}

