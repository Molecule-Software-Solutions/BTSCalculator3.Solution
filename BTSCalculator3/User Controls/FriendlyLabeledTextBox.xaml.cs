using System.Windows;
using System.Windows.Controls;

namespace BTSCalculator3
{
    /// <summary>
    /// Interaction logic for FriendlyLabeledTextBox.xaml
    /// </summary>
    public partial class FriendlyLabeledTextBox : UserControl
    {
        
        public FriendlyLabeledTextBox()
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
            DependencyProperty.Register("LabelText", typeof(string), typeof(FriendlyLabeledTextBox), new PropertyMetadata(string.Empty));



        public string TextboxText
        {
            get { return (string)GetValue(TextboxTextProperty); }
            set { SetValue(TextboxTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextboxText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextboxTextProperty =
            DependencyProperty.Register("TextboxText", typeof(string), typeof(FriendlyLabeledTextBox), new PropertyMetadata(string.Empty));




        public string TextboxMask
        {
            get { return (string)GetValue(TextboxMaskProperty); }
            set { SetValue(TextboxMaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextboxMask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextboxMaskProperty =
            DependencyProperty.Register("TextboxMask", typeof(string), typeof(FriendlyLabeledTextBox), new PropertyMetadata(string.Empty));


    }
}
