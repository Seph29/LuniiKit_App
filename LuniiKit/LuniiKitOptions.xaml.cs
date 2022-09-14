using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using WPFCustomMessageBox;

namespace LuniiKit
{
    public partial class LuniiKitOptions : Window
    {
        public LuniiKitOptions()
        {
            InitializeComponent();
            Checkconfig();
        }
        private void Checkconfig()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.confhost))
            {
                Chost.Text = "localhost";
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.confport))
            {
                Cport.Text = "8080";
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.offdb))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string offdbfolder = path + "\\.studio\\db\\official.json";
                    Offdb.Text = offdbfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string offdbfolder = path + "\\.studio\\db\\official.json";
                    Offdb.Text = offdbfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.unoffdb))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string unoffdbfolder = path + "\\.studio\\db\\unofficial.json";
                    Unoffdb.Text = unoffdbfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string unoffdbfolder = path + "\\.studio\\db\\unofficial.json";
                    Unoffdb.Text = unoffdbfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.library))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string libraryfolder = path + "\\.studio\\library";
                    Library.Text = libraryfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string libraryfolder = path + "\\.studio\\library";
                    Library.Text = libraryfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.tmp))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string tmpfolder = path + "\\.studio\\tmp";
                    Tmpfolder.Text = tmpfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string tmpfolder = path + "\\.studio\\tmp";
                    Tmpfolder.Text = tmpfolder;
                }
            }
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
            Properties.Settings.Default.eraselog = Eraselog.IsChecked.Value;
            Properties.Settings.Default.nlogs = Convert.ToInt16(Nombrelogs.Text);
            Properties.Settings.Default.offdb = Offdb.Text;
            Properties.Settings.Default.unoffdb = Unoffdb.Text;
            Properties.Settings.Default.library = Library.Text;
            Properties.Settings.Default.tmp = Tmpfolder.Text;
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
        private void tmpselect(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.Description = "Choisir le dossier temporaire";
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                //Cancel
            }
            else
            {
                Tmpfolder.Text = folderDialog.SelectedPath;
            }
        }
        private void Isport_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.offdb = null;
            Properties.Settings.Default.unoffdb = null;
            Properties.Settings.Default.library = null;
            Properties.Settings.Default.tmp = null;

            if (string.IsNullOrEmpty(Properties.Settings.Default.offdb))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string offdbfolder = path + "\\.studio\\db\\official.json";
                    Offdb.Text = offdbfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string offdbfolder = path + "\\.studio\\db\\official.json";
                    Offdb.Text = offdbfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.unoffdb))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string unoffdbfolder = path + "\\.studio\\db\\unofficial.json";
                    Unoffdb.Text = unoffdbfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string unoffdbfolder = path + "\\.studio\\db\\unofficial.json";
                    Unoffdb.Text = unoffdbfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.library))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string libraryfolder = path + "\\.studio\\library";
                    Library.Text = libraryfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string libraryfolder = path + "\\.studio\\library";
                    Library.Text = libraryfolder;
                }
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.tmp))
            {
                if (Properties.Settings.Default.studioportable == true)
                {
                    string path = Directory.GetCurrentDirectory();
                    string tmpfolder = path + "\\.studio\\tmp";
                    Tmpfolder.Text = tmpfolder;
                }
                else
                {
                    string path = Environment.GetEnvironmentVariable("USERPROFILE");
                    string tmpfolder = path + "\\.studio\\tmp";
                    Tmpfolder.Text = tmpfolder;
                }
            }
        }
    }
}

