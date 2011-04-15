using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace dotnetpro.WPF.TableReport
{
    [ValueConversion(typeof(bool?), typeof(bool))]
    public class RadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool param = bool.Parse(parameter.ToString());
            if (value == null)
            {
                return false;
            }
            else
            {
                return !((bool)value ^ param);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool param = bool.Parse(parameter.ToString());
            return !((bool)value ^ param);
        }
    }
}
