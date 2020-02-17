using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class RetrieveDates
    {
        /// <summary>
        /// Retrieves an <see cref="ObservableCollection{ExclusionDate}"/> from the database
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ExclusionDate> RetrieveExclusionDates()
        {
            return RetrieveDatabaseDates(true);
        }

        /// <summary>
        /// Retrieves a <see cref="ObservableCollection{ExclusionDate}"/> from the database 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ExclusionDate> RetrieveHolidays()
        {
            return RetrieveDatabaseDates(false);
        }

        /// <summary>
        /// Command text that is used to retrieve dates from the database. Returns ExclusionDates and Holidays based on the parameter passed
        /// </summary>
        /// <param name="exclusionDates"></param>
        /// <returns></returns>
        private static string RetrieveDatesCommandText(bool exclusionDates)
        {
            if(exclusionDates)
            {
                return @"SELECT * FROM ExclusionDates;";
            }
            else
            {
                return @"SELECT * FROM Holidays;";
            }
        }

        /// <summary>
        /// Method which returns ExclusionDates and Holidays based on the parameter passed
        /// </summary>
        /// <param name="exclusionDates"></param>
        /// <returns></returns>
        private static ObservableCollection<ExclusionDate> RetrieveDatabaseDates(bool exclusionDates)
        {
            ObservableCollection<ExclusionDate> returnDates = new ObservableCollection<ExclusionDate>(); 
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = RetrieveDatesCommandText(exclusionDates);
                    comm.Connection.Open();
                    SQLiteDataReader reader = comm.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            returnDates.Add(new ExclusionDate() { Date = reader.GetSafeDateTime("Date"), RecordID = reader.GetSafeInt("RecordID") });
                        }
                        return returnDates; 
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        reader.Close(); 
                        comm.Connection.Close();
                        comm.Connection.Dispose();
                        comm.Dispose(); 
                    }
                }
            }
        }
    }
}
