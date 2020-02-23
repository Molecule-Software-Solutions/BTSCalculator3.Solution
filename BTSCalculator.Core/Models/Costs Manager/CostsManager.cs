using System;
using System.Data.SQLite;

namespace BTSCalculator.Core
{
    internal static class CostsManager
    {
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

        private static string GetCostsCommandText()
        {
            return @"SELECT * FROM SystemSettings WHERE SettingKey = 'CourtCost';";
        }

        private static string SetCostsCommandText()
        {
            return @"UPDATE SystemSettings SET SettingValue = @CostValue WHERE SettingKey = 'CourtCost';";
        }
    }
}
