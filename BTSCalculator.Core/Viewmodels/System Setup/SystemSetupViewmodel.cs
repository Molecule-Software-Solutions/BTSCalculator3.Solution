using System;
using System.Collections.ObjectModel;

namespace BTSCalculator.Core
{
    public class SystemSetupViewmodel : BaseViewmodel
    {
        public SystemSetupViewmodel()
        {
            Setup(); 
        }

        private void Setup()
        {
            ExclusionDates = RetrieveDates.RetrieveExclusionDates();
            Holidays = RetrieveDates.RetrieveHolidays();
            CountyName = CountyNameManagement.GetCountyName(); 
        }

        private string _CountyName = string.Empty; 

        public ObservableCollection<ExclusionDate> ExclusionDates { get; private set; } = new ObservableCollection<ExclusionDate>();
        public ObservableCollection<ExclusionDate> Holidays { get; private set; } = new ObservableCollection<ExclusionDate>();
        public string CountyName
        {
            get => _CountyName;
            set
            {
                _CountyName = value.ToUpper(); 
            }
        }
        public DateTime WorkingExclusionDate { get; set; } = DateTime.Today;
        public ExclusionDate SelectedExclusionDate { get; set; } 
        public ExclusionDate SelectedHoliday { get; set; }
        public DateTime WorkingHoliday { get; set; } = DateTime.Today;

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

        public RelayCommand DeleteExclusionDate_COMMAND => new RelayCommand(() =>
        {
            if(SelectedExclusionDate == null)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Trying To Delete New Entry", DialogMessage = "No Entry Selected. Please select an entry to delete it" });
                return;
            }

            if(SelectedExclusionDate.RecordID == 0)
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


    }
}
