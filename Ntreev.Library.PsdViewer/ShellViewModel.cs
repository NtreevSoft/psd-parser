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
using Ntreev.Library.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;
using Ntreev.Library.IO;

namespace Ntreev.Library.PsdViewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : TreeViewViewModel, IShell
    {
        private string filename;
        private ICommand openFileCommand;

        public ShellViewModel()
        {
            this.DisplayName = "Photoshop File Viewer";
            this.openFileCommand = new DelegateCommand((p) => this.OpenFile(p as string), (p) => this.CanOpenFile);
        }

        public void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "psd files (*.psd)|*.psd|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                this.OpenFile(dlg.FileName);
            }
        }

        public void Export()
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.Export(dialog.FileName);
            }
        }

        public bool CanOpenFile
        {
            get
            {
                if (string.IsNullOrEmpty(this.filename) == false)
                    return false;
                return true;
            }
        }

        public bool CanExport
        {
            get
            {
                if (string.IsNullOrEmpty(this.filename) == true)
                    return false;
                if (this.IsProgressing == true)
                    return false;
                return true;
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

        public ICommand OpenFileCommand
        {
            get { return this.openFileCommand; }
        }

        protected override void OnProgress()
        {
            base.OnProgress();
            this.NotifyOfPropertyChange(nameof(this.CanExport));
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

        private void OpenFile(string filename)
        {
            this.BeginProgress();
            try
            {
                PsdDocument document = PsdDocument.Create(filename);

                this.filename = filename;
                this.Items.Clear();
                this.Items.Add(new PSDItemViewModel(document));
                this.NotifyOfPropertyChange(() => this.Items);
                this.NotifyOfPropertyChange(() => this.CanOpenFile);
                this.NotifyOfPropertyChange(() => this.CanExport);
                this.NotifyOfPropertyChange(() => this.Title);
            }
            finally
            {
                this.EndProgress();
            }
        }

        private void Export(string path)
        {
            var items = EnumerableUtility.FamilyTree(this.Items, item => item.Items);
            var layerList = new Dictionary<IPsdLayer, string>();
            foreach (var item in items)
            {
                if (item is LayerItemViewModel layerViewModel && layerViewModel.HasLinkedLayer == false)
                {
                    var layer = layerViewModel.Layer;
                    if (layerList.ContainsKey(layer) == true)
                        continue;
                    var documentViewModel = layerViewModel.GetDocumentViewModel();
                    if (documentViewModel != null)
                    {
                        if (documentViewModel.Parent is LinkedLayerItemViewModel linkedLayer)
                        {
                            layerList.Add(layer, linkedLayer.LinkedLayer.Name);
                        }
                        else
                        {
                            layerList.Add(layer, string.Empty);
                        }
                    }
                }
            }

            var items1 = layerList.Distinct().ToArray();
            foreach (var item in items1)
            {
                var layer = item.Key;
                var name = item.Value;
                var filename = name == string.Empty ? Path.Combine(path, layer.Name + ".png") : Path.Combine(path, name, layer.Name + ".png");
                var bitmap = item.Key.GetBitmap();
                FileUtility.Prepare(filename);
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(stream);
                }

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