namespace InullKit
{
    using AdonisUI.Controls;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    public enum InputBoxMode
    {
        Url,
        Folder
    }

    public partial class InputBox : AdonisWindow
    {
        public InputBox(InputBoxMode mode)
        {
            InitializeComponent();
            if (mode == InputBoxMode.Folder)
            {
                UrlSection.Visibility = Visibility.Collapsed;
            }
        }

        public string Options => OptionsTextBox.Text;

        public string Url => UrlTextBox.Text;

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jersou/studio-pack-generator#usage");
        }

        private void OKButtoninputbox_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OKButtoninputbox_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DialogResult = true;
                e.Handled = true;
            }
        }
    }
}