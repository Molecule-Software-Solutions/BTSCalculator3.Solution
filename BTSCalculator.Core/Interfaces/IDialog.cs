using System.ComponentModel;

namespace BTSCalculator.Core
{
    public interface IDialog : INotifyPropertyChanged
    {
        DialogTypes DialogType { get; set; }
        string DialogHeader { get; set; }
        string DialogMessage { get; set; }
        int DialogTimer { get; set; }
        bool DialogYes { get; set; }
        bool DialogNo { get; set; }
        bool DialogCancel { get; set; }
    }
}
