using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModBotInstallerUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            string launcherDownloadLink = args[0];
            string installerExecutablePath = args[1];

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (DownloadFile(launcherDownloadLink, out FileStream launcherFileStream))
            {
                string launcherZipFilePath = launcherFileStream.Name;
                launcherFileStream.Close();
                launcherFileStream.Dispose();

                string targetDirectoryPath = new FileInfo(installerExecutablePath).DirectoryName;

                DirectoryInfo targetDirectory = new DirectoryInfo(targetDirectoryPath);
                foreach (FileSystemInfo item in targetDirectory.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    item.Delete();
                }

                ZipFile.ExtractToDirectory(launcherZipFilePath, targetDirectoryPath);
            }

            ProcessStartInfo updaterStartInfo = new ProcessStartInfo();

            updaterStartInfo.FileName = installerExecutablePath;
            updaterStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            updaterStartInfo.UseShellExecute = true;

            Process.Start(updaterStartInfo);
        }

        public static bool DownloadFile(string url, out FileStream fileStream, int timeout = 1500)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeout);
                Stream stream = client.GetStreamAsync(url).Result;

                fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create);
                stream.CopyTo(fileStream);

                return true;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                fileStream = null;
                return false;
            }
        }
    }
}
