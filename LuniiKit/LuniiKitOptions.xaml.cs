using System.ComponentModel;
using System.Windows;

namespace LuniiKit
{
    public partial class LuniiKitOptions : Window
    {
        public LuniiKitOptions()
        {
            InitializeComponent();
            Properties.Settings.Default.Upgrade();
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

