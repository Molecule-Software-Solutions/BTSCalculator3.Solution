using System;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Class that provides methods for performing necessary calculations for this application
    /// </summary>
    internal static class Calculations
    {
        /// <summary>
        /// Static method that calculates the total number of non-business days between two dates, including any exclusion dates
        /// passed via the exclusionDates parameter. The returned integer should then be subtracted from the total number of days
        /// between the judgment and rent due dates. 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exclusionDates"></param>
        /// <returns></returns>
        public static int ExclusionDateCount(DateTime start, DateTime end, DateTime[] exclusionDates)
        {
            // Define variables
            DateTime stDate = start;
            stDate = stDate.AddDays(1.0); // Adds a day to start date in order to comply with rules of civil procedure
            DateTime enDate = end;
            int exclusionCount = 0;

            // While comparison shows the start date variable is on or before the end date...
            while (DateTime.Compare(stDate, enDate) <= 0)
            {
                if (stDate.DayOfWeek == DayOfWeek.Saturday || stDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    exclusionCount += 1;
                }
                else
                {
                    foreach (DateTime dateTime in exclusionDates)
                    {
                        if (stDate == dateTime)
                        {
                            exclusionCount += 1;
                        }
                    }
                }
                stDate = stDate.AddDays(1);
            }
            return exclusionCount;
        }

        /// <summary>
        /// This method calculates the per diem rent due between the judgment date and rent due date
        /// </summary>
        /// <param name="exclusionDates"></param>
        /// <param name="monthlyRentalRate"></param>
        /// <param name="judgmentDate"></param>
        /// <param name="rentDueDate"></param>
        /// <returns></returns>
        public static decimal CalculatePerDiemRent(decimal monthlyRentalRate, DateTime judgmentDate, DateTime rentDueDate)
        {
            int businessTimeSpan = CalculateTotalTimeSpan(judgmentDate, rentDueDate);
            if (businessTimeSpan < 0) throw new Exception("ER-C02: An error has occurred while calculating the per diem rent due. Please contact the system developer");
            return (monthlyRentalRate / 30) * businessTimeSpan;
        }

        /// <summary>
        /// This method calculates the per diem rental rate that will be used to calculate the total per diem amount due
        /// </summary>
        /// <param name="monthlyRentalRate"></param>
        /// <param name="judgmentDate"></param>
        /// <param name="rentDueDate"></param>
        /// <returns></returns>
        public static decimal CalculatePerDiemRate(decimal monthlyRentalRate, DateTime judgmentDate, DateTime rentDueDate)
        {
            int businessTimeSpan = CalculateTotalTimeSpan(judgmentDate, rentDueDate);
            if (businessTimeSpan < 0) throw new Exception("ER-C03: An error has occurred while calculating the per diem rental rate. Please contact the system developer");
            return (monthlyRentalRate / 30);
        }

        /// <summary>
        /// Calculates the raw time span between two dates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int CalculateTotalTimeSpan(DateTime start, DateTime end)
        {
            int comparison = DateTime.Compare(start, end);
            if (comparison == 0) return 0;
            if (comparison > 0) return -1;
            if (comparison < 0)
            {
                TimeSpan ts = end - start;
                return ts.Days;
            }

            return -99;
        }

        /// <summary>
        /// Calculates the business day time span between two dates. 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exclusionDates"></param>
        /// <returns></returns>
        public static int CalculateBusinessTimeSpan(DateTime start, DateTime end, DateTime[] exclusionDates)
        {
            int totalDays = CalculateTotalTimeSpan(start, end);
            if (totalDays == 0) return 0;
            if (totalDays == -1) return -1;
            if (totalDays == -99) throw new Exception("ER-C01: Error with TimeSpan calculation of judgment and rent due dates. Please report this error to the developer");

            return totalDays - ExclusionDateCount(start, end, exclusionDates);
        }
    }
}
