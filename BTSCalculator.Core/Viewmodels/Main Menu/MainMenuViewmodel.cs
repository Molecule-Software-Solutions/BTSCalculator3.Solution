using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel for the Main Menu
    /// </summary>
    public class MainMenuViewmodel : BaseViewmodel
    {
        #region Primary Constructor

        public MainMenuViewmodel()
        {
            SetupDataItems(); 
            ShowSetupErrors(); 
        }

        #endregion

        #region Properties 

        /// <summary>
        /// Date judgment was rendered
        /// </summary>
        public DateTime JudgmentDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Next rental due date
        /// </summary>
        public DateTime RentDueDate { get; set; } = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day + 1);

        /// <summary>
        /// Amount of arrears that the defendant DOES NOT contest during the appeal process
        /// </summary>
        public decimal UndisputedArrears { get; set; }

        /// <summary>
        /// Calculation logic sets this property and uses it as a final total of arrears to be paid
        /// </summary>
        public decimal UndisputedArrearsFromCalculation { get; set; }

        /// <summary>
        /// Rate of rent paid...NOTE: RateOfRent should be the name of this property, but due to UI binding, will be leaving this as is for now
        /// </summary>
        public decimal MonthlyRentalRate { get; set; }

        /// <summary>
        /// Property that is set to true if the defendant is indigent
        /// </summary>
        public bool Indigent { get; set; }

        /// <summary>
        /// Calculation determines the number of business days between <see cref="JudgmentDate"/> and <see cref="RentDueDate"/>
        /// NOTE: this property will be overwritten upon each calculation and cannot be set externally
        /// </summary>
        public int BusinessDayCount { get; private set; }

        /// <summary>
        /// Total number of days between <see cref="JudgmentDate"/> and <see cref="RentDueDate"/>
        /// </summary>
        public int TotalDayCount { get; private set; }

        /// <summary>
        /// Calculation determines the amount of prorated rent due
        /// </summary>
        public decimal PerDiemRentDue { get; private set; }

        /// <summary>
        /// Calculation determines the amount of rent to charge for each day
        /// </summary>
        public decimal PerDiemRentRate { get; private set; }

        /// <summary>
        /// Calculation determines what the total amount that will be paid by the defendant at execution 
        /// </summary>
        public decimal TotalDueToday { get; private set; }

        /// <summary>
        /// Calculation determines what the total amount that will be paid on the next rent due date
        /// </summary>
        public decimal TotalDueOnRentDate { get; private set; }

        /// <summary>
        /// Calculation determines the total court costs that are due
        /// </summary>
        public decimal TotalCourtCostsDue { get; private set; }

        /// <summary>
        /// Calculation sets this boolean value if Prorated rent should be collected 
        /// </summary>
        public bool ShouldPerDiemRentBeCollected { get; private set; }

        /// <summary>
        /// Calculation sets this boolean value if undispluted rent in arrears should be collected 
        /// </summary>
        public bool ShouldUndisputedRentBeCollected { get; private set; }

        /// <summary>
        /// Calculation sets this boolean value if court costs should be collected 
        /// </summary>
        public bool ShouldCourtCostsBeCollected { get; private set; }

        /// <summary>
        /// Contains a list of exclusion dates stored in the database
        /// </summary>
        public ObservableCollection<ExclusionDate> ExclusionDates { get; private set; } = new ObservableCollection<ExclusionDate>();

        /// <summary>
        /// Contains a list of Holidays stored in the database 
        /// </summary>
        public ObservableCollection<ExclusionDate> Holidays { get; private set; } = new ObservableCollection<ExclusionDate>();

        /// <summary>
        /// Contains an array of exclusion dates that are populated by the internal database 
        /// </summary>
        public ExclusionDate[] ExclusionDatesArray { get; private set; }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Sets up all initial data items, including holidays and exclusion dates
        /// </summary>
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

        /// <summary>
        /// Returns an array to fill the exclusion dates array (from <see cref="List{ExclusionDate}"/>)
        /// </summary>
        /// <param name="combinedList"></param>
        private void SetupExclusionsArray(List<ExclusionDate> combinedList)
        {
            ExclusionDatesArray = combinedList.ToArray();
        }

        /// <summary>
        /// Displays any setup errors that were found during the initial construction of the viewmodel
        /// </summary>
        private void ShowSetupErrors()
        {
            string dialogErrors = string.Empty;
            bool showDialog = false;
            if (StaticAccessSystem.ApplicationVM.County == "NOT SET")
            {
                dialogErrors += "COUNTY NAME IS NOT SET\n";
                showDialog = true;
            }
            if (ExclusionDatesArray.Length == 0)
            {
                dialogErrors += "HOLIDAY AND EXCLUSION DATES HAVE NOT BEEN SET\n";
                showDialog = true;
            }

            if (showDialog)
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

        /// <summary>
        /// Performs a calculation to determine the total that will be due
        /// </summary>
        private void CalculateTotalDue()
        {
            TotalDueToday = PerDiemRentDue + UndisputedArrearsFromCalculation + TotalCourtCostsDue;
            TotalDueOnRentDate = MonthlyRentalRate;
        }

        #endregion

        #region Commands 

        /// <summary>
        /// Triggers bond calculation
        /// </summary>
        public RelayCommand CalculateBond_COMMAND => new RelayCommand(() =>
        {
            // Error if no holidays or exclusion dates are present
            if (ExclusionDatesArray.Length == 0)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Holiday and Exclusion Date Error", DialogMessage = "At least one holiday or exclusion date must be entered prior to calculating a bond. Please add holidays or exclusion dates in System Setup" });
                return;
            }

            // Performs calculations and populates all calculation properties 
            DateTime[] datesFromExclusionArray = ExclusionDatesArray.Select(c => c.Date).ToArray();
            BusinessDayCount = Calculations.CalculateBusinessTimeSpan(JudgmentDate, RentDueDate, datesFromExclusionArray);
            TotalDayCount = Calculations.CalculateTotalTimeSpan(JudgmentDate, RentDueDate);

            // If indigent, calculates the per diem rental rate and amount due... else sets these amounts to 0
            if (!Indigent)
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
                if (BusinessDayCount > 5)
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

        #endregion 
    }
}
