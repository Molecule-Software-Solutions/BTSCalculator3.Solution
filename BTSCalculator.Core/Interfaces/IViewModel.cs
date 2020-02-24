namespace BTSCalculator.Core
{
    /// <summary>
    /// Interface that determines a contract for all base viewmodels so that they are forced to carry the <see cref="ReturnToMainMenu_COMMAND"/>
    /// </summary>
    internal interface IViewModel
    {
        /// <summary>
        /// Requirement for <see cref="BaseViewmodel"/>
        /// </summary>
        RelayCommand ReturnToMainMenu_COMMAND { get; }
    }
}
