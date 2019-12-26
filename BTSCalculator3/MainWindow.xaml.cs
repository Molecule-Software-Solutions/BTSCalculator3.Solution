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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationViewmodel ViewModel;
        public MainWindow()
        {
            ViewModel = StaticAccessSystem.ApplicationVM;
            DataContext = ViewModel;
            InitializeComponent();
            ViewmodelPropertyChangeMonitor();
            SetDefaultPage(); 
        }

        private void SetDefaultPage()
        {
            WindowContentFrame.Content = ViewModel.CurrentPage.GetApplicationPage(); 
        }

        private void ViewmodelPropertyChangeMonitor()
        {
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ViewModel.CurrentPage))
            {
                WindowContentFrame.Content = ViewModel.CurrentPage.GetApplicationPage();
            }
        }
    }
}
