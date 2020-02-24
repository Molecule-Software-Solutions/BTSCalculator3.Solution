using System.ComponentModel;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Base viewmodel that will be inherited by all subordinate viewmodels
    /// </summary>
    public class BaseViewmodel : INotifyPropertyChanged, IViewModel
    {
        /// <summary>
        /// General command which returns the UI to the main menu
        /// </summary>
        public RelayCommand ReturnToMainMenu_COMMAND => new RelayCommand(() =>
        {
            StaticAccessSystem.ApplicationVM.CurrentPage = ApplicationPageTypes.MainMenu;
        });

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
