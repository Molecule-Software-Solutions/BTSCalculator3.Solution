using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTSCalculator.Core
{
    public class MainMenuViewmodel : BaseViewmodel
    {
        public MainMenuViewmodel()
        {

        }

        private void SubscriptionSystem()
        {
            PropertyChanged += MainMenuViewmodel_PropertyChanged;
        }

        private void MainMenuViewmodel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public DateTime JudgmentDate { get; set; }
        public DateTime RentDueDate { get; set; }
        public decimal DisputedArrears { get; set; }
        public decimal UndisputedArrears { get; set; }
        public decimal MonthlyRentalRate { get; set; }
        public bool DisputesArrears { get; set; }
        public bool Indigent { get; set; }

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

    }
}
