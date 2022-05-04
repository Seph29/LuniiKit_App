using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LuniiKit
{
    public partial class MainWindow : Window
    {
        private string skiptests;

        public MainWindow()
        {
            InitializeComponent();
            if (IsConnectedToInternet())
            {
                Checkver();
            }
            else
            {
                vertext.Document.Blocks.Clear();
                vertext.Document.Blocks.Add(new Paragraph(new Run("Version : " + LuniiKit.Properties.Settings.Default.appver + " - La recherche d'une mise à jour a échoué, car vous n'êtes pas connecté à Internet.")));
                DataContext = this;
            }
            Closing += MainWindow_Closing;
            vertext.IsDocumentEnabled = true;
            vertext.Document.Blocks.FirstBlock.Margin = new Thickness(0);
            string nompartiel = "SNAPSHOT";

            DirectoryInfo rechercherepertoire = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileSystemInfo[] fichieretrepertoire = rechercherepertoire.GetFileSystemInfos("*" + nompartiel + "*");

            foreach (FileSystemInfo fichiertrouve in fichieretrepertoire)
            {
                studio3.Visibility = Visibility.Visible;
            }
            
            

        }
            public bool IsConnectedToInternet()
        {
            string host = "8.8.8.8";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 10000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (MessageBox.Show("Voulez vous quitter LuniiKit?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                if (System.IO.File.Exists("STUdio.bat"))
                {
                    System.IO.File.Delete("STUdio.bat");
                }
            Environment.Exit(0);
        }
        private void AddHyperlinkText(string linkURL, string linkName,
          string TextBeforeLink, string TextAfterLink)
        {
            Paragraph para = new Paragraph();
            para.Margin = new Thickness(0); // remove indent between paragraphs

            Hyperlink link = new Hyperlink();
            link.IsEnabled = true;
            link.Inlines.Add(linkName);
            link.NavigateUri = new Uri(linkURL);
            link.RequestNavigate += (sender, args) => Process.Start(args.Uri.ToString());
            para.Inlines.Add(TextBeforeLink);
            para.Inlines.Add(link);
            para.Inlines.Add(new Run(TextAfterLink));
            vertext.Document.Blocks.Add(para);
        }
        public void Checkver()
        {
            using (WebClient client = new WebClient())
            {
#pragma warning disable CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
                try
                {
                    String cver = client.DownloadString(LuniiKit.Properties.Settings.Default.urlver);
                    var version1 = new Version(LuniiKit.Properties.Settings.Default.appver);
                    var version2 = new Version(cver);
                    var result = version1.CompareTo(version2);

                    if (result > 0)
                    {
                        vertext.Document.Blocks.Clear();
                        AddHyperlinkText(LuniiKit.Properties.Settings.Default.url, "\nLuniiKit", "Version : " + LuniiKit.Properties.Settings.Default.appver, "");
                    }
                    else if (result < 0)
                    {
                        vertext.Document.Blocks.Clear();
                        AddHyperlinkText(LuniiKit.Properties.Settings.Default.urldownload, "\nNew version " + cver + " available", "version : " + LuniiKit.Properties.Settings.Default.appver, "");
                    }
                    else
                    {
                        vertext.Document.Blocks.Clear();
                        AddHyperlinkText(LuniiKit.Properties.Settings.Default.url, "\nLuniiKit", "Version : " + LuniiKit.Properties.Settings.Default.appver, "");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    vertext.Document.Blocks.Clear();
                    vertext.Document.Blocks.Add(new Paragraph(new Run("Version : " + LuniiKit.Properties.Settings.Default.appver + " - La recherche d'une mise à jour a échoué, car le site distant ne répond pas.")));
                }
#pragma warning restore CS0168 // La variable 'ex' est déclarée, mais jamais utilisée
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("javaInstallButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }
        private void jdk11_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C winget install EclipseAdoptium.Temurin.11";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
        private void jdk17_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C winget install EclipseAdoptium.Temurin.17";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void idriver_Click(object sender, RoutedEventArgs e)
        {
            if (System.Environment.Is64BitOperatingSystem == true)
            {
                ProcessStartInfo myProcess = new ProcessStartInfo("dpinst64.exe");
                myProcess.WorkingDirectory = @"driver";
                myProcess.UseShellExecute = true;
                myProcess.Verb = "runas";
                Process.Start(myProcess);
            }
            else
            {
                ProcessStartInfo myProcess = new ProcessStartInfo("dpinst32.exe");
                myProcess.WorkingDirectory = @"driver";
                myProcess.UseShellExecute = true;
                myProcess.Verb = "runas";
                Process.Start(myProcess);
            }
        }
        private void studio1_Click(object sender, RoutedEventArgs e)
        {
            {
                System.IO.StreamWriter SW = new System.IO.StreamWriter("STudio.bat");
                SW.WriteLine(@"@echo off
mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 850>nul
color 9");
                SW.WriteLine(@"title STUdio " + Properties.Settings.Default.studio1ver);
                SW.WriteLine(@"set version_LUNII=" + Properties.Settings.Default.studio1ver);

                if (LuniiKit.Properties.Settings.Default.studioportable == true)
                {
                    SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
                }
                else
                {
                    SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=""%UserProfile%\.studio""
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
                SW.WriteLine(@"echo Suppression des logs");
                SW.Write(@"for /f ""skip=" + Properties.Settings.Default.nlogs);
                SW.Write(@" delims="" %%A in ('dir /a:-d /b /o:-d /t:c *.log ^2^>nul') do if exist ""%%~fA"" del ""%%~fA""");
                SW.WriteLine("");
                SW.WriteLine(@"if not exist %DOT_STUDIO%\db\* mkdir %DOT_STUDIO%\db
if not exist %DOT_STUDIO%\library\* mkdir %DOT_STUDIO%\library
if not exist %DOT_STUDIO%\agent\* mkdir %DOT_STUDIO%\agent

copy %STUDIO_PATH%\agent\studio-agent-%version_LUNII%-jar-with-dependencies.jar %DOT_STUDIO%\agent\studio-agent.jar
copy %STUDIO_PATH%\agent\studio-metadata-%version_LUNII%-jar-with-dependencies.jar %DOT_STUDIO%\agent\studio-metadata.jar");

                if (LuniiKit.Properties.Settings.Default.studioportable == true)
                {
                    SW.WriteLine(@"java -Duser.home=%STUDIO_PATH% -Dvertx.disableDnsResolver=true -Djava.util.logging.manager=org.apache.logging.log4j.jul.LogManager -Dvertx.logger-delegate-factory-class-name=io.vertx.core.logging.Log4j2LogDelegateFactory -Dfile.encoding=UTF-8 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. io.vertx.core.Launcher run studio.webui.MainVerticle");
                }
                else
                {
                    SW.WriteLine(@"java -Dvertx.disableDnsResolver=true -Djava.util.logging.manager=org.apache.logging.log4j.jul.LogManager -Dvertx.logger-delegate-factory-class-name=io.vertx.core.logging.Log4j2LogDelegateFactory -Dfile.encoding=UTF-8 -cp %STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar;%STUDIO_PATH%/lib-%version_LUNII%/*;. io.vertx.core.Launcher run studio.webui.MainVerticle");
                }

                SW.Flush();
                SW.Close();

                SW = null;
                System.Diagnostics.Process.Start("STudio.bat");
            }
        }

        private void studio2_Click(object sender, RoutedEventArgs e)
        {
            if (LuniiKit.Properties.Settings.Default.skiptests == true)
            {
                skiptests = " -DskipTests";
            }
            System.IO.StreamWriter SW = new System.IO.StreamWriter("STudio.bat");

            SW.WriteLine(@"@echo off");
            if (LuniiKit.Properties.Settings.Default.defhost == LuniiKit.Properties.Settings.Default.confhost)
            {
                SW.WriteLine(@"set STUDIO_HOST=localhost");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_HOST=" + LuniiKit.Properties.Settings.Default.confhost);
            }
            if (LuniiKit.Properties.Settings.Default.defport == LuniiKit.Properties.Settings.Default.confport)
            {
                SW.WriteLine(@"set STUDIO_PORT=8080");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_PORT=" + LuniiKit.Properties.Settings.Default.confport);
            }
            if (LuniiKit.Properties.Settings.Default.autoopenweb == false)
            {
                SW.WriteLine(@"set STUDIO_OPEN_BROWSER=false");
            }
            SW.WriteLine(@"mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 850>nul
color B");
            SW.WriteLine(@"title STUdio " + Properties.Settings.Default.studio2ver);
            SW.WriteLine(@"set version_LUNII=" + Properties.Settings.Default.studio2ver);

            if (LuniiKit.Properties.Settings.Default.studioportable == true)
            {
                SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=""%UserProfile%\.studio""
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
            SW.WriteLine(@"echo Suppression des logs");
            SW.Write(@"for /f ""skip=" + Properties.Settings.Default.nlogs);
            SW.Write(@" delims="" %%A in ('dir /a:-d /b /o:-d /t:c *.log ^2^>nul') do if exist ""%%~fA"" del ""%%~fA""");
            SW.WriteLine("");
            SW.WriteLine(@"if not exist %DOT_STUDIO%\db\* mkdir %DOT_STUDIO%\db
if not exist %DOT_STUDIO%\library\* mkdir %DOT_STUDIO%\library");
            if (LuniiKit.Properties.Settings.Default.studioportable == true)
            {
                SW.WriteLine(@"java -Duser.home=%STUDIO_PATH% -Dvertx.disableDnsResolver=true -Dfile.encoding=UTF-8 ^
 -cp ""%STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar"";""%STUDIO_PATH%/lib-%version_LUNII%/*"";. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle" + skiptests);
            }
            else
            {
                SW.WriteLine(@"java -Dvertx.disableDnsResolver=true -Dfile.encoding=UTF-8 ^
 -cp ""%STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar"";""%STUDIO_PATH%/lib-%version_LUNII%/*"";. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle" + skiptests);
            }

            SW.Flush();
            SW.Close();

            SW = null;
            System.Diagnostics.Process.Start("STudio.bat");
        }

        private void studio3_Click(object sender, RoutedEventArgs e)
        {
            if (LuniiKit.Properties.Settings.Default.skiptests == true)
            {
                skiptests = " -DskipTests";
            }
            System.IO.StreamWriter SW = new System.IO.StreamWriter("STudio.bat");

            SW.WriteLine(@"@echo off");
            if (LuniiKit.Properties.Settings.Default.defhost == LuniiKit.Properties.Settings.Default.confhost)
            {
                SW.WriteLine(@"set STUDIO_HOST=localhost");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_HOST=" + LuniiKit.Properties.Settings.Default.confhost);
            }
            if (LuniiKit.Properties.Settings.Default.defport == LuniiKit.Properties.Settings.Default.confport)
            {
                SW.WriteLine(@"set STUDIO_PORT=8080");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_PORT=" + LuniiKit.Properties.Settings.Default.confport);
            }
            if (LuniiKit.Properties.Settings.Default.autoopenweb == false)
            {
                SW.WriteLine(@"set STUDIO_OPEN_BROWSER=false");
            }
            SW.WriteLine(@"mode con cols=120 lines=30
""%__APPDIR__%chcp.com"" 850>nul
color D");
            SW.WriteLine(@"title STUdio " + Properties.Settings.Default.studio3ver);
            SW.WriteLine(@"set version_LUNII=" + Properties.Settings.Default.studio3ver);

            if (LuniiKit.Properties.Settings.Default.studioportable == true)
            {
                SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=%~dps0.studio
set PATH=%STUDIO_PATH%jre11\bin;%PATH%");
            }
            else
            {
                SW.WriteLine(@"set STUDIO_PATH=%~dps0
set DOT_STUDIO=""%UserProfile%\.studio""
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
            SW.WriteLine(@"echo Suppression des logs");
            SW.Write(@"for /f ""skip=" + Properties.Settings.Default.nlogs);
            SW.Write(@" delims="" %%A in ('dir /a:-d /b /o:-d /t:c *.log ^2^>nul') do if exist ""%%~fA"" del ""%%~fA""");
            SW.WriteLine("");
            SW.WriteLine(@"if not exist %DOT_STUDIO%\db\* mkdir %DOT_STUDIO%\db
if not exist %DOT_STUDIO%\library\* mkdir %DOT_STUDIO%\library");
            if (LuniiKit.Properties.Settings.Default.studioportable == true)
            {
                SW.WriteLine(@"java -Duser.home=%STUDIO_PATH% -Dvertx.disableDnsResolver=true -Dfile.encoding=UTF-8 ^
 -cp ""%STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar"";""%STUDIO_PATH%/lib-%version_LUNII%/*"";. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle" + skiptests);
            }
            else
            {
                SW.WriteLine(@"java -Dvertx.disableDnsResolver=true -Dfile.encoding=UTF-8 ^
 -cp ""%STUDIO_PATH%/studio-web-ui-%version_LUNII%.jar"";""%STUDIO_PATH%/lib-%version_LUNII%/*"";. ^
 io.vertx.core.Launcher run studio.webui.MainVerticle" + skiptests);
            }

            SW.Flush();
            SW.Close();

            SW = null;
            System.Diagnostics.Process.Start("STudio.bat");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/invite/jg9MjHBWQC");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Seph29");
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            LuniiKit.LuniiKitOptions objOptions = new LuniiKit.LuniiKitOptions();
            objOptions.ShowDialog();
        }
    }
}
