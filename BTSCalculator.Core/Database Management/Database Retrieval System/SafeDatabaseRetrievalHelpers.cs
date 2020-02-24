using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Helpers that return data from a database command
    /// </summary>
    internal static class SafeDatabaseRetrievalHelpers
    {
        /// <summary>
        /// Returns a safe <see cref="DateTime"/> from a <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a safe <see cref="String"/> from a <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetSafeString(this SQLiteDataReader reader, string columnName)
        {
            if (reader.GetType() != typeof(DBNull))
            {
                return reader[columnName].ToString();
            }
            else return string.Empty;
        }

        /// <summary>
        /// Returns a safe <see cref="int"/> from a <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int GetSafeInt(this SQLiteDataReader reader, string columnName)
        {
            if (reader.GetType() != typeof(DBNull))
            {
                return Convert.ToInt32(reader[columnName]);
            }
            else return 0;
        }

        /// <summary>
        /// returns a safe <see cref="decimal"/> from a <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static decimal GetSafeDecimal(this SQLiteDataReader reader, string columnName)
        {
            if(reader.GetType() != typeof(DBNull)) {
                return Convert.ToDecimal(reader[columnName]);
            }
            else return 0;
        }
    }
}
