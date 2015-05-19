using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lnkE")]
    class Reader_lnkE : ResourceReaderBase
    {
        public Reader_lnkE(PsdReader reader, long length)
            : base(reader, length)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            List<EmbeddedLayer> linkedLayers = new List<EmbeddedLayer>();
            while (reader.Position < this.EndPosition)
            {
                linkedLayers.Add(new EmbeddedLayer(reader, this.baseUri));
            }

            props["Items"] = linkedLayers.ToArray();
            value = props;
        }
    }
}
