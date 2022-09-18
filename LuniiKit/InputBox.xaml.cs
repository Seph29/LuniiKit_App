using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace LuniiKit
{
    public enum InputBoxMode
    {
        Url,
        Folder
    }

    public partial class InputBox : Window
    {
        public InputBox(InputBoxMode mode)
        {
            InitializeComponent();
            if (mode == InputBoxMode.Folder)
            {
                this.UrlSection.Visibility = Visibility.Collapsed;
            }
        }

        public string Options => OptionsTextBox.Text;

        public string Url => UrlTextBox.Text;
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

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jersou/studio-pack-generator#usage");
        }
    }
}