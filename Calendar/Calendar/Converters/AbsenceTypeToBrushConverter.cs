using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Calendar.Converters
{
    public class AbsenceTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                switch (type)
                {
                    case "SickLeave":
                        return Brushes.LightPink;
                    case "AnnualLeave":
                        return Brushes.LightGreen;
                    case "ReligiousHoliday":
                        return Brushes.LightYellow;
                    case "DayOff":
                        return Brushes.LightSalmon;
                    case "Other":
                        return Brushes.LightGray;
                    default:
                        return Brushes.LightGray;
                }
            }
            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
