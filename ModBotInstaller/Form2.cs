using ModBotInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        ModBotInstallationState _installationState;

        /*
        Constants in Windows API
        0x84 = WM_NCHITTEST - Mouse Capture Test
        0x1 = HTCLIENT - Application Client Area
        0x2 = HTCAPTION - Application Title Bar
        
        This function intercepts all the commands sent to the application. 
        It checks to see of the message is a mouse click in the application. 
        It passes the action to the base action by default. It reassigns 
        the action to the title bar if it occured in the client area
        to allow the drag and move behavior.
        */
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Disables maximizing the window by setting the maximized size to the default size
            MaximizedBounds = Bounds;

            string installedModBotVersion;
            do
            {
                _installationState = ModBotInstallerManager.GetModBotInstallationState(UserPreferences.Current.GameInstallationDirectory, out installedModBotVersion, out string errorMessage);

                if (_installationState == ModBotInstallationState.Failed)
                {
                    DialogResult dialogResult = MessageBox.Show(errorMessage, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    
                    if (dialogResult == DialogResult.Cancel)
                    {
                        // End current process
                        Process.GetCurrentProcess().Kill();
                        return;
                    }
                    // If the selected option is not Cancel, we should try again
                }
            }
            while (_installationState == ModBotInstallationState.Failed);

            refreshItemsBasedOnCurrentState();

            if (installedModBotVersion != null) // Just to be safe
            {
                LocalVersionLabel.Text = "Local version: " + installedModBotVersion;
            }
            else
            {
                LocalVersionLabel.Visible = false;
            }

            if (DownloadedData.HasData)
            {
                LatestVersionLabel.Text = "Latest version: " + DownloadedData.LatestModBotVersion;
            }
            else
            {
                LatestVersionLabel.Visible = false;
            }

            intitializeInstalledModsView();
        }

        void refreshItemsBasedOnCurrentState()
        {
            switch (_installationState)
            {
                case ModBotInstallationState.NotInstalled:
                    StatusLabel.Text = "Mod-Bot not installed";
                    StatusLabel.ForeColor = Color.FromArgb(252, 124, 0);
                    InstallButton.Text = "Install Mod-Bot";
                    Reinstall.Hide();
                    break;
                case ModBotInstallationState.OutOfDate:
                    StatusLabel.Text = "Mod-Bot out of date";
                    StatusLabel.ForeColor = Color.FromArgb(255, 255, 0);
                    InstallButton.Text = "Update Mod-Bot";
                    Reinstall.Hide();
                    break;
                case ModBotInstallationState.UpToDate:
                    InstallButton.Text = "Start Game";
                    Reinstall.Text = "Reinstall";
                    StatusLabel.Text = "Mod-Bot up to date!";
                    StatusLabel.ForeColor = Color.FromArgb(124, 252, 0);
                    break;
                case ModBotInstallationState.BetaVersion:
                    InstallButton.Text = "Start Game (beta)";
                    Reinstall.Text = "Reinstall stable";
                    StatusLabel.Text = "Mod-Bot beta installed";
                    StatusLabel.ForeColor = Color.FromArgb(255, 0, 255);
                    break;
                case ModBotInstallationState.UnableToVerify:
                    StatusLabel.Text = "Unable to check latest Mod-Bot version";
                    StatusLabel.ForeColor = Color.FromArgb(125, 0, 125);
                    InstallButton.Text = "Start Game";
                    Reinstall.Hide();
                    break;
            }

            if (UserPreferences.Current.ShouldUseBetaPath)
            {
                InstallButton.Text = "Start (beta)";
                Reinstall.Text = "Start (non beta)";
            }
        }

        void intitializeInstalledModsView()
        {
            // I have no clue why, but for some ungodly reason this is what you have to do to disable the HORIZONTAL scroll slider....
            installedModsView.AutoScroll = false;
            installedModsView.HorizontalScroll.Maximum = 0;
            installedModsView.VerticalScroll.Visible = false;
            installedModsView.AutoScroll = true;

            DirectoryInfo modsFolder = new DirectoryInfo(UserPreferences.Current.GameInstallationDirectory + "/mods");

            DirectoryInfo[] subDirectories = modsFolder.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo modDirectory in subDirectories)
            {
                FileInfo modInfoFile = Utils.GetFile(modDirectory.GetFiles(), "ModInfo.json");
                if (modInfoFile != null)
                {
                    ModInfo modInfo = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modInfoFile.FullName));
                    createModItemFor(modDirectory.FullName, modInfo);
                }
            }
        }

        void createModItemFor(string modFolderPath, ModInfo modInfo)
        {
            Panel modPanel = new Panel
            {
                Size = new Size(465, 58),
                BorderStyle = BorderStyle.FixedSingle,
            };

            PictureBox modImage = new PictureBox
            {
                Parent = modPanel,
                Location = new Point(3, 3),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Left,
                ErrorImage = Resources.NoImageAvailable,
                InitialImage = Resources.NoImageAvailable,
                Image = Resources.NoImageAvailable
            };

            if (!string.IsNullOrWhiteSpace(modInfo.ImageFileName))
            {
                string imageFilePath = modFolderPath + "/" + modInfo.ImageFileName;

                try
                {
                    modImage.Image = Image.FromFile(imageFilePath);
                }
                catch // If there is any unexpexted error loading the file, default to the "No Image Available" image
                {
                }
            }

            Label modNameLabel = new Label
            {
                Parent = modPanel,
                Location = new Point(57, 0),
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                UseMnemonic = false,
                Font = new Font("Consolas", 10f, FontStyle.Bold),
                ForeColor = Color.White,
                Text = modInfo.DisplayName,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label modDescriptionLabel = new Label
            {
                Parent = modPanel,
                Location = new Point(59, 17),
                AutoSize = false,
                Size = new Size(256, 36),
                Anchor = AnchorStyles.Bottom,
                UseMnemonic = false,
                AutoEllipsis = true,
                Font = new Font("Consolas", 7f),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopLeft,
                Text = modInfo.Description
            };

            Label modVersionLabel = new Label
            {
                Text = "Version: " + modInfo.Version,
                Parent = modPanel,
                AutoSize = true,
                Location = new Point(321, 33),
                UseMnemonic = false,
                Font = new Font("Consolas", 6.5f),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label modAuthorLabel = new Label
            {
                Text = "By: " + modInfo.Author,
                Parent = modPanel,
                Location = new Point(321, 42),
                AutoSize = true,
                UseMnemonic = false,
                Font = new Font("Consolas", 6.5f),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            PictureBox settingsIcon = new PictureBox
            {
                Parent = modPanel,
                Location = new Point(435, 3),
                Size = new Size(25, 25),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Image = Resources.SettingsIcon
            };

            ToolTip settingsIconTooltip = new ToolTip();
            settingsIconTooltip.SetToolTip(settingsIcon, "Mod settings for " + modInfo.DisplayName);

            // If we couldn't download any data, either the Mod-Bot servers are down, or we don't have an internet connection,
            // either way, we shouldn't try to compare versions if we couldn't connect earlier.
            if (DownloadedData.HasData)
            {
                new Thread(delegate ()
                {

                }).Start();
            }

            installedModsView.Controls.Add(modPanel);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }

        void OnInstallFinished()
        {
            if (UserPreferences.Current.ShouldUseBetaPath)
            {
                StartGameAndExit();
                return;
            }

            Form2 form = new Form2();
            form.Show();

            Close();
        }


        public void StartGameAndExit()
        {
            Process.Start("steam://rungameid/597170");

            Application.Exit();
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if ((_installationState != ModBotInstallationState.UpToDate && _installationState != ModBotInstallationState.BetaVersion) || UserPreferences.Current.ShouldUseBetaPath)
            {
                StatusLabel.Hide();
                installedModsView.Hide();
                installedModsTitle.Hide();
                InstallButton.Hide();
                CloseButton.Hide();
                Reinstall.Hide();
                modBotSettingsButton.Hide();
                LocalVersionLabel.Hide();
                LatestVersionLabel.Hide();
                ProgressBar.Show();
                installingModBotLabel.Show();
                ModBotInstallerManager.ProgressBar = ProgressBar;
                ModBotInstallerManager.Install(UserPreferences.Current.GameInstallationDirectory, OnInstallFinished);
            }
            else
            {
                StartGameAndExit();
            }
        }

        private void Reinstall_Click(object sender, EventArgs e)
        {
            if (UserPreferences.Current.ShouldUseBetaPath)
                UserPreferences.Current.ResetBetaSourceDirectory();

            StatusLabel.Hide();
            installedModsView.Hide();
            installedModsTitle.Hide();
            InstallButton.Hide();
            CloseButton.Hide();
            Reinstall.Hide();
            modBotSettingsButton.Hide();
            LocalVersionLabel.Hide();
            LatestVersionLabel.Hide();
            ProgressBar.Show();
            installingModBotLabel.Show();
            ModBotInstallerManager.ProgressBar = ProgressBar;
            ModBotInstallerManager.Install(UserPreferences.Current.GameInstallationDirectory, OnInstallFinished);
        }

        private void modBotSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;

            settingsWindow.ShowDialog();

            refreshItemsBasedOnCurrentState();
        }
    }
}
