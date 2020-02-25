using System;
using System.Collections.ObjectModel;
using System.Data;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel that controls the UI's System Setup page
    /// </summary>
    public class SystemSetupViewmodel : BaseViewmodel, IFileSelection
    {
        #region Private Members and Backing Fields 

        /// <summary>
        /// Backing field for <see cref="CountyName"/> property
        /// </summary>
        private string _CountyName = string.Empty;

        #endregion

        #region Constructor 

        /// <summary>
        /// Default constructor that triggers the <see cref="Setup"/> method and takes no parameters 
        /// </summary>
        public SystemSetupViewmodel()
        {
            Setup(); 
        }

        #endregion

        #region Properties 

        /// <summary>
        /// <see cref="ObservableCollection{ExclusionDate}"/>of basic exclusion dates populated by the database 
        /// </summary>
        public ObservableCollection<ExclusionDate> ExclusionDates { get; private set; } = new ObservableCollection<ExclusionDate>();

        /// <summary>
        /// <see cref="ObservableCollection{ExclusionDate}"/> containing holidays, populated by the database 
        /// </summary>
        public ObservableCollection<ExclusionDate> Holidays { get; private set; } = new ObservableCollection<ExclusionDate>();

        /// <summary>
        /// Property containing the current County name
        /// </summary>
        public string CountyName
        {
            get => _CountyName;
            set
            {
                _CountyName = value.ToUpper();
            }
        }

        /// <summary>
        /// Property containing the default court costs
        /// </summary>
        public decimal DefaultCosts { get; set; }

        /// <summary>
        /// Exclusion date that is currently being entered, but which isn't added to the database yet
        /// </summary>
        public DateTime WorkingExclusionDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Holiday that is currently being entered, but which isn't added to the database yet
        /// </summary>
        public DateTime WorkingHoliday { get; set; } = DateTime.Today;

        /// <summary>
        /// The current exclusion date that is selected 
        /// </summary>
        public ExclusionDate SelectedExclusionDate { get; set; }

        /// <summary>
        /// The current holiday that is selected
        /// </summary>
        public ExclusionDate SelectedHoliday { get; set; }

        /// <summary>
        /// Batch holiday file path
        /// </summary>
        public string SelectedFilePath { get; set; }


        #endregion

        #region Private Methods 

        /// <summary>
        /// Method which populates the properties in the viewmodel upon construction
        /// </summary>
        private void Setup()
        {
            ExclusionDates = RetrieveDates.RetrieveExclusionDates();
            Holidays = RetrieveDates.RetrieveHolidays();
            CountyName = CountyNameManagement.GetCountyName();
            DefaultCosts = CostsManager.GetCosts();
        }

        private bool CheckForExistingDate(DateTime date)
        {
            foreach (ExclusionDate exclusionDate in ExclusionDates)
            {
                if (date == exclusionDate.Date)
                    return true; 
            }
            foreach (ExclusionDate exclusionDate1 in Holidays)
            {
                if (date == exclusionDate1.Date)
                    return true;
            }
            return false; 
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command that updates the County Name in the database 
        /// </summary>
        public RelayCommand SetCountyName_COMMAND => new RelayCommand(() =>
        {
            try
            {
                CountyNameManagement.ModifyCountyName(CountyName.ToUpper());
                StaticAccessSystem.ApplicationVM.County = CountyName.ToUpper();
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Update Successful", DialogMessage = "County name updated successfully" });
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Error", DialogMessage = ex.Message });
                throw;
            }
        }, !string.IsNullOrEmpty(CountyName));

        /// <summary>
        /// Command that adds the <see cref="WorkingExclusionDate"/> to the database
        /// </summary>
        public RelayCommand AddExclusionDate_COMMAND => new RelayCommand(() =>
        {
            try
            {
                if (!CheckForExistingDate(WorkingExclusionDate))
                {
                    MaintainExclusionDates.AddExclusionDate(WorkingExclusionDate);
                    ExclusionDates.Add(new ExclusionDate() { Date = WorkingExclusionDate });
                    Setup(); 
                }
                else
                {
                    StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Existing Dates", DialogMessage = "The date you are adding has already been added as either an exclusion date or a holiday. Please check your entry and try again" });
                }
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Error", DialogMessage = ex.Message });
                throw;
            }
        });

        /// <summary>
        /// Command that adds the <see cref="WorkingHoliday"/> to the database 
        /// </summary>
        public RelayCommand AddHoliday_COMMAND => new RelayCommand(() =>
        {
            try
            {
                if (!CheckForExistingDate(WorkingHoliday))
                {
                    MaintainExclusionDates.AddHoliday(WorkingHoliday);
                    Holidays.Add(new ExclusionDate() { Date = WorkingHoliday });
                    Setup(); 
                }
                else
                {
                    StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Existing Dates", DialogMessage = "The date you are adding has already been added as either an exclusion date or a holiday. Please check your entry and try again" });
                }
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Error", DialogMessage = ex.Message });
                throw;
            }
        });

        /// <summary>
        /// Command that deletes the <see cref="SelectedExclusionDate"/> from the database 
        /// </summary>
        public RelayCommand DeleteExclusionDate_COMMAND => new RelayCommand(() =>
        {
            if (SelectedExclusionDate == null)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Trying To Delete New Entry", DialogMessage = "No Entry Selected. Please select an entry to delete it" });
                return;
            }

            if (SelectedExclusionDate.RecordID == 0)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Trying To Delete New Entry", DialogMessage = "You are attempting to delete a new entry. Please exit System Settings and launch again in order to delete any new records" });
                return;
            }

            try
            {

                MaintainExclusionDates.DeleteExclusionDate(SelectedExclusionDate.RecordID);
                Setup();
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Update Successful", DialogMessage = "Exclusion Date Deleted Successfully" });
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Error", DialogMessage = ex.Message });
                throw;
            }
        }, SelectedExclusionDate != null);

        /// <summary>
        /// Command that deletes the <see cref="SelectedHoliday"/> from the database 
        /// </summary>
        public RelayCommand DeleteHoliday_COMMAND => new RelayCommand(() =>
        {
            if (SelectedHoliday == null)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Trying To Delete New Entry", DialogMessage = "No Entry Selected. Please select an entry to delete it" });
                return;
            }

            if (SelectedHoliday.RecordID == 0)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Trying To Delete New Entry", DialogMessage = "You are attempting to delete a new entry. Please exit System Settings and launch again in order to delete any new records" });
                return;
            }

            try
            {
                MaintainExclusionDates.DeleteHoliday(SelectedHoliday.RecordID);
                Setup();
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Update Successful", DialogMessage = "Holiday Date Deleted Successfully" });
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Error", DialogMessage = ex.Message });
                throw;
            }
        }, SelectedHoliday != null);

        /// <summary>
        /// Command that updates the default court costs in the database 
        /// </summary>
        public RelayCommand SetCosts_COMMAND => new RelayCommand(() =>
        {
            CostsManager.SetCosts(DefaultCosts);
            StaticAccessSystem.ApplicationVM.DefaultCosts = DefaultCosts;
        }, DefaultCosts != 0);

        /// <summary>
        /// Adds batch holidays to the database
        /// </summary>
        public ParamRelayCommand HolidayBatchAdd_COMMAND => new ParamRelayCommand((param) => 
        {
            var path = param as IFileSelection;
            SelectedFilePath = path.SelectedFilePath;
            SaveBatchHolidays();
        }, true);

        /// <summary>
        /// Determines whether a date from a batch is already entered
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool CheckBatchDateAgainstExisting(DateTime date)
        {
            foreach (ExclusionDate exclusionDate in Holidays)
            {
                if (date == exclusionDate.Date)
                    return true;
            }
            return false; 
        }

        /// <summary>
        /// Accesses the batch file and adds all qualifying dates to the database 
        /// </summary>
        private void SaveBatchHolidays()
        {
            if(System.IO.File.Exists(SelectedFilePath))
            {
                if(System.IO.Path.GetExtension(SelectedFilePath) == ".BTS3H")
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(SelectedFilePath);
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        DateTime result;
                        if (DateTime.TryParse(dataRow[0].ToString(), out result))
                        {
                            if (!CheckBatchDateAgainstExisting(result))
                            {
                                MaintainExclusionDates.AddHoliday(result);
                                Holidays.Add(new ExclusionDate() { Date = result });
                            }
                        }
                    }
                    StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Success!", DialogMessage = "Holidays recorded successfully" });
                }
                else
                {
                    StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "File Path Error", DialogMessage = "The file you have selected is not a batch holiday file. Please make your selection again" });
                }
            }
            else
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "File Path Error", DialogMessage = "The file path selected was not accurate. Please select the file again" });
            }
        }

        #endregion
    }
}
