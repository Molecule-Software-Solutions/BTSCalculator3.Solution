using System;
using System.Data;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Connection string system for the internal database 
    /// </summary>
    internal sealed class ApplicationConnectionStringSystem
    {
        public readonly string ConnectionString;
        private readonly string DatabaseLocation = AppDomain.CurrentDomain.BaseDirectory + "ApplicationDatabase.db";

        /// <summary>
        /// Default constructor 
        /// </summary>
        public ApplicationConnectionStringSystem()
        {
            ConnectionString = ConnectionStringBuilder(); 
        }

        /// <summary>
        /// Returns the database connection state 
        /// </summary>
        /// <returns></returns>
        public static ConnectionState TextConnection()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
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
                    conn.Dispose(); 
                }
            }
        }

        /// <summary>
        /// Builds and returns the database connection string 
        /// </summary>
        /// <returns></returns>
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
