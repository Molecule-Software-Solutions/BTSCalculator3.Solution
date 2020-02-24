namespace BTSCalculator.Core
{
    /// <summary>
    /// Static class which is responsible for identifying necessary database updates and performs them if required
    /// </summary>
    internal static class DatabaseMigrationSystem
    {
        /// <summary>
        /// Checks migration status and performs migration operations
        /// </summary>
        public static void CheckMigrationStatus()
        {
            // Builds out initial tables for version 0 and converts to version 1
            if(DatabaseVersionRetrieval.GetDatabaseVersion() == 0)
            {
                DatabaseConstructor dc = new DatabaseConstructor();
                dc.ConstructTables();
            }
            if(DatabaseVersionRetrieval.GetDatabaseVersion() == 1)
            {
                Version1ToVersion2Migration.PerformMigration();
            }
            if(DatabaseVersionRetrieval.GetDatabaseVersion() == 2)
            {
                Version2ToVersion3Migration.PerformMigration(); 
            }
        }
    }
}
