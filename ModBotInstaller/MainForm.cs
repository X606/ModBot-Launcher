using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public partial class MainForm : Form
    {
        private List<GameInstancesPanelUIItem> _gameInstanceItems;

        public MainForm()
        {
            InitializeComponent();
        }

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


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Disables maximizing the window by setting the maximized size to the default size
            MaximizedBounds = Bounds;

            detectedGamesView.AutoScroll = false;
            detectedGamesView.HorizontalScroll.Maximum = 0;
            detectedGamesView.VerticalScroll.Visible = false;
            detectedGamesView.AutoScroll = true;

            if (File.Exists(UserPreferences.FilePath))
            {
                UserPreferences.Current = new UserPreferences(File.ReadAllBytes(UserPreferences.FilePath));
            }
            else
            {
                // Create empty user preferences if they don't exist
                UserPreferences.Current = new UserPreferences();
            }

            UserPreferences.Current.MigrateFromOldSaveFormatsAndRemoveFiles();

            tryGetValidGameInstallDirectory();
        }

        private void tryGetValidGameInstallDirectory()
        {
            detectedGamesLabel.Visible = false;
            detectedGamesView.Visible = false;

            int windowHeight = 160;
            GameInstanceInfo customGameInstance = new GameInstanceInfo(UserPreferences.Current.GameInstallationDirectory);
            List<GameInstanceInfo> games = GameInstanceInfo.FindAllGameInstancesOnComputer();
            bool hasSeveralGames = games.Count > 1;

            if (games.Count == 0)
            {
                SelectGameInstance(customGameInstance.IsValid ? customGameInstance : null);
                setHeight(windowHeight);
                return;
            }

            SelectGameInstance(customGameInstance.IsValid ? customGameInstance : games[0],
                !customGameInstance.IsValid || UserPreferences.Current.IsLicensedInstall);

            if (hasSeveralGames)
            {
                windowHeight = 390;
                detectedGamesLabel.Visible = true;
                detectedGamesView.Visible = true;

                _gameInstanceItems = new List<GameInstancesPanelUIItem>();
                foreach (GameInstanceInfo game in games)
                {
                    _gameInstanceItems.Add(new GameInstancesPanelUIItem(game, detectedGamesView, this));
                }
            }

            setHeight(windowHeight);
        }

        public void SelectGameInstance(GameInstanceInfo gameInstance, bool isLicensed = true)
        {
            GameInstanceInfo.Current = gameInstance;
            if (_gameInstanceItems != null)
            {
                foreach (GameInstancesPanelUIItem game in _gameInstanceItems)
                    game.RefreshState();
            }

            if (gameInstance == null)
            {
                setPickerState(false);
                return;
            }

            UserPreferences.Current.IsLicensedInstall = isLicensed;
            UserPreferences.Current.GameInstallationDirectory = gameInstance.InstallationPath;
            UserPreferences.Current.SaveToFile();

            setPickerState(true);
        }

        private void setPickerState(bool isValid)
        {
            if (isValid)
            {
                BigTitle.Text = "Selected game install location";
                BigTitle.ForeColor = Color.FromArgb(124, 252, 0);
                InstallLocationDisplay.Text = UserPreferences.Current.GameInstallationDirectory;
                ContinueButton.Enabled = true;
            }
            else
            {
                BigTitle.Text = "Game install location not found";
                BigTitle.ForeColor = Color.FromArgb(252, 124, 0);
                InstallLocationDisplay.Text = "Please provide a valid installation path";
                ContinueButton.Enabled = false;
            }
        }

        private void setHeight(int height)
        {
            Size size = Size;
            size.Height = height;
            Size = size;
        }

        private void continueToNextWindow()
        {
            UserPreferences.Current.SaveToFile();

            Form2 form = new Form2();
            form.Show();

            Close();
        }

        private void ChangeLocationButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = "C:/",
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                GameInstanceInfo customGameInstance = new GameInstanceInfo(dialog.FileName);
                if (customGameInstance.IsValid)
                {
                    // if user selects licensed game in hard way
                    bool isLicensed = false;
                    foreach (GameInstancesPanelUIItem gameInstanceElement in _gameInstanceItems)
                    {
                        if (GameInstanceInfo.Compare(gameInstanceElement.GameInfo, customGameInstance))
                        {
                            isLicensed = true;
                            customGameInstance = gameInstanceElement.GameInfo;
                            break;
                        }
                    }

                    SelectGameInstance(customGameInstance, isLicensed);
                }
                else
                {
                    SelectGameInstance(null);
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            continueToNextWindow();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}