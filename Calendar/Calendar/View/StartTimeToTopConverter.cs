using System;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.View
{
    public class StartTimeToTopConverter : IValueConverter
    {
        private const double PixelsPerHour = 50;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                return dt.Hour * PixelsPerHour + dt.Minute * (PixelsPerHour / 60.0);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
