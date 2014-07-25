using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Globalization;


namespace Ntreev.Library.PsdViewer.Converters
{
    /// <summary>
    /// Convert Level to left margin
    /// Pass a prarameter if you want a unit length other than 19.0.
    /// </summary>
    public class LevelToIndentConverter : IValueConverter
    {
        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            return new Thickness((int)o * c_IndentSize, 0, 0, 0);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private const double c_IndentSize = 19.0;
    }
}
