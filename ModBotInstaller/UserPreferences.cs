using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    [Serializable]
    public class UserPreferences
    {
        public static UserPreferences Current;

        public static string FilePath => Application.UserAppDataPath + "/userprefs.mbi";

        public const string DEFAULT_INSTALL_DIRECTORY = "C:/Program Files (x86)/Steam/steamapps/common/Clone Drone in the Danger Zone";

        #region Old save formats

        public const string OLD_SAVE_DATA_FILE_NAME = "SaveFile.txt";
        public const string SKIP_SAVE_FILE = "skip.txt";
        public const string BETA_DIRECTORY = "beta.txt";

        public string OldSaveFilePath => Application.UserAppDataPath + "/" + OLD_SAVE_DATA_FILE_NAME;
        public static string SkipFirstPageSaveFilePath => Application.UserAppDataPath + "/" + SKIP_SAVE_FILE;
        public static string UseBetaFilePath => Application.UserAppDataPath + "/" + BETA_DIRECTORY;

        #endregion

        const ushort CURRENT_VERSION = 3;

        public ushort Version;
        public string GameInstallationDirectory;
        public bool IsSteamInstall;

        public bool EnableModBotBeta;
        public string ModBotBetaSourceDirectory; // The source directory to install the beta version from
        public bool ShouldUseBetaPath => EnableModBotBeta && !string.IsNullOrEmpty(ModBotBetaSourceDirectory);

        public bool DontShowFirstPage;

        public bool AutoUpdateMods;

        public void ResetBetaSourceDirectory()
        {
            ModBotBetaSourceDirectory = string.Empty;
        }

        public UserPreferences()
        {
            Version = CURRENT_VERSION;
            GameInstallationDirectory = DEFAULT_INSTALL_DIRECTORY;
            IsSteamInstall = true;
			DontShowFirstPage = true;
            EnableModBotBeta = false;
            AutoUpdateMods = false;
            ResetBetaSourceDirectory();
        }

        public UserPreferences(byte[] rawBytes)
        {
            using (MemoryStream stream = new MemoryStream(rawBytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    Version = reader.ReadUInt16();
                    GameInstallationDirectory = reader.ReadString();
                    ModBotBetaSourceDirectory = reader.ReadString();
                    DontShowFirstPage = reader.ReadBoolean();

                    if (Version >= 1)
                    {
                        EnableModBotBeta = reader.ReadBoolean();
                    }
                    else
                    {
                        EnableModBotBeta = false;
                    }

                    if (Version >= 2)
                    {
                        AutoUpdateMods = reader.ReadBoolean();
                    }
                    else
                    {
                        AutoUpdateMods = false;
                    }

					if (Version >= 3)
					{
						IsSteamInstall = reader.ReadBoolean();
					}
					else
					{
						IsSteamInstall = false;
					}

					Version = CURRENT_VERSION;
                }
            }
        }

        public void SaveToFile()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Version);
                    writer.Write(GameInstallationDirectory);
                    writer.Write(ModBotBetaSourceDirectory);
                    writer.Write(DontShowFirstPage);
                    writer.Write(EnableModBotBeta);
                    writer.Write(AutoUpdateMods);
                    writer.Write(IsSteamInstall);
                }

                File.WriteAllBytes(FilePath, stream.ToArray());
            }
        }

        public void MigrateFromOldSaveFormatsAndRemoveFiles()
        {
            if (File.Exists(UseBetaFilePath))
            {
                ModBotBetaSourceDirectory = File.ReadAllText(UseBetaFilePath);
                File.Delete(UseBetaFilePath);
            }

            if (File.Exists(OldSaveFilePath))
            {
                GameInstallationDirectory = File.ReadAllText(OldSaveFilePath);
                File.Delete(OldSaveFilePath);
            }

            if (File.Exists(SkipFirstPageSaveFilePath))
            {
                DontShowFirstPage = File.ReadAllText(SkipFirstPageSaveFilePath) == "true";
                File.Delete(SkipFirstPageSaveFilePath);
            }
        }
    }
}
