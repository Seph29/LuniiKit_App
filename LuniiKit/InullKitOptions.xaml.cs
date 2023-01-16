namespace InullKit
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    using AdonisUI.Controls;

    using InullKit.Properties;

    using WPFCustomMessageBox;

    using Application = System.Windows.Application;
    using MessageBoxImage = System.Windows.MessageBoxImage;
    using MessageBoxResult = System.Windows.MessageBoxResult;
    using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

    public partial class InullKitOptions : AdonisWindow
    {
        private const string DbFileModalFilter = "Db files (*.json)|*.json|All files (*.*)|*.*";

        private static Settings Settings => Settings.Default;

        public InullKitOptions()
        {
            InitializeComponent();
            CheckConfig();
        }

        private void CheckConfig()
        {
            string path = Settings.studioportable
                              ? Directory.GetCurrentDirectory()
                              : Environment.GetEnvironmentVariable("USERPROFILE");

            if (string.IsNullOrEmpty(Settings.offdb))
            {
                Offdb.Text = path + "\\.studio\\db\\official.json";
            }

            if (string.IsNullOrEmpty(Settings.unoffdb))
            {
                Unoffdb.Text = path + "\\.studio\\db\\unofficial.json";
            }

            if (string.IsNullOrEmpty(Settings.library))
            {
                Library.Text = path + "\\.studio\\library";
            }

            if (string.IsNullOrEmpty(Settings.tmp))
            {
                Tmpfolder.Text = path + "\\.studio\\tmp";
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string msg = "Voulez vous annuler ?";
            MessageBoxResult result = CustomMessageBox.ShowYesNo(
                Application.Current.MainWindow,
                msg,
                "Annuler",
                "Oui",
                "Non",
                MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Settings.Reload();
                e.Cancel = false;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Settings.Save();
            Hide();
        }

        private void CheckUpdate(object sender, RoutedEventArgs e)
        {
            Updater.WinSparkle.win_sparkle_check_update_with_ui();
        }

        private void Nombrelogs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Nombrelogs.Text, "^[1-9][0-9]?$"))
            {
                CustomMessageBox.ShowOK(
                    Application.Current.MainWindow,
                    "Vous devez choisir un nombre en 1 et 99",
                    "Warning",
                    "OK",
                    MessageBoxImage.Error);
                Nombrelogs.Text = "3";
            }
        }

        private void offselect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog offselect = new OpenFileDialog();
            offselect.Filter = DbFileModalFilter;
            offselect.FilterIndex = 1;
            bool? result = offselect.ShowDialog();
            if (result == true)
            {
                Offdb.Text = offselect.FileName;
            }
        }

        private void unoffselect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog unoffselect = new OpenFileDialog();
            unoffselect.Filter = DbFileModalFilter;
            unoffselect.FilterIndex = 1;
            bool? result = unoffselect.ShowDialog();
            if (result == true)
            {
                Unoffdb.Text = unoffselect.FileName;
            }
        }

        private void libraryselect(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choisir le dossier de votre librairie";
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                // Cancel
            }
            else
            {
                Library.Text = folderDialog.SelectedPath;
            }
        }

        private void tmpselect(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choisir le dossier temporaire";
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                // Cancel
            }
            else
            {
                Tmpfolder.Text = folderDialog.SelectedPath;
            }
        }

        private void Isport_Click(object sender, RoutedEventArgs e)
        {
            string path = Settings.studioportable
                              ? Directory.GetCurrentDirectory()
                              : Environment.GetEnvironmentVariable("USERPROFILE");

            Settings.offdb = path + "\\.studio\\db\\official.json";
            Settings.unoffdb = path + "\\.studio\\db\\unofficial.json";
            Settings.library = path + "\\.studio\\library";
            Settings.tmp = path + "\\.studio\\tmp";
        }

        private void Autoopen_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}