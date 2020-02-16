using System.ComponentModel;

namespace BTSCalculator.Core
{
    public class DialogModel : IDialog
    {
        public DialogTypes DialogType { get; set; }
        public string DialogHeader { get; set; }
        public string DialogMessage { get; set; }
        public int DialogTimer { get; set; }
        public bool DialogYes { get; set; }
        public bool DialogNo { get; set; }
        public bool DialogCancel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
