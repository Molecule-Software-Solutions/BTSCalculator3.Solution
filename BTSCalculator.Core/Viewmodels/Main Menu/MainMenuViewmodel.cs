using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BTSCalculator.Core
{
    public class MainMenuViewmodel : BaseViewmodel
    {
        public MainMenuViewmodel()
        {
            SetupDataItems(); 
            ShowSetupErrors(); 
        }

        private void SetupDataItems()
        {
            // Fills collections 
            Holidays = RetrieveDates.RetrieveHolidays();
            ExclusionDates = RetrieveDates.RetrieveExclusionDates();

            // Creates a list that will contain all dates
            List<ExclusionDate> combinedList = new List<ExclusionDate>();

            // Adds all dates to the list
            foreach (ExclusionDate exclusionDate in Holidays)
            {
                combinedList.Add(exclusionDate);
            }
            foreach (ExclusionDate exclusionDate1 in ExclusionDates)
            {
                combinedList.Add(exclusionDate1);
            }

            // Sets Exclusion Dates Array for use during calculations
            SetupExclusionsArray(combinedList); 
        }

        private void SetupExclusionsArray(List<ExclusionDate> combinedList)
        {
            ExclusionDatesArray = combinedList.ToArray();
        }

        private void ShowSetupErrors()
        {
            string dialogErrors = string.Empty;
            bool showDialog = false; 
            if(StaticAccessSystem.ApplicationVM.County == "NOT SET")
            {
                dialogErrors += "COUNTY NAME IS NOT SET\n";
                showDialog = true;
            }
            if(ExclusionDatesArray.Length == 0)
            {
                dialogErrors += "HOLIDAY AND EXCLUSION DATES HAVE NOT BEEN SET\n";
                showDialog = true;  
            }

            if(showDialog)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel()
                {
                    DialogType = DialogTypes.Standard,
                    DialogHeader = "Setup Errors Detected",
                    DialogMessage = "The following setup errors have been detected\n\n" + dialogErrors +
                                    "\nPlease use the setup system to correct these issues before proceeding"
                });
            }
        }

        public DateTime JudgmentDate { get; set; } = DateTime.Today;
        public DateTime RentDueDate { get; set; } = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day + 1);
        public decimal UndisputedArrears { get; set; }
        public decimal UndisputedArrearsFromCalculation { get; set; }
        public decimal MonthlyRentalRate { get; set; }
        public bool Indigent { get; set; }
        public int BusinessDayCount { get; private set; }
        public int TotalDayCount { get; private set; }
        public decimal PerDiemRentDue { get; private set; }
        public decimal PerDiemRentRate { get; private set; }
        public decimal TotalDueToday { get; set; }
        public decimal TotalDueOnRentDate { get; set; }
        public decimal TotalCourtCostsDue { get; set; }
        public bool ShouldPerDiemRentBeCollected { get; set; }
        public bool ShouldUndisputedRentBeCollected { get; set; }
        public bool ShouldCourtCostsBeCollected { get; set; }

        /// <summary>
        /// Contains a list of exclusion dates stored in the database
        /// </summary>
        public ObservableCollection<ExclusionDate> ExclusionDates { get; private set; } = new ObservableCollection<ExclusionDate>();

        /// <summary>
        /// Contains a list of Holidays stored in the database 
        /// </summary>
        public ObservableCollection<ExclusionDate> Holidays { get; private set; } = new ObservableCollection<ExclusionDate>(); 

        public ExclusionDate[] ExclusionDatesArray { get; set; }

        public RelayCommand CalculateBond_COMMAND => new RelayCommand(() =>
        {
            // Error if no holidays or exclusion dates are present
            //if(ExclusionDatesArray.Length == 0)
            //{
            //    StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Holiday and Exclusion Date Error", DialogMessage = "At least one holiday or exclusion date must be entered prior to calculating a bond. Please add holidays or exclusion dates in System Setup" });
            //    return;
            //}

            // Performs calculations and populates all calculation properties 
            DateTime[] datesFromExclusionArray = ExclusionDatesArray.Select(c => c.Date).ToArray();
            BusinessDayCount = Calculations.CalculateBusinessTimeSpan(JudgmentDate, RentDueDate, datesFromExclusionArray);
            TotalDayCount = Calculations.CalculateTotalTimeSpan(JudgmentDate, RentDueDate);

            // If indigent, calculates the per diem rental rate and amount due... else sets these amounts to 0
            if(!Indigent)
            {
                if (BusinessDayCount > 5)
                {
                    PerDiemRentDue = Calculations.CalculatePerDiemRent(MonthlyRentalRate, JudgmentDate, RentDueDate);
                    PerDiemRentRate = Calculations.CalculatePerDiemRate(MonthlyRentalRate, JudgmentDate, RentDueDate);
                    TotalCourtCostsDue = StaticAccessSystem.ApplicationVM.DefaultCosts;
                    UndisputedArrearsFromCalculation = UndisputedArrears;
                    ShouldPerDiemRentBeCollected = true;
                    ShouldCourtCostsBeCollected = true;
                    ShouldUndisputedRentBeCollected = true;
                }
                else
                {
                    PerDiemRentDue = 0;
                    PerDiemRentRate = 0;
                    TotalCourtCostsDue = StaticAccessSystem.ApplicationVM.DefaultCosts;
                    UndisputedArrearsFromCalculation = UndisputedArrears;
                    ShouldPerDiemRentBeCollected = false;
                    ShouldCourtCostsBeCollected = true;
                    ShouldUndisputedRentBeCollected = true;
                }
            }
            else
            {
                if(BusinessDayCount > 5)
                {
                    PerDiemRentDue = Calculations.CalculatePerDiemRent(MonthlyRentalRate, JudgmentDate, RentDueDate);
                    PerDiemRentRate = Calculations.CalculatePerDiemRate(MonthlyRentalRate, JudgmentDate, RentDueDate);
                    TotalCourtCostsDue = 0;
                    UndisputedArrearsFromCalculation = 0;
                    ShouldUndisputedRentBeCollected = false;
                    ShouldPerDiemRentBeCollected = true;
                    ShouldCourtCostsBeCollected = false;
                }
                else
                {
                    PerDiemRentDue = 0;
                    PerDiemRentRate = 0;
                    TotalCourtCostsDue = 0;
                    UndisputedArrearsFromCalculation = 0;
                    ShouldUndisputedRentBeCollected = false;
                    ShouldPerDiemRentBeCollected = false;
                    ShouldCourtCostsBeCollected = false;
                }
            }
            // Calculates the total due
            CalculateTotalDue(); 
        });

        /// <summary>
        /// Performs a calculation to determine the total that will be due
        /// </summary>
        private void CalculateTotalDue()
        {
            TotalDueToday = PerDiemRentDue + UndisputedArrearsFromCalculation + TotalCourtCostsDue;
            TotalDueOnRentDate = MonthlyRentalRate;
        }

        /// <summary>
        /// Command that directs the UI to change to the System Settings page
        /// </summary>
        public RelayCommand GoToSystemSetup_COMMAND => new RelayCommand(() =>
        {
            StaticAccessSystem.ApplicationVM.CurrentPage = ApplicationPageTypes.ApplicationSetup;
        });

        /// <summary>
        /// Command that prepares a new viewmodel for the Form Generator and commands the UI to change screens to the form generator page
        /// </summary>
        public RelayCommand GoToFormGenerator_COMMAND => new RelayCommand(() =>
        {
            StaticAccessSystem.ClearFormGeneratorVM(); 
            StaticAccessSystem.CreateNewFormGeneratorViewmodel(new PDFForm()
            {
                RentalRate = MonthlyRentalRate,
                UndisputedRent = UndisputedArrearsFromCalculation,
                ProratedRent = PerDiemRentDue,
                TotalRentDeposited = UndisputedArrearsFromCalculation + PerDiemRentDue,
                JudgmentDate = JudgmentDate,
            });
            StaticAccessSystem.ApplicationVM.CurrentPage = ApplicationPageTypes.CompleteFormPage;
        });
    }
}
