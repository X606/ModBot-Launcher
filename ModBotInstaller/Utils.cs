using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
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

            string[] executables;
            if (Constants.SUPPORT_HYPERDOME)
            {
                executables = new string[]
                {
                    "Clone Drone in the Danger Zone.exe",
                    "Clone Drone in the Danger Zone.exe.exe",
                    "Clone Drone in the Hyperdome.exe",
                };
            }
            else
            {
                executables = new string[]
                {
                    "Clone Drone in the Danger Zone.exe",
                    "Clone Drone in the Danger Zone.exe.exe",
                };
            }

            if (directory.FindAnyFile(executables) == null)
            {
                return false;
            }

            string[] crashHandlers = new string[]
            {
                "UnityCrashHandler.exe",
                "UnityCrashHandler64.exe",
            };
            if (directory.FindAnyFile(crashHandlers) == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidBetaInstallationPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            bool isDirectory = Directory.Exists(path);
            bool isFile = File.Exists(path);

            if (Constants.SUPPORT_INSTALLATION_FROM_ARCHIVE && isFile && path.EndsWith(".zip"))
            {
                return IsValidBetaInstallationArchive(path);
            }
            else if (isDirectory)
            {
                return IsValidBetaInstallationDirectory(path);
            }
            return false;
        }

        public static bool IsValidBetaInstallationDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.FindSubDirectory("modbot_dz") != null && directory.FindSubDirectory("libs") != null;
        }

        public static bool IsValidBetaInstallationArchive(string path)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(path))
                {
                    int numFoundEntries = 0;
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.Replace('\\', '/') == "libs/"
                            || entry.FullName.Replace('\\', '/') == "modbot_dz/")
                        {
                            numFoundEntries++;
                        }
                    }

                    if (numFoundEntries == 2)
                    {
                        return true;
                    }
                }
            }
            catch (Exception exc) { }
            return false;
        }

        public static GameInstanceInfo GetGameInstanceInfo(string installationPath, out GameReleaseType release, out GamePlatformType platform)
        {
            release = GameReleaseType.None;
            platform = GamePlatformType.None;

            DirectoryInfo directory = new DirectoryInfo(installationPath);
            if (!directory.Exists) return null;

            if (directory.FindFile("Clone Drone in the Danger Zone.exe") != null)
            {
                release = GameReleaseType.DangerZone;
                platform = GamePlatformType.Steam;
            }
            else if (directory.FindFile("Clone Drone in the Danger Zone.exe.exe") != null)
            {
                release = GameReleaseType.DangerZone;
                platform = GamePlatformType.EpicGames;
            }
            else if (directory.FindFile("Clone Drone in the Hyperdome.exe") != null)
            {
                release = GameReleaseType.Hyperdome;
                platform = GamePlatformType.Steam;
            }
            else return null;

            return new GameInstanceInfo();
        }

        public static string GetReleaseTitle(GameReleaseType release)
        {
            string name;
            if (release == GameReleaseType.DangerZone)
            {
                name = "Clone Drone in the Danger Zone";
            }
            else if (release == GameReleaseType.Hyperdome)
            {
                name = "Clone Drone in the Hyperdome";
            }
            else
            {
                name = "Clone Drone in the Space";
            }
            return name;
        }

        public static string GetPlatformName(GamePlatformType platform)
        {
            string name;
            if (platform == GamePlatformType.Steam)
            {
                name = "Steam";
            }
            else if (platform == GamePlatformType.EpicGames)
            {
                name = "Epic Games";
            }
            else if (platform == GamePlatformType.XBox)
            {
                name = "XBOX";
            }
            else
            {
                name = "Unknown";
            }
            return name;
        }

        public static Color GetPlatformColor(GamePlatformType platform)
        {
            Color color;
            if (platform == GamePlatformType.Steam)
            {
                color = Color.DodgerBlue;
            }
            else if (platform == GamePlatformType.EpicGames)
            {
                color = Color.Orange;
            }
            else if (platform == GamePlatformType.XBox)
            {
                color = Color.ForestGreen;
            }
            else
            {
                color = Color.Silver;
            }
            return color;
        }

        public static string GetExecutableName(GameReleaseType release, GamePlatformType platform)
        {
            string name;
            if (release == GameReleaseType.DangerZone)
            {
                if (platform == GamePlatformType.EpicGames) name = "Clone Drone in the Danger Zone.exe.exe";
                else name = "Clone Drone in the Danger Zone.exe";
            }
            else if (release == GameReleaseType.Hyperdome) name = "Clone Drone in the Hyperdome.exe";
            else name = "Clone Drone in the Space.exe";
            return name;
        }

        public static bool IsZipArchiveEntryADirectory(ZipArchiveEntry archiveEntry)
        {
            string name = archiveEntry.Name;
            int nameLength = name.Length;
            bool result = (nameLength > 0) && ((name[nameLength - 1] == '/') || (name[nameLength - 1] == '\\'));
            return result;
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

        public static FileInfo FindAnyFile(this DirectoryInfo directoryInfo, string[] names, SearchOption mode = SearchOption.TopDirectoryOnly)
        {
            FileInfo[] files = directoryInfo.GetFiles("*", mode);
            return files.FirstOrDefault((FileInfo file) => names.Contains(file.Name));
        }

        public static async Task<Stream> DownloadFileAsync(string url, int timeout = 1500)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromMilliseconds(timeout)
                };
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
                request.UserAgent = $"X606/ModBot-Launcher-{Application.ProductVersion}";
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
    }
}
