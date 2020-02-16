using System;
using System.Windows;
using System.Windows.Controls;

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for FriendlyLabeledTextBox.xaml
    /// </summary>
    public partial class FriendlyLabeledDatePicker : UserControl
    {
        
        public FriendlyLabeledDatePicker()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(FriendlyLabeledDatePicker), new PropertyMetadata(string.Empty));

        public DateTime DateTimeSelected
        {
            get { return (DateTime)GetValue(DateTimeSelectedProperty); }
            set { SetValue(DateTimeSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeSelectedProperty =
            DependencyProperty.Register("DateTimeSelected", typeof(DateTime), typeof(FriendlyLabeledDatePicker), new PropertyMetadata(DateTime.Today));

    }
}
