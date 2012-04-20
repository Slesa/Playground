using System;
using System.Globalization;
using System.Windows.Data;

namespace Converter
{
    // To add special currency functionality, I'd rather like to call it CurrencyConverter.
    // Including currency sign, return type is string, decimal/thousand sign and so on
    [ValueConversion(typeof(string), typeof(double))]
    public class RateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var input = (string) value;
                var toConvert = Double.Parse(input/*.Replace(',','.')*/, culture);

                var rate = (double)parameter;
                return toConvert * rate;
            }
            catch
            {
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rate = (double) parameter;
            return Convert(value,targetType,1/rate,culture);
        }
    }
}
