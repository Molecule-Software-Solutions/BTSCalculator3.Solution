namespace BTSCalculator.Core
{
    /// <summary>
    /// Represents the dialog types that can be called by the UI
    /// </summary>
    public enum DialogTypes
    {
        /// <summary>
        ///  None represents a dialog that is not visible
        /// </summary>
        None,

        /// <summary>
        /// Standard system dialog with a header, message, and ok button
        /// </summary>
        Standard,

        /// <summary>
        /// This dialog is not used by this system currently, but further expansion may require it. DO NOT USE
        /// </summary>
        MessageOnlyTimed
    }
}
