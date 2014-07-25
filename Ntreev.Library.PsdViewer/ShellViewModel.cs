using Ntreev.Library.PsdParser;
using Ntreev.Library.PsdViewer.ViewModels;
using System.Collections;
using System.Collections.Generic;
namespace Ntreev.Library.PsdViewer
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {
        private readonly PSD psd = new PSD();
        private List<TreeViewItemViewModel> itemsSource = new List<TreeViewItemViewModel>();

        public ShellViewModel()
        {
            psd.Read(@"D:\Users\s2quake\Documents\New Unity Project 4\Assets\sprite_test.psd");
            //psd.loadHeader(@"D:\Users\s2quake\Documents\New Unity Project 4\Assets\game login process_140716.psd");
            //psd.loadHeader(@"D:\Users\s2quake\Documents\GW 받은 파일\holecup's height.psd");
            

            //psd.loadData();

            //var dsdd  = psd.layerInfo.layers[8].props["TypeToolInfo.Transforms"];

            this.itemsSource.Add(new PSDItemViewModel(psd));
            
        }

        public IEnumerable ItemsSource
        {
            get { return this.itemsSource; }
        }

        public void Setting()
        {
            int qwer = 0;
        }

        public void Wow()
        {
            int qwer = 0;
        }
    }
}