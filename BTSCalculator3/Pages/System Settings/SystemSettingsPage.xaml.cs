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
using Microsoft.Win32;

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for SystemSettingsPage.xaml
    /// </summary>
    public partial class SystemSettingsPage : Page, IFileSelection
    {
        SystemSetupViewmodel ViewModel = new SystemSetupViewmodel(); 
        public SystemSettingsPage()
        {
            DataContext = ViewModel; 
            InitializeComponent();
        }

        public string SelectedFilePath { get; set; }

        private void BatchHolidayEntry_BTN_Click(object sender, RoutedEventArgs e)
        {
            if(CallFileSelectionBox())
            {
                ViewModel.HolidayBatchAdd_COMMAND.Execute(this);
            }
        }

        private bool CallFileSelectionBox()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Batch Holiday Files (*.BTS3H) | *.BTS3H";
            if(ofd.ShowDialog() == true)
            {
                SelectedFilePath = ofd.FileName;
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
