using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ModBotInstaller
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public const string EXPECTED_DIRECTORY = "C:/Program Files (x86)/Steam/steamapps/common/Clone Drone in the Danger Zone";
		public const string DIRECTORY_SAVE_FILE = "SaveFile.txt";
		public const string SKIP_SAVE_FILE = "skip.txt";
		public const string BETA_DIRECTORY = "beta.txt";

		public string SaveFilePath => Application.UserAppDataPath + "/" + DIRECTORY_SAVE_FILE;
		public static string SkipFirstPageSaveFilePath => Application.UserAppDataPath + "/" + SKIP_SAVE_FILE;
		public static string UseBetaFilePath => Application.UserAppDataPath + "/" + BETA_DIRECTORY;


		public static string SelectedDirectory;
		public static string BetaGetDirectory = null;

		private void form_onLoad(object sender, EventArgs e)
		{
			try
			{
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://modbot.org/api?operation=getCurrentModBotVersion");
					request.Timeout = 1500;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
					DownloadedData.LatestModBotVersion = content;
				}
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://modbot.org/api?operation=getModBotDownload");
					request.Timeout = 1500;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
					DownloadedData.ModBotDownloadLink = content;
				}

			}
			catch
			{
				MessageBox.Show("Could not connect to the Mod-Bot server, please try again later.", "Error");
				Process.GetCurrentProcess().Kill();
				return;
			}

			

			//LatestModBotVersionLabel.Text = "Latest Mod-Bot version: " + LatestModBotVersion;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (File.Exists(UseBetaFilePath))
			{
				BetaGetDirectory = File.ReadAllText(UseBetaFilePath);
			}


			string directory = EXPECTED_DIRECTORY;
			if (File.Exists(SaveFilePath))
			{
				directory = File.ReadAllText(SaveFilePath);
			}

			bool sucsess = TrySet(directory, false);

			if (sucsess && File.Exists(SkipFirstPageSaveFilePath))
			{
				if (File.ReadAllText(SkipFirstPageSaveFilePath) == "true")
				{
					ContinueButton_Click(null, null);
				}
			}
		}

		bool TrySet(string directory, bool save = false)
		{
			if (Directory.Exists(directory) && IsValidCloneDroneInstallation(directory))
			{
				BigTitle.ForeColor = Color.FromArgb(124, 252, 0);
				BigTitle.Text = "Game install location detected";
				InstallLocationDisplay.Text = directory;
				ChangeLocationButton.Text = "Change";
				ContinueButton.Enabled = true;

				SelectedDirectory = directory;
				if (save)
				{
					File.WriteAllText(SaveFilePath, directory);
				}
				return true;
			}
			else
			{
				BigTitle.ForeColor = Color.FromArgb(252, 124, 0);
				BigTitle.Text = "Game install location not found";
				InstallLocationDisplay.Text = "Please provide a installation path";
				ChangeLocationButton.Text = "Set";

				ContinueButton.Enabled = false;

				return false;
			}
		}

		private void ChangeLocationButton_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.InitialDirectory = "C:/Program Files (x86)/Steam/steamapps/common";
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				TrySet(dialog.FileName, true);
				//MessageBox.Show("You selected: " + dialog.FileName);
			}

		}

		DirectoryInfo GetDirectory(DirectoryInfo[] infos, string name)
		{
			for (int i = 0; i < infos.Length; i++)
			{
				if (infos[i].Name == name)
					return infos[i];
			}

			return null;
		}
		FileInfo GetFile(FileInfo[] infos, string name)
		{
			for (int i = 0; i < infos.Length; i++)
			{
				if (infos[i].Name == name)
					return infos[i];
			}

			return null;
		}
		

		bool IsValidCloneDroneInstallation(string path)
		{
			DirectoryInfo directory = new DirectoryInfo(path);

			DirectoryInfo dataDirectory = GetDirectory(directory.GetDirectories(), "Clone Drone in the Danger Zone_Data");
			if (dataDirectory == null)
				return false;

			DirectoryInfo managedDirectory = GetDirectory(dataDirectory.GetDirectories(), "Managed");
			if (managedDirectory == null)
				return false;


			FileInfo assembly = GetFile(managedDirectory.GetFiles(), "Assembly-CSharp.dll");
			if (assembly == null)
				return false;
			

			return true;
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ContinueButton_Click(object sender, EventArgs e)
		{
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
