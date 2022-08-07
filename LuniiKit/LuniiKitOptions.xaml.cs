using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
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
            MessageBoxResult result = CustomMessageBox.ShowYesNo(Application.Current.MainWindow, msg, "Annuler", "Oui", "Non", MessageBoxImage.Question);
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
        private void Ook(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            Properties.Settings.Default.confhost = Chost.Text;
            Properties.Settings.Default.confport = Cport.Text;
            Properties.Settings.Default.studioportable = Isport.IsChecked.Value;
            Properties.Settings.Default.autoopenweb = Autoopen.IsChecked.Value;
            Properties.Settings.Default.nlogs = Convert.ToInt16(Nombrelogs.Text);
            Properties.Settings.Default.offdb = Offdb.Text;
            Properties.Settings.Default.unoffdb = Unoffdb.Text;
            Properties.Settings.Default.library = Library.Text;
            Properties.Settings.Default.Save();
            objMainWindow.Hide();
            Hide();
        }
        private void CheckUpdate(object sender, RoutedEventArgs e)
        {
            Updater.WinSparkle.win_sparkle_check_update_with_ui();
        }

        private void Nombrelogs_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Nombrelogs.Text, "^[1-9][0-9]?$"))
            {
                CustomMessageBox.ShowOK(Application.Current.MainWindow, "Vous devez choisir un nombre en 1 et 99", "Warning", "OK", MessageBoxImage.Error);
                Nombrelogs.Text = "3";
            }
        }



        private void offselect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog offselect = new OpenFileDialog();
            offselect.Filter = "Db files (*.json)|*.json|All files (*.*)|*.*";
            offselect.FilterIndex = 1;
            Nullable<bool> result = offselect.ShowDialog();
            if (result == true)
            {
                Offdb.Text = offselect.FileName;
            }
        }
        private void unoffselect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog unoffselect = new OpenFileDialog();
            unoffselect.Filter = "Db files (*.json)|*.json|All files (*.*)|*.*";
            unoffselect.FilterIndex = 1;
            Nullable<bool> result = unoffselect.ShowDialog();
            if (result == true)
            {
                Unoffdb.Text = unoffselect.FileName;
            }
        }

        private void libraryselect(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.Description = "Choisir le dossier de votre librairie";
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                //Cancel
            }
            else
            {
                Library.Text = folderDialog.SelectedPath;
            }
        }
    }
}

