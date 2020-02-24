using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Migration sequence from version 1 to version 2 of the internal database 
    /// </summary>
    internal static class Version2ToVersion3Migration
    {
        /// <summary>
        /// Performs the migration
        /// </summary>
        public static void PerformMigration()
        {
            Migrate();
        }

        /// <summary>
        /// Method that contains the connection and command objects that will execute the database migration 
        /// </summary>
        private static void Migrate()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = MigrationCommandText();
                    comm.Parameters.AddWithValue("@SettingKey", "CourtCost");
                    comm.Parameters.AddWithValue("@SettingValue", "150");
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery(); 
                        transaction.Commit(); 
                    }
                    catch (Exception ex)
                    {
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Migration Error", DialogMessage = ex.Message });
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
        /// Command text for the migration command 
        /// </summary>
        /// <returns></returns>
        private static string MigrationCommandText()
        {
            return @"INSERT INTO SystemSettings (SettingKey, SettingValue) VALUES (@SettingKey, @SettingValue); PRAGMA user_version = 3;";
        }
    }
}
