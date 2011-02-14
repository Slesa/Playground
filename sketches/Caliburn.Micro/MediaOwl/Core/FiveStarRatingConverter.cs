using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IValueConverter"/> which converts a <see cref="double"/>-Rating from 0 to 5 to a <see cref="double"/>-Rating from 0 to 1.
    /// </summary>
    public class FiveStarRatingConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        /// <summary>
        /// Convertst to 20 % of the initial value
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dbl = System.Convert.ToDouble(value);
            if (dbl > 0)
                return dbl / 5;
            return dbl;
        }

        /// <summary>
        /// Converts to 500 % of the initial value
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dbl = System.Convert.ToDouble(value);
            if (dbl > 0)
                return dbl * 5;
            return dbl;
        }

        #endregion
    }
}
