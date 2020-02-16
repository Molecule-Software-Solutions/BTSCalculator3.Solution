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
            SubscriptionSystem();
            ShowSetupErrors(); 
        }

        private void ShowSetupErrors()
        {
            string dialogErrors = string.Empty;
            if(StaticAccessSystem.ApplicationVM.County == "NOT SET")
            {
                dialogErrors += "COUNTY NAME IS NOT SET\n";
            }
            // other test to check for list
            dialogErrors += "HOLIDAY / CLOSINGS LIST CONTAINS NO DATES\n";

            StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel()
            {
                DialogType = DialogTypes.Standard,
                DialogHeader = "Setup Errors Detected",
                DialogMessage = "The following setup errors have been detected\n\n" + dialogErrors +
                "\nPlease use the setup system to correct these issues before proceeding"
            });
        }

        private void SubscriptionSystem()
        {
            PropertyChanged += MainMenuViewmodel_PropertyChanged;
        }

        private void MainMenuViewmodel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
        public DateTime JudgmentDate { get; set; } = DateTime.Today;
        public DateTime RentDueDate { get; set; } = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day + 1);
        public decimal UndisputedArrears { get; set; }
        public decimal MonthlyRentalRate { get; set; }
        public bool DisputesArrears { get; set; }
        public bool Indigent { get; set; }

        public int DateCalculation { get; set; }
        public int DateCalculation2 { get; set; }
        public decimal PerDiemRent { get; set; }

        /// <summary>
        /// Contains a list of exclusion dates stored in the database
        /// </summary>
        public ObservableCollection<ExclusionDate> ExclusionDates { get; set; } = new ObservableCollection<ExclusionDate>(); 

        private decimal RecalculateArrearsToBePaid()
        {
            throw new NotImplementedException(); 
        }

        private decimal RecalculateRentToBePaid()
        {
            throw new NotImplementedException();
        }

        private decimal RecalculatePerDiem()
        {
            throw new NotImplementedException();
        }

        private decimal CalculateTotal()
        {
            throw new NotImplementedException();
        }

        private int ExclusionDateCount(DateTime start, DateTime end, DateTime[] exclusionDates)
        {
            DateTime sd = start;
            sd = sd.AddDays(1.0);
            DateTime ed = end;
            int exclusionCount = 0;
            while(DateTime.Compare(sd, ed) <= 0)
            {
                if(sd.DayOfWeek == DayOfWeek.Saturday || sd.DayOfWeek == DayOfWeek.Sunday)
                {
                    exclusionCount += 1;
                }
                else
                {
                    foreach (DateTime dateTime in exclusionDates)
                    {
                        if (sd == dateTime)
                        {
                            exclusionCount += 1;
                        }
                    }
                }
                sd = sd.AddDays(1);
            }
            return exclusionCount;
        }

        private decimal CalculatePerDiemRent(DateTime[] exclusionDates)
        {
            return (MonthlyRentalRate / 30) * CalculateBusinessTimeSpan(JudgmentDate, RentDueDate, exclusionDates);
        }

        private int CalculateTotalTimeSpan(DateTime start, DateTime end)
        {
            int comparison = DateTime.Compare(start, end);
            if (comparison == 0) return 0;
            if (comparison > 0) return -1;
            if (comparison < 0)
            {
                TimeSpan ts = end - start;
                return ts.Days;
            }
            return -00;
        }

        private int CalculateBusinessTimeSpan(DateTime start, DateTime end, DateTime[] exclusionDates)
        {
            int comparison = DateTime.Compare(start, end);
            if (comparison == 0) return 0;
            if (comparison > 0) return -1;
            if (comparison < 0)
            {
                TimeSpan ts = end - start;
                return ts.Days - ExclusionDateCount(start, end, exclusionDates);
            }
            return -99;
        }

        public RelayCommand ShowTestDialog_COMMAND => new RelayCommand(() =>
        {
            DateTime[] dtexc = new DateTime[5];
            dtexc[0] = DateTime.Parse("2/17/2020");
            DateCalculation = CalculateBusinessTimeSpan(JudgmentDate, RentDueDate, dtexc);
            DateCalculation2 = CalculateTotalTimeSpan(JudgmentDate, RentDueDate);
            PerDiemRent = CalculatePerDiemRent(dtexc);
        });

    }
}
