using ModBotInstaller.Injector;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Resources;
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
                    if (!DownloadedData.HasData)
                        throw new InvalidOperationException("Attempting to install non-local Mod-Bot version without downloaded data!");

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string zipPath = Path.GetTempPath() + "modbot.zip";
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(DownloadedData.ModBotDownloadLink, zipPath);
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

            // WARNING: This code gets executed in its own AppDomain, so using any variables will not work, you will have to add a field to the GetModBotInstallationInputStateInfo class and use it instead
            GetModBotInstallationOutputStateInfo state = AppDomainExecutor.Execute(new GetModBotInstallationInputStateInfo()
            {
                AssemblyPath = assemblyPath,
                ModlibraryPath = modlibraryPath,
                HasDownloadedData = DownloadedData.HasData,
                LatestModBotVersion = DownloadedData.LatestModBotVersion
            },
            delegate (GetModBotInstallationInputStateInfo input)
            {
                Assembly assemblyCSharpAssembly;
                try
                {
                    assemblyCSharpAssembly = Assembly.LoadFrom(input.AssemblyPath);
                }
                catch
                {
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.Failed, null, "Could not load one or more required files, if you have Clone Drone running, please close it and try again");
                }

                bool foundModdedObject = false;
                Type[] types = assemblyCSharpAssembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.Name == "ModdedObject")
                    {
                        foundModdedObject = true;
                        break;
                    }
                }

                if (!foundModdedObject)
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.NotInstalled);

                Assembly modlibraryAssembly;
                try
                {
                    modlibraryAssembly = Assembly.LoadFrom(input.ModlibraryPath);
                }
                catch (IOException io) when ((io.HResult & 0x0000FFFF) == 0x000004C8) // ERROR_USER_MAPPED_FILE
                {
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.Failed, null, "Could not load one or more required files, if you have Clone Drone running, please close it and try again");
                }
                catch
                {
                    // If there is an error reading the ModLibrary assembly, we assume the ModBot installation is faulty, so be mark it as not installed
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.NotInstalled);
                }

                string version = getResurceFromAssembly(modlibraryAssembly, "ModBotVersion");
                if (string.IsNullOrEmpty(version))
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.NotInstalled, version);

                if (version.Contains("beta"))
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.BetaVersion, version);

                if (!input.HasDownloadedData)
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.UnableToVerify, version);

                if (isCloudVersionNewer(version, input.LatestModBotVersion))
                    return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.OutOfDate, version);

                return new GetModBotInstallationOutputStateInfo(ModBotInstallationState.UpToDate, version);
            });

            currentlyInstalledModBotVersion = state.ModBotVersion;
            errorMessage = state.ErrorMessage;
            return state.State;
        }

        static string getResurceFromAssembly(Assembly assembly, string resourceName)
        {
            string[] resources = assembly.GetManifestResourceNames();
            foreach (string resource in resources)
            {
                ResourceManager resourceManager = new ResourceManager(resource, assembly);

                // Get the fully qualified resource type name
                // Resources are suffixed with .resource
                string typeName = resource.Substring(0, resource.IndexOf(".resource"));
                Type type = assembly.GetType(typeName, false);

                // if type is null then its not .resx resource
                if (type != null)
                {
                    PropertyInfo[] resourceProperties = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (PropertyInfo resourceProperty in resourceProperties)
                    {
                        // collect string type resources
                        if (resourceProperty.PropertyType == typeof(string) && resourceProperty.Name == resourceName)
                        {
                            // get value from static property
                            return resourceProperty.GetValue(null, null) as string;
                        }
                    }
                }
            }

            return null;
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

        [Serializable]
        public class GetModBotInstallationInputStateInfo
        {
            public string AssemblyPath;
            public string ModlibraryPath;
            public bool HasDownloadedData;
            public string LatestModBotVersion;
        }

        [Serializable]
        public class GetModBotInstallationOutputStateInfo
        {
            public ModBotInstallationState State;
            public string ModBotVersion;

            public string ErrorMessage;

            public GetModBotInstallationOutputStateInfo(ModBotInstallationState state) : this(state, null, null)
            {
            }

            public GetModBotInstallationOutputStateInfo(ModBotInstallationState state, string version) : this(state, version, null)
            {
            }

            public GetModBotInstallationOutputStateInfo(ModBotInstallationState state, string version, string errorMessage)
            {
                State = state;
                ModBotVersion = version;
                ErrorMessage = errorMessage;
            }
        }
    }
}
