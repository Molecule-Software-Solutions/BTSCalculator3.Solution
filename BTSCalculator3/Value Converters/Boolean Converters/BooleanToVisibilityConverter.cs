using System;
using System.Globalization;
using System.Windows.Data;

namespace BTSCalculator3
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(bool))
            {
                var val = System.Convert.ToBoolean(value);
                if (val)
                {
                    return System.Windows.Visibility.Visible;
                }
                else return System.Windows.Visibility.Collapsed;
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; 
        }
    }
}
