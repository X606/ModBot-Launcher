using ModBotInstaller.Injector;
using Mono.Cecil;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public static class ModBotInstallerManager
    {
        private const string TARGET_ASSEMBLY_VAR = "target_assembly=";

        private const string MODBOT_TARGET_ASSEMBLY_VALUE = "ModBot\\ModLibrary.Preloader.dll";

        public static NewProgressBar ProgressBar;

        public static void Install(string installationPath, GameReleaseType gameVersion, Action onComplete)
        {
            SetProgress(0f);
            new Thread(delegate ()
            {
                // get installation files from server or local computer
                string extractedZipFilePath;
                if (UserPreferences.Current.ShouldUseBetaPath)
                {
                    extractedZipFilePath = UserPreferences.Current.ModBotBetaSourceDirectory;
                    SetProgress(0.3f);
                }
                else
                {
                    if (!ServerData.HasData)
                        throw new InvalidOperationException("Attempting to install non-local Mod-Bot version without downloaded data!");

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string zipPath = Path.GetTempPath() + "modbot.zip";
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(ServerData.ModBotDownloadLink, zipPath);
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

                    ModBotInjector.AddModdedObjectClass(assemblyPath, injectionClassesPath, delegate (float progress) { SetProgress(0.3f + (progress * 0.2f)); });
                }

                SetProgress(0.5f);

                // if the user has other doorstop plugins (like bepinex),
                // to make everything work, move the path to the plugin to modbot.ini file
                // to modbot to mimic doorstop initialization process
                bool replaceTargetAssemblyPath = false;
                string otherPluginAssemblyPath = null;
                string doorstopConfigPath = Path.Combine(installationPath, "doorstop_config.ini");
                if (File.Exists(doorstopConfigPath) && tryParseAssemblyPath(doorstopConfigPath, out otherPluginAssemblyPath))
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
                if (!UserPreferences.Current.ShouldUseBetaPath)
                    Utils.TryDeleteDirectory(extractedZipFilePath, true);

                SetProgress(1f);

                if (onComplete != null)
                    CallInMainThread(onComplete);
            }).Start();
        }

        public static void Uninstall(string installationPath)
        {
            throw new NotImplementedException("wip");
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
            ProgressBar.BeginInvoke((MethodInvoker)delegate ()
            {
                action();
            });
        }

        public static ModBotInstallationState GetModBotInstallationState(string installationPath, GameReleaseType gameVersion, out string currentlyInstalledModBotVersion, out string errorMessage)
        {
            string modLibraryPath = Path.Combine(installationPath, "ModBot", "ModLibrary.dll");

            if (!File.Exists(modLibraryPath))
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

        static bool tryParseAssemblyPath(string iniFile, out string path)
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
                        if (line.StartsWith(TARGET_ASSEMBLY_VAR))
                        {
                            path = line.Substring(TARGET_ASSEMBLY_VAR.Length);
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
    }
}
