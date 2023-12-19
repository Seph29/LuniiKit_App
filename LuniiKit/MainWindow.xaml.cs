namespace InullKit
{
    using AdonisUI.Controls;
    using InullKit.Images;
    using InullKit.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Forms;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Updater;
    using WPFCustomMessageBox;
    using Application = System.Windows.Application;
    using Button = System.Windows.Controls.Button;
    using ContextMenu = System.Windows.Controls.ContextMenu;
    using MessageBoxImage = System.Windows.MessageBoxImage;
    using MessageBoxResult = System.Windows.MessageBoxResult;

    public partial class MainWindow : AdonisWindow
    {
        public string logpath;
        private const string studioBatFileName = "STUdio.bat";
        private readonly Random random = new Random();
        private const int NumSnowflakes = 30; // Nombre de flocons de neige


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

#if DEBUG
            DateTime dateLimite = new DateTime(2023, 12, 31);
            CustomMessageBox.ShowOK("Cette version expire le : " + dateLimite, "DEBUG", "OK", MessageBoxImage.Warning);
            if (DateTime.Now > dateLimite)
            {
                CustomMessageBox.ShowOK("L'application a expiré et ne peut plus être lancée après cette date.", "Date d'expiration atteinte", "OK", MessageBoxImage.Error);
                Close();
                return;
            }
            WinSparkle.win_sparkle_set_appcast_url(
                "http://192.168.20.2/inullkit/version");
#elif ZIP
            WinSparkle.win_sparkle_set_appcast_url(
                "https://raw.githubusercontent.com/Seph29/LuniiKit_App/master/docs/update_zip.ver");
#elif ZIP_LITE
            WinSparkle.win_sparkle_set_appcast_url(
                "https://raw.githubusercontent.com/Seph29/LuniiKit_App/master/docs/update_zip_lite.ver");
#elif EXE
            WinSparkle.win_sparkle_set_appcast_url(
                "https://raw.githubusercontent.com/Seph29/LuniiKit_App/master/docs/update.ver");
#else
            WinSparkle.win_sparkle_set_appcast_url(
                "https://raw.githubusercontent.com/iinul-team/InullKit_App/master/docs/test.ver");
#endif
            StartApp();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < NumSnowflakes; i++)
            {
                AddSnowflake();
            }
        }

        private void AddSnowflake()
        {
            Snow snowflake = new Snow();
            
            double scale = random.NextDouble() * 0.6 + 0.3;
            snowflake.RenderTransform = new ScaleTransform(scale, scale);

            canvas.Children.Add(snowflake);

            double left = random.Next((int)canvas.ActualWidth);
            Canvas.SetLeft(snowflake, left);

            double duration = random.Next(8000, 15000);
            double endY = canvas.ActualHeight;

            double speedFactor = random.NextDouble() + 0.5;
            duration /= speedFactor;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = -snowflake.Height,
                To = endY,
                Duration = TimeSpan.FromMilliseconds(duration)
            };

            DoubleAnimation horizontalAnimation = new DoubleAnimation
            {
                To = left + random.Next(-20, 20),
                Duration = TimeSpan.FromMilliseconds(random.Next(2000, 4000)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            snowflake.BeginAnimation(Canvas.LeftProperty, horizontalAnimation);

            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(duration / 2),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            snowflake.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            animation.Completed += (s, e) =>
            {
                canvas.Children.Remove(snowflake);
                AddSnowflake();
            };

            snowflake.BeginAnimation(Canvas.TopProperty, animation);
        }

        private static Settings Settings => Settings.Default;

        private string SpgFolderPath => Path.Combine(Directory.GetCurrentDirectory(), "spg");
        private string SpgOutputPath => Path.Combine(SpgFolderPath, "Mes packs");
 
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo dirSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(dirSourceSubDir.Name);
                CopyAll(dirSourceSubDir, nextTargetSubDir);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = FindResource("JavaInstallButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.com/invite/jg9MjHBWQC");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Seph29/LuniiKit_App");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/marian-m12l/studio");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/kairoh/studio");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jersou/studio-pack-generator");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/olup/lunii-admin");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/NSV/studio-pack-checker");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/DantSu/studio");
        }

        private void FolderChoice(object sender, RoutedEventArgs e)
        {
            string path = Settings.studioportable
                              ? Directory.GetCurrentDirectory()
                              : Environment.GetEnvironmentVariable("USERPROFILE");

            Settings.folderstudio = Directory.Exists(Path.Combine(path, ".studio"));

            logpath = Directory.GetCurrentDirectory() + "\\log";
            if (Directory.Exists(logpath))
            {
                Settings.folderlog = true;
            }
            else
            {
                Settings.folderlog = false;
            }

            ContextMenu cm = FindResource("Choixfolder") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void Githubchoix(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = FindResource("Github") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void Idriver_Click(object sender, RoutedEventArgs e)
        {
            string processToStart = Environment.Is64BitOperatingSystem ? "dpinst64.exe" : "dpinst32.exe";

            ProcessStartInfo process = new ProcessStartInfo(processToStart)
            {
                WorkingDirectory = @"driver",
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(process);
        }

        private void Jdk11_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C winget install EclipseAdoptium.Temurin.11";
            Process.Start("CMD.exe", strCmdText);
        }

        private void Jdk17_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C winget install EclipseAdoptium.Temurin.17";
            Process.Start("CMD.exe", strCmdText);
        }

        private void LaunchSPG(string pathOrUrl, string outputpath, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = SpgFolderPath + "\\studio-pack-generator-x86_64-windows.exe";
            process.StartInfo.Arguments = arguments + " --output-folder " + "\"" + outputpath + "\"" + " \"" + pathOrUrl + "\"";
            process.Start();
            process.WaitForExit();
            Process.Start("explorer.exe", outputpath);
        }

        private void Luniiadminweb_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://lunii-admin-web.pages.dev/");
        }

        private void Luniibuilder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://lunii-admin-builder.pages.dev/");
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (CustomMessageBox.ShowYesNo(
                    Application.Current.MainWindow,
                    "Voulez vous quitter InullKit?",
                    "Quitter",
                    "Oui",
                    "Non",
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (File.Exists(studioBatFileName))
                {
                    File.Delete(studioBatFileName);
                }

                WinSparkle.win_sparkle_cleanup();
                Environment.Exit(0);
            }
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            Process.Start("explorer.exe", path);
        }

        private void OpenFolderLog(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", logpath);
        }

        private void OpenFolderSPG(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", SpgFolderPath);
        }

        private void OpenSPG(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = SpgFolderPath;
            MessageBoxResult result = CustomMessageBox.ShowYesNoCancel(
                Application.Current.MainWindow,
                "Vous voulez utiliser :",
                "Studio Pack Generator",
                "Un dossier",
                "Une URL",
                "Annuler",
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                folderDialog.Description = "Choisir le dossier du pack à créer";
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    // Cancel
                }
                else
                {
                    string spath = folderDialog.SelectedPath;
                    var dialog = new InputBox(InputBoxMode.Folder);

                    if (dialog.ShowDialog() == true)
                    {
                        folderDialog.SelectedPath = SpgOutputPath;
                        folderDialog.Description = "Choisir le dossier de sauvegarde du pack";
                        if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                        {
                            // Cancel
                        }
                        else
                        {
                            string spath2 = folderDialog.SelectedPath;
                            LaunchSPG(spath, spath2, dialog.Options);
                        }
                    }
                }
            }
            else if (result == MessageBoxResult.No)
            {
                var dialog = new InputBox(InputBoxMode.Url);
                if (dialog.ShowDialog() == true)
                {
                    folderDialog.SelectedPath = SpgOutputPath;
                    folderDialog.Description = "Choisir le dossier de sauvegarde du pack";
                    if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        // Cancel
                    }
                    else
                    {
                        string spath2 = folderDialog.SelectedPath;
                        LaunchSPG(dialog.Url, spath2, dialog.Options);
                    }
                }
            }
        }

        private void OpenSTUdioFolder(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.library))
            {
                Process.Start("explorer.exe", Settings.library);
                return;
            }

            var path = Settings.studioportable
                           ? Directory.GetCurrentDirectory()
                           : Environment.GetEnvironmentVariable("USERPROFILE");

            var studioFolder = Path.Combine(path, ".studio", "library");
            Process.Start("explorer.exe", studioFolder);
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            InullKitOptions objOptions = new InullKitOptions();
            objOptions.ShowDialog();
        }

        private void SelectPackApp(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = FindResource("ChoixPackApp") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void StartApp()
        {
            using (Process process = new Process())
            {
                process.StartInfo.WorkingDirectory = @"C:\Windows\SysWOW64\";
                process.StartInfo.FileName = "cmd";
                process.StartInfo.Arguments = "/C pnputil -e | findstr Lunii";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                StreamReader reader = process.StandardOutput;
                string outputpnputil = reader.ReadToEnd();
                if (outputpnputil == string.Empty)
                {
                    MessageBoxResult result = CustomMessageBox.ShowYesNo(
                        "Le driver Lunii semble absent de votre PC, voulez vous l'installer ?",
                        "Detection du driver Lunii",
                        "Installer",
                        "Annuler",
                        MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        string processToStart = Environment.Is64BitOperatingSystem ? "dpinst64.exe" : "dpinst32.exe";

                        ProcessStartInfo processdriver = new ProcessStartInfo(processToStart)
                        {
                            WorkingDirectory = @"driver",
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        Process.Start(processdriver);
                    }
                }
                else
                {
                    Idriver.Visibility = Visibility.Hidden;
                }

                process.WaitForExit();
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "java.exe";
                process.StartInfo.Arguments = "-fullversion";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                try
                {
                    process.Start();
                    string input = process.StandardError.ReadToEnd();
                    Regex regex = new Regex(@"\D+");
                    if (int.Parse(string.Join(string.Empty, regex.Split(input))) > 110000)
                    {
                        Ijava.Visibility = Visibility.Hidden;
                    }

                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
                WinSparkle.win_sparkle_init();
            }

            Settings.folderstudio = false;
            Settings.folderspg = false;
            Closing += MainWindow_Closing;
            Vertext.IsDocumentEnabled = true;
            Vertext.Document.Blocks.FirstBlock.Margin = new Thickness(0);
            string nompartiel = Settings.studio1ver;
            DirectoryInfo rechercherepertoire = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileSystemInfo[] fichieretrepertoire = rechercherepertoire.GetFileSystemInfos("*" + nompartiel + "*");
            foreach (FileSystemInfo fichiertrouve in fichieretrepertoire)
            {
                Studio1.Visibility = Visibility.Visible;
            }

            string nompartiel2 = Settings.studio2ver;
            DirectoryInfo rechercherepertoire2 = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileSystemInfo[] fichieretrepertoire2 = rechercherepertoire2.GetFileSystemInfos("*" + nompartiel2 + "*");
            foreach (FileSystemInfo fichiertrouve2 in fichieretrepertoire2)
            {
                Studio2.IsEnabled = true;
            }

            string nompartiel3 = "quarkus";
            DirectoryInfo rechercherepertoire3 = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileSystemInfo[] fichieretrepertoire3 = rechercherepertoire3.GetFileSystemInfos("*" + nompartiel3 + "*");
            foreach (FileSystemInfo fichiertrouve3 in fichieretrepertoire3)
            {
                Studio3.Visibility = Visibility.Visible;
            }

            if (Directory.Exists(SpgFolderPath))
            {
                Settings.LancerSPG = true;
                Settings.folderspg = true;
            }

#if DEBUG
            Vertext.Document.Blocks.Clear();
            Vertext.Document.Blocks.Add(
                new Paragraph(
                    new Run(
                        "InullKit Version : " + FileVersionInfo
                            .GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion + " - DEBUG")));
#elif ZIP
            Vertext.Document.Blocks.Clear();
            Vertext.Document.Blocks.Add(
                new Paragraph(
                    new Run(
                        "InullKit Version : " + FileVersionInfo
                            .GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion + " - ZIP")));
#elif ZIP_LITE
            Vertext.Document.Blocks.Clear();
            Vertext.Document.Blocks.Add(
                new Paragraph(
                    new Run(
                        "InullKit Version : " + FileVersionInfo
                            .GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion + " - ZIP_LITE")));
#elif EXE
            Vertext.Document.Blocks.Clear();
            Vertext.Document.Blocks.Add(
                new Paragraph(
                    new Run(
                        "InullKit Version : " + FileVersionInfo
                            .GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion + " - EXE")));
#else
            Vertext.Document.Blocks.Clear();
            Vertext.Document.Blocks.Add(
                new Paragraph(
                    new Run(
                        "InullKit Version : " + FileVersionInfo
                            .GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion)));
#endif
            var fixoldfolder = "LuniiKit.exe_StrongName_";
            var fixdirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory, $"{fixoldfolder}*");
            foreach (string dirs in fixdirs)
            {
                if (Directory.Exists(dirs))
                {
                    Directory.Delete(dirs, true);
                }
            }
        }
        private void Studio1_Click(object sender, RoutedEventArgs e)
        {
            {
                StreamWriter sw = new StreamWriter(studioBatFileName);
                sw.WriteLine(@"@echo off
mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 65001>nul
color 9");
                sw.WriteLine(@"title STUdio " + Settings.studio1ver);
                sw.WriteLine(@"set version_LUNII=" + Settings.studio1ver);
                if (Settings.activmount == true)
                {
                    sw.WriteLine(@"set FS_MOUNTPOINT=" + Settings.mount);
                }
                if (Settings.studioportable == true)
                {
                    sw.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
                }
                else
                {
                    sw.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%UserProfile%\.studio
PATH %PATH%;%JAVA_HOME%\bin\
java -version 1>nul 2>nul || (
   echo Java n'est pas detecte sur votre OS
   pause
   exit /b 2
)
for /f tokens^=2-5^ delims^=.-_^"" %%j in ('java -fullversion 2^>^&1') do set ""jver=%%j%%k%%l%%m""

if %jver% LSS 110000 (
  echo Vous n'avez pas la bonne version de Java
  echo merci d'installer Java 11
  pause
  exit /b 1
)");
                }

                if (Settings.eraseOfficial == true)
                {
                    sw.WriteLine(@"IF EXIST %DOT_STUDIO%\db\official.json del %DOT_STUDIO%\db\official.json");
                }

                if (Settings.eraselog == true)
                {
                    sw.WriteLine(@"echo Suppression des logs");
                    sw.Write(@"for /f ""skip=" + Settings.nlogs);
                    sw.Write(@" delims="" %%A in ('dir /a:-d /b /o:-d /t:c log\STUdio-%version_LUNII%\*.log ^2^>nul') do if exist ""%STUDIO_PATH%log\STUdio-%version_LUNII%\%%A"" del ""%STUDIO_PATH%log\STUdio-%version_LUNII%\%%A""");
                    sw.WriteLine(string.Empty);
                }

                sw.WriteLine(@"if not exist %DOT_STUDIO%\db\* mkdir %DOT_STUDIO%\db
if not exist %DOT_STUDIO%\library\* mkdir %DOT_STUDIO%\library
if not exist %DOT_STUDIO%\agent\* mkdir %DOT_STUDIO%\agent

copy %STUDIO_PATH%\agent\studio-agent-%version_LUNII%-jar-with-dependencies.jar %DOT_STUDIO%\agent\studio-agent.jar
copy %STUDIO_PATH%\agent\studio-metadata-%version_LUNII%-jar-with-dependencies.jar %DOT_STUDIO%\agent\studio-metadata.jar");
                if (Settings.studioportable == true)
                {
                    sw.WriteLine(@"java -Duser.home=%STUDIO_PATH% -Dvertx.disableDnsResolver=true -Djava.util.logging.manager=org.apache.logging.log4j.jul.LogManager -Dvertx.logger-delegate-factory-class-name=io.vertx.core.logging.Log4j2LogDelegateFactory -Dsun.stdout.encoding=UTF-8 -Dfile.encoding=UTF-8 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. io.vertx.core.Launcher run studio.webui.MainVerticle");
                }
                else
                {
                    sw.WriteLine(@"java -Dvertx.disableDnsResolver=true -Djava.util.logging.manager=org.apache.logging.log4j.jul.LogManager -Dvertx.logger-delegate-factory-class-name=io.vertx.core.logging.Log4j2LogDelegateFactory -Dsun.stdout.encoding=UTF-8 -Dfile.encoding=UTF-8 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. io.vertx.core.Launcher run studio.webui.MainVerticle");
                }

                sw.Flush();
                sw.Close();
                Process.Start(studioBatFileName);
            }
        }

        private void Studio2_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(studioBatFileName);
            sw.WriteLine(@"@echo off");
            sw.WriteLine(@"mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 65001>nul
color B");
            if (!string.IsNullOrEmpty(Settings.confhost))
            {
                sw.WriteLine(@"set STUDIO_HOST=" + Settings.confhost);
            }

            if (!string.IsNullOrEmpty(Settings.confport))
            {
                sw.WriteLine(@"set STUDIO_PORT=" + Settings.confport);
            }

            if (!string.IsNullOrEmpty(Settings.offdb))
            {
                sw.WriteLine(@"set STUDIO_DB_OFFICIAL=" + Settings.offdb);
            }

            if (!string.IsNullOrEmpty(Settings.unoffdb))
            {
                sw.WriteLine(@"set STUDIO_DB_UNOFFICIAL=" + Settings.unoffdb);
            }

            if (!string.IsNullOrEmpty(Settings.library))
            {
                sw.WriteLine(@"set STUDIO_LIBRARY=" + Settings.library);
            }

            if (!string.IsNullOrEmpty(Settings.tmp))
            {
                sw.WriteLine(@"set STUDIO_TMPDIR=" + Settings.tmp);
            }
            sw.WriteLine(@"title STUdio " + Settings.studio2ver);
            sw.WriteLine(@"set version_LUNII=" + Settings.studio2ver);
            if (Settings.activmount == true)
            {
                sw.WriteLine(@"set FS_MOUNTPOINT=" + Settings.mount);
            }
            if (Settings.autoopenweb == false)
            {
                sw.WriteLine(@"set STUDIO_OPEN_BROWSER=false");
            }
            if (Settings.studioportable == true)
            {
                sw.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
            }
            else
            {
                sw.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%UserProfile%\.studio
PATH %PATH%;%JAVA_HOME%\bin\
java -version 1>nul 2>nul || (
   echo Java n'est pas detecte sur votre OS
   pause
   exit /b 2
)
for /f tokens^=2-5^ delims^=.-_^"" %%j in ('java -fullversion 2^>^&1') do set ""jver=%%j%%k%%l%%m""

if %jver% LSS 110000 (
  echo Vous n'avez pas la bonne version de Java
  echo merci d'installer Java 11
  pause
  exit /b 1
)");
            }

            if (Settings.eraseOfficial == true)
            {
                sw.WriteLine(@"IF EXIST %DOT_STUDIO%\db\official.json del %DOT_STUDIO%\db\official.json");
                sw.WriteLine(@"IF EXIST ""%STUDIO_DB_OFFICIAL%"" del ""%STUDIO_DB_OFFICIAL%""");
            }

            if (Settings.eraselog == true)
            {
                sw.WriteLine(@"echo Suppression des logs");
                sw.Write(@"for /f ""skip=" + Settings.nlogs);
                sw.Write(@" delims="" %%A in ('dir /a:-d /b /o:-d /t:c log\STUdio-%version_LUNII%\*.log ^2^>nul') do if exist ""%STUDIO_PATH%log\STUdio-%version_LUNII%\%%A"" del ""%STUDIO_PATH%log\STUdio-%version_LUNII%\%%A""");
                sw.WriteLine(string.Empty);
            }

            sw.WriteLine(@"if not exist %DOT_STUDIO%\db\* mkdir %DOT_STUDIO%\db
if not exist %DOT_STUDIO%\library\* mkdir %DOT_STUDIO%\library");
            if (Settings.studioportable == true)
            {
                sw.WriteLine(@"java -Duser.home=%STUDIO_PATH% -Dvertx.disableDnsResolver=true -Dsun.stdout.encoding=UTF-8 -Dfile.encoding=UTF-8 ^
 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle");
            }
            else
            {
                sw.WriteLine(@"java -Dvertx.disableDnsResolver=true -Dsun.stdout.encoding=UTF-8 -Dfile.encoding=UTF-8 ^
 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle");
            }

            sw.Flush();
            sw.Close();
            Process.Start(studioBatFileName);
        }

        private void Studio3_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(studioBatFileName);
            sw.WriteLine(@"@echo off");
            sw.WriteLine(@"mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 65001>nul
color D");
            if (!string.IsNullOrEmpty(Settings.confhost))
            {
                sw.WriteLine(@"set STUDIO_HOST=" + Settings.confhost);
            }

            if (!string.IsNullOrEmpty(Settings.confport))
            {
                sw.WriteLine(@"set STUDIO_PORT=" + Settings.confport);
            }

            if (!string.IsNullOrEmpty(Settings.offdb))
            {
                sw.WriteLine(@"set STUDIO_DB_OFFICIAL=" + Settings.offdb);
            }

            if (!string.IsNullOrEmpty(Settings.unoffdb))
            {
                sw.WriteLine(@"set STUDIO_DB_UNOFFICIAL=" + Settings.unoffdb);
            }

            if (!string.IsNullOrEmpty(Settings.library))
            {
                sw.WriteLine(@"set STUDIO_LIBRARY=" + Settings.library);
            }

            if (!string.IsNullOrEmpty(Settings.tmp))
            {
                sw.WriteLine(@"set STUDIO_TMPDIR=" + Settings.tmp);
            }
            sw.WriteLine(@"title STUdio " + Settings.studio3ver);
            sw.WriteLine(@"set version_LUNII=" + Settings.studio3ver);
            if (Settings.activmount == true)
            {
                sw.WriteLine(@"set FS_MOUNTPOINT=" + Settings.mount);
            }
            if (Settings.autoopenweb == false)
            {
                sw.WriteLine(@"set STUDIO_OPEN_BROWSER=false");
            }
            if (Settings.studioportable == true)
            {
                sw.WriteLine(@"set STUDIO_PATH=%~dps0
set STUDIO_HOME=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
            }
            else
            {
                sw.WriteLine(@"set STUDIO_PATH=%~dps0
set STUDIO_HOME=%UserProfile%\.studio
PATH %PATH%;%JAVA_HOME%\bin\
java -version 1>nul 2>nul || (
   echo Java n'est pas detecte sur votre OS
   pause
   exit /b 2
)
for /f tokens^=2-5^ delims^=.-_^"" %%j in ('java -fullversion 2^>^&1') do set ""jver=%%j%%k%%l%%m""

if %jver% LSS 110000 (
  echo Vous n'avez pas la bonne version de Java
  echo merci d'installer Java 11
  pause
  exit /b 1
)");
            }

            if (Settings.eraseOfficial == true)
            {
                sw.WriteLine(@"IF EXIST %DOT_STUDIO%\db\official.json del %DOT_STUDIO%\db\official.json");
                sw.WriteLine(@"IF EXIST ""%STUDIO_DB_OFFICIAL%"" del ""%STUDIO_DB_OFFICIAL%""");
            }

            sw.WriteLine(@"if not exist %STUDIO_HOME%\db\* mkdir %STUDIO_HOME%\db
if not exist %STUDIO_HOME%\library\* mkdir %STUDIO_HOME%\library");
            sw.WriteLine(@"java %* -Dfile.encoding=UTF-8 -Duser.home=%STUDIO_HOME% -Dquarkus.log.file.path=log\STUdio-" + Settings.studio3ver + "\\studio.log -Dquarkus.http.limits.max-body-size=2G -jar %STUDIO_PATH%\\quarkus-run.jar");

            sw.Flush();
            sw.Close();
            Process.Start(studioBatFileName);
        }
        private string TrouverFichierPlusRecent(string dossier)
        {
            try
            {
                var fichiers = Directory.GetFiles(dossier, "*", SearchOption.AllDirectories);
                if (fichiers.Length > 0)
                {
                    var fichierPlusRecent = fichiers.OrderByDescending(f => File.GetLastWriteTime(f)).First();
                    return fichierPlusRecent;
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors de la recherche du fichier le plus récent : {ex.Message}");
                return null;
            }
        }

        private void USB_Copy(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choisir le dossier où copier InullKit";
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                // Cancel
            }
            else
            {
                string spath = folderDialog.SelectedPath;
                string appfolder = Directory.GetCurrentDirectory();
                DirectoryInfo appFolder = new DirectoryInfo(appfolder);
                if (spath != null || spath.Length != 0)
                {
                    DirectoryInfo sPath = new DirectoryInfo(spath);
                    {
                        try
                        {
                            CopyAll(appFolder, sPath);
                            CustomMessageBox.Show(Application.Current.MainWindow, "Copy OK !");
                            Process.Start("explorer.exe", spath);
                        }
                        catch (FileNotFoundException ex)
                        {
                            CustomMessageBox.Show(Application.Current.MainWindow, ex.Message);
                        }
                    }
                }
            }
        }
    }
}