using System;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Object that represents an exclusion date as it is recorded in the internal database 
    /// NOTE: This object represents both basic exclusion dates and holidays since their structure is the same
    /// </summary>
    public class ExclusionDate
    {
        /// <summary>
        /// Record ID of the exclusion date
        /// </summary>
        public int RecordID { get; set; }

        /// <summary>
        /// Date to be excluded from calculations
        /// </summary>
        public DateTime Date { get; set; }
    }
}
