using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class CountyNameManagement
    {
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

        private static string GetCountyNameCommandText()
        {
            return @"SELECT SettingValue FROM SystemSettings WHERE SettingKey = 'County';";
        }

        private static string SetCountyNameCommandText()
        {
            return @"UPDATE SystemSettings SET SettingValue = @SettingValue WHERE SettingKey = 'County'";
        }
    }
}
