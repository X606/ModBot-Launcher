using ModBotInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public class InstalledModsPanelUIItem
    {
        Panel _modPanel;

        PictureBox _modImage;

        Label _modNameLabel;
        Label _modDescriptionLabel;

        Label _modAuthorLabel;
        Label _modVersionLabel;
        Label _modIDLabel;

        PictureBox _settingsIcon;
        ToolTip _settingsIconTooltip;

        Label _updateStatusLabel;
        Button _updateButton;
        NewProgressBar _updateProgressBar;

        ModInfo _localModInfo;
        ModInfo _serverModInfo;

        Form2 _owner;

        public InstalledModsPanelUIItem(ModInfo modInfo, Control parent, Form2 owner)
        {
            _owner = owner;
            _owner.OnModStartedLoading();

            _localModInfo = modInfo;

            _modPanel = new Panel
            {
                Parent = parent,
                Name = "installedModsItem_" + _localModInfo.UniqueID,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(8, 8),
                Size = new Size(465, 98)
            };
            parent.Controls.Add(_modPanel);

            _modImage = new PictureBox
            {
                Name = _modPanel.Name + "_ModImage",
                Parent = _modPanel,
                Location = new Point(3, 3),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                ErrorImage = Resources.NoImageAvailable,
                Image = Resources.NoImageAvailable,
                InitialImage = Resources.NoImageAvailable
            };
            _modPanel.Controls.Add(_modImage);

            if (!string.IsNullOrEmpty(_localModInfo.ImageFileName))
                _modImage.LoadAsync("file:///" + _localModInfo.ModFolderPath + "/" + _localModInfo.ImageFileName);

            _modNameLabel = new Label
            {
                Name = _modPanel.Name + "_ModName",
                Parent = _modPanel,
                AutoSize = true,
                Font = new Font("Consolas", 10f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(58, 3),
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                Text = _localModInfo.DisplayName
            };
            _modPanel.Controls.Add(_modNameLabel);

            _modDescriptionLabel = new Label
            {
                Name = _modPanel.Name + "_ModDescription",
                Parent = _modPanel,
                Anchor = AnchorStyles.None,
                AutoEllipsis = true,
                Font = new Font("Consolas", 6.5f),
                ForeColor = Color.White,
                Location = new Point(59, 23),
                Size = new Size(335, 30),
                AutoSize = false,
                UseMnemonic = false,
                TextAlign = ContentAlignment.TopLeft,
                Text = string.IsNullOrEmpty(_localModInfo.Description) ? string.Empty : _localModInfo.Description
            };
            _modPanel.Controls.Add(_modDescriptionLabel);

            _modAuthorLabel = new Label
            {
                Name = _modPanel.Name + "_ModAuthor",
                Parent = _modPanel,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                AutoSize = true,
                Font = new Font("Consolas", 7f),
                ForeColor = Color.White,
                Location = new Point(3, 56),
                UseMnemonic = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "By: " + _localModInfo.Author
            };
            _modPanel.Controls.Add(_modAuthorLabel);

            _modVersionLabel = new Label
            {
                Name = _modPanel.Name + "_ModVersion",
                Parent = _modPanel,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                AutoSize = true,
                Font = new Font("Consolas", 7f),
                ForeColor = Color.White,
                Location = new Point(3, 68),
                UseMnemonic = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Version: " + _localModInfo.Version
            };
            _modPanel.Controls.Add(_modVersionLabel);

            _modIDLabel = new Label
            {
                Name = _modPanel.Name + "_ModID",
                Parent = _modPanel,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                AutoSize = true,
                Font = new Font("Consolas", 7f),
                ForeColor = Color.White,
                Location = new Point(3, 80),
                UseMnemonic = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Mod ID: " + _localModInfo.UniqueID
            };
            _modPanel.Controls.Add(_modIDLabel);

            _settingsIcon = new PictureBox
            {
                Name = _modPanel.Name + "_SettingsIcon",
                Parent = _modPanel,
                Location = new Point(435, 3),
                Size = new Size(25, 25),
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Image = Resources.SettingsIcon
            };
            _modPanel.Controls.Add(_settingsIcon);

            _settingsIconTooltip = new ToolTip();
            _settingsIconTooltip.SetToolTip(_settingsIcon, "Mod settings for " + _localModInfo.DisplayName);


            _updateStatusLabel = new Label
            {
                Name = _modPanel.Name + _modPanel.Name + "_UpdateStatus",
                Parent = _modPanel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Consolas", 7f),
                ForeColor = Color.White,
                Location = new Point(373, 56),
                TextAlign = ContentAlignment.MiddleCenter,
                UseMnemonic = false
            };
            _modPanel.Controls.Add(_updateStatusLabel);

            _updateButton = new Button
            {
                Name = _modPanel.Name + "_UpdateButton",
                Parent = _modPanel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(65, 65, 65),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Consolas", 8.25f),
                Location = new Point(386, 70),
                Size = new Size(64, 21),
                UseVisualStyleBackColor = false,
                Text = "Update"
            };
            _updateButton.Click += onUpdateButtonClicked;
            _updateButton.FlatAppearance.BorderSize = 0;
            _modPanel.Controls.Add(_updateButton);

            _updateProgressBar = new NewProgressBar
            {
                Name = _modPanel.Name + "_UpdateProgressBar",
                Parent = _modPanel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = Color.FromArgb(26, 26, 26),
                ForeColor = Color.FromArgb(255, 104, 0),
                Location = new Point(253, 70),
                Size = new Size(207, 21)
            };
            _modPanel.Controls.Add(_updateProgressBar);

            _updateStatusLabel.Visible = false;
            _updateButton.Visible = false;
            _updateProgressBar.Visible = false;

            getServerModInfoAndCheckForUpdates();
        }

        void refreshState()
        {
            if (!string.IsNullOrEmpty(_localModInfo.ImageFileName))
                _modImage.LoadAsync("file:///" + _localModInfo.ModFolderPath + "/" + _localModInfo.ImageFileName);

            _modNameLabel.Text = _localModInfo.DisplayName;
            _modDescriptionLabel.Text = string.IsNullOrEmpty(_localModInfo.Description) ? string.Empty : _localModInfo.Description;

            _modAuthorLabel.Text = "By: " + _localModInfo.Author;
            _modVersionLabel.Text = "Version: " + _localModInfo.Version;
            _modIDLabel.Text = "Mod ID: " + _localModInfo.UniqueID;

            _settingsIconTooltip.SetToolTip(_settingsIcon, "Mod settings for " + _localModInfo.DisplayName);

            _updateButton.Visible = false;
            _updateProgressBar.Visible = false;
            _updateStatusLabel.Visible = false;
        }

        void getServerModInfoAndCheckForUpdates()
        {
            Utils.SendWebRequest(@"https://modbot.org/api?operation=getModData&id=" + _localModInfo.UniqueID, onServerModInfoReceived, onServerModInfoRequestError);
        }

        void onServerModInfoRequestError(WebExceptionStatus status)
        {
            _owner.OnModFinishedLoading();
        }

        void onServerModInfoReceived(WebRequestResult result)
        {
            if (!result.IsError)
            {
                // If the result is "null" as a string, it means a mod with the passed ID doesn't exist on the server, so it's only a local file
                if (result.Result != "null")
                {
                    // I think it's safe to not try-catch here? The server should validate the ModInfo.json when the mod gets uploaded, so this should be fine.
                    _serverModInfo = JsonConvert.DeserializeObject<ModInfo>(result.Result);
                    checkForUpdates();
                }
            }

            _owner.OnModFinishedLoading();
        }

        void checkForUpdates()
        {
            if (_serverModInfo.Version > _localModInfo.Version)
            {
                if (UserPreferences.Current.AutoUpdateMods)
                {
                    updateMod();
                }
                else
                {
                    _updateStatusLabel.Visible = true;
                    _updateStatusLabel.Text = "Update available!";

                    _updateProgressBar.Visible = false;

                    _updateButton.Visible = true;
                    _updateButton.Enabled = true;
                }
            }
        }

        void onUpdateButtonClicked(object sender, EventArgs e)
        {
            _updateButton.Visible = false;

            _updateStatusLabel.Visible = true;
            _updateStatusLabel.Text = "Updating mod...";

            _updateProgressBar.Visible = true;

            updateMod();
        }

        async void updateMod()
        {
            _owner.OnModStartedUpdating();

            _updateProgressBar.Progress = 0f;

            string updateFilesDirectory = Path.GetTempPath() + Utils.StripAllInvalidPathCharacters(_localModInfo.UniqueID) + "_modupdatefiles";

            string tempModFolderCopyPath = Path.GetTempPath() + Utils.StripAllInvalidPathCharacters(_localModInfo.UniqueID) + "_backup";
            if (Directory.Exists(tempModFolderCopyPath))
                Directory.Delete(tempModFolderCopyPath, true);

            Utils.CopyDirectory(_localModInfo.ModFolderPath, tempModFolderCopyPath);

            using (Stream downloadedModFile = await Utils.DownloadFileAsync(@"https://modbot.org/api?operation=downloadMod&id=" + _localModInfo.UniqueID))
            {
                _updateProgressBar.Progress = 0.1f;

                if (Directory.Exists(updateFilesDirectory))
                    Directory.Delete(updateFilesDirectory, true);

                Directory.CreateDirectory(updateFilesDirectory);

                using (ZipArchive zip = new ZipArchive(downloadedModFile, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        string entryPath = Path.GetFullPath(Path.Combine(updateFilesDirectory, entry.FullName));

                        if (Path.GetFileName(entryPath).Length == 0)
                        {
                            Directory.CreateDirectory(entryPath);
                        }
                        else
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(entryPath));
                            await entry.ExtractToFileAsync(entryPath);
                        }

                        _updateProgressBar.Progress += 0.8f / zip.Entries.Count;
                    }
                }

                _updateProgressBar.Progress = 0.9f;
            }

            Utils.CopyDirectory(updateFilesDirectory, _localModInfo.ModFolderPath);
            Directory.Delete(updateFilesDirectory, true);

            _updateProgressBar.Progress = 1f;

            onModUpdated(tempModFolderCopyPath);
        }

        void onModUpdated(string tempModFolderCopyPath)
        {
            _updateProgressBar.Hide();
            _updateStatusLabel.Hide();
            _updateButton.Hide();

            string modFolderPath = _localModInfo.ModFolderPath;
            string modName = _localModInfo.DisplayName;

            try
            {
                _localModInfo = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modFolderPath + "/ModInfo.json"));
            }
            catch (JsonException)
            {
                DialogResult dialogResult = MessageBox.Show("Invalid ModInfo.json file! Please contact the creator of said mod. Mod will be reverted to the previous version.", "Error updating mod: '" + modName + "'", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.OK)
                {
                    Directory.Delete(modFolderPath, true); // Remove all downloaded mod files
                    Utils.CopyDirectory(tempModFolderCopyPath, modFolderPath);

                    // Remove temporary local copy of mod
                    Directory.Delete(tempModFolderCopyPath, true);

                    return;
                }
            }

            _localModInfo.ModFolderPath = modFolderPath;

            refreshState();

            // Remove temporary local copy of mod
            Directory.Delete(tempModFolderCopyPath, true);

            _owner.OnModFinishedUpdating();
        }

        public bool AreAllDependenciesInstalled(List<InstalledModsPanelUIItem> installedMods, out List<string> missingModIDs)
        {
            if (_localModInfo.ModDependencies == null)
            {
                missingModIDs = null;
                return true;
            }

            missingModIDs = new List<string>();
            foreach (string dependencyModID in _localModInfo.ModDependencies)
            {
                bool hasDependency = false;

                foreach (InstalledModsPanelUIItem installedMod in installedMods)
                {
                    if (installedMod == this)
                        continue;

                    if (installedMod._localModInfo.UniqueID == dependencyModID)
                    {
                        hasDependency = true;
                        break;
                    }
                }

                if (!hasDependency)
                    missingModIDs.Add(dependencyModID);
            }

            // If the missing dependencies list is empty, all required mods are already installed
            return missingModIDs.Count == 0;
        }
    }
}
