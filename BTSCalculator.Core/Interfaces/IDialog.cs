using System.ComponentModel;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Interface that allows for the production and display of a dialog box
    /// </summary>
    public interface IDialog : INotifyPropertyChanged
    {
        /// <summary>
        /// Type of dialog to be displayed
        /// </summary>
        DialogTypes DialogType { get; set; }

        /// <summary>
        /// Header message to be displayed
        /// </summary>
        string DialogHeader { get; set; }

        /// <summary>
        /// Message to be displayed in the body of the dialog
        /// </summary>
        string DialogMessage { get; set; }

        /// <summary>
        /// Timer that will be used to determine how long a message only dialog is on screen: NOT USED
        /// </summary>
        int DialogTimer { get; set; }

        /// <summary>
        /// Indicates that a dialog YES result was returned: NOT USED
        /// </summary>
        bool DialogYes { get; set; }

        /// <summary>
        /// Indicates that a dialog NO result was returned: NOT USED
        /// </summary>
        bool DialogNo { get; set; }

        /// <summary>
        /// Indicates that a dialog CANCEL result was returned: NOT USED
        /// </summary>
        bool DialogCancel { get; set; }
    }
}
