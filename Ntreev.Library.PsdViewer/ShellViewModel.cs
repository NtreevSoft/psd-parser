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

        public ShellViewModel()
        {
            System.Security.Cryptography.SHA1 md5 = System.Security.Cryptography.SHA1.Create();
            //System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            using (FileStream sr = File.OpenRead(@"C:\Users\S2QUAKE\Documents\NPF\GUIResources\TeamsView.psd"))
            {
                byte[] data = md5.ComputeHash(sr);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                string id = sb.ToString();
            }

            //this.Refresh(@"C:\Users\S2QUAKE\Desktop\TopMenuView.psd");
            //this.Refresh(@"C:\Users\S2QUAKE\Documents\NPF\GUIResources\Icons.psd");
            this.Refresh(@"C:\Users\S2QUAKE\Documents\NPF\GUIResources\TeamManagementView.psd");
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
            PsdDocument document = new PsdDocument();
            document.Read(filename);
            this.itemsSource = new List<TreeViewItemViewModel>();
            this.itemsSource.Add(new PSDItemViewModel(document));
            this.NotifyOfPropertyChange(() => this.ItemsSource);
        }
    }

}