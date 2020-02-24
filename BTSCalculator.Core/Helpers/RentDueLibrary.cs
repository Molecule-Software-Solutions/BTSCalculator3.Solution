using System.Collections.Generic;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Library that contains helpers which return dictionaries of commonly used rental periods
    /// </summary>
    public static class RentDueLibrary
    {
        /// <summary>
        /// Returns a dictionary containing rent due periods
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> RentDueDictionary()
        {
            return new Dictionary<string, string>()
            {
                {"MO", "MONTHLY" },
                {"WE", "WEEKLY" }
            };
        }

    }
}
