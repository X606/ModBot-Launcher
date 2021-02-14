using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public partial class MainForm : Form
    {
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

        void tryRequestModBotDownloadInfoFromServer()
        {
            try
            {
                // throw new Exception(); // For testing handling server not responding

                HttpWebRequest newestModBotVersionRequest = (HttpWebRequest)WebRequest.Create(@"https://modbot.org/api?operation=getCurrentModBotVersion");
                newestModBotVersionRequest.Timeout = 1500;
                HttpWebResponse newestModBotVersionResponse = (HttpWebResponse)newestModBotVersionRequest.GetResponse();
                string newestModBotVersionContent = new StreamReader(newestModBotVersionResponse.GetResponseStream()).ReadToEnd();
                DownloadedData.LatestModBotVersion = newestModBotVersionContent;

                HttpWebRequest modBotDownloadLinkRequest = (HttpWebRequest)WebRequest.Create(@"https://modbot.org/api?operation=getModBotDownload");
                modBotDownloadLinkRequest.Timeout = 1500;
                HttpWebResponse modBotDownloadLinkResponse = (HttpWebResponse)modBotDownloadLinkRequest.GetResponse();
                string modBotDownloadLink = new StreamReader(modBotDownloadLinkResponse.GetResponseStream()).ReadToEnd();
                DownloadedData.ModBotDownloadLink = modBotDownloadLink;

                DownloadedData.HasData = true;
            }
            catch
            {
                // TODO: Make the user able to continue without the server connection, since some lancher settings can still be changed further on, however, since Windows does not support custom text on buttons, this won't be very intuitive, so figure out a better way of doing this, maybe a custom error message window?

                DialogResult dialogResult = MessageBox.Show("Unable to connect to the Mod-Bot server", "Connection failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (dialogResult == DialogResult.Retry)
                {
                    tryRequestModBotDownloadInfoFromServer();
                }
                else if (dialogResult == DialogResult.Ignore)
                {
                    // Set to default values
                    DownloadedData.LatestModBotVersion = null;
                    DownloadedData.ModBotDownloadLink = null;
                    DownloadedData.HasData = false;
                }
                else
                {
                    Process.GetCurrentProcess().Kill();
                    return;
                }
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Disables maximizing the window by setting the maximized size to the default size
            MaximizedBounds = Bounds;

            tryRequestModBotDownloadInfoFromServer();

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

            if (validateCurrentInstallationDirectory())
            {
                if (UserPreferences.Current.DontShowFirstPage)
                    continueToNextWindow();
            }
        }

        bool validateCurrentInstallationDirectory(bool refreshState = true)
        {
            if (Utils.IsValidCloneDroneInstallationDirectory(UserPreferences.Current.GameInstallationDirectory))
            {
                if (refreshState)
                    setPickerState(true);

                return true;
            }
            else
            {
                if (refreshState)
                    setPickerState(false);

                return false;
            }
        }

        void setPickerState(bool isValid)
        {
            if (isValid)
            {
                BigTitle.ForeColor = Color.FromArgb(124, 252, 0);
                BigTitle.Text = "Game install location detected";
                InstallLocationDisplay.Text = UserPreferences.Current.GameInstallationDirectory;
                ChangeLocationButton.Text = "Change game install directory";
                ContinueButton.Enabled = true;
            }
            else
            {
                BigTitle.ForeColor = Color.FromArgb(252, 124, 0);
                BigTitle.Text = "Game install location not found";
                InstallLocationDisplay.Text = "Please provide a valid installation path";
                ChangeLocationButton.Text = "Specify game install directory";
                ContinueButton.Enabled = false;
            }
        }

        private void ChangeLocationButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:/Program Files (x86)/Steam/steamapps/common";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (Utils.IsValidCloneDroneInstallationDirectory(dialog.FileName))
                {
                    setPickerState(true);
                    UserPreferences.Current.GameInstallationDirectory = dialog.FileName;
                }
                else
                {
                    setPickerState(false);
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

        void continueToNextWindow()
        {
            Form2 form = new Form2();
            form.Show();

            UserPreferences.Current.SaveToFile();

            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
