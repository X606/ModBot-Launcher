using Microsoft.Win32;
using ModBotInstaller.AcfParsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security;
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

            DirectoryInfo dataDirectory = directory.FindSubDirectory("Clone Drone in the Danger Zone_Data");
            if (dataDirectory == null)
                return false;

            DirectoryInfo managedDirectory = dataDirectory.FindSubDirectory("Managed");
            if (managedDirectory == null)
                return false;

            FileInfo assembly = managedDirectory.FindFile("Assembly-CSharp.dll");
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

            DirectoryInfo dataDirectory = directory.FindSubDirectory("Clone Drone in the Danger Zone_Data");
            if (dataDirectory == null)
                return false;

            DirectoryInfo managedDirectory = dataDirectory.FindSubDirectory("Managed");
            if (managedDirectory == null)
                return false;

            FileInfo assembly = managedDirectory.FindFile("ModLibrary.dll");
            if (assembly == null)
                return false;

            return true;
        }

        public static DirectoryInfo FindSubDirectory(this DirectoryInfo directoryInfo, string name, SearchOption mode = SearchOption.TopDirectoryOnly)
        {
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories("*", mode);
            return subDirectories.FirstOrDefault((DirectoryInfo subDir) => subDir.Name == name);
        }

        public static FileInfo FindFile(this DirectoryInfo directoryInfo, string name, SearchOption mode = SearchOption.TopDirectoryOnly)
        {
            FileInfo[] files = directoryInfo.GetFiles("*", mode);
            return files.FirstOrDefault((FileInfo file) => file.Name == name);
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
            catch (HttpRequestException)
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
                    str = str.Remove(i, 1);
            }

            return str;
        }

        public static async Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName)
        {
            using (FileStream destination = File.Create(destinationFileName, 4096, FileOptions.Asynchronous))
            {
                using (Stream zipEntryStream = source.Open())
                {
                    await TryExtractFileAsync(zipEntryStream, destination);
                }
            }
        }

        public static async Task TryExtractFileAsync(Stream zipStream, FileStream destination)
        {
            try
            {
                await zipStream.CopyToAsync(destination);
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x00000070) // ERROR_DISK_FULL
            {
                MessageBox.Show("There is not enough disk space to extract files, clear up some space and try again", "Unable to extract files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await TryExtractFileAsync(zipStream, destination); // Try extracting the file again
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x00000020) // ERROR_SHARING_VIOLATION
            {
                MessageBox.Show("Source or destination files are open in another program, please close them and try again", "Unable to extract files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await TryExtractFileAsync(zipStream, destination); // Try extracting the file again
            }
            catch (SecurityException)
            {
                MessageBox.Show("Installer is not authorized to access files for extracting, please make sure all files have the correct permissions and try again", "Unable to extract files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await TryExtractFileAsync(zipStream, destination); // Try extracting the file again
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Installer is not authorized to access files for extracting, please make sure all files have the correct permissions and try again", "Unable to extract files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await TryExtractFileAsync(zipStream, destination); // Try extracting the file again
            }
            catch (Exception e) // Any other error type
            {
                MessageBox.Show("An error uccured while extracting files, installer cannot continue. Please report this to the Mod-Bot team.\n\nException details:\n" + e.ToString(), "Unable to extract files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndCurrentProcess();
            }
        }

        public static void MoveAllPersistentFoldersTo(string sourceRootPath, string destinationRootPath, PersistentFolderData[] persistentFoldersData)
        {
            TryDeleteDirectory(destinationRootPath, true);
            Directory.CreateDirectory(destinationRootPath);

            foreach (PersistentFolderData persistentFolder in persistentFoldersData)
            {
                string sourceFolderPath = Path.Combine(sourceRootPath, persistentFolder.FolderName);

                string destinationFolderName = StripAllInvalidPathCharacters(persistentFolder.GUID);
                if (string.IsNullOrEmpty(destinationFolderName))
                    destinationFolderName = Path.GetRandomFileName();

                string destinationFolderPath = Path.Combine(destinationRootPath, destinationFolderName);
                if (Directory.Exists(sourceFolderPath))
                {
                    TryMoveDirectory(sourceFolderPath, destinationFolderPath);
                }
                else
                {
                    Directory.CreateDirectory(destinationFolderPath);
                }
            }
        }

        public static void TryMoveDirectory(string sourceDir, string destinationDir)
        {
            try
            {
                Directory.Move(sourceDir, destinationDir);
            }
            catch (IOException io) when (((io.HResult & 0x0000FFFF) == 0x000004C8) || ((io.HResult & 0x0000FFFF) == 0x00000020)) // ERROR_USER_MAPPED_FILE or ERROR_SHARING_VIOLATION
            {
                DialogResult dialogResult = MessageBox.Show("Source or destination files are open in another program, please close any programs that are using Clone Drone's files and try again.", "Unable to move files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Retry)
                {
                    TryMoveDirectory(sourceDir, destinationDir);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    EndCurrentProcess();
                }
            }
            catch (IOException io) when (((io.HResult & 0x0000FFFF) == 0x000000E1) || ((io.HResult & 0x0000FFFF) == 0x000000E2)) // ERROR_VIRUS_INFECTED or ERROR_VIRUS_DELETED
            {
                // This will pretty much only happen if Windows Defender stops the installer from accessing the old Injector.exe file, since it may still exist. But, we don't want to do anything with it anyway, so we just ignore the exception, and in turn, skip moving the file.
            }
            catch (Exception e) when ((e is SecurityException) || (e is UnauthorizedAccessException))
            {
                DialogResult dialogResult = MessageBox.Show("Installer is not authorized to access files for moving, please make sure all files have the correct permissions and try again", "Unable to move files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Retry)
                {
                    TryMoveDirectory(sourceDir, destinationDir);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    EndCurrentProcess();
                }
            }
            catch (Exception e) // Any other error type
            {
                MessageBox.Show("An error uccured while moving files, installer cannot continue. Please report this to the Mod-Bot team.\n\nException details:\n" + e.ToString(), "Unable to move files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndCurrentProcess();
            }
        }

        public static void CopyDirectory(string sourceDirPath, string destDirPath)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirPath);

            if (!sourceDirectory.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirPath);
            }

            DirectoryInfo[] directories = sourceDirectory.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirPath);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirPath, file.Name);
                TryCopyFile(file, tempPath, true);
            }

            foreach (DirectoryInfo subdirectory in directories)
            {
                string tempPath = Path.Combine(destDirPath, subdirectory.Name);
                CopyDirectory(subdirectory.FullName, tempPath);
            }
        }

        public static void TryCopyFile(FileInfo source, string destinationPath, bool overwrite)
        {
            try
            {
                source.CopyTo(destinationPath, overwrite);
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x00000070) // ERROR_DISK_FULL
            {
                DialogResult dialogResult = MessageBox.Show("There is not enough disk space to copy files, clear up some space and try again", "Unable to copy files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Retry)
                {
                    TryCopyFile(source, destinationPath, overwrite);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    EndCurrentProcess();
                }
            }
            catch (IOException io) when (((io.HResult & 0x0000FFFF) == 0x000004C8) || ((io.HResult & 0x0000FFFF) == 0x00000020)) // ERROR_USER_MAPPED_FILE or ERROR_SHARING_VIOLATION
            {
                DialogResult dialogResult = MessageBox.Show("Source or destination files are open in another program, please close any programs that are using Clone Drone's files and try again.", "Unable to copy files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Retry)
                {
                    TryCopyFile(source, destinationPath, overwrite);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    EndCurrentProcess();
                }
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x00000050) // ERROR_FILE_EXISTS
            {
                MessageBox.Show("Destination file already exists. Please report this to the Mod-Bot team.", "Unable to copy files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndCurrentProcess(); // Not sure how to handle this, since the user won't really be able to fix it, maybe we could just delete the destination file?? For now though, the overwrite parameter is always set to true, so this should never happen
            }
            catch (IOException io) when (((io.HResult & 0x0000FFFF) == 0x000000E1) || ((io.HResult & 0x0000FFFF) == 0x000000E2)) // ERROR_VIRUS_INFECTED or ERROR_VIRUS_DELETED
            {
                // This will pretty much only happen if Windows Defender stops the installer from accessing the old Injector.exe file, since it may still exist. But, we don't want to do anything with it anyway, so we just ignore the exception, and in turn, skip copying the file.
            }
            catch (Exception e) when ((e is SecurityException) || (e is UnauthorizedAccessException))
            {
                DialogResult dialogResult = MessageBox.Show("Installer is not authorized to access files for copy, please make sure all files have the correct permissions and try again", "Unable to copy files", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Retry)
                {
                    TryCopyFile(source, destinationPath, overwrite);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    EndCurrentProcess();
                }
            }
            catch (Exception e) // Any other error type
            {
                MessageBox.Show("An error uccured while copying files, installer cannot continue. Please report this to the Mod-Bot team.\n\nException details:\n" + e.ToString(), "Unable to copy files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndCurrentProcess();
            }
        }

        public static float Lerp(float start, float end, float t)
        {
            return start + ((end - start) * t);
        }

        public static void EndCurrentProcess()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static string GetTempDirectoryPath()
        {
            string path = Path.GetTempPath() + Guid.NewGuid().ToString();
            if (Directory.Exists(path)) // If the directory exists, find a new name
                return GetTempDirectoryPath();

            return path;
        }

        public static void TryDeleteDirectory(string path, bool recursive)
        {
            try
            {
                Directory.Delete(path, recursive);
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x000004C8 || (io.HResult & 0x0000FFFF) == 0x00000020) // ERROR_USER_MAPPED_FILE or ERROR_SHARING_VIOLATION
            {
                MessageBox.Show("Source or destination files are open in another program, please close them and try again", "Unable to remove directories", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TryDeleteDirectory(path, recursive);
            }
            catch (DirectoryNotFoundException)
            {
                // If the directory doesn't exist, we ignore it
            }
            catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x00000091 && recursive) // ERROR_DIR_NOT_EMPTY and 'recursive' is true
            {
                // For some reason, this exception is thrown even when 'recursive' is true. But it still removes the directory if this exception is ignored
            }
            catch (Exception e)
            {
                MessageBox.Show("An error uccured while deleting temporary files, installer cannot continue. Please report this to the Mod-Bot team.\n\nException details:\n" + e.ToString(), "Unable to remove files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndCurrentProcess();
            }
        }

        public static string FindGameInstallDirectoryFromSteam()
        {
            string registryKey;

            // The registry key is different on 64-bit and 32-bit
            if (Environment.Is64BitOperatingSystem)
            {
                registryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
            }
            else
            {
                registryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
            }

            // Gets the path to the "Steam" folder
            string steamInstallPath = Registry.GetValue(registryKey, "InstallPath", null) as string;
            if (!string.IsNullOrEmpty(steamInstallPath) && Directory.Exists(steamInstallPath))
            {
                // The path to the "steamapps" folder
                string steamappsPath = steamInstallPath + @"\steamapps";
                
                // Clone Drone's "appmanifest.acf" file
                string appmanifestFilePath = steamappsPath + @"\appmanifest_" + Constants.CLONE_DRONE_STEAMAPPID + ".acf";

                if (File.Exists(appmanifestFilePath))
                {
                    // Parse the .acf file
                    string[] appmanifestLines = File.ReadAllLines(appmanifestFilePath);
                    if (AcfParser.TryParseAcf(appmanifestLines, out AcfTree tree))
                    {
                        // Here we can also check if the user is on the experimental branch by getting the value of "UserConfig/betakey", if the value in that node is "experimental", then the user is on the experimental branch, otherwise, the value is an empty string
                        if (tree.TryGetNodeValue("installdir", out string installFolderName))
                            return steamappsPath + @"\common\" + installFolderName;
                    }
                }
            }

            return string.Empty;
        }

        public static Type GetTypeIgnoringNamespace(this Assembly assembly, string name, bool ignoreCase)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (string.Equals(type.Name, name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    return type;
            }

            return null;
        }

        public static string AddSpacesToCamelCasedString(string camelCasedString)
        {
            string text = "";
            char[] array = camelCasedString.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (i != 0 && char.IsUpper(array[i]))
                    text += " ";
                
                text += array[i].ToString();
            }
            return text;
        }
    }
}
