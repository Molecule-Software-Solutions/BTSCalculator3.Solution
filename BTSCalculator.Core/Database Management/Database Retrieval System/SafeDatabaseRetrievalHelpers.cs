using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class SafeDatabaseRetrievalHelpers
    {
        public static DateTime GetSafeDateTime(this SQLiteDataReader reader, string columnName)
        {
            if(reader.GetType() != typeof(DBNull))
            {
                DateTime result;
                if (DateTime.TryParse(reader[columnName].ToString(), out result))
                {
                    return result;
                }
                else
                    throw new Exception("Unable to retrieve exclusion dates. System failure. Contact developer");
            }
            else throw new Exception("Unable to retrieve exclusion dates. System failure. Contact developer");
        }
    }
}
