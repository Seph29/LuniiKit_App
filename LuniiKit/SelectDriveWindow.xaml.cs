using AdonisUI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InullKit
{
    public class DriveInfoWrapper
    {
        public string Letter { get; set; }
        public string Type { get; set; }
        public string DisplayName { get; set; }
        public ImageSource DriveIcon { get; set; }
    }

    public partial class SelectDriveWindow : AdonisWindow
    {
        public string SelectedDrive { get; private set; }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_SMALLICON = 0x000000001;

        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private string GetDriveTypeString(DriveType driveType)
        {
            switch (driveType)
            {
                case DriveType.Fixed:
                    return "Disque local";

                case DriveType.Removable:
                    return "USB";

                case DriveType.Network:
                    return "Réseau";

                default:
                    return "Type inconnu";
            }
        }

        public SelectDriveWindow(List<string> availableDriveLetters)
        {
            InitializeComponent();
            List<DriveInfoWrapper> driveInfoList = new List<DriveInfoWrapper>();

            foreach (string letter in availableDriveLetters)
            {
                DriveInfo driveInfo = new DriveInfo(letter);
                DriveInfoWrapper driveInfoWrapper = new DriveInfoWrapper
                {
                    Letter = driveInfo.Name,
                    DisplayName = string.IsNullOrEmpty(driveInfo.VolumeLabel) ? GetDriveTypeString(driveInfo.DriveType) : driveInfo.VolumeLabel,
                    Type = GetDriveTypeString(driveInfo.DriveType), // Ajoutez le type de disque ici
                    DriveIcon = GetDriveIcon(driveInfo.Name)
                };
                driveInfoList.Add(driveInfoWrapper);
            }

            DriveList.ItemsSource = driveInfoList;
        }

        private ImageSource GetDriveIcon(string driveLetter)
        {
            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                IntPtr hImgSmall = SHGetFileInfo(driveLetter, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);

                if (hImgSmall != IntPtr.Zero)
                {
                    System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                    return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch (Exception)
            {
                // Gestion des exceptions, par exemple si l'icône n'est pas disponible
            }

            return null; // Renvoie null si l'icône n'a pas pu être obtenue
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DriveInfoWrapper selectedDriveInfo = (DriveInfoWrapper)DriveList.SelectedItem;
            if (selectedDriveInfo != null)
            {
                SelectedDrive = selectedDriveInfo.Letter;
            }
            DialogResult = true;
            Close();
        }
    }
}