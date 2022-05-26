using System;
using System.ComponentModel;
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
        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
