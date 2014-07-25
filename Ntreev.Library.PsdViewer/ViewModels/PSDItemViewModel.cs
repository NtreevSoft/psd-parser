using Ntreev.Library.PsdParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class PSDItemViewModel : TreeViewItemViewModel
    {
        private readonly PSD psd;

        public PSDItemViewModel(PSD psd)
            : base(null)
        {
            this.psd = psd;
            foreach (var item in psd.Layers)
            {
                this.Children.Add(new LayerItemViewModel(item, this));
            }
        }

        public override string Text
        {
            get { return "Root"; }
        }
    }
}
