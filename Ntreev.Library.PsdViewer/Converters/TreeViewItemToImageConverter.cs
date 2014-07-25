using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Ntreev.Library.PsdViewer.Converters
{
    //public class TreeViewItemToImageConverter : IValueConverter
    //{
    //    private static BitmapImage storage;
    //    private static BitmapImage folder_c;
    //    private static BitmapImage folder_o;
    //    private static BitmapImage file;

    //    static TreeViewItemToImageConverter()
    //    {
    //        storage = BitmapLoader.LoadBitmapFromResource("Images/storage.png");
    //        folder_c = BitmapLoader.LoadBitmapFromResource("Images/folder_c.png");
    //        folder_o = BitmapLoader.LoadBitmapFromResource("Images/folder_o.png");
    //        file = BitmapLoader.LoadBitmapFromResource("Images/file.png");
    //    }

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is StorageTreeViewItemViewModel == true)
    //        {
    //            return storage;
    //        }
    //        else if (value is FolderTreeViewItemViewModel == true)
    //        {
    //            FolderTreeViewItemViewModel treeViewItem = value as FolderTreeViewItemViewModel;
    //            if (treeViewItem.IsExpanded == true)
    //                return folder_o;
    //            return folder_c;
    //        }
    //        return file;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}