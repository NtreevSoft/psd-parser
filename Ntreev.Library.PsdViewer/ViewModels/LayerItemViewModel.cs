using Ntreev.Library.PsdParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class LayerItemViewModel : TreeViewItemViewModel
    {
        private readonly PSDLayer layer;

        public LayerItemViewModel(PSDLayer layer, PSDItemViewModel parent)
            : base(parent)
        {
            this.layer = layer;

            foreach (var item in layer)
            {
                if (item.Value is IProperties == true)
                {
                    this.Children.Add(new PropertiesItemViewModel(item.Key, item.Value as IProperties, this));
                }
                else
                {
                    this.Children.Add(new PropertiesItemViewModel(item.Key, item.Value, this));
                }
            }
        }

        public override string Text
        {
            get { return this.layer.Name; }
        }
    }
}
