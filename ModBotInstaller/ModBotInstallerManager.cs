using ModBotInstaller.Injector;
using Mono.Cecil;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public static class ModBotInstallerManager
    {
        public static NewProgressBar ProgressBar;

        public static void Install(string installationPath, Action onComplete)
        {
            new Thread(delegate ()
            {
                SetProgress(0.1f);

                string extractedZipFilePath;
                if (UserPreferences.Current.ShouldUseBetaPath)
                {
                    extractedZipFilePath = UserPreferences.Current.ModBotBetaSourceDirectory;
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

                    SetProgress(0.5f);

                    extractedZipFilePath = Path.GetTempPath() + "modbot/";

                    // Clear the folder of all files and subdirectories if it already exists
                    if (Directory.Exists(extractedZipFilePath))
                        Utils.TryDeleteDirectory(extractedZipFilePath, true);

                    ZipFile.ExtractToDirectory(zipPath, extractedZipFilePath);
                    File.Delete(zipPath); // Delete the temporary sip file
                }

                SetProgress(0.55f);

                Utils.CopyDirectory(extractedZipFilePath, installationPath);

                if (!UserPreferences.Current.ShouldUseBetaPath)
                    Utils.TryDeleteDirectory(extractedZipFilePath, true);

                string managedFolderPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/";
                Directory.SetCurrentDirectory(managedFolderPath);

                SetProgress(0.6f);
                ModBotInjector.InstallModBot(managedFolderPath, delegate(float progress) { SetProgress(0.6f + (progress * 0.4f)); });

                SetProgress(1f);

                if (onComplete != null)
                    CallInMainThread(onComplete);
            }).Start();
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

        public static ModBotInstallationState GetModBotInstallationState(string installationPath, out string currentlyInstalledModBotVersion, out string errorMessage)
        {
            string assemblyPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/Assembly-CSharp.dll";
            string modlibraryPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/ModLibrary.dll";

            if (!File.Exists(assemblyPath))
                throw new Exception("This was not a valid game installation path");

            if (!File.Exists(modlibraryPath))
            {
                currentlyInstalledModBotVersion = null;
                errorMessage = null;
                return ModBotInstallationState.NotInstalled;
            }

            {
				AssemblyDefinition assembly = null;
				try
				{
					assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
				}
				catch
				{
					currentlyInstalledModBotVersion = null;
					errorMessage = "Could not load one or more required files, if you have Clone Drone running, please close it and try again";
					return ModBotInstallationState.Failed;
				}
				using (AssemblyDefinition assemblycharp = assembly)
				{
                    bool containsModdedObject = assembly.MainModule.Types.Any(x => x.Name == "ModdedObject");
					if (!containsModdedObject)
					{
						currentlyInstalledModBotVersion = null;
						errorMessage = null;
						return ModBotInstallationState.NotInstalled;
					}
				}
			}

            {
                AssemblyDefinition assembly = null;
                try
                {
                    assembly = AssemblyDefinition.ReadAssembly(modlibraryPath);
                }
				catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x000004C8) // ERROR_USER_MAPPED_FILE
				{
					currentlyInstalledModBotVersion = null;
					errorMessage = "Could not load one or more required files, if you have Clone Drone running, please close it and try again";
					return ModBotInstallationState.Failed;
				}
				catch // If there is an error reading the ModLibrary assembly, we assume the ModBot installation is faulty, so be mark it as not installed
				{
                    currentlyInstalledModBotVersion = null;
                    errorMessage = null;
                    return ModBotInstallationState.NotInstalled;
                }
                using (AssemblyDefinition modlibraryAssembly = assembly)
                {
                    string data = Encoding.UTF8.GetString((modlibraryAssembly.MainModule.Resources.First() as EmbeddedResource).GetResourceData());

                    string version = null;
                    CustomAttribute fileVersionAttribute = modlibraryAssembly.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "AssemblyFileVersionAttribute");
                    if (fileVersionAttribute != null)
                    {
                        version = fileVersionAttribute.ConstructorArguments.FirstOrDefault().Value as string;
					}

                    if (string.IsNullOrEmpty(version))
                    {
						currentlyInstalledModBotVersion = null;
						errorMessage = null;
						return ModBotInstallationState.NotInstalled;
					}

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
				}
            }

			errorMessage = null;
			return ModBotInstallationState.UpToDate;
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
    }
}
