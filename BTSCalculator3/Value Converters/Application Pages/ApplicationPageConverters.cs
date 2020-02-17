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
                case ApplicationPageTypes.ApplicationSetup:
                    return new SystemSettingsPage();
                case ApplicationPageTypes.BidResultsScreen:
                case ApplicationPageTypes.CompleteFormPage:
                default:
                    return new Page();
            }
        }

        public static Page GetDialogPage(this IDialog dialog)
        {
            switch (dialog.DialogType)
            {
                case DialogTypes.None:
                    return new Page(); 
                case DialogTypes.Standard:
                    return new StandardDialogPage(dialog);
                case DialogTypes.MessageOnlyTimed:
                    return new Page(); 
                default:
                    return new Page(); 
            }
        }
    }
}
