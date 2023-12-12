using System.Linq;
using System.Threading;
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
            string[] links = new string[4]
            {
				"https://modbot.org/api?operation=getCurrentModBotVersion",
				"https://modbot.org/api?operation=getCurrentModBotLauncherVersion",
				"https://modbot.org/api?operation=getModBotDownload",
				"https://modbot.org/api?operation=getModBotLauncherDownload"
			};
            bool[] results = new bool[4];
            string[] data = new string[4];
            Thread[] threads = new Thread[4];

            for(int i = 0; i < 4; i++)
            {
                threads[i] = new Thread(delegate (object indexObj)
                {
                    int index = (int)indexObj;
                    results[index] = Utils.SendWebRequest(links[index], out data[index], 1500);
				});
                threads[i].Start(i);
			}
            
            for(int i = 0; i < 4; i++)
            {
                threads[i].Join();
            }

            if (results.All(x => x == true))
            {
                LatestModBotVersion = data[0];
				LatestModBotLauncherVersion = data[1];
				ModBotDownloadLink = data[2];
                ModBotLauncherDownloadLink = data[3];
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
