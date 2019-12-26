using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BTSCalculator.Core;

namespace BTSCalculator3
{
    internal static class ApplicationPageConverters
    {
        public static Page GetApplicationPage(this ApplicationPageTypes page)
        {
            switch (page)
            {
                case ApplicationPageTypes.MainMenu:
                    return new MainMenu();
                case ApplicationPageTypes.EnterCaseInformation:
                case ApplicationPageTypes.BidResultsScreen:
                case ApplicationPageTypes.CompleteFormPage:
                default:
                    return new Page();
            }
        }
    }
}
