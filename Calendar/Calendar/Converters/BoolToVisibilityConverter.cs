using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Calendar.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (parameter?.ToString() == "Invert")
                boolValue = !boolValue;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = (Visibility)value == Visibility.Visible;
            if (parameter?.ToString() == "Invert")
                result = !result;
            return result;
        }
    }

}
