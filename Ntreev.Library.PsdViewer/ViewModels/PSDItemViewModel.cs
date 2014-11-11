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
        private readonly PsdDocument document;

        public PSDItemViewModel(PsdDocument document)
            : base(null)
        {
            this.document = document;
            foreach (var item in document.Childs)
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
