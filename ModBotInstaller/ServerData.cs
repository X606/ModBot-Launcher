using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public static class ServerData
    {
        public static bool HasData;

        public static string LatestModBotVersion;
        public static string LatestModBotLauncherVersion;
        public const string ModBotDownloadLink = "https://github.com/X606/ModBot/releases/latest/download/Mod-Bot.zip";
        public const string ModBotLauncherDownloadLink = "https://github.com/X606/ModBot-Launcher/releases/latest/download/Mod-Bot_Launcher.zip";

        struct GithubTagData
        {
            public string name;
        }

        static Version GetLatestVersionFromGithubTagsData(string data)
        {
            GithubTagData[] tags = JsonConvert.DeserializeObject<GithubTagData[]>(data);

            Version maxVersion = new Version(0, 0, 0, 0);
            foreach (GithubTagData tag in tags)
            {
                string filteredCurrentVersion = tag.name.Trim().Trim('v');

                if (filteredCurrentVersion.EndsWith("-beta"))
                {
                    filteredCurrentVersion = filteredCurrentVersion.Substring(0, filteredCurrentVersion.Length - "-beta".Length);
                }

                Version currentVersion = new Version(filteredCurrentVersion);

                if (currentVersion > maxVersion)
                {
                    maxVersion = currentVersion;
                }
            }

            return maxVersion;
        }

        public static void TryDownload()
        {
            string[] links = new string[2]
            {
                "https://api.github.com/repos/X606/ModBot/tags",
                "https://api.github.com/repos/X606/ModBot-Launcher/tags",
            };
            bool[] results = new bool[2];
            string[] data = new string[2];
            Thread[] threads = new Thread[2];

            for (int i = 0; i < 2; i++)
            {
                threads[i] = new Thread(delegate (object indexObj)
                {
                    int index = (int)indexObj;
                    results[index] = Utils.SendWebRequest(links[index], out data[index], 1500);
                });
                threads[i].Start(i);
            }

            for (int i = 0; i < 2; i++)
            {
                threads[i].Join();
            }

            if (results.All(x => x == true))
            {
                LatestModBotVersion = GetLatestVersionFromGithubTagsData(data[0]).ToString();
                LatestModBotLauncherVersion = GetLatestVersionFromGithubTagsData(data[1]).ToString();
                HasData = true;
            }
            else
            {
                // Set to default values
                LatestModBotVersion = null;
                LatestModBotLauncherVersion = null;
                HasData = false;

                return;
                DialogResult dialogResult = MessageBox.Show($"Unable to get the latest info", "Connection failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (dialogResult == DialogResult.Retry)
                {
                    TryDownload();
                }
                else if (dialogResult == DialogResult.Ignore)
                {
                    // Set to default values
                    LatestModBotVersion = null;
                    LatestModBotLauncherVersion = null;
                    HasData = false;
                }
                else if (dialogResult == DialogResult.Abort)
                {
                    Utils.EndCurrentProcess();
                    return;
                }
            }
        }
    }
}
