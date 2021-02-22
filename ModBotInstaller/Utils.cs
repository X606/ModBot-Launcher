using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public static class Utils
    {
        public static bool IsValidCloneDroneInstallationDirectory(string directory)
        {
            return !string.IsNullOrEmpty(directory) && Directory.Exists(directory) && IsValidCloneDroneInstallation(directory);
        }

        public static bool IsValidCloneDroneInstallation(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            DirectoryInfo dataDirectory = GetDirectory(directory.GetDirectories(), "Clone Drone in the Danger Zone_Data");
            if (dataDirectory == null)
                return false;

            DirectoryInfo managedDirectory = GetDirectory(dataDirectory.GetDirectories(), "Managed");
            if (managedDirectory == null)
                return false;

            FileInfo assembly = GetFile(managedDirectory.GetFiles(), "Assembly-CSharp.dll");
            if (assembly == null)
                return false;

            return true;
        }

        public static bool IsValidBetaInstallationDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory == null)
                return false;

            DirectoryInfo dataDirectory = GetDirectory(directory.GetDirectories(), "Clone Drone in the Danger Zone_Data");
            if (dataDirectory == null)
                return false;

            DirectoryInfo managedDirectory = GetDirectory(dataDirectory.GetDirectories(), "Managed");
            if (managedDirectory == null)
                return false;

            FileInfo assembly = GetFile(managedDirectory.GetFiles(), "ModLibrary.dll");
            if (assembly == null)
                return false;

            return true;
        }

        public static DirectoryInfo GetDirectory(DirectoryInfo[] infos, string name)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i].Name == name)
                    return infos[i];
            }

            return null;
        }

        public static FileInfo GetFile(FileInfo[] infos, string name)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i].Name == name)
                    return infos[i];
            }

            return null;
        }

        public static async Task<Stream> DownloadFileAsync(string url, int timeout = 1500)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeout);
                Stream stream = await client.GetStreamAsync(url);

                return stream;
            }
            catch (WebException)
            {
                return null;
            }
        }

        public static bool SendWebRequest(string url, out string result, int timeout = 1500)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return true;
            }
            catch (WebException)
            {
                result = null;
                return false;
            }
        }

        public static void SendWebRequest(string url, Action<WebRequestResult> onResult, Action<WebExceptionStatus> onError = null, int timeout = 1500)
        {
            SendWebRequestAsync(url, onResult, onError, timeout);
        }

        public static async Task SendWebRequestAsync(string url, Action<WebRequestResult> onResult, Action<WebExceptionStatus> onError = null, int timeout = 1500)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());

                string result = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
                onResult(new WebRequestResult(result));
            }
            catch (WebException webException)
            {
                if (onError != null)
                    onError(webException.Status);
            }
        }

        public static async Task<WebRequestResult> SendWebRequestAsync(string url, int timeout = 1500)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
                
                string result = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
                return new WebRequestResult(result);
            }
            catch (WebException webException)
            {
                return new WebRequestResult(webException.Status);
            }
        }

        public static string StripAllInvalidPathCharacters(string path)
        {
            path = RemoveAll(path, Path.GetInvalidFileNameChars());
            path = RemoveAll(path, Path.GetInvalidPathChars());

            return path;
        }

        public static string RemoveAll(string str, char[] chars)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (chars.Contains(str[i]))
                    str.Remove(i, 1);
            }

            return str;
        }

        public static async Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (destinationFileName == null)
                throw new ArgumentNullException(nameof(destinationFileName));

            using (Stream destination = File.Open(destinationFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (Stream zipEntryStream = source.Open())
                {
                    await zipEntryStream.CopyToAsync(destination);
                }
            }
        }

        public static void CopyDirectory(string sourceDirPath, string destDirPath)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirPath);

            if (!sourceDirectory.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirPath);

            DirectoryInfo[] directories = sourceDirectory.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirPath);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(tempPath, true);
            }

            foreach (DirectoryInfo subdirectory in directories)
            {
                string tempPath = Path.Combine(destDirPath, subdirectory.Name);
                CopyDirectory(subdirectory.FullName, tempPath);
            }
        }

        public static void RemoveRecursive(Control control)
        {
            if (control.IsDisposed)
                return;

            foreach (Control childControl in control.Controls)
            {
                RemoveRecursive(childControl);
            }

            if (control.Parent != null)
                control.Parent.Controls.Remove(control);
            
            control.Dispose();
        }

        public static float Lerp(float start, float end, float t)
        {
            return start + ((end - start) * t);
        }
    }
}
