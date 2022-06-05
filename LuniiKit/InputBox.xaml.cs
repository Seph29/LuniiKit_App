using System.Windows;
using System.Windows.Input;


namespace LuniiKit
{
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
        }
        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }
        public string ResponseText2
        {
            get { return ResponseTextBox2.Text; }
            set { ResponseTextBox2.Text = value; }
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OKButton_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DialogResult = true;
                e.Handled = true;
            }

        }
    }

}