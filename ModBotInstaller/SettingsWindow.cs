using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public partial class SettingsWindow : Form
    {
        string betaSourceDirectory = null;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            // Disables maximizing the window by setting the maximized size to the default size
            MaximizedBounds = Bounds;

            skipFirstPageCheckbox.Checked = UserPreferences.Current.DontShowFirstPage;
            enableLocalBetaVersionCheckbox.Checked = UserPreferences.Current.EnableModBotBeta;
            betaSourceDirectory = UserPreferences.Current.ModBotBetaSourceDirectory;
            refreshBetaLocationItemsVisible();
            autoUpdateModsCheckbox.Checked = UserPreferences.Current.AutoUpdateMods;
            isSteamInstallCheckBox.Checked = UserPreferences.Current.IsLicensedInstall;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            closeWindow();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            UserPreferences.Current.DontShowFirstPage = skipFirstPageCheckbox.Checked;
            UserPreferences.Current.EnableModBotBeta = enableLocalBetaVersionCheckbox.Checked;
            UserPreferences.Current.ModBotBetaSourceDirectory = Utils.IsValidBetaInstallationPath(betaSourceDirectory) ? betaSourceDirectory : string.Empty;
            UserPreferences.Current.AutoUpdateMods = autoUpdateModsCheckbox.Checked;
            UserPreferences.Current.IsLicensedInstall = isSteamInstallCheckBox.Checked;
            UserPreferences.Current.SaveToFile();

            closeWindow();
        }

        void closeWindow()
        {
            Close();
        }

        private void enableLocalBetaVersionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            refreshBetaLocationItemsVisible();
        }

        void refreshBetaLocationItemsVisible()
        {
            changeBetaLocationButton.Visible = enableLocalBetaVersionCheckbox.Checked;
            changeBetaLocationNoteLabel.Visible = enableLocalBetaVersionCheckbox.Checked;
            setChangeBetaLabel(Utils.IsValidBetaInstallationPath(betaSourceDirectory));
        }

        private void changeBetaLocationButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = !string.IsNullOrEmpty(betaSourceDirectory) ? betaSourceDirectory : "C:/Program Files (x86)/Steam/steamapps/common",
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                betaSourceDirectory = dialog.FileName;

                if (Utils.IsValidBetaInstallationPath(dialog.FileName))
                {
                    setChangeBetaLabel(true);
                }
                else
                {
                    setChangeBetaLabel(false);
                }
            }
        }

        void setChangeBetaLabel(bool isValidPath)
        {
            if (isValidPath)
            {
                changeBetaLocationNoteLabel.Text = "(Valid path)";
                changeBetaLocationNoteLabel.ForeColor = Color.Chartreuse;
            }
            else
            {
                changeBetaLocationNoteLabel.Text = "(Invalid path)";
                changeBetaLocationNoteLabel.ForeColor = Color.Brown;
            }
        }

        private void isSteamInstall_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
