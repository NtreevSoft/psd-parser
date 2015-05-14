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
namespace Ntreev.Library.PsdViewer
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        private List<TreeViewItemViewModel> itemsSource;
        private string filename;

        public ShellViewModel()
        {

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

        private void RefreshFile(string filename)
        {
            PsdDocument document = new PsdDocument();
            var t = DateTime.Now;
            document.Read(filename);
            var ddd = recursive(document).Distinct().ToArray();
            string text = string.Join(System.Environment.NewLine, recursive(document).ToArray());


            var time = DateTime.Now - t;
            Console.WriteLine(text);
            Console.WriteLine(time);
            this.filename = filename;
            this.itemsSource = new List<TreeViewItemViewModel>();
            this.itemsSource.Add(new PSDItemViewModel(document));
            this.NotifyOfPropertyChange(() => this.ItemsSource);
            this.NotifyOfPropertyChange(() => this.CanRefresh);
            this.NotifyOfPropertyChange(() => this.Title);
        }

        private static IEnumerable<string> recursive(Ntreev.Library.Psd.IPsdLayer layer)
        {
            if (layer.LinkedLayer != null)
            {
                if(layer.LinkedLayer.AbsoluteUri != null)
                    yield return layer.LinkedLayer.Name;
                foreach (var item in recursive(layer.LinkedLayer.Document))
                {
                    yield return item;
                }
            }

            foreach (var item in layer.Childs)
            {
                foreach (var i in recursive(item))
                {
                    yield return i;
                }
            }
        }
    }
}