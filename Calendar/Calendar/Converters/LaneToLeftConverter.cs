using System;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Converters
{
    public class LaneToLeftConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is int laneIndex &&
                values[1] is int laneCount &&
                values[2] is double canvasWidth)
            {
                return laneIndex * (canvasWidth / laneCount);
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
