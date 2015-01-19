using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

            var bmp = this.document.GetBitmap();
            if (bmp != null)
            {
                PngBitmapEncoder d = new PngBitmapEncoder();
                d.Frames.Add(BitmapFrame.Create(bmp));
                string n = Regex.Replace("root", "[\\\\/:*?\"<>|]", "_");
                using (FileStream stream = new FileStream(n + ".png", FileMode.Create))
                {
                    d.Save(stream);
                }
            }

            //foreach (var item in document.Properties)
            //{
            //    if (item.Value is IProperties == true)
            //    {
            //        this.Children.Add(new PropertiesItemViewModel(item.Key, item.Value as IProperties, this));
            //    }
            //    else
            //    {
            //        this.Children.Add(new PropertiesItemViewModel(item.Key, item.Value, this));
            //    }
            //}
        }

        public override string Text
        {
            get { return "Root"; }
        }
    }
}
