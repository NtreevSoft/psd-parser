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
using Ntreev.ModernUI.Framework.ViewModels;
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

        public LinkedLayerItemViewModel(ILinkedLayer linkedLayer, PSDItemViewModel parent)
        {
            this.linkedLayer = linkedLayer;

            this.Items.Add(new PSDItemViewModel(linkedLayer.Document));
            //foreach (var item in linkedLayer.Document.Childs)
            //{
            //    this.Children.Add(new LayerItemViewModel(item, parent));
            //}

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

        public override string DisplayName
        {
            get
            {
                return string.Format("LinkedLayer({0})", this.linkedLayer.Name);
            }
        }
    }
}
