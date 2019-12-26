using System;
using System.Data;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal sealed class ApplicationConnectionStringSystem
    {
        public readonly string ConnectionString;
        private readonly string DatabaseLocation = AppDomain.CurrentDomain.BaseDirectory + "ApplicationDatabase.db";

        public ApplicationConnectionStringSystem()
        {
            ConnectionString = ConnectionStringBuilder(); 
        }

        public ConnectionState TextConnection()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    return conn.State;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close(); 
                }
            }
        }

        private string ConnectionStringBuilder()
        {
            SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder()
            {
                Password = "12256124",
                DataSource = DatabaseLocation,
                ForeignKeys = true,
                Version = 3,
                FailIfMissing = false
            };
            return csb.ConnectionString;
        }
    }
}
