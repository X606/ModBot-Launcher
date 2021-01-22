using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

		ModBotInstallationState _installationState;

		private void Form2_Load(object sender, EventArgs e)
		{
			if (File.Exists(MainForm.SkipFirstPageSaveFilePath))
			{
				SkipSelectScreen.Checked = File.ReadAllText(MainForm.SkipFirstPageSaveFilePath) == "true";
			}

			ModBotInstallationState state = ModBotInstallerManager.GetModBotInstallationState(MainForm.SelectedDirectory, out string modBotVersion);
			if (state == ModBotInstallationState.NotInstalled)
			{
				StatusLabel.Text = "Mod-Bot not installed";
				StatusLabel.ForeColor = Color.FromArgb(252, 124, 0);
				Reinstall.Hide();
			}
			else if (state == ModBotInstallationState.OutOfDate) 
			{
				StatusLabel.Text = "Mod-Bot out of date";
				StatusLabel.ForeColor = Color.FromArgb(255, 255, 0);
				InstallButton.Text = "Update Mod-Bot";
				Reinstall.Hide();
			}
			else if (state == ModBotInstallationState.UpToDate)
			{
				InstallButton.Text = "Start Game";
				StatusLabel.Text = "Mod-Bot up to date!";
				StatusLabel.ForeColor = Color.FromArgb(124, 252, 0);
			}
			else if(state == ModBotInstallationState.BetaVersion)
			{
				InstallButton.Text = "Start Game (beta)";
				StatusLabel.Text = "Mod-Bot beta installed";
				StatusLabel.ForeColor = Color.FromArgb(255, 0, 255);
			}
			_installationState = state;

			LocalVersionLabel.Text = "Local version: " + modBotVersion;
			LatestVersionLabel.Text = "Latest version: " + DownloadedData.LatestModBotVersion;
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
			Form2 form = new Form2();
			form.Show();

			Close();

			//StartGameAndExit();
		}


		public void StartGameAndExit()
		{
			Process.Start("steam://rungameid/597170");

			Application.Exit();

		}

		private void SkipSelectScreen_CheckedChanged(object sender, EventArgs e)
		{
			string path = MainForm.SkipFirstPageSaveFilePath;
			
			if (SkipSelectScreen.Checked)
			{
				File.WriteAllText(path, "true");
			} else
			{
				File.WriteAllText(path, "false");
			}

		}

		private void InstallButton_Click(object sender, EventArgs e)
		{
			if (_installationState != ModBotInstallationState.UpToDate && _installationState != ModBotInstallationState.BetaVersion)
			{
				InstallButton.Hide();
				CloseButton.Hide();
				Reinstall.Hide();
				ModBotInstallerManager.ProgressBar = ProgressBar;
				ModBotInstallerManager.Install(MainForm.SelectedDirectory, OnInstallFinished);
			}
			else
			{
				StartGameAndExit();
			}

		}

		private void Reinstall_Click(object sender, EventArgs e)
		{
			InstallButton.Hide();
			CloseButton.Hide();
			Reinstall.Hide();
			ModBotInstallerManager.ProgressBar = ProgressBar;
			ModBotInstallerManager.Install(MainForm.SelectedDirectory, OnInstallFinished);
		}
	}
}
