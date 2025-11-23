using System;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Converters
{
    public class LaneToWidthConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is int laneIndex &&
                values[1] is int laneCount &&
                values[2] is double canvasWidth &&
                laneCount > 0)
            {
                return (canvasWidth / laneCount) - 4; // 4 px razmak
            }
            return 140;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
