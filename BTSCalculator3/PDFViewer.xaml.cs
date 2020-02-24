using System.Windows;
using System.Windows.Controls;
using BTSCalculator.Core; 

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        PDFViewerViewmodel ViewModel;
        public PDFViewer()
        {
            ViewModel = StaticAccessSystem.PDFViewerViewmodel;
            InitializeComponent();
            WebBrowser.Navigate(ViewModel.Path);
        }
    }
}
