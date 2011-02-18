using System;
using System.Globalization;
using System.Windows.Data;
using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }

    public class BoolVisibilityInvertConverter : IValueConverter
    {
        readonly BooleanToVisibilityConverter _btvConverter = new BooleanToVisibilityConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _btvConverter.Convert(!((bool) value), targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _btvConverter.ConvertBack(!((bool)value), targetType, parameter, culture);
        }
    }
}