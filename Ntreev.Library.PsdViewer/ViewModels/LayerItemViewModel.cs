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
    class LayerItemViewModel : TreeViewItemViewModel
    {
        private readonly IPsdLayer layer;
        static int i = 0;

        public LayerItemViewModel(IPsdLayer layer, PSDItemViewModel parent)
            : base(parent)
        {
            this.layer = layer;

            foreach (var item in layer.Childs)
            {
                this.Children.Add(new LayerItemViewModel(item, parent));
            }

            if(layer.LinkedLayer != null)
            {
                this.Children.Add(new LinkedLayerItemViewModel(layer.LinkedLayer, parent));
            }

            if (this.layer.Name == "Message")
            {
                //IProperties textProps = this.layer.Properties["Resources.TySh.Text"] as IProperties;
                //var d = (double)textProps["bounds.Top.Value"];
                //double dl = (double)textProps["bounds.Left.Value"];
            }

            //var bmp = this.layer.GetBitmap();
            //if (bmp != null)
            //{
            //    PngBitmapEncoder d = new PngBitmapEncoder();
            //    d.Frames.Add(BitmapFrame.Create(bmp));
            //    string n = Regex.Replace(this.layer.Name, "[\\\\/:*?\"<>|]", "_");
            //    using (FileStream stream = new FileStream(n + ".png", FileMode.Create))
            //    {
            //        d.Save(stream);
            //    }
            //}

            foreach (var item in layer.Properties)
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
