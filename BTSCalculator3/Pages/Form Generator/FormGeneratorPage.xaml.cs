using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BTSCalculator.Core;

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
