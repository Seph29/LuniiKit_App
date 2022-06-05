using System;
using System.ComponentModel;
using System.Windows;
using Newtonsoft.Json;
using WPFCustomMessageBox;

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
            string msg = "Voulez vous annuler ?";
            MessageBoxResult result = CustomMessageBox.ShowYesNo(Application.Current.MainWindow, msg, "Warning", "Oui", "Non", MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Properties.Settings.Default.Reload();
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
            Properties.Settings.Default.nlogs = Convert.ToInt16(nombrelogs.Text);
            Properties.Settings.Default.Save();
            objMainWindow.Hide();
            this.Hide();
        }
        private void CheckUpdate(object sender, RoutedEventArgs e)
        {
            Updater.WinSparkle.win_sparkle_check_update_with_ui();
        }

        private void nombrelogs_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(nombrelogs.Text, "^[1-9][0-9]?$"))
            {
                CustomMessageBox.ShowOK(Application.Current.MainWindow, "Vous devez choisir un nombre en 1 et 99", "Warning", "OK", MessageBoxImage.Error);
                nombrelogs.Text = "3";
            }
        }
    }
}

