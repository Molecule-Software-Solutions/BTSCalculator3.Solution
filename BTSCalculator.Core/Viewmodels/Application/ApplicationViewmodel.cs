using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BTSCalculator.Core
{
    public class ApplicationViewmodel : BaseViewmodel
    {
        public ApplicationViewmodel()
        {
            if(TestConnectionState())
            {
                Setup();
            }
        }

        private void Setup()
        {
            DatabaseMigrationSystem.CheckMigrationStatus();
            GetCounty();
        }

        /// <summary>
        /// Indicates the current page that the application is displaying
        /// </summary>
        public ApplicationPageTypes CurrentPage { get; set; } = ApplicationPageTypes.MainMenu;
        public IDialog CurrentDialog { get; set; }
        public string County { get; set; }

        public void ShowDialog(IDialog dialog)
        {
            CurrentDialog = dialog;
        }

        public RelayCommand CloseDialog_COMMAND => new RelayCommand(() =>
        {
            IDialog currentDialog = StaticAccessSystem.ApplicationVM.CurrentDialog;
            currentDialog.DialogType = DialogTypes.None;
            CurrentDialog.DialogYes = true;
            // Forces property changed on current dialog
            CurrentDialog = null;
            // Populates current dialog with result of last dialog so that properties can be accessed
            CurrentDialog = currentDialog;
        });

        private bool TestConnectionState()
        {
            switch (ApplicationConnectionStringSystem.TextConnection())
            {
                case System.Data.ConnectionState.Closed:
                    return false;
                case System.Data.ConnectionState.Open:
                    return true;
                case System.Data.ConnectionState.Connecting:
                    return false;
                case System.Data.ConnectionState.Executing:
                    return false;
                case System.Data.ConnectionState.Fetching:
                    return false;
                case System.Data.ConnectionState.Broken:
                    return false;
                default:
                    return false;
            }
        }

        private void GetCounty()
        {
            using (SQLiteConnection conn = new SQLiteConnection(new ApplicationConnectionStringSystem().ConnectionString))
            {
                using (SQLiteCommand comm = new SQLiteCommand(conn))
                {
                    comm.CommandText = @"SELECT * FROM SystemSettings WHERE SettingKey = @SettingValue;";
                    comm.Parameters.AddWithValue("@SettingValue", "County");
                    comm.Connection.Open();
                    SQLiteDataReader reader = comm.ExecuteReader(); 
                    try
                    {
                        if(reader.Read())
                        {
                            County = reader.GetSafeString("SettingValue");
                        }
                        else
                        {
                            ShowDialog(new DialogModel() { DialogHeader = "Error", DialogMessage = "Could not retrieve county name" });
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowDialog(new DialogModel() { DialogHeader = "Error", DialogMessage = "Could not retrieve county name" });
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
    }
}
