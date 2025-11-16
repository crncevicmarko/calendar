using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Calendar.View
{
    public class AbsenceTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                switch (type)
                {
                    case "BOLOVANJE":
                        return Brushes.LightPink;
                    case "GODISNJI_ODMOR":
                        return Brushes.LightGreen;
                    case "VERSKI_PRAZNIK":
                        return Brushes.LightYellow;
                    case "SLOBODAN_DAN":
                        return Brushes.LightBlue;
                    case "OSTALO":
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
