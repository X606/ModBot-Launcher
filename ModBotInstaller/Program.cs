using Microsoft.Win32;
using ModBotInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            checkForUpdatesAndStart();
        }

        static void checkForUpdatesAndStart()
        {
            ServerData.TryDownload();

            if (ServerData.HasData)
            {
                Version latestVersion = Version.Parse(ServerData.LatestModBotLauncherVersion);
                Version currentVersion = Version.Parse(Application.ProductVersion);

                if (currentVersion < latestVersion)
                {
                    ProcessStartInfo updaterStartInfo = new ProcessStartInfo();

                    string updaterPath = Path.GetTempPath() + "ModBotInstallerUpdater.exe";
                    File.WriteAllBytes(updaterPath, Resources.ModBotInstallerUpdater_ExeBytes);

                    updaterStartInfo.FileName = updaterPath;
                    updaterStartInfo.Arguments = "\"" + ServerData.ModBotLauncherDownloadLink + "\" \"" + Application.ExecutablePath + "\"";
                    updaterStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    Process.Start(updaterStartInfo);
                    return;
                }
            }

            MainForm mainForm = new MainForm();
            mainForm.Show();
            Application.Run();
        }
    }
}
