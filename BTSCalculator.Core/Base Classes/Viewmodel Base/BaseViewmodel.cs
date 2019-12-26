using System.ComponentModel;

namespace BTSCalculator.Core
{
    public class BaseViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
