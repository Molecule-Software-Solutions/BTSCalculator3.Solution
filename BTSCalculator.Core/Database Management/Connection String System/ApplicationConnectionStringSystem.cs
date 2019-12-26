using System;
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
