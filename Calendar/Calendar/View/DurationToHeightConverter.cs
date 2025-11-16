using Calendar.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.View
{
    public class DurationToHeightConverter : IValueConverter
    {
        private const double PixelsPerHour = 50;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DayItem item)
            {
                TimeSpan duration = item.End - item.Start;
                return duration.TotalHours * PixelsPerHour;
            }

            return 30;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
