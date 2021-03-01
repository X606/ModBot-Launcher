using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public static class ServerData
    {
        public static bool HasData;

        public static string LatestModBotVersion;
        public static string LatestModBotLauncherVersion;
        public static string ModBotDownloadLink;
        public static string ModBotLauncherDownloadLink;

        public static void TryDownload()
        {
            if (Utils.SendWebRequest(@"https://modbot.org/api?operation=getCurrentModBotVersion", out LatestModBotVersion) &&
                Utils.SendWebRequest(@"https://modbot.org/api?operation=getCurrentModBotLauncherVersion", out LatestModBotLauncherVersion) &&
                Utils.SendWebRequest(@"https://modbot.org/api?operation=getModBotDownload", out ModBotDownloadLink) &&
                Utils.SendWebRequest(@"https://modbot.org/api?operation=getModBotLauncherDownload", out ModBotLauncherDownloadLink))
            {
                HasData = true;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Unable to connect to the Mod-Bot server", "Connection failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (dialogResult == DialogResult.Retry)
                {
                    TryDownload();
                }
                else if (dialogResult == DialogResult.Ignore)
                {
                    // Set to default values
                    LatestModBotVersion = null;
                    LatestModBotLauncherVersion = null;
                    ModBotDownloadLink = null;
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
