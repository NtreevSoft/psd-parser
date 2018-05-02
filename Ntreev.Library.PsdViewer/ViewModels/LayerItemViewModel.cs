//Released under the MIT License.
//
//Copyright (c) 2018 Ntreev Soft co., Ltd.
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

using Ntreev.Library.Linq;
using Ntreev.Library.Psd;
using Ntreev.Library.PsdViewer.Dialogs.ViewModels;
using Ntreev.ModernUI.Framework;
using Ntreev.ModernUI.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class LayerItemViewModel : TreeViewItemViewModel
    {
        private readonly IPsdLayer layer;
        private readonly ICommand previewCommand;

        public LayerItemViewModel(IPsdLayer layer, PSDItemViewModel parent)
        {
            this.layer = layer;

            foreach (var item in layer.Childs)
            {
                this.Items.Add(new LayerItemViewModel(item, parent));
            }

            if (layer.LinkedLayer != null)
            {
                this.Items.Add(new LinkedLayerItemViewModel(layer.LinkedLayer, parent));
            }

            this.Items.Add(new PropertiesItemViewModel("Resources", layer.Resources, this));
            this.previewCommand = new DelegateCommand((p) => this.Preview(), (p) => this.CanPreview);
        }

        public void Save(string filename)
        {
            var bitmap = this.layer.GetBitmap();
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }
        }

        public PSDItemViewModel GetDocumentViewModel()
        {
            var items = EnumerableUtility.Ancestors<TreeViewItemViewModel>(this, item => item.Parent);
            foreach (var item in items)
            {
                if (item is PSDItemViewModel viewModel)
                {
                    return viewModel;
                }
            }
            return null;
        }

        public void Preview()
        {
            var dialog = new PreviewViewModel(this.layer);
            dialog.ShowDialog();
        }

        public bool CanPreview
        {
            get { return true; }
        }

        public bool HasLinkedLayer
        {
            get
            {
                return this.layer.LinkedLayer != null;
            }
        }

        public IPsdLayer Layer
        {
            get
            {
                return this.layer;
            }
        }

        public override ICommand DefaultCommand => this.previewCommand;

        public override string DisplayName
        {
            get { return this.layer.Name; }
        }

        public object Value => null;

        public string Type => null;
    }
}
