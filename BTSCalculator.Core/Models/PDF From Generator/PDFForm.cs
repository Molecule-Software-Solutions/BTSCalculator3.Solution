using NC_Juvenile_Case_Number_Parser;

namespace BTSCalculator.Core
{
    public class PDFForm
    {
        #region Private Backing Fields

        private string _CaseNumber = string.Empty;
        private string _DefendantName;
        private string _DefendantAddress;
        private string _DefendantAddress2;
        private string _DefendantCity;
        private string _DefendantState;
        private string _PlaintiffName;
        private string _PlaintiffAddress;
        private string _PlaintiffAddress2;
        private string _PlaintiffCity;
        private string _PlaintiffState;
        private string _DayOfWeekRentDue;

        #endregion

        #region Automatic Properties 

        public string DefendantZip { get; set; }
        public string PlaintiffZip { get; set; }
        public RentalPaymentTerms RentalTerm { get; set; }
        public decimal RentalRate { get; set; }
        public decimal UndisputedRent { get; set; }
        public decimal ProratedRent { get; set; }
        public decimal TotalRentDeposited { get; set; }
        public int DayOfMonthRentDue { get; set; }

        public string OutputPath { get; set; }

        #endregion 

        #region Backed Properties 

        public string CaseNumber
        {
            get => _CaseNumber;
            set
            {
                CaseNumberParser cnp = new CaseNumberParser(value);
                _CaseNumber = cnp.OutputCaseNumber;
            }
        }

        public string DefendantName
        {
            get { return _DefendantName; }
            set { _DefendantName = value.ToUpper(); }
        }

        public string DefendantAddress
        {
            get { return _DefendantAddress; }
            set { _DefendantAddress = value.ToUpper(); }
        }

        public string DefendantAddress2
        {
            get { return _DefendantAddress2; }
            set { _DefendantAddress2 = value.ToUpper(); }
        }

        public string DefendantCity
        {
            get { return _DefendantCity; }
            set { _DefendantCity = value.ToUpper(); }
        }

        public string DefendantState
        {
            get { return _DefendantState; }
            set { _DefendantState = value.ToUpper(); }
        }

        public string PlaintiffName
        {
            get { return _PlaintiffName; }
            set { _PlaintiffName = value.ToUpper(); }
        }

        public string PlaintiffAddress
        {
            get { return _PlaintiffAddress; }
            set { _PlaintiffAddress = value.ToUpper(); }
        }

        public string PlaintiffAddress2
        {
            get { return _PlaintiffAddress2; }
            set { _PlaintiffAddress2 = value.ToUpper(); }
        }

        public string PlaintiffCity
        {
            get { return _PlaintiffCity; }
            set { _PlaintiffCity = value.ToUpper(); }
        }

        public string PlaintiffState
        {
            get { return _PlaintiffState; }
            set { _PlaintiffState = value.ToUpper(); }
        }

        public string DayOfWeekRentDue
        {
            get { return _DayOfWeekRentDue; }
            set { _DayOfWeekRentDue = value.ToUpper(); }
        }

        #endregion
    }
}
