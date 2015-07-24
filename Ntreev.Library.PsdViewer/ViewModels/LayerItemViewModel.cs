#region License
//Ntreev Photoshop Document Parser for .Net
//
//Released under the MIT License.
//
//Copyright (c) 2015 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

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


            var c = this.layer.Channels;
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

            this.Children.Add(new PropertiesItemViewModel("Resources", layer.Resources, this));

            //this.Children.Add(new PropertiesItemViewModel("ID", layer.id, this));
            this.Children.Add(new PropertiesItemViewModel("Name", layer.Name, this));
            //this.Children.Add(new PropertiesItemViewModel("SectionType", layer.sectionType, this));
            this.Children.Add(new PropertiesItemViewModel("BlendMode", layer.BlendMode, this));
            this.Children.Add(new PropertiesItemViewModel("Opacity", layer.Opacity, this));
            this.Children.Add(new PropertiesItemViewModel("Left", layer.Left, this));
            this.Children.Add(new PropertiesItemViewModel("Top", layer.Top, this));
            this.Children.Add(new PropertiesItemViewModel("Right", layer.Right, this));
            this.Children.Add(new PropertiesItemViewModel("Bottom", layer.Bottom, this));
            this.Children.Add(new PropertiesItemViewModel("Width", layer.Width, this));
            this.Children.Add(new PropertiesItemViewModel("Height", layer.Height, this));
            this.Children.Add(new PropertiesItemViewModel("IsClipping", layer.IsClipping, this));
            

            //foreach (var item in layer.Resources)
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
            get { return this.layer.Name; }
        }
    }
}
