using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Class that contains utilities for maintaining exclusion dates that are stored in the internal database 
    /// </summary>
    internal static class MaintainExclusionDates
    {
        #region Public Methods 

        /// <summary>
        /// Adds an exclusion date to the database
        /// </summary>
        /// <param name="date"></param>
        public static void AddExclusionDate(DateTime date)
        {
            AddDate(true, date);
        }

        /// <summary>
        /// Adds a holiday to the database
        /// </summary>
        /// <param name="date"></param>
        public static void AddHoliday(DateTime date)
        {
            AddDate(false, date);
        }

        /// <summary>
        /// Deletes an exclusion date from the database
        /// </summary>
        /// <param name="recordID"></param>
        public static void DeleteExclusionDate(int recordID)
        {
            DeleteDate(true, recordID);
        }

        /// <summary>
        /// Deletes a holiday from the database 
        /// </summary>
        /// <param name="recordID"></param>
        public static void DeleteHoliday(int recordID)
        {
            DeleteDate(false, recordID);
        }

        #endregion

        #region Private SQLite Command Methods

        /// <summary>
        /// Method that adds a date to the Holidays or ExclusionDates table depending on the parameter passed
        /// </summary>
        /// <param name="exclusionDate">Determines whether the date will be recorded in the ExclusionDates table, or if false, the Holidays table</param>
        /// <param name="date">Date which is being recorded</param>
        private static void AddDate(bool exclusionDate, DateTime date)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = AddDateCommandText(exclusionDate);
                    comm.Parameters.AddWithValue("@Date", date);
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        comm.Connection.Close();
                        comm.Connection.Dispose();
                        comm.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Method that deletes a particular date record from the database
        /// </summary>
        /// <param name="exclusionDate">Determines if the command text should contain ExclusionDates or Holiday Table</param>
        /// <param name="recordID">Record ID of the record being deleted</param>
        private static void DeleteDate(bool exclusionDate, int recordID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = DeleteDateCommandText(exclusionDate);
                    comm.Parameters.AddWithValue("@RecordID", recordID);
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        comm.Connection.Close();
                        comm.Connection.Dispose();
                        comm.Dispose();
                    }
                }
            }
        }

        #endregion

        #region Private Command Text Strings 

        /// <summary>
        /// Command text that will be used to add either a holiday or an exclusion date depending on the parameter passed
        /// </summary>
        /// <param name="exclusionDate"></param>
        /// <returns></returns>
        private static string AddDateCommandText(bool exclusionDate)
        {
            if(exclusionDate)
            {
                return @"
                    INSERT INTO ExclusionDates
                               (Date)
                         VALUES
                               (@Date);";
            }
            else
            {
                return @"
                    INSERT INTO Holidays
                               (Date)
                         VALUES
                               (@Date);";
            }
        }

        /// <summary>
        /// Command text that will be used to execute a deletion for either holidays or exclusion dates, depending on the parameter passed
        /// </summary>
        /// <param name="exclusionDate"></param>
        /// <returns></returns>
        private static string DeleteDateCommandText(bool exclusionDate)
        {
            if(exclusionDate)
            {
                return @"DELETE FROM ExclusionDates WHERE RecordID = @RecordID;";
            }
            else
            {
                return @"DELETE FROM Holidays WHERE RecordID = @RecordID;";
            }
        }

        #endregion 
    }
}
