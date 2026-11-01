using System.Collections.Generic;
using System.IO;

namespace ModBotInstaller
{
    public class GameInstanceInfo
    {
        private static GameInstanceInfo _current;
        public static GameInstanceInfo Current
        {
            get => _current;
            set => _current = value;
        }

        public GameReleaseType Release;

        public GamePlatformType Platform;

        public string InstallationPath;

        public bool IsLicensed;

        private string _dataDirectory;

        private string _managedDirectory;

        private bool _is64Bit;

        public bool Exists => Directory.Exists(InstallationPath);

        public bool IsValid => Utils.IsValidCloneDroneInstallationDirectory(InstallationPath);

        public bool Is64Bit => _is64Bit;

        public string DataDirectory => _dataDirectory;

        public string ManagedDirectory => _managedDirectory;

        public GameInstanceInfo() { }

        public GameInstanceInfo(string installationPath)
        {
            if (installationPath == null) throw new System.ArgumentNullException(nameof(installationPath));

            InstallationPath = installationPath.Replace("/", "\\").Trim('\\');
            IsLicensed = true;
            setReleaseAndPlatformFromFiles();
            setExtraValues();
        }

        public GameInstanceInfo(GameReleaseType release, GamePlatformType platform)
        {
            Release = release;
            Platform = platform;
            IsLicensed = true;
            setInstallationPathFromReleaseAndPlatform();
            setExtraValues();
        }

        public GameInstanceInfo(string installationPath, GameReleaseType release, GamePlatformType platform)
        {
            if (installationPath == null) throw new System.ArgumentNullException(nameof(installationPath));

            InstallationPath = installationPath.Replace("/", "\\").Trim('\\');
            Release = release;
            Platform = platform;
            IsLicensed = false;
            setExtraValues();
        }

        private void setReleaseAndPlatformFromFiles()
        {
            Release = GameReleaseType.None;
            Platform = GamePlatformType.None;

            DirectoryInfo directory = new DirectoryInfo(InstallationPath);
            if (!directory.Exists) return;

            if (directory.FindFile("Clone Drone in the Danger Zone.exe") != null)
            {
                Release = GameReleaseType.DangerZone;
                Platform = GamePlatformType.Steam;
            }
            else if (directory.FindFile("Clone Drone in the Danger Zone.exe.exe") != null)
            {
                Release = GameReleaseType.DangerZone;
                Platform = GamePlatformType.EpicGames;
            }
            else if (directory.FindFile("Clone Drone in the Hyperdome.exe") != null)
            {
                Release = GameReleaseType.Hyperdome;
                Platform = GamePlatformType.Steam;
            }
        }

        private void setInstallationPathFromReleaseAndPlatform()
        {
            if (Release == GameReleaseType.DangerZone)
            {
                if (Platform == GamePlatformType.Steam)
                {
                    SteamInstallLocationFinder.TryGetInstallPath(GameReleaseType.DangerZone, out InstallationPath);
                }
                else if (Platform == GamePlatformType.EpicGames)
                {
                    EpicGamesInstallLocationFinder.TryGetInstallPath(out InstallationPath);
                }
            }
            else if (Release == GameReleaseType.Hyperdome)
            {
                SteamInstallLocationFinder.TryGetInstallPath(GameReleaseType.Hyperdome, out InstallationPath);
            }

            if (!string.IsNullOrEmpty(InstallationPath)) InstallationPath = InstallationPath.Replace("/", "\\").Trim('\\');
        }

        private void setExtraValues()
        {
            _dataDirectory = null;
            _managedDirectory = null;
            _is64Bit = false;

            if (string.IsNullOrEmpty(InstallationPath) || !Directory.Exists(InstallationPath)) return;

            string dataFolder = null;
            if (Release == GameReleaseType.DangerZone)
            {
                if (Platform == GamePlatformType.Steam) dataFolder = "Clone Drone in the Danger Zone_Data";
                else if (Platform == GamePlatformType.EpicGames) dataFolder = "Clone Drone in the Danger Zone.exe_Data";
            }
            else if (Release == GameReleaseType.Hyperdome) dataFolder = "Clone Drone in the Hyperdome_Data";

            if (dataFolder != null)
            {
                _dataDirectory = Path.Combine(InstallationPath, dataFolder);
                _managedDirectory = Path.Combine(InstallationPath, dataFolder, "Managed");
            }

            DirectoryInfo directory = new DirectoryInfo(InstallationPath);
            if (directory.Exists)
            {
                _is64Bit = directory.FindFile("UnityCrashHandler64.exe") != null;
            }
        }

        public static List<GameInstanceInfo> FindAllGameInstancesOnComputer()
        {
            List<GameInstanceInfo> list = new List<GameInstanceInfo>(3);

            GameInstanceInfo dangerZoneSteam = new GameInstanceInfo(GameReleaseType.DangerZone, GamePlatformType.Steam);
            if (dangerZoneSteam.Exists && dangerZoneSteam.IsValid) list.Add(dangerZoneSteam);

            GameInstanceInfo dangerZoneEpic = new GameInstanceInfo(GameReleaseType.DangerZone, GamePlatformType.EpicGames);
            if (dangerZoneEpic.Exists && dangerZoneEpic.IsValid) list.Add(dangerZoneEpic);

            if (Constants.SUPPORT_HYPERDOME)
            {
                GameInstanceInfo hyperdomeSteam = new GameInstanceInfo(GameReleaseType.Hyperdome, GamePlatformType.Steam);
                if (hyperdomeSteam.Exists && hyperdomeSteam.IsValid) list.Add(hyperdomeSteam);
            }

            return list;
        }

        public static bool Compare(GameInstanceInfo a, GameInstanceInfo b)
        {
            if (Equals(a, b)) return true;
            if (a == null || b == null) return false;
            return a.InstallationPath == b.InstallationPath;
        }
    }
}