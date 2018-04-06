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
using Ntreev.Library.PsdViewer.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System;
using System.ComponentModel.Composition;
using Ntreev.ModernUI.Framework;
using Ntreev.ModernUI.Framework.ViewModels;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : ScreenBase, IShell
    {
        private List<TreeViewItemViewModel> itemsSource;
        private string filename;

        public ShellViewModel()
        {
            this.DisplayName = "Photoshop File Viewer";
        }

        public IEnumerable ItemsSource
        {
            get { return this.itemsSource; }
        }

        public void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "psd files (*.psd)|*.psd|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                this.RefreshFile(dlg.FileName);
            }
        }

        public void RefreshFile()
        {
            this.RefreshFile(this.filename);
        }

        public bool CanRefresh
        {
            get
            {
                return File.Exists(this.filename);
            }
        }

        public string Title
        {
            get
            {
                string title = "PsdViewer";
                if (string.IsNullOrEmpty(this.filename) == true)
                    return title;
                return string.Format("{0} - {1}", title, Path.GetFileName(this.filename));
            }
        }

        private void Test()
        {
            string filename = string.Empty;
            using (PsdDocument document = PsdDocument.Create(filename))
            {
                foreach (var item in document.Childs)
                {
                    Console.WriteLine("LayerName : " + item.Name);
                }
            }
        }

        private void RefreshFile(string filename)
        {
            this.BeginProgress();
            try
            {
                PsdDocument document = PsdDocument.Create(filename);

                this.filename = filename;
                this.itemsSource = new List<TreeViewItemViewModel>();
                this.itemsSource.Add(new PSDItemViewModel(document));
                this.NotifyOfPropertyChange(() => this.ItemsSource);
                this.NotifyOfPropertyChange(() => this.CanRefresh);
                this.NotifyOfPropertyChange(() => this.Title);
            }
            finally
            {
                this.EndProgress();
            }
        }

        private static IEnumerable<string> Recursive(Ntreev.Library.Psd.IPsdLayer layer)
        {
            if (layer.LinkedLayer != null)
            {
                if (layer.LinkedLayer.AbsoluteUri != null)
                    yield return layer.LinkedLayer.Name;
                foreach (var item in Recursive(layer.LinkedLayer.Document))
                {
                    yield return item;
                }
            }

            foreach (var item in layer.Childs)
            {
                foreach (var i in Recursive(item))
                {
                    yield return i;
                }
            }
        }
    }
}