using ModBotInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public class InstalledModsPanelUIItem_Non2_0
    {
        static Dictionary<string, FirebaseModInfo> _firebaseModDataCache = new Dictionary<string, FirebaseModInfo>();
        static List<InstalledModsPanelUIItem_Non2_0> _allInstances = new List<InstalledModsPanelUIItem_Non2_0>();

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

        FileInfo _localAssemblyFile;
        Control _parent;
        Form2 _owner;

        public InstalledModsPanelUIItem_Non2_0(FileInfo assemblyFile, Control parent, Form2 owner)
        {
            _allInstances.Add(this);

            _owner = owner;

            _parent = parent;
            _localAssemblyFile = assemblyFile;

            _modPanel = new Panel
            {
                Parent = _parent,
                Name = "installedModsItem_" + assemblyFile.Name,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(8, 8),
                Size = new Size(465, 98)
            };
            _parent.Controls.Add(_modPanel);

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
                Text = Utils.AddSpacesToCamelCasedString(Path.GetFileNameWithoutExtension(assemblyFile.FullName))
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
                Text = ""
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
                Text = "By: Unknown"
            };
            _modPanel.Controls.Add(_modAuthorLabel);

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
                Text = ""
            };
            _modPanel.Controls.Add(_modIDLabel);

            _updateStatusLabel = new Label
            {
                Name = _modPanel.Name + "_UpdateStatus",
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
        }

        void onUpdateButtonClicked(object sender, EventArgs e)
        {
            updateMod(null);
        }

        async Task updateMod(Stream newModFileStream)
        {
            _owner.OnModStartedUpdating();

            _updateButton.Visible = false;

            _updateStatusLabel.Visible = true;
            _updateStatusLabel.Text = "Updating mod...";

            _updateProgressBar.Visible = true;

            _updateProgressBar.Progress = 0f;

            if (newModFileStream == null)
                newModFileStream = await Utils.DownloadFileAsync(_firebaseModDataCache[_localAssemblyFile.Name].DownloadLink);

            _updateProgressBar.Progress = 0.5f;

            FileStream fileStream = new FileStream(_localAssemblyFile.FullName, FileMode.Create, FileAccess.Write);
            await newModFileStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            _updateProgressBar.Progress = 0.9f;

            fileStream.Dispose();
            newModFileStream.Dispose();

            _updateProgressBar.Progress = 1f;

            _updateProgressBar.Hide();
            _updateStatusLabel.Hide();
            _updateButton.Hide();

            _owner.OnModFinishedUpdating();
        }

        public static async Task DownloadFirebaseModData(Form2 form)
        {
            form.SetFirebaseModDataLoading(true);

            WebRequestResult webRequest = await Utils.SendWebRequestAsync("https://modbot-d8a58.firebaseio.com/mods/mods/.json");
            if (!webRequest.IsError)
            {
                FirebaseModInfo[] firebaseModInfos = null;
                try
                {
                    firebaseModInfos = JsonConvert.DeserializeObject<FirebaseModInfo[]>(webRequest.Result);
                }
                catch (JsonException)
                {
                }

                if (firebaseModInfos != null)
                {
                    _firebaseModDataCache = new Dictionary<string, FirebaseModInfo>();

                    foreach (FirebaseModInfo fireBaseModInfo in firebaseModInfos)
                    {
                        if (!string.IsNullOrEmpty(fireBaseModInfo.DownloadLink))
                        {
                            // This is a terrible way of getting the file name, but I'm pretty sure it's the only one
                            string fileName = fireBaseModInfo.DownloadLink.Split('/').Last();

                            _firebaseModDataCache.Add(fileName, fireBaseModInfo);
                        }
                    }

                    foreach (InstalledModsPanelUIItem_Non2_0 item in _allInstances)
                    {
                        item.refreshStateFromFirebaseModInfo();
                    }
                }
            }

            form.SetFirebaseModDataLoading(false);
        }

        static List<InstalledModsPanelUIItem_Non2_0> tmp = new List<InstalledModsPanelUIItem_Non2_0>();

        void refreshStateFromFirebaseModInfo()
        {
            tmp.Add(this);
            _owner.OnModStartedLoading();

            if (_firebaseModDataCache.TryGetValue(_localAssemblyFile.Name, out FirebaseModInfo firebaseModInfo))
            {
                if (!string.IsNullOrEmpty(firebaseModInfo.ImageDownloadLink))
                    _modImage.LoadAsync(firebaseModInfo.ImageDownloadLink);

                if (!string.IsNullOrEmpty(firebaseModInfo.ModName))
                    _modNameLabel.Text = firebaseModInfo.ModName;

                if (firebaseModInfo.Description != null)
                    _modDescriptionLabel.Text = firebaseModInfo.Description;

                if (!string.IsNullOrEmpty(firebaseModInfo.Creator))
                    _modAuthorLabel.Text = "By: " + firebaseModInfo.Creator;

                if (!string.IsNullOrEmpty(firebaseModInfo.ModID))
                    _modIDLabel.Text = "Mod ID: " + firebaseModInfo.ModID;

                checkForUpdates(firebaseModInfo.DownloadLink);
            }

            _owner.OnModFinishedLoading();
            tmp.Remove(this);
        }

        async Task checkForUpdates(string downloadLink)
        {
            using (Stream newestVersionFileStream = await Utils.DownloadFileAsync(downloadLink))
            {
                // Workaround because newestVersionFileStream.Length throws a NotSupportedException, so we have to create a temporary stream, then read the length of that
                using (MemoryStream tempStream = new MemoryStream())
                {
                    await newestVersionFileStream.CopyToAsync(tempStream);

                    if (!tempStream.ToArray().SequenceEqual(File.ReadAllBytes(_localAssemblyFile.FullName)))
                    {
                        if (UserPreferences.Current.AutoUpdateMods)
                        {
                            await updateMod(newestVersionFileStream);
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
            }
        }

        void onLoadError(string message)
        {
            _owner.OnModLoadingFailed(message);
            _owner.OnModFinishedLoading();
        }
    }
}
