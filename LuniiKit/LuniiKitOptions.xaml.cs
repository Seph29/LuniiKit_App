using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Bluegrams.Application;
using Newtonsoft.Json;

namespace LuniiKit
{
    public partial class LuniiKitOptions : Window
    {
        public LuniiKitOptions()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            string msg = "Voulez vous annuler ?";
            MessageBoxResult result = MessageBox.Show(msg, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
        private void ook(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            Properties.Settings.Default.confhost = chost.Text;
            Properties.Settings.Default.confport = cport.Text;
            Properties.Settings.Default.studioportable = isport.IsChecked.Value;
            Properties.Settings.Default.autoopenweb = autoopen.IsChecked.Value;
            Properties.Settings.Default.skiptests = skip.IsChecked.Value;
            Properties.Settings.Default.nlogs = Convert.ToInt16(nombrelogs.Text);
            Properties.Settings.Default.Save();
            objMainWindow.Hide();
            this.Hide();
        }
        private void CheckUpdate(object sender, RoutedEventArgs e)
        {
            Updater.WinSparkle.win_sparkle_check_update_with_ui();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 99;
        }
    }
}

