using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Class that provides utilities for retrieving and modifying the standard court costs
    /// </summary>
    internal static class CostsManager
    {
        /// <summary>
        /// Retrieves the default court costs from the internal database 
        /// </summary>
        /// <returns></returns>
        public static decimal GetCosts()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = GetCostsCommandText();
                    comm.Connection.Open();
                    SQLiteDataReader reader = comm.ExecuteReader();
                    try
                    {
                        if (reader.Read())
                        {
                            return reader.GetSafeDecimal("SettingValue");
                        }
                        else return 150;
                    }
                    catch (Exception)
                    {
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogMessage = "Error retrieving default court costs", DialogHeader = "Error" });
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

        /// <summary>
        /// Modifies the current default court costs in the internal database 
        /// </summary>
        /// <param name="costs"></param>
        public static void SetCosts(decimal costs)
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = SetCostsCommandText();
                    comm.Parameters.AddWithValue("@CostValue", costs);
                    comm.Connection.Open();
                    try
                    {
                        SQLiteTransaction transaction = comm.Connection.BeginTransaction();
                        comm.ExecuteNonQuery(); 
                        transaction.Commit();
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogMessage = "Court costs updated successfully", DialogHeader = "Court Costs Update" });
                    }
                    catch (Exception)
                    {
                        StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogMessage = "Error Setting Costs", DialogHeader = "Error" });
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
        /// Command text for retrieving the default court costs
        /// </summary>
        /// <returns></returns>
        private static string GetCostsCommandText()
        {
            return @"SELECT * FROM SystemSettings WHERE SettingKey = 'CourtCost';";
        }

        /// <summary>
        /// Command text for setting the default court costs
        /// </summary>
        /// <returns></returns>
        private static string SetCostsCommandText()
        {
            return @"UPDATE SystemSettings SET SettingValue = @CostValue WHERE SettingKey = 'CourtCost';";
        }
    }
}
