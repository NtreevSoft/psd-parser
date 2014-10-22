using Ntreev.Library.PsdParser;
using Ntreev.Library.PsdViewer.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Linq;
namespace Ntreev.Library.PsdViewer
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {
        private bool a = true;
        private readonly PSD psd = new PSD();
        private List<TreeViewItemViewModel> itemsSource = new List<TreeViewItemViewModel>();

        public ShellViewModel()
        {
            //psd.Read(@"D:\Users\s2quake\Documents\New Unity Project 4\Assets\sprite_test.psd");

            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\CharacterInfo.psd");
            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\CaddieInfo.psd");
            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\lobby_main.psd");
            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\StageSelect.psd");
            //psd.Read(@"D:\Users\s2quake\Documents\GW 받은 파일\lobby03_flat ver-4.psd");

            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\Progress.psd");
            psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\test.psd");
            //psd.Read(@"D:\test.psd");
            
            //psd.Read(@"D:\게임시작버튼.psd");
            //psd.Read(@"D:\Project_PangyaM\PMGame\Assets\GUI Sources\TopMenu2.psd");


            //psd.loadHeader(@"D:\Users\s2quake\Documents\New Unity Project 4\Assets\game login process_140716.psd");
            //psd.loadHeader(@"D:\Users\s2quake\Documents\GW 받은 파일\holecup's height.psd");
            //return;
            //{
            //    var bmp = psd.GetBitmap();
            //    PngBitmapEncoder d = new PngBitmapEncoder();
            //    d.Frames.Add(BitmapFrame.Create(bmp));
            //    using (FileStream stream = new FileStream("main.png", FileMode.Create))
            //    {
            //        d.Save(stream);
            //    }
            //}

            //var dsrs = psd.LinkedLayers.Select(item => item.FileName).ToArray();

            //foreach (var item in psd.LinkedLayers)
            //{
            //    if (item.PSD == null)
            //        continue;

            //    var bmp = item.PSD.GetBitmap();
            //    if (bmp != null)
            //    {
            //        PngBitmapEncoder d = new PngBitmapEncoder();
            //        d.Frames.Add(BitmapFrame.Create(bmp));
            //        string path = item.FileName.Replace("/", "_").Replace(":", "_").Replace("?", "_").Replace("=", "_");
            //        using (FileStream stream = new FileStream(path + ".png", FileMode.Create))
            //        {
            //            d.Save(stream);
            //        }
            //    }
            //}


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