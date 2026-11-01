using ModBotInstaller.Injector;
using Mono.Cecil;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ModBotInstaller
{
    public static class ModBotInstallerManager
    {
        private const string TARGET_ASSEMBLY_VAR = "target_assembly=";

        private const string ADDITIONAL_TARGET_ASSEMBLY_VAR = "additional_target_assembly=";

        private const string MODBOT_TARGET_ASSEMBLY_VALUE = "ModBot\\ModLibrary.Preloader.dll";

        private static readonly string[] _oldFilesInManaged = new string[]
        {
            "0Harmony.dll",
            "0Harmony.xml",
            "ICSharpCode.SharpZipLib.dll",
            "ICSharpCode.SharpZipLib.xml",
            "ModLibrary.dll",
            "ModLibrary.xml",
            "ModBotWebsiteAPI.dll",
            "InjectionClasses.dll",
        };

        private static readonly string[] _oldFilesInData = new string[]
        {
            "modbot",
        };

        private static readonly string[] _doorstopFilesINGameFolder = new string[]
        {
            ".doorstop_version",
            "doorstop_config.ini",
            "winhttp.dll"
        };

        public static NewProgressBar ProgressBar;

        public static void Install(string installationPath, bool useBeta, GameReleaseType gameVersion, Form2 askingForm, Action onComplete)
        {
            SetProgress(0f);
            new Thread(delegate ()
            {
                Thread.Sleep(1000);

                // get installation files from server or local computer
                string extractedZipFilePath;
                if (useBeta)
                {
                    extractedZipFilePath = UserPreferences.Current.ModBotBetaSourceDirectory;
                    SetProgress(0.3f);
                }
                else
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string zipPath = Path.GetTempPath() + "modbot.zip";
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadProgressChanged += OnArchiveDownloadProgress;
                            client.DownloadFileAsync(new Uri(ServerData.ModBotDownloadLink), zipPath);
                            while (client.IsBusy)
                            {
                                Thread.Sleep(10);
                            }
                            client.DownloadProgressChanged -= OnArchiveDownloadProgress;
                        }
                    }
                    catch (Exception exc)
                    {
                        string firstLine;
                        if (exc is WebException we)
                        {
                            firstLine = "Unable to reach to the server to get installation files.\r\n\r\n";
                        }
                        else
                        {
                            firstLine = $"Unknown error ({exc.GetType().Name})\r\n\r\n";
                        }

                        CallInMainThread(delegate
                        {
                            askingForm.ShowDefaultVisuals();
                        });

                        ErrorHandler.DownloadError(firstLine +
                                "Check you internet connection or try manually installing Mod-Bot from GitHub\r\n" +
                                "1. Download \"Mod-Bot.zip\" archive\r\n" +
                                "2. Extract it somewhere\r\n" +
                                "3. Open launcher settings, enable \"Enable local beta version\"\r\n" +
                                "4. Specify the folder you have extracted files to. \"Valid path\" label will show up\r\n" +
                                "5. Close settings window\r\n" +
                                "6. Press \"Start game (beta)\"\r\n\r\n" +
                                "Would you like to go to Mod-Bot GitHub site?");

                        return;
                    }

                    SetProgress(0.2f);

                    extractedZipFilePath = Path.GetTempPath() + "modbot/";

                    // Clear the folder of all files and subdirectories if it already exists
                    if (Directory.Exists(extractedZipFilePath))
                        Utils.TryDeleteDirectory(extractedZipFilePath, true);

                    ZipFile.ExtractToDirectory(zipPath, extractedZipFilePath);
                    File.Delete(zipPath); // Delete the temporary sip file

                    SetProgress(0.3f);
                }

                // make sure the ModdedObject class is in the game (it should be there starting with version 1.12)
                // if it's not, add it
                string assemblyPath = Path.Combine(GameInstanceInfo.Current.ManagedDirectory, "Assembly-CSharp.dll");
                if (!CheckModdedObjectClass(assemblyPath))
                {
                    Console.WriteLine("The game instance doesn't have ModdedObject class. Lets fix this");

                    string injectionClassesPath = Path.Combine(extractedZipFilePath, "libs", "InjectionClasses.dll");
                    if (!File.Exists(injectionClassesPath))
                    {
                        ErrorHandler.Crash("Could not find the following file: libs/InjectionClasses.dll");
                        return;
                    }

                    ModBotInjector.AddModdedObjectClass(assemblyPath, injectionClassesPath, delegate (float progress) { SetProgress(0.3f + (progress * 0.15f)); });
                }

                SetProgress(0.45f);

                // delete remnants of old modbot 
                string dataPath = GameInstanceInfo.Current.DataDirectory;
                string managedDirectoryPath = GameInstanceInfo.Current.ManagedDirectory;
                foreach (string file in _oldFilesInData)
                {
                    string oldFilePath = Path.Combine(dataPath, file);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                        Console.WriteLine($"Deleted {file} under Data folder");
                    }
                }
                foreach (string file in _oldFilesInManaged)
                {
                    string oldFilePath = Path.Combine(managedDirectoryPath, file);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                        Console.WriteLine($"Deleted {file} under Managed folder");
                    }
                }

                // if the user has other doorstop plugins (like bepinex),
                // to make everything work, move the path to the plugin to modbot.ini file
                // to modbot to mimic doorstop initialization process
                bool replaceTargetAssemblyPath = false;
                string otherPluginAssemblyPath = null;
                string doorstopConfigPath = Path.Combine(installationPath, "doorstop_config.ini");
                if (File.Exists(doorstopConfigPath) && tryParseAssemblyPath(doorstopConfigPath, TARGET_ASSEMBLY_VAR, out otherPluginAssemblyPath))
                {
                    Console.WriteLine($"Successfully read other plugin path: {otherPluginAssemblyPath}");
                    if (otherPluginAssemblyPath.Replace('/', '\\') != MODBOT_TARGET_ASSEMBLY_VALUE)
                    {
                        replaceTargetAssemblyPath = true;
                        Console.WriteLine($"Will replace the path");
                    }
                }

                SetProgress(0.6f);

                // copy release-specific files
                // todo: add hyperdome support
                Utils.CopyDirectory(Path.Combine(extractedZipFilePath, "modbot_dz"), installationPath);
                string modBotFolder = Path.Combine(installationPath, "ModBot");
                if (!Directory.Exists(modBotFolder))
                {
                    throw new InvalidOperationException("Something went wrong when copying Mod-Bot files!");
                }

                SetProgress(0.7f);

                // copy non release-specific files
                Utils.CopyDirectory(Path.Combine(extractedZipFilePath, "libs"), modBotFolder);

                SetProgress(0.8f);

                // create modbot.ini file for supporting additonal plugins
                string iniFile = Path.Combine(installationPath, "modbot.ini");
                if (!File.Exists(iniFile))
                {
                    string modbotIniFileContent = "# Mod-Bot overrides the target_assembly in doorstop_config in order to work\r\n" +
                    "# If you need a plugin to run, type its path below\r\n" +
                    "additional_target_assembly=";

                    if (replaceTargetAssemblyPath)
                    {
                        modbotIniFileContent += otherPluginAssemblyPath;
                        Console.WriteLine($"Added {otherPluginAssemblyPath} as additional target assembly");
                    }

                    File.WriteAllText(iniFile, modbotIniFileContent);
                }

                SetProgress(0.9f);

                // create mods folder
                string modsFolder = Path.Combine(installationPath, "mods");
                if (!Directory.Exists(modsFolder))
                {
                    Directory.CreateDirectory(modsFolder);
                }

                SetProgress(0.95f);

                // delete downloaded files
                if (!useBeta)
                    Utils.TryDeleteDirectory(extractedZipFilePath, true);

                SetProgress(1f);

                if (onComplete != null)
                    CallInMainThread(onComplete);
            }).Start();
        }

        public static void Uninstall(string installationPath, Action onComplete)
        {
            SetProgress(0f);
            new Thread(delegate ()
            {
                Thread.Sleep(1000);

                string modbotFolder = Path.Combine(installationPath, "ModBot");
                if (Directory.Exists(modbotFolder))
                {
                    Utils.TryDeleteDirectory(modbotFolder, true);
                    Console.WriteLine("Deleted ModBot directory");
                }

                SetProgress(0.5f);

                bool hasAdditionalPlugin = false;
                string doorstopConfigPath = Path.Combine(installationPath, "doorstop_config.ini");
                string modbotConfigPath = Path.Combine(installationPath, "modbot.ini");
                bool modbotConfigExists = File.Exists(modbotConfigPath);
                if (File.Exists(doorstopConfigPath) && modbotConfigExists)
                {
                    if (tryParseAssemblyPath(modbotConfigPath, ADDITIONAL_TARGET_ASSEMBLY_VAR, out string modbotConfigValue)
                        && tryParseAssemblyPath(doorstopConfigPath, TARGET_ASSEMBLY_VAR, out string doorstopConfigValue)
                        && doorstopConfigValue == MODBOT_TARGET_ASSEMBLY_VALUE
                        && modbotConfigValue != MODBOT_TARGET_ASSEMBLY_VALUE)
                    {
                        hasAdditionalPlugin = true;
                        replaceVarValueInFile(doorstopConfigPath, TARGET_ASSEMBLY_VAR, modbotConfigValue);
                        Console.WriteLine($"Replaced target assembly value from modbot.ini in doorstop_config.ini");
                    }
                }

                if (!hasAdditionalPlugin)
                {
                    SetProgress(0.8f);
                    Console.WriteLine($"Deleting Doorstop files...");
                    foreach (string file in _doorstopFilesINGameFolder)
                    {
                        string oldFilePath = Path.Combine(installationPath, file);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                            Console.WriteLine($"Deleted {file} under Game folder");
                        }
                    }
                }                

                if (modbotConfigExists)
                {
                    SetProgress(0.9f);
                    File.Delete(modbotConfigPath);
                    Console.WriteLine($"Deleted modbot.ini");
                }

                SetProgress(1f);

                if (onComplete != null)
                    CallInMainThread(onComplete);

                Console.WriteLine($"Finished uninstalling");
            }).Start();
        }

        static void OnArchiveDownloadProgress(object sender, DownloadProgressChangedEventArgs args)
        {
            SetProgress((int)(args.ProgressPercentage * 0.2f));
        }

        static void SetProgress(int percentage)
        {
            CallInMainThread(delegate
            {
                ProgressBar.Value = percentage;
            });
        }

        static void SetProgress(float val)
        {
            CallInMainThread(delegate
            {
                ProgressBar.Value = (int)(val * 100);
            });
        }

        static void CallInMainThread(Action action)
        {
            ProgressBar.BeginInvoke(action);
        }

        public static ModBotInstallationState GetModBotInstallationState(string installationPath, GameReleaseType gameVersion, out string currentlyInstalledModBotVersion, out string errorMessage)
        {
            if(!Directory.Exists(Path.Combine(installationPath, "ModBot")))
            {
                currentlyInstalledModBotVersion = null;
                errorMessage = null;
                return ModBotInstallationState.NotInstalled;
            }

            string modLibraryPath = Path.Combine(installationPath, "ModBot", "ModLibrary.dll");
            string preloaderPath = Path.Combine(installationPath, "ModBot", "ModLibrary.Preloader.dll");
            if (!File.Exists(preloaderPath) || !File.Exists(modLibraryPath))
            {
                currentlyInstalledModBotVersion = null;
                errorMessage = null;
                return ModBotInstallationState.NotInstalled;
            }

            System.Diagnostics.FileVersionInfo modLibraryFvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(modLibraryPath);
            string version = modLibraryFvi.FileVersion;

            if (string.IsNullOrEmpty(version))
            {
                currentlyInstalledModBotVersion = null;
                errorMessage = null;
                return ModBotInstallationState.NotInstalled;
            }

            currentlyInstalledModBotVersion = version;

            if (version.Contains("beta"))
            {
                errorMessage = null;
                return ModBotInstallationState.BetaVersion;
            }

            if (!ServerData.HasData)
            {
                errorMessage = null;
                return ModBotInstallationState.UnableToVerify;
            }

            if (isCloudVersionNewer(version, ServerData.LatestModBotVersion))
            {
                errorMessage = null;
                return ModBotInstallationState.OutOfDate;
            }

            errorMessage = null;
            return ModBotInstallationState.UpToDate;
        }

        public static bool CheckModdedObjectClass(string assemblyPath)
        {
            try
            {
                using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath))
                {
                    return assembly.MainModule.Types.Any(x => x.Name == "ModdedObject");
                }
            }
            catch (Exception exc)
            {
                return true;
            }
        }

        static bool isCloudVersionNewer(string installedVersion, string cloudVersion)
        {
            Version installed = new Version(installedVersion);
            Version cloud = new Version(cloudVersion);

            int result = installed.CompareTo(cloud);
            if (result > 0)
            {
                return false;
            }
            else if (result < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool tryParseAssemblyPath(string iniFile, string varName, out string path)
        {
            path = null;

            try
            {
                using (FileStream fileStream = File.OpenRead(iniFile))
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.StartsWith(varName))
                        {
                            path = line.Substring(varName.Length).Trim(' ', '\\', '/').Replace('/', '\\');
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }

            return !string.IsNullOrEmpty(path);
        }

        static bool replaceVarValueInFile(string iniFile, string varName, string newValue)
        {
            try
            {
                int lineIndex = -1;
                string[] lines = new string[64];

                int length = 0;
                using (FileStream fileStream = File.OpenRead(iniFile))
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256))
                {
                    string line;
                    while (length < lines.Length && (line = streamReader.ReadLine()) != null)
                    {
                        lines[length] = line;
                        if (lineIndex == -1 && line.StartsWith(varName))
                        {
                            lineIndex = length;
                        }
                        length++;
                    }
                }

                if (length == -1) return false;

                /*
                Console.WriteLine($"LINE NUMBER: {lineIndex + 1}");
                Console.WriteLine($"LENGTH: {length}");
                */

                using (FileStream fileStream = File.Open(iniFile, FileMode.Truncate, FileAccess.Write))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8, 256))
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (i == lineIndex)
                        {
                            streamWriter.WriteLine(varName + newValue);
                        }
                        else
                        {
                            streamWriter.WriteLine(lines[i]);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }
    }
}
