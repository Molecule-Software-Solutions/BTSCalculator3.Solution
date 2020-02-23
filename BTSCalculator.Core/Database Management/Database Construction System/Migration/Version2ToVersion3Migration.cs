using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class Version2ToVersion3Migration
    {
        public static void PerformMigration()
        {
            Migrate();
        }

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

        private static string MigrationCommandText()
        {
            return @"INSERT INTO SystemSettings (SettingKey, SettingValue) VALUES (@SettingKey, @SettingValue); PRAGMA user_version = 3;";
        }
    }
}
