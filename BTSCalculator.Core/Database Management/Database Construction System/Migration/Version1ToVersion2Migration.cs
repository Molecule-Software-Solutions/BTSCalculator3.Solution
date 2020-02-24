using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Migration sequence from version 1 to version 2 of the internal database 
    /// </summary>
    internal static class Version1ToVersion2Migration
    {
        /// <summary>
        /// Performs the migration
        /// </summary>
        public static void PerformMigration()
        {
            try
            {
                Migrate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method that contains the connection and command objects that will carry out the migration 
        /// </summary>
        private static void Migrate()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = MigrationCommandText();
                    comm.Parameters.AddWithValue("@SettingKey", "County");
                    comm.Parameters.AddWithValue("@SettingValue", "NOT SET");
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
        /// Command text that will be used by the migration command 
        /// </summary>
        /// <returns></returns>
        private static string MigrationCommandText()
        {
            return @"CREATE TABLE IF NOT EXISTS SystemSettings (
                    RecordID     INTEGER PRIMARY KEY AUTOINCREMENT
                                         UNIQUE
                                         NOT NULL,
                    SettingKey   STRING  UNIQUE
                                         NOT NULL,
                    SettingValue);

                    INSERT INTO SystemSettings (SettingKey, SettingValue)
                            VALUES
                                (@SettingKey, @SettingValue);

                    PRAGMA user_version = 2";
        }
    }
}
