using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class ExclusionDateCleanup
    {
        /// <summary>
        /// Holds the list of current holidays stored in the system 
        /// </summary>
        private static List<ExclusionDate> _Holidays = new List<ExclusionDate>();

        /// <summary>
        /// Holds the list of current exclusion dates stored in the system 
        /// </summary>
        private static List<ExclusionDate> _ExclusionDates = new List<ExclusionDate>(); 

        /// <summary>
        /// Publicly facing method that begins the cleanup process
        /// </summary>
        public static void PerformCleanup()
        {
            GetDatabaseRecords();
            CheckAndCommandCleanup();
        }

        /// <summary>
        /// Method that triggers the sub-methods that are responsible for filling the exclusion date lists
        /// </summary>
        private static void GetDatabaseRecords()
        {
            GetHolidays();
            GetExclusionDates(); 
        }

        /// <summary>
        /// Retrieves holidays from the database 
        /// </summary>
        private static void GetHolidays()
        {
            _Holidays = RetrieveDatabaseRecords("Holidays");
        }

        /// <summary>
        /// Retrieves exclusion dates from the database 
        /// </summary>
        private static void GetExclusionDates()
        {
            _ExclusionDates = RetrieveDatabaseRecords("ExclusionDates");
        }

        /// <summary>
        /// Malleable method that pulls records from the database
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static List<ExclusionDate> RetrieveDatabaseRecords(string tableName)
        {
            List<ExclusionDate> returnList = new List<ExclusionDate>(); 
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = GetDatabaseRecordsCommandText(tableName);
                    comm.Connection.Open();
                    SQLiteDataReader reader = comm.ExecuteReader();
                    try
                    {
                        while(reader.Read())
                        {
                            returnList.Add(new ExclusionDate() { Date = reader.GetSafeDateTime("Date"), RecordID = reader.GetSafeInt("RecordID")});
                        }
                        return returnList;
                    }
                    catch (Exception ex)
                    {
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogMessage = ex.Message, DialogHeader = "Error" });
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

        /// <summary>
        /// Checks each list and deletes any holidays that are in the past
        /// </summary>
        private static void CheckAndCommandCleanup()
        {
            foreach (ExclusionDate exclusion in _Holidays)
            {
                if(exclusion.Date < DateTime.Today.AddDays(-10))
                {
                    DeleteRecord("Holidays", exclusion.RecordID);
                }
            }

            foreach (ExclusionDate exclusion2 in _ExclusionDates)
            {
                if (exclusion2.Date < DateTime.Today.AddDays(-10))
                {
                    DeleteRecord("ExclusionDates", exclusion2.RecordID);
                }
            }
        }

        /// <summary>
        /// Method that performs the holiday deletion
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        private static void DeleteRecord(string tableName, int recordID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = DeleteRecordCommandText(tableName);
                    comm.Parameters.AddWithValue("@RecordID", recordID);
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery(); 
                        transaction.Commit(); 
                    }
                    catch (Exception ex)
                    {
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogMessage = ex.Message, DialogHeader = "Error" });
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
        /// Command text used to delete records from the database 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string DeleteRecordCommandText(string tableName)
        {
            return $"DELETE FROM {tableName} WHERE RecordID = @RecordID;";
        }

        /// <summary>
        /// Command text used to retrieve records from the database 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetDatabaseRecordsCommandText(string tableName)
        {
            return $"SELECT * FROM {tableName};";
        }
    }


}
