using BTSCalculator.Core;
using System.ComponentModel;
using System.Windows.Controls;

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for StandardDialogPage.xaml
    /// </summary>
    public partial class StandardDialogPage : Page, IDialog
    {
        public StandardDialogPage(IDialog dialog)
        {
            DataContext = StaticAccessSystem.ApplicationVM;
            InitializeComponent();
            var dialogContent = dialog;
            DialogMessage = dialogContent.DialogMessage;
            DialogHeader = dialogContent.DialogHeader;
            DialogType = dialogContent.DialogType;
        }

        public string DialogMessage { get; set; }
        public int DialogTimer { get; set; }
        public bool DialogYes { get; set; }
        public bool DialogNo { get; set; }
        public bool DialogCancel { get; set; }
        public string DialogHeader { get; set; }
        public DialogTypes DialogType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
