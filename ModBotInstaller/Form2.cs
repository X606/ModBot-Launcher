using ModBotInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        bool _hasInitializedInstalledModsView = false;

        ModBotInstallationState _installationState;
        
        int _numCurrentlyLoadingModItems;
        int _numCurrentlyUpdatingModItems;

        bool _isLoadingFirebaseModData;

        List<InstalledModsPanelUIItem> _installedModsUIItems;
        List<InstalledModsPanelUIItem_Non2_0> _installedModsUIItems_Non2_0;

        HashSet<string> _missingModDependencies;

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
                        Utils.EndProcess();
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
        }

        void refreshItemsBasedOnCurrentState()
        {
            switch (_installationState)
            {
                case ModBotInstallationState.NotInstalled:
                    StatusLabel.Text = "Mod-Bot not installed";
                    StatusLabel.ForeColor = Color.FromArgb(252, 124, 0);
                    InstallButton.Show();
                    InstallButton.Text = "Install Mod-Bot";
                    Reinstall.Hide();
                    break;
                case ModBotInstallationState.OutOfDate:
                    StatusLabel.Text = "Mod-Bot out of date";
                    StatusLabel.ForeColor = Color.FromArgb(255, 255, 0);
                    InstallButton.Show();
                    InstallButton.Text = "Update Mod-Bot";
                    Reinstall.Hide();
                    break;
                case ModBotInstallationState.UpToDate:
                    InstallButton.Show();
                    InstallButton.Text = "Start Game";
                    Reinstall.Show();
                    Reinstall.Text = "Reinstall";
                    StatusLabel.Text = "Mod-Bot up to date!";
                    StatusLabel.ForeColor = Color.FromArgb(124, 252, 0);
                    break;
                case ModBotInstallationState.BetaVersion:
                    InstallButton.Show();
                    InstallButton.Text = "Start Game (beta)";
                    Reinstall.Text = "Reinstall stable";
                    StatusLabel.Text = "Mod-Bot beta installed";
                    StatusLabel.ForeColor = Color.FromArgb(255, 0, 255);
                    break;
                case ModBotInstallationState.UnableToVerify:
                    StatusLabel.Text = "Unable to check latest Mod-Bot version";
                    StatusLabel.ForeColor = Color.FromArgb(125, 0, 125);
                    InstallButton.Show();
                    InstallButton.Text = "Start Game";
                    Reinstall.Hide();
                    break;
            }

            if (UserPreferences.Current.ShouldUseBetaPath)
            {
                InstallButton.Text = "Start (beta)";
                Reinstall.Text = "Start (non beta)";
            }

            InstallButton.Visible = _numCurrentlyLoadingModItems == 0 && _numCurrentlyUpdatingModItems == 0 && (_missingModDependencies == null || _missingModDependencies.Count == 0) && !_isLoadingFirebaseModData;
            Reinstall.Visible = _numCurrentlyLoadingModItems == 0 && _numCurrentlyUpdatingModItems == 0 && (_missingModDependencies == null || _missingModDependencies.Count == 0) && !_isLoadingFirebaseModData;
            installedModsLoading.Visible = _numCurrentlyLoadingModItems > 0 || _isLoadingFirebaseModData;

            if (!_hasInitializedInstalledModsView && _installationState != ModBotInstallationState.Failed && _installationState != ModBotInstallationState.NotInstalled)
                initializeInstalledModsView();
        }

        void initializeInstalledModsView()
        {
            // I have no clue why, but for some ungodly reason this is what you have to do to disable the HORIZONTAL scroll slider....
            installedModsView.AutoScroll = false;
            installedModsView.HorizontalScroll.Maximum = 0;
            installedModsView.VerticalScroll.Visible = false;
            installedModsView.AutoScroll = true;

            _numCurrentlyLoadingModItems = 0;
            _numCurrentlyUpdatingModItems = 0;

            _hasInitializedInstalledModsView = true;

            DirectoryInfo modsFolder = new DirectoryInfo(UserPreferences.Current.GameInstallationDirectory + "/mods");
            if (Constants.IS_MODBOT_2_0)
            {
                _installedModsUIItems = new List<InstalledModsPanelUIItem>();
                _missingModDependencies = new HashSet<string>();

                DirectoryInfo[] subDirectories = modsFolder.GetDirectories();

                foreach (DirectoryInfo modDirectory in subDirectories)
                {
                    FileInfo modInfoFile = modDirectory.FindFile("ModInfo.json");
                    if (modInfoFile != null)
                    {
                        ModInfo modInfo;
                        try
                        {
                            modInfo = JsonConvert.DeserializeObject<ModInfo>(File.ReadAllText(modInfoFile.FullName));
                        }
                        catch (JsonException) // If there was an error deserializing the ModInfo.json file, just skip it
                        {
                            MessageBox.Show("Error loading mod at path \"" + modDirectory.FullName + "\": Invalid ModInfo.json file. Please report this to the creator of said mod.", "Unable to load mod", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            continue;
                        }

                        modInfo.ModFolderPath = modDirectory.FullName;
                        _installedModsUIItems.Add(new InstalledModsPanelUIItem(modInfo, installedModsView, this));
                    }
                }

                foreach (InstalledModsPanelUIItem installedModUIItem in _installedModsUIItems)
                {
                    if (!installedModUIItem.AreAllDependenciesInstalled(_installedModsUIItems, out List<string> missingMods) && missingMods != null)
                    {
                        _missingModDependencies.IntersectWith(missingMods);
                    }
                }
            }
            else
            {
                _installedModsUIItems_Non2_0 = new List<InstalledModsPanelUIItem_Non2_0>();

                FileInfo[] dllFiles = modsFolder.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
                foreach (FileInfo modFile in dllFiles)
                {
                    _installedModsUIItems_Non2_0.Add(new InstalledModsPanelUIItem_Non2_0(modFile, installedModsView, this));
                }

                InstalledModsPanelUIItem_Non2_0.DownloadFirebaseModData(this);
            }

            refreshItemsBasedOnCurrentState();
        }

        public void OnModLoadingFailed(string message)
        {
            MessageBox.Show(message, "Error loading mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OnModStartedUpdating()
        {
            _numCurrentlyUpdatingModItems++;
            refreshItemsBasedOnCurrentState();
        }

        public void OnModFinishedUpdating()
        {
            _numCurrentlyUpdatingModItems--;
            refreshItemsBasedOnCurrentState();
        }

        public void OnModStartedLoading()
        {
            _numCurrentlyLoadingModItems++;
            refreshItemsBasedOnCurrentState();
        }

        public void OnModFinishedLoading()
        {
            _numCurrentlyLoadingModItems--;
            refreshItemsBasedOnCurrentState();
        }

        public void SetFirebaseModDataLoading(bool isLoading)
        {
            _isLoadingFirebaseModData = isLoading;
            refreshItemsBasedOnCurrentState();
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

        public static void StartGameAndExit()
        {
            Process.Start("steam://rungameid/" + Constants.CLONE_DRONE_STEAMAPPID); // Start Clone Drone via Steam
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
            SettingsWindow settingsWindow = new SettingsWindow
            {
                Owner = this
            };

            settingsWindow.ShowDialog();

            refreshItemsBasedOnCurrentState();
        }
    }
}
