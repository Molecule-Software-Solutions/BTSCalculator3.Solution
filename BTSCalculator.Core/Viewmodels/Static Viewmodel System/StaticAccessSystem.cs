using System;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Provides static access to the more commonly used viewmodels within the core, and also allows crosstalk capabilities between viewmodels
    /// </summary>
    public class StaticAccessSystem
    {
        /// <summary>
        /// Static instance of the Application Viewmodel which is accessible to any core class during the lifetime of the application 
        /// </summary>
        public static ApplicationViewmodel ApplicationVM = new ApplicationViewmodel();

        /// <summary>
        /// Static instance of the Form Generator viewmodel
        /// </summary>
        public static FormGeneratorViewmodel FormGeneratorVM = new FormGeneratorViewmodel(new PDFForm());

        /// <summary>
        /// Static instance of the PDF Viewer viewmodel
        /// </summary>
        public static PDFViewerViewmodel PDFViewerViewmodel = new PDFViewerViewmodel(string.Empty); 

        /// <summary>
        /// Creates a new instance of the PDF Viewer viewmodel by passing in a path where the PDF will be loaded
        /// </summary>
        /// <param name="path"></param>
        public static void CreateNewPDFViewerViewmodel(string path)
        {
            PDFViewerViewmodel = new PDFViewerViewmodel(path); 
        }

        /// <summary>
        /// Creates a new instance of the Form Generator viewmodel by passing in a new <see cref="PDFForm"/>
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static void CreateNewFormGeneratorViewmodel(PDFForm form)
        {
            FormGeneratorVM = new FormGeneratorViewmodel(form);
        }

        /// <summary>
        /// Clears the form generator viewmodel 
        /// </summary>
        public static void ClearFormGeneratorVM()
        {
            FormGeneratorVM = new FormGeneratorViewmodel(new PDFForm()); 
        }

        /// <summary>
        /// Clears the PDF Viewer viewmodel
        /// </summary>
        public static void ClearPDFViewerVM()
        {
            PDFViewerViewmodel = new PDFViewerViewmodel(string.Empty);
        }

        /// <summary>
        /// Invokes the event <see cref="PDFWindowCalled"/>
        /// </summary>
        public static void CallPDFWindowOpen()
        {
            PDFWindowCalled.Invoke(null, EventArgs.Empty); 
        }

        /// <summary>
        /// Event that is called in order to trigger the opening of the PDF Viewer window
        /// </summary>
        public static event EventHandler PDFWindowCalled = (sender, e) => { };
    }
}
