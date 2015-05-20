using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lnkD")]
    class Reader_lnkD : ResourceReaderBase
    {
        public Reader_lnkD(PsdReader reader, long length)
            : base(reader, length)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();
            List<LinkedLayer> linkedLayers = new List<LinkedLayer>();
            while (reader.Position < this.EndPosition)
            {
                LinkedLayerReader r = new LinkedLayerReader(reader);
                linkedLayers.Add(r.Value);
            }

            props["Items"] = linkedLayers.ToArray();
            value = props;
        }
    }
}
