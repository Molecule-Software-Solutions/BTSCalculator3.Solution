using System.ComponentModel;

namespace BTSCalculator.Core
{
    public class BaseViewmodel : INotifyPropertyChanged, IViewModel
    {
        public RelayCommand ReturnToMainMenu_COMMAND => new RelayCommand(() =>
        {
            StaticAccessSystem.ApplicationVM.CurrentPage = ApplicationPageTypes.MainMenu;
        });

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
