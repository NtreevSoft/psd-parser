using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer
{
    partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            this.InitializeComponent();
        }

        private void Setting_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int qwer = 0;

        }

        private void OpenFile_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Caliburn.Micro.Bind.SetModel(sender as System.Windows.DependencyObject, e.NewValue);
        }
    }
}
