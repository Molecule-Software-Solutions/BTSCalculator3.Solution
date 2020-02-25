using BTSCalculator.Core;
using System.Windows.Controls;
using System.Windows.Input;

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for FormGeneratorPage.xaml
    /// </summary>
    public partial class FormGeneratorPage : Page
    {
        private FormGeneratorViewmodel ViewModel = StaticAccessSystem.FormGeneratorVM;
        public FormGeneratorPage()
        {
            DataContext = ViewModel; 
            InitializeComponent();
            CaseNumber_MTB.Focus(); 
            CaseNumber_MTB.SelectAll(); 
        }

        private void DayOfMonth_TB_GotMouseCapture(object sender, MouseEventArgs e)
        {
            DayOfMonth_TB_Selectall(); 
        }

        private void DayOfMonth_TB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DayOfMonth_TB_Selectall();
        }

        private void DayOfMonth_TB_Selectall()
        {
            DayOfMonth_TB.SelectAll(); 
        }

        private void CaseNumber_MTB_GotMouseCapture(object sender, MouseEventArgs e)
        {
            CaseNumber_MTB_Selectall();
        }

        private void CaseNumber_MTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CaseNumber_MTB_Selectall(); 
        }

        private void CaseNumber_MTB_Selectall()
        {
            CaseNumber_MTB.SelectAll(); 
        }
    }
}
