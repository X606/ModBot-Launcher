using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ModBotInstaller
{
    public static class EpicGamesInstallLocationFinder
    {
        const string PATH_TO_LAUNCHER_EXECUTABLE = "Launcher/Portal/Binaries/Win64/EpicGamesLauncher.exe";

        const string MANIFEST_DISPLAY_NAME_KEY = "DisplayName";

        const string MANIFEST_DISPLAY_KEY_VALUE = "Clone Drone in the Danger Zone";

        const string MANIFEST_INSTALL_LOCATION_KEY = "InstallLocation";

        public static bool TryGetInstallPath(out string installPath)
        {
            installPath = null;

            // epic games launcher not installed?
            string epicGameLocation = GetEpicGamesLocation();
            if (epicGameLocation == null || !Directory.Exists(epicGameLocation)) return false;

            string launcherFilePath = Path.Combine(epicGameLocation, PATH_TO_LAUNCHER_EXECUTABLE);
            if (!File.Exists(launcherFilePath)) return false;

            DirectoryInfo manifestsDirectoryInfo = new DirectoryInfo(GetEpicGamesLauncherManifestsLocation());
            if (!manifestsDirectoryInfo.Exists) return false;

            foreach (FileInfo file in manifestsDirectoryInfo.GetFiles())
            {
                if (file.Extension != ".item") continue;

                JObject data = DeserializeManifest(file);
                if (data == null) continue;

                JToken displayNameToken = data[MANIFEST_DISPLAY_NAME_KEY];
                if (displayNameToken != null && displayNameToken.ToObject<string>() == MANIFEST_DISPLAY_KEY_VALUE)
                {
                    JToken installLocationToken = data[MANIFEST_INSTALL_LOCATION_KEY];
                    if (installLocationToken != null)
                    {
                        installPath = installLocationToken.ToObject<string>();
                        break;
                    }
                }
            }

            if (installPath != null && !Directory.Exists(installPath)) installPath = null;

            return true;
        }

        static string GetEpicGamesLocation()
        {
            string registryKey;

            if (Environment.Is64BitOperatingSystem)
            {
                registryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\EpicGames\Epic Games Updater";
            }
            else
            {
                registryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\EpicGames\Epic Games Updater";
            }

            string path = Registry.GetValue(registryKey, "EpicGamesLauncherExecutable", null) as string;
            if (path != null)
            {
                path = path.Replace("\\", "/").TrimEnd('/').Remove(path.IndexOf("/Launcher/"));
            }

            return path;
        }

        static string GetEpicGamesLocationAlt()
        {
            string registryKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\EpicGames\Unreal Engine\";

            string path = Registry.GetValue(registryKeyPath, "INSTALLDIR", null) as string; // usually returns "C:\Program Files\Epic Games\"
            if (path != null)
            {
                path = path.TrimEnd('/', '\\');
            }

            return path;
        }

        static string GetEpicGamesLauncherManifestsLocation()
        {
            string registryKey = @"HKEY_CURRENT_USER\SOFTWARE\Epic Games\EOS";

            string path = Registry.GetValue(registryKey, "ModSdkMetadataDir", null) as string; // usually returns "C:/ProgramData/Epic/EpicGamesLauncher/Data/Manifests"
            if (path != null)
            {
                path = path.TrimEnd('/', '\\');
            }

            return path;
        }

        static JObject DeserializeManifest(FileInfo file)
        {
            JObject jObject;
            using (FileStream fileStream = file.OpenRead())
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                try
                {
                    jObject = JsonSerializer.Create().Deserialize<JObject>(jsonTextReader);
                }
                catch (Exception exc)
                {
                    jObject = null;
                }
            }
            return jObject;
        }
    }
}