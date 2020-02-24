using System.ComponentModel;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Object that represents a new dialog that will be displayed
    /// </summary>
    public class DialogModel : IDialog
    {
        /// <summary>
        /// Dialog type to be displayed 
        /// </summary>
        public DialogTypes DialogType { get; set; }

        /// <summary>
        /// Header of the dialog to be displayed
        /// </summary>
        public string DialogHeader { get; set; }

        /// <summary>
        /// Message that will be displayed in the body of the dialog
        /// </summary>
        public string DialogMessage { get; set; }

        /// <summary>
        /// Not Used
        /// </summary>
        public int DialogTimer { get; set; }

        /// <summary>
        /// Not Used
        /// </summary>
        public bool DialogYes { get; set; }

        /// <summary>
        /// Not Used
        /// </summary>
        public bool DialogNo { get; set; }

        /// <summary>
        /// Not Used
        /// </summary>
        public bool DialogCancel { get; set; }

        /// <summary>
        /// Fired when a property changes in this model
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
