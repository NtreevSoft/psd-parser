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
        private readonly IProperties documentResources;

        private ILinkedLayer[] linkedLayers;

        public LayerAndMaskInformationSection(LayerInfoReader layerInfo, GlobalLayerMaskInfoReader globalLayerMask, IProperties documentResources)
        {
            this.layerInfo = layerInfo;
            this.globalLayerMask = globalLayerMask;
            this.documentResources = documentResources;
        }

        public PsdLayer[] Layers
        {
            get { return this.layerInfo.Value; }
        }

        public ILinkedLayer[] LinkedLayers
        {
            get
            {
                if (this.linkedLayers == null)
                {
                    List<ILinkedLayer> list = new List<ILinkedLayer>();
                    string[] ids = { "lnk2", "lnk3", "lnkD", "lnkE", };

                    foreach (var item in ids)
                    {
                        if (this.documentResources.Contains(item))
                        {
                            var items = this.documentResources.ToValue<ILinkedLayer[]>(item, "Items");
                            list.AddRange(items);
                        }
                    }
                    this.linkedLayers = list.ToArray();
                }
                return this.linkedLayers;
            }
        }

        public IProperties Resources
        {
            get { return this.documentResources; }
        }
    }
}
