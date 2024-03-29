﻿using Microsoft.WindowsAPICodePack.Dialogs;
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


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Disables maximizing the window by setting the maximized size to the default size
            MaximizedBounds = Bounds;

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

            if (tryGetValidGameInstallDirectory())
            {
                if (UserPreferences.Current.DontShowFirstPage)
                    continueToNextWindow();
            }
        }

        bool tryGetValidGameInstallDirectory()
        {
            if (validateCurrentInstallationDirectory())
                return true;

            if (SteamInstallLocationFinder.TryGetInstallPath(out string steamInstallPath))
            {
				if (Utils.IsValidCloneDroneInstallationDirectory(steamInstallPath))
				{
					UserPreferences.Current.IsSteamInstall = true;
					UserPreferences.Current.GameInstallationDirectory = steamInstallPath;
					UserPreferences.Current.SaveToFile();
					return true;
				}
			}

            return false;
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
            dialog.InitialDirectory = "C:/";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (Utils.IsValidCloneDroneInstallationDirectory(dialog.FileName))
                {
                    UserPreferences.Current.IsSteamInstall = false;
					UserPreferences.Current.GameInstallationDirectory = dialog.FileName;
                    UserPreferences.Current.SaveToFile();

                    setPickerState(true);
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
            UserPreferences.Current.SaveToFile();

            Form2 form = new Form2();
            form.Show();

            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
