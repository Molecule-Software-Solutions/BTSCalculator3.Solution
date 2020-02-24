using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Contains utilities for maintaining and retrieving the county name from the internal database 
    /// </summary>
    internal static class CountyNameManagement
    {
        /// <summary>
        /// Retrieves the county name from the internal database 
        /// </summary>
        /// <returns></returns>
        public static string GetCountyName()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = GetCountyNameCommandText();
                    comm.Connection.Open();
                    SQLiteDataReader reader = comm.ExecuteReader();
                    try
                    {
                        if (reader.Read())
                        {
                            return reader.GetSafeString("SettingValue");
                        }
                        else return "NOT SET";
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

        /// <summary>
        /// Modifies the county name stored in the internal database 
        /// </summary>
        /// <param name="newName"></param>
        public static void ModifyCountyName(string newName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = SetCountyNameCommandText();
                    comm.Parameters.AddWithValue("@SettingValue", newName);
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
        /// Command text for retrieving the county name
        /// </summary>
        /// <returns></returns>
        private static string GetCountyNameCommandText()
        {
            return @"SELECT SettingValue FROM SystemSettings WHERE SettingKey = 'County';";
        }

        /// <summary>
        /// Command text for setting the County Name
        /// </summary>
        /// <returns></returns>
        private static string SetCountyNameCommandText()
        {
            return @"UPDATE SystemSettings SET SettingValue = @SettingValue WHERE SettingKey = 'County'";
        }
    }
}
