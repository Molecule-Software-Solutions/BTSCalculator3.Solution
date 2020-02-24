using iText.Forms;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;

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
            WorkingPDFForm.PropertyChanged += WorkingPDFForm_PropertyChanged;
            MonthlyRentalElementsVisible = true; 
        }

        /// <summary>
        /// Monitors the property changed events within the PDF form property 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkingPDFForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(WorkingPDFForm.RentalTerm))
            {
                if (WorkingPDFForm.RentalTerm == RentalPaymentTerms.Monthly)
                {
                    MonthlyRentalElementsVisible = true;
                    WeeklyRentalElementsVisible = false; 
                }
                else if(WorkingPDFForm.RentalTerm == RentalPaymentTerms.Weekly)
                {
                    MonthlyRentalElementsVisible = false;
                    WeeklyRentalElementsVisible = true; 
                }
            }
        }

        #endregion

        #region Properties 

        /// <summary>
        /// The current PDF Form being constructed
        /// </summary>
        public PDFForm WorkingPDFForm { get; private set; }

        /// <summary>
        /// Contains a human readable version of the Rental Due Periods
        /// </summary>
        public Dictionary<string, string> RentDuePeriods { get; set; } = RentDueLibrary.RentDueDictionary();

        /// <summary>
        /// Contains a human readable version of the weekdays
        /// </summary>
        public Dictionary<string, string> DaySelections { get; set; } = DayOfWeekLibrary.DaysOfTheWeek();

        /// <summary>
        /// Determines whether the rental terms for a month will be visible on the UI (if utilized)
        /// </summary>
        public bool MonthlyRentalElementsVisible { get; set; }

        /// <summary>
        /// Determines whether the rental terms for a week will be visible on the UI (if utilized)
        /// </summary>
        public bool WeeklyRentalElementsVisible { get; set; }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Constructs the PDF form by using the viewmodel's <see cref="WorkingPDFForm"/> property
        /// </summary>
        /// <returns></returns>
        private string FormConstructor()
        {
            string formPath = AppDomain.CurrentDomain.BaseDirectory + @"Resources/BTSForm.pdf";
            string outputPath = System.IO.Path.GetTempFileName();
            string finalOutputPath = System.IO.Path.GetTempPath() + System.IO.Path.GetFileNameWithoutExtension(outputPath) + ".pdf";

            try
            {
                using (PdfDocument document = new PdfDocument(new PdfReader(formPath), new PdfWriter(finalOutputPath)))
                {
                    PdfAcroForm acro = PdfAcroForm.GetAcroForm(document, false);
                    acro.GetField("FileNo").SetValue(WorkingPDFForm.CaseNumber);
                    acro.GetField("County").SetValue(StaticAccessSystem.ApplicationVM.County);
                    acro.GetField("PlaintiffName").SetValue(WorkingPDFForm.PlaintiffName);
                    acro.GetField("PlaintiffAddr1").SetValue(WorkingPDFForm.PlaintiffAddress);
                    acro.GetField("PlaintiffAddr2").SetValue(WorkingPDFForm.PlaintiffAddress2);
                    acro.GetField("PlaintiffCity").SetValue(WorkingPDFForm.PlaintiffCity);
                    acro.GetField("PlaintiffState").SetValue(WorkingPDFForm.PlaintiffState);
                    acro.GetField("PlaintiffZip").SetValue(WorkingPDFForm.PlaintiffZip);
                    acro.GetField("DefendantName").SetValue(WorkingPDFForm.DefendantName);
                    acro.GetField("DefendantAddr1").SetValue(WorkingPDFForm.DefendantAddress);
                    acro.GetField("DefendantAddr2").SetValue(WorkingPDFForm.DefendantAddress2);
                    acro.GetField("DefendantCity").SetValue(WorkingPDFForm.DefendantCity);
                    acro.GetField("DefendantState").SetValue(WorkingPDFForm.DefendantState);
                    acro.GetField("DefendantZip").SetValue(WorkingPDFForm.DefendantZip);
                    acro.GetField("JudgmentDate").SetValue(WorkingPDFForm.JudgmentDate.ToShortDateString());
                    acro.GetField("RentAmount").SetValue(WorkingPDFForm.RentalRate.ToString());
                    if (WorkingPDFForm.RentalTerm == RentalPaymentTerms.Monthly)
                    {
                        acro.GetField("PerMonth").SetValue("Yes");
                        acro.GetField("RentDueDate").SetValue(WorkingPDFForm.DayOfMonthRentDue.GetMonthlyDueDateStringFromInt());
                    }
                    else if (WorkingPDFForm.RentalTerm == RentalPaymentTerms.Weekly)
                    {
                        acro.GetField("PerWeek").SetValue("Yes");
                        acro.GetField("RentDueDate").SetValue($"Weekly on {WorkingPDFForm.DayOfWeekRentDue}");
                    }
                    acro.GetField("UndisputedRent").SetValue(decimal.Round(WorkingPDFForm.UndisputedRent, 2, MidpointRounding.AwayFromZero).ToString());
                    acro.GetField("ProratedRent").SetValue(decimal.Round(WorkingPDFForm.ProratedRent, 2, MidpointRounding.AwayFromZero).ToString());
                    acro.GetField("Total").SetValue(decimal.Round(WorkingPDFForm.TotalRentDeposited, 2, MidpointRounding.AwayFromZero).ToString());
                    acro.GetField("SignedDate1").SetValue(DateTime.Today.ToShortDateString());
                    acro.GetField("SignedDate2").SetValue(DateTime.Today.ToShortDateString());
                    acro.GetField("DepositDate").SetValue(DateTime.Today.ToShortDateString());
                    document.Close();
                }
            }
            catch (Exception ex)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Form Generation Error", DialogMessage = ex.Message });
            }

            return finalOutputPath; 
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command that instructs the viewmodel to produce a PDF form
        /// </summary>
        public RelayCommand GenerateForm_COMMAND => new RelayCommand(() =>
        {
            // Error checking sequence 
            if(string.IsNullOrEmpty(WorkingPDFForm.CaseNumber.Replace("_","").Replace(" ", "")))
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Missing Information", DialogMessage = "The case number field was not filled properly. Please check your entries and try again" });
                return; 
            }
            if(!WorkingPDFForm.CaseNumber.Contains("CVD"))
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Incorrect Case Number", DialogMessage = "It appears that the case number may not be valid. Please check entries and try again. NOTE: Case number must be a new CVD file" });
                return; 
            }
            if(string.IsNullOrEmpty(WorkingPDFForm.PlaintiffName) || string.IsNullOrEmpty(WorkingPDFForm.DefendantName))
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Missing Information", DialogMessage = "Please check plaintiff and defendant names. These values are required" });
                return;
            }
            if(WorkingPDFForm.RentalTerm == RentalPaymentTerms.Monthly && WorkingPDFForm.DayOfMonthRentDue == 0)
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Missing Information", DialogMessage = "Day of month that rent is due is a required field. Please check your entries and try again" });
                return;
            }
            if(WorkingPDFForm.RentalTerm == RentalPaymentTerms.Weekly && string.IsNullOrEmpty(WorkingPDFForm.DayOfWeekRentDue))
            {
                StaticAccessSystem.ApplicationVM.ShowDialog(new DialogModel() { DialogType = DialogTypes.Standard, DialogHeader = "Missing Information", DialogMessage = "Day of week that rent is due is a required field. Please check your entries and try again" });
                return; 
            }

            // If error checking passes, produce form
            WorkingPDFForm.OutputPath = FormConstructor();
            CallPDFViewer(); 
        });

        /// <summary>
        /// Method which populates the PDF Viewer viewmodel and calls the window to open
        /// </summary>
        private void CallPDFViewer()
        {
            StaticAccessSystem.CreateNewPDFViewerViewmodel(WorkingPDFForm.OutputPath);
            StaticAccessSystem.CallPDFWindowOpen();
        }

        #endregion 
    }
}
