namespace BTSCalculator.Core
{
    /// <summary>
    /// Provides an interface which transmits selected file path to a model
    /// </summary>
    public interface IFileSelection
    {
        /// <summary>
        /// Selected File Path
        /// </summary>
         string SelectedFilePath { get; set; }
    }
}
