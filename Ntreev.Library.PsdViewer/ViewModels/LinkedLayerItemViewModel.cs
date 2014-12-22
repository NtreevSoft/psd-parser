using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class LinkedLayerItemViewModel : TreeViewItemViewModel
    {
        private readonly ILinkedLayer linkedLayer;

        static int i = 0;

        public LinkedLayerItemViewModel(ILinkedLayer linkedLayer, PSDItemViewModel parent)
            : base(parent)
        {
            this.linkedLayer = linkedLayer;

            foreach (var item in linkedLayer.Document.Childs)
            {
                this.Children.Add(new LayerItemViewModel(item, parent));
            }


        }

        public override string Text
        {
            get
            {
                return string.Format("LinkedLayer({0})", this.linkedLayer.Document.Name);
            }
        }
    }
}
