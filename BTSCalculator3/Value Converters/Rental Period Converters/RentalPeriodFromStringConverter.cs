using BTSCalculator.Core;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BTSCalculator3
{
    public class RentalPeriodFromStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(RentalPaymentTerms))
            {
                var val = (RentalPaymentTerms)value;
                if (val == RentalPaymentTerms.Monthly) return "MO";
                else if (val == RentalPaymentTerms.Weekly) return "WE";
                return value;
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(string))
            {
                var val = value as string;
                if (val == "MO") return RentalPaymentTerms.Monthly;
                else if (val == "WE") return RentalPaymentTerms.Weekly;
                return value;
            }
            else return value;
        }
    }
}
