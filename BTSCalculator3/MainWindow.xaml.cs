using BTSCalculator.Core;
using System.Windows;

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
            if(e.PropertyName == nameof(ViewModel.CurrentDialog))
            {
                if(ViewModel.CurrentDialog == null)
                {
                    DialogContentFrame.Content = null;
                    return;
                }
                if(ViewModel.CurrentDialog.DialogType == DialogTypes.None)
                {
                    DialogContentFrame.Content = null;
                    return;
                }
                else
                {
                    DialogContentFrame.Content = ViewModel.CurrentDialog.GetDialogPage();
                }
            }
        }
    }
}
