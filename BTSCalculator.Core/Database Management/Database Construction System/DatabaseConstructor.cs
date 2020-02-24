using System;
using System.Data.SQLite;
using System.Text;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Responsible for the initial construction of the internal database 
    /// </summary>
    internal class DatabaseConstructor
    {
        /// <summary>
        /// Calls the various methods necessary to construct the database tables
        /// </summary>
        /// <param name="connectionString"></param>
        public void ConstructTables()
        {
            ConstructHolidayTable();
        }

        /// <summary>
        /// Constructs the holiday table within the database 
        /// </summary>
        /// <param name="connectionString"></param>
        private void ConstructHolidayTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = HolidayTableConstructionCommandText(); 
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // TODO: Create dialog error reporting system for this application / and a logger 
                        throw;
                    }
                    finally
                    {
                        comm.Connection.Close(); 
                    }
                }
            }
        }

        /// <summary>
        /// Command text for creating a holiday table 
        /// </summary>
        /// <returns></returns>
        private string HolidayTableConstructionCommandText()
        {
            StringBuilder sb = new StringBuilder();

            // Creates holiday table
            sb.AppendLine("CREATE TABLE IF NOT EXISTS Holidays (");
            sb.AppendLine("    RecordID INTEGER PRIMARY KEY AUTOINCREMENT");
            sb.AppendLine("                     UNIQUE");
            sb.AppendLine("                     NOT NULL,");
            sb.AppendLine("    Date     DATE    NOT NULL");
            sb.AppendLine(");");

            // Creates specific exclusions table
            sb.AppendLine("CREATE TABLE IF NOT EXISTS ExclusionDates (");
            sb.AppendLine("    RecordID INTEGER PRIMARY KEY AUTOINCREMENT");
            sb.AppendLine("                     UNIQUE");
            sb.AppendLine("                     NOT NULL,");
            sb.AppendLine("    Date     DATE    NOT NULL");
            sb.AppendLine(");");

            // Updates user version 
            sb.AppendLine("PRAGMA user_version = 1");

            return sb.ToString(); 
        }
    }
}
