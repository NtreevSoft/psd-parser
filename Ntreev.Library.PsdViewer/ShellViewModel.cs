using Ntreev.Library.Psd;
using Ntreev.Library.PsdViewer.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Linq;
using Microsoft.Win32;
namespace Ntreev.Library.PsdViewer
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        private List<TreeViewItemViewModel> itemsSource;

        public ShellViewModel()
        {
            this.Refresh(@"C:\Users\S2QUAKE\Desktop\321.psd");

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
                this.Refresh(dlg.FileName);
            }
        }

        private void Refresh(string filename)
        {
            Psd psd = new Psd();
            psd.Read(filename);
            this.itemsSource = new List<TreeViewItemViewModel>();
            this.itemsSource.Add(new PSDItemViewModel(psd));
            this.NotifyOfPropertyChange(() => this.ItemsSource);
        }
    }

}