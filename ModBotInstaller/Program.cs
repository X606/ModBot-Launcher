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
                    startUpdateAvailable();
                    return;
                }
            }

            startNormal();
        }

        static void startUpdateAvailable()
        {
            UpdateAvailable updateAvailable = new UpdateAvailable();
            updateAvailable.Show();
            Application.Run();
        }

        static void startNormal()
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            Application.Run();
        }
    }
}
