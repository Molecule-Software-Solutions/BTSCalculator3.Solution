using System;
using System.Collections.ObjectModel;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel that controls the UI's System Setup page
    /// </summary>
    public class SystemSetupViewmodel : BaseViewmodel
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
                MaintainExclusionDates.AddExclusionDate(WorkingExclusionDate);
                ExclusionDates.Add(new ExclusionDate() { Date = WorkingExclusionDate });
                Setup();
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
                MaintainExclusionDates.AddHoliday(WorkingHoliday);
                Holidays.Add(new ExclusionDate() { Date = WorkingHoliday });
                Setup();
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

        #endregion 
    }
}
