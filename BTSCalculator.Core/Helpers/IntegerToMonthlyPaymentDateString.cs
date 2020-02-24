namespace BTSCalculator.Core
{
    /// <summary>
    /// Helpers for producing strings that represent due dates and date postfix values
    /// </summary>
    public static class IntegerToMonthlyPaymentDateString
    {
        /// <summary>
        /// Extension method that returns a string representing a monthly due date
        /// </summary>
        /// <param name="dayOfMonth"></param>
        /// <returns></returns>
        public static string GetMonthlyDueDateStringFromInt(this int dayOfMonth)
        {
            if(dayOfMonth != 0)
            {
                return $"{dayOfMonth.GetDayPostfix()} of each month";
            }
            else
            {
                return "1st of each month";
            }
        }

        /// <summary>
        /// Returns a postfix value for a number which is meant to represent a date
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetDayPostfix(this int day)
        {
            if (day < 0 || day > 31)
                return "1st";

            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    {
                        return $"{day}st";
                    }
                case 2:
                case 22:
                    {
                        return $"{day}nd";
                    }
                case 3:
                case 23:
                    {
                        return $"{day}rd";
                    }
                default:
                    return $"{day}th";
            }
        }
    }
}
