using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Lucifer.Caliburn.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : new BitmapImage(new Uri(value.ToString(), UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmap = value as BitmapImage;
            return bitmap!=null ? bitmap.UriSource.ToString() : null;
            /*if (value is BitmapImage)
            {
                return ((BitmapImage)value).UriSource.ToString();
            }*/
        }
    }
}