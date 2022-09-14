using LuniiKit.Properties;
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
		private const string DbFileModalFilter = "Db files (*.json)|*.json|All files (*.*)|*.*";

		private static Settings Settings => Settings.Default;

		public LuniiKitOptions()
		{
			InitializeComponent();
			CheckConfig();
		}
		private void CheckConfig()
		{
			if (string.IsNullOrEmpty(Settings.confhost))
			{
				Chost.Text = "localhost";
			}
			if (string.IsNullOrEmpty(Settings.confport))
			{
				Cport.Text = "8080";
			}

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
			if (string.IsNullOrEmpty(Settings.tmp)) {
				Tmpfolder.Text = path + "\\.studio\\tmp";
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
			offselect.Filter = DbFileModalFilter;
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
			unoffselect.Filter = DbFileModalFilter;
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
			Settings.offdb = null;
			Settings.unoffdb = null;
			Settings.library = null;
			Settings.tmp = null;

			string path = Settings.studioportable
				? Directory.GetCurrentDirectory()
				: Environment.GetEnvironmentVariable("USERPROFILE");

			Offdb.Text = path + "\\.studio\\db\\official.json";
			Unoffdb.Text = path + "\\.studio\\db\\unofficial.json"; ;
			Library.Text = path + "\\.studio\\library";
			Tmpfolder.Text = path + "\\.studio\\tmp";
		}
	}
}
