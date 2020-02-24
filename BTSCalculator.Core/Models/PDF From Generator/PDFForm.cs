using NC_Civil_Case_Number_Parser;
using System;
using System.ComponentModel;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Object that defines the values to be held within a PDF form
    /// </summary>
    public class PDFForm : INotifyPropertyChanged
    {
        #region Private Backing Fields

        private string _CaseNumber = string.Empty;
        private string _DefendantName = string.Empty; 
        private string _DefendantAddress = string.Empty;
        private string _DefendantAddress2 = string.Empty;
        private string _DefendantCity = string.Empty;
        private string _DefendantState = string.Empty;
        private string _PlaintiffName = string.Empty;
        private string _PlaintiffAddress = string.Empty;
        private string _PlaintiffAddress2 = string.Empty;
        private string _PlaintiffCity = string.Empty;
        private string _PlaintiffState = string.Empty;

        #endregion

        #region Automatic Properties 

        /// <summary>
        /// Defendant's zip code
        /// </summary>
        public string DefendantZip { get; set; } = string.Empty;

        /// <summary>
        /// Plaintiff's city
        /// </summary>
        public string PlaintiffZip { get; set; } = string.Empty;

        /// <summary>
        /// Rental term (How often rent is collected)
        /// </summary>
        public RentalPaymentTerms RentalTerm { get; set; } = RentalPaymentTerms.Monthly;

        /// <summary>
        /// Rate collected at the beginning of each rental period
        /// </summary>
        public decimal RentalRate { get; set; } = 0;

        /// <summary>
        /// Amount of undisputed rent in arrears that is to be collected
        /// </summary>
        public decimal UndisputedRent { get; set; } = 0;

        /// <summary>
        /// Amount of prorated rent that is to be collected
        /// </summary>
        public decimal ProratedRent { get; set; } = 0;

        /// <summary>
        /// Total amount of rent that will be deposited with the clerk
        /// NOTE: Includes prorated and undisputed rent amounts
        /// </summary>
        public decimal TotalRentDeposited { get; set; } = 0;

        /// <summary>
        /// Day of the month that rent will be due (if term is monthly)
        /// </summary>
        public int DayOfMonthRentDue { get; set; } = 1;

        /// <summary>
        /// Output path of the new pdf form
        /// </summary>
        public string OutputPath { get; set; } = string.Empty; 

        /// <summary>
        /// Date judgment was rendered 
        /// </summary>
        public DateTime JudgmentDate { get; set; }

        /// <summary>
        /// Day of week that rent is due (for weekly rental periods)
        /// </summary>
        public string DayOfWeekRentDue { get; set; } = string.Empty; 

        #endregion 

        #region Backed Properties 

        /// <summary>
        /// New CVD case number that will be assigned to the appeal
        /// </summary>
        public string CaseNumber
        {
            get => _CaseNumber;
            set
            {
                ParseSystem.CivilParse cp = new ParseSystem.CivilParse();
                _CaseNumber = cp.ParseCivilCaseNumber(value.Replace("_",""));
            }
        }

        /// <summary>
        /// Defendant's name
        /// </summary>
        public string DefendantName
        {
            get { return _DefendantName; }
            set { _DefendantName = value.ToUpper(); }
        }

        /// <summary>
        /// First line of defendant's address
        /// </summary>
        public string DefendantAddress
        {
            get { return _DefendantAddress; }
            set { _DefendantAddress = value.ToUpper(); }
        }

        /// <summary>
        /// Second line of defendant's address 
        /// </summary>
        public string DefendantAddress2
        {
            get { return _DefendantAddress2; }
            set { _DefendantAddress2 = value.ToUpper(); }
        }

        /// <summary>
        /// Defendant's city
        /// </summary>
        public string DefendantCity
        {
            get { return _DefendantCity; }
            set { _DefendantCity = value.ToUpper(); }
        }

        /// <summary>
        /// Defendant's state
        /// </summary>
        public string DefendantState
        {
            get { return _DefendantState; }
            set { _DefendantState = value.ToUpper(); }
        }

        /// <summary>
        /// Plaintiff's Name
        /// </summary>
        public string PlaintiffName
        {
            get { return _PlaintiffName; }
            set { _PlaintiffName = value.ToUpper(); }
        }

        /// <summary>
        /// First line of plaintiff's address 
        /// </summary>
        public string PlaintiffAddress
        {
            get { return _PlaintiffAddress; }
            set { _PlaintiffAddress = value.ToUpper(); }
        }

        /// <summary>
        /// Second line of plaintiff's address
        /// </summary>
        public string PlaintiffAddress2
        {
            get { return _PlaintiffAddress2; }
            set { _PlaintiffAddress2 = value.ToUpper(); }
        }

        /// <summary>
        /// Plaintiff's city
        /// </summary>
        public string PlaintiffCity
        {
            get { return _PlaintiffCity; }
            set { _PlaintiffCity = value.ToUpper(); }
        }

        /// <summary>
        /// Plaintiff's state
        /// </summary>
        public string PlaintiffState
        {
            get { return _PlaintiffState; }
            set { _PlaintiffState = value.ToUpper(); }
        }

        /// <summary>
        /// Property changed event that is fired upon the modification of any properties herein 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
