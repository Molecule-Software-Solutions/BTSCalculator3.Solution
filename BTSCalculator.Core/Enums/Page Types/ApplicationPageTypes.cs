namespace BTSCalculator.Core
{
    /// <summary>
    /// Represents the different page types that can be displayed by the UI
    /// </summary>
    public enum ApplicationPageTypes
    {
        /// <summary>
        /// Main menu
        /// </summary>
        MainMenu,

        /// <summary>
        /// Application setup screen
        /// </summary>
        ApplicationSetup,

        /// <summary>
        /// This page is not used by this system, but this option is build in order to provide for future expansion. DO NOT USE
        /// </summary>
        BidResultsScreen,

        /// <summary>
        /// Form generation screen
        /// </summary>
        CompleteFormPage,
    }
}
