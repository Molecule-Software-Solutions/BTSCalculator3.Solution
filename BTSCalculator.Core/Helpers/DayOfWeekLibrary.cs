using System.Collections.Generic;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Methods contain collections of the days of the week
    /// </summary>
    public static class DayOfWeekLibrary
    {
        /// <summary>
        /// Returns a dictionary containing the days of the week. This can be bound to controls that require a list of those days
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> DaysOfTheWeek()
        {
            return new Dictionary<string, string>()
            {
                {"SU", "SUNDAY" },
                {"MO", "MONDAY" },
                {"TU", "TUESDAY" },
                {"WE", "WEDNESDAY" },
                {"TH", "THURSDAY" },
                {"FR", "FRIDAY" },
                {"SA", "SATURDAY" }
            };
        }
    }
}
