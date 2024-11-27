using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Biblioteca
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Si el valor es una cadena vacía o null, se oculta el control
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // No necesitamos la conversión inversa, por lo tanto, retornamos DependencyProperty.UnsetValue
            return DependencyProperty.UnsetValue;
        }
    }
}
