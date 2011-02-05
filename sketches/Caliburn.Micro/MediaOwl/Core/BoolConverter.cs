using System;
using System.Globalization;
using System.Windows.Data;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IValueConverter"/> which inverts <see cref="bool"/>.
    /// </summary>
    public class BoolInvertConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Inverts a boolean value
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        /// <summary>
        /// Inverts a boolean value. This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <param name="value">The target data being passed to the source.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        #endregion
    }

    /// <summary>
    /// An <see cref="IValueConverter"/> which inverts <see cref="bool"/> and returns the corresponding <see cref="System.Windows.Visibility"/>.
    /// </summary>
    public class BoolVisibilityInvertConverter : IValueConverter
    {
        /// <summary>For the conversion to a <see cref="System.Windows.Visibility"/>,
        /// a <see cref="BooleanToVisibilityConverter"/> is used.</summary>
        private readonly BooleanToVisibilityConverter btvConverter = new BooleanToVisibilityConverter();
        
        #region Implementation of IValueConverter
        
        /// <summary>
        /// Inverts the boolean value and returns the corresponding visibility.
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return btvConverter.Convert(!((bool) value), targetType, parameter, culture);
        }

        /// <summary>
        /// Takes a visibility, converts it to a boolean value and returns the inverted boolean value. This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <param name="value">The target data being passed to the source.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return btvConverter.ConvertBack(!((bool)value), targetType, parameter, culture);
        }

        #endregion
    }
}