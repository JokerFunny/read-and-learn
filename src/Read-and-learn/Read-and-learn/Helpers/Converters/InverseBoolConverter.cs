using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Helpers.Converters
{
    /// <summary>
    /// Inverse bool converter to support some features on View.
    /// </summary>
    public class InverseBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !((bool)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;

        public object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}
