using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerInfoReader : LazyValueReader<PsdLayer[]>
    {
        public LayerInfoReader(PsdReader reader, PsdDocument document)
            : base(reader, document)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out PsdLayer[] value)
        {
            PsdDocument document = userData as PsdDocument;
            int layerCount = Math.Abs((int)reader.ReadInt16());

            PsdLayer[] layers = new PsdLayer[layerCount];
            for (int i = 0; i < layerCount; i++)
            {
                layers[i] = new PsdLayer(reader, document);
            }

            foreach (var item in layers)
            {
                item.ReadChannels(reader);
            }

            layers = Initialize(null, layers);

            foreach (var item in layers.SelectMany(item => item.Descendants()).Reverse())
            {
                item.ComputeBounds();
            }

            value = layers;
        }

        public static PsdLayer[] Initialize(PsdLayer parent, PsdLayer[] layers)
        {
            Stack<PsdLayer> stack = new Stack<PsdLayer>();
            List<PsdLayer> rootLayers = new List<PsdLayer>();
            Dictionary<PsdLayer, List<PsdLayer>> layerToChilds = new Dictionary<PsdLayer, List<PsdLayer>>();

            foreach (var item in layers.Reverse())
            {
                if (item.SectionType == SectionType.Divider)
                {
                    parent = stack.Pop();
                    continue;
                }

                if (parent != null)
                {
                    if (layerToChilds.ContainsKey(parent) == false)
                    {
                        layerToChilds.Add(parent, new List<PsdLayer>());
                    }

                    List<PsdLayer> childs = layerToChilds[parent];
                    childs.Insert(0, item);
                    item.Parent = parent;
                }
                else
                {
                    rootLayers.Insert(0, item);
                }

                if (item.SectionType == SectionType.Opend || item.SectionType == SectionType.Closed)
                {
                    stack.Push(parent);
                    parent = item;
                }
            }

            foreach (var item in layerToChilds)
            {
                item.Key.Childs = item.Value.ToArray();
            }

            return rootLayers.ToArray();
        }
    }
}

