using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IValueConverter"/> which converts an Url-String to an <see cref="BitmapImage"/>.
    /// </summary>
    public class ImageSourceConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Takes a string value and converts to a <see cref="BitmapImage"/>.
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return new BitmapImage(new Uri(value.ToString(), UriKind.Absolute));
        }

        /// <summary>
        /// Ändert die Zieldaten vor der Übergabe an das Quellobjekt.  Diese Methode wird nur in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/>-Bindungen aufgerufen.
        /// </summary>
        /// <returns>
        /// Der Wert, der an das Quellobjekt weitergeleitet wird.
        /// </returns>
        /// <param name="value">Die Zieldaten, die an die Quelle übergeben werden.</param><param name="targetType">Der von dem Quellobjekt erwartete <see cref="T:System.Type"/> der Daten.</param><param name="parameter">Ein optionaler Parameter, der in der Konverterlogik verwendet wird.</param><param name="culture">Die Kultur der Konvertierung.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage)
            {
                return ((BitmapImage) value).UriSource.ToString();
            }
            return null;
        }

        #endregion
    }
}