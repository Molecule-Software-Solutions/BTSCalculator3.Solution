using System;
using System.Globalization;
using System.Windows.Data;

namespace BTSCalculator3
{
    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(bool))
            {
                var val = System.Convert.ToBoolean(value);
                if (val) return "YES";
                else return "NO";
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(string))
            {
                var val = value as string;
                if (val == "YES") return true;
                else return false; 
            }
            else return value;
        }
    }
}
