using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Class that retrieves the database version for the migration system
    /// </summary>
    internal static class DatabaseVersionRetrieval
    {
        /// <summary>
        /// returns an integer that identifies the version of the internal database 
        /// </summary>
        /// <returns></returns>
        public static int GetDatabaseVersion()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = "PRAGMA user_version;";
                    comm.Connection.Open();
                    try
                    {
                        return Convert.ToInt32(comm.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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
    }
}
