using System;
using System.Globalization;
using System.Windows.Data;

namespace Nubis.Core.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
                return decimal.Parse(value as string);
            if( value is decimal)
                return (decimal) value;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
