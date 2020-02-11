using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class DatabaseVersionRetrieval
    {
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
