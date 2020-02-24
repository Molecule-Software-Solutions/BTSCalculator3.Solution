using BTSCalculator.Core;
using System;
using System.Timers;
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
            EventMonitor();
            SpecialNoticeTimer();
        }

        private void SpecialNoticeTimer()
        {
            SpecialNoticeFrame.Content = new SpecialNoticePage();
            Timer tmr = new Timer(4000);
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SpecialNoticeFrame.Content = null;
            });
        }

        private void SetDefaultPage()
        {
            WindowContentFrame.Content = ViewModel.CurrentPage.GetApplicationPage(); 
        }

        private void EventMonitor()
        {
            StaticAccessSystem.PDFWindowCalled += StaticAccessSystem_PDFWindowCalled;
        }

        private void StaticAccessSystem_PDFWindowCalled(object sender, System.EventArgs e)
        {
            PDFViewer pdfv = new PDFViewer();
            pdfv.Show(); 
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
