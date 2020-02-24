namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel that controls the PDF Viewer window
    /// </summary>
    public class PDFViewerViewmodel : BaseViewmodel
    {
        /// <summary>
        /// Constructor for the PDF Viewer viewmodel
        /// </summary>
        /// <param name="path"></param>
        public PDFViewerViewmodel( string path )
        {
            Path = path; 
        }

        /// <summary>
        /// Contains the path to the PDF file that will be viewed
        /// </summary>
        public string Path { get; private set; }
    }
}
