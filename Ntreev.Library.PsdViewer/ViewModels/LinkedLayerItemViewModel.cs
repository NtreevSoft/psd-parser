using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            //var bmp = this.linkedLayer.Document.GetBitmap();
            //if (bmp != null)
            //{
            //    PngBitmapEncoder d = new PngBitmapEncoder();
            //    d.Frames.Add(BitmapFrame.Create(bmp));
            //    string n = Regex.Replace(this.linkedLayer.FileName, "[\\\\/:*?\"<>|]", "_");
            //    using (FileStream stream = new FileStream(n + ".png", FileMode.Create))
            //    {
            //        d.Save(stream);
            //    }
            //}

        }

        public override string Text
        {
            get
            {
                return string.Format("LinkedLayer({0})", this.linkedLayer.FileName);
            }
        }
    }
}
