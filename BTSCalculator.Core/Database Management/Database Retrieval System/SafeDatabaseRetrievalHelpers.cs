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

        public static string GetSafeString(this SQLiteDataReader reader, string columnName)
        {
            if (reader.GetType() != typeof(DBNull))
            {
                return reader[columnName].ToString();
            }
            else return string.Empty;
        }

        public static int GetSafeInt(this SQLiteDataReader reader, string columnName)
        {
            if (reader.GetType() != typeof(DBNull))
            {
                return Convert.ToInt32(reader[columnName]);
            }
            else return 0;
        }
    }
}
