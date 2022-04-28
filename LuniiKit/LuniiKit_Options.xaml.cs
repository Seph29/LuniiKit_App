using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LuniiKit
{
    public partial class LuniiKit_Options : Window
    {
        public LuniiKit_Options()
        {
            LuniiKit.MainWindow objMainWindow = new LuniiKit.MainWindow();
            InitializeComponent();
            Properties.Settings.Default.Upgrade();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            string msg = "Voulez vous annuler ?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
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
            LuniiKit.MainWindow objMainWindow = new LuniiKit.MainWindow();
            Properties.Settings.Default.confhost = chost.Text;
            Properties.Settings.Default.confport = cport.Text;
            Properties.Settings.Default.studioportable = isport.IsChecked.Value;
            Properties.Settings.Default.autoopenweb = autoopen.IsChecked.Value;
            Properties.Settings.Default.Save();
            objMainWindow.Hide();
            this.Hide();

        }
    }
}

