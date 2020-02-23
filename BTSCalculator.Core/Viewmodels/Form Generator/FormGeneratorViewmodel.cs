using iText;
using iText.Kernel.Pdf;
using iText.Forms; 
using System;

namespace BTSCalculator.Core
{
    /// <summary>
    /// Viewmodel that controls the system logic for generating PDF forms
    /// </summary>
    public class FormGeneratorViewmodel : BaseViewmodel
    {
        #region Constructor 

        /// <summary>
        /// Default constructor containing a <see cref="PDFForm"/> parameter for passing case information into the viewmodel
        /// </summary>
        /// <param name="form"></param>
        public FormGeneratorViewmodel(PDFForm form)
        {
            WorkingPDFForm = form;
        }

        #endregion 

        #region Properties 

        /// <summary>
        /// The current PDF Form being constructed
        /// </summary>
        public PDFForm WorkingPDFForm { get; private set; }

        /// <summary>
        /// Property that represents either a monthly or weekly rental cycle
        /// False for Monthly, and True for Weekly. 
        /// 
        /// NOTE: This would represent an override of standard monthly rental periods 
        /// </summary>
        public bool RentalPaymentPeriod { get; set; }

        #endregion

        #region Private Methods 

        private string FormConstructor()
        {
            string formPath = "";
            using (PdfReader reader = new PdfReader(formPath))
            {
                PdfDocument document = new PdfDocument(reader);
                PdfAcroForm acro = PdfAcroForm.GetAcroForm(document, false);
                acro.GetField("Test").SetValue("Value");
                document.Close(); 
            }

            return string.Empty; 
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command that instructs the viewmodel to produce a PDF form
        /// </summary>
        public RelayCommand GenerateForm_COMMAND => new RelayCommand(() =>
        {
            throw new NotImplementedException(); 
        });

        #endregion 
    }
}
