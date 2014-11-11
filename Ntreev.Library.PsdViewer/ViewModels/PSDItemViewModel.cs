using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class PSDItemViewModel : TreeViewItemViewModel
    {
        private readonly Psd psd;

        public PSDItemViewModel(Psd psd)
            : base(null)
        {
            this.psd = psd;
            foreach (var item in psd.Childs)
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
