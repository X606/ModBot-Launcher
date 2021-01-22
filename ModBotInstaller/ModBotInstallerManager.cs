using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace ModBotInstaller
{
	public static class ModBotInstallerManager
	{
		public static NewProgressBar ProgressBar;

		public static void Install(string installationPath, Action onComplete)
		{
			Thread worker = new Thread(delegate()
			{
				SetProgress(0.1f);

				string extractedZipFilePath;
				if (MainForm.BetaGetDirectory == null)
				{
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

					string zipPath = Path.GetTempPath() + "modbot.zip";
					using (var client = new WebClient())
					{
						client.DownloadFile(DownloadedData.ModBotDownloadLink, zipPath);
					}

					SetProgress(0.5f);

					extractedZipFilePath = Path.GetTempPath() + "modbot/";
					if (Directory.Exists(extractedZipFilePath))
					{
						Directory.Delete(extractedZipFilePath, true);
					}

					ZipFile.ExtractToDirectory(zipPath, extractedZipFilePath);
					File.Delete(zipPath);
				} else
				{
					extractedZipFilePath = MainForm.BetaGetDirectory;
				}

				SetProgress(0.55f);

				directoryCopy(extractedZipFilePath, installationPath, true);

				if (MainForm.BetaGetDirectory == null)
				{
					Directory.Delete(extractedZipFilePath, true);
				}
					

				SetProgress(0.6f);

				string managedFolderPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/";

				Directory.SetCurrentDirectory(managedFolderPath);

				// The injector is now packaged with this, so no need to run the injector
				/*
				
				string injectorPath = managedFolderPath + "Injector.exe";

				if (!File.Exists(injectorPath))
				{
					throw new Exception("Unable to find injetor... Please consult the idiot who uploaded this mod-bot version");
				}

				Process injector = Process.Start(injectorPath);
				injector.WaitForExit();
				*/

				Injector.ModBotInjector.InstallModBot(
					managedFolderPath + "Assembly-CSharp.dll",
					managedFolderPath + "InjectionClasses.dll",
					managedFolderPath + "ModLibrary.dll",
					managedFolderPath
					);
				
				
				SetProgress(1.0f);
				
				if (onComplete != null)
					CallInMainThread(onComplete);
			});
			worker.Start();
			
		}

		static void SetProgress(float val)
		{
			CallInMainThread(delegate
			{
				ProgressBar.Value = (int)(val * 100);
			});

		}
		static void CallInMainThread(Action action)
		{
			ProgressBar.BeginInvoke((MethodInvoker)delegate ()
			{
				action();
			});
		}

		static void directoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(destDirName, subdir.Name);
					directoryCopy(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}


		public static ModBotInstallationState GetModBotInstallationState(string installationPath, out string modBotVersion)
		{
			string assemblyPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/Assembly-CSharp.dll";
			string modlibraryPath = installationPath + "/Clone Drone in the Danger Zone_Data/Managed/ModLibrary.dll";

			if (!File.Exists(assemblyPath))
				throw new Exception("This was not a valid game installation path");
				

			if (!File.Exists(modlibraryPath))
			{
				modBotVersion = null;
				return ModBotInstallationState.NotInstalled;
			}


			GetModBotInstallationOutputStateInfo state = NewAppDomain.Execute<GetModBotInstallationInputStateInfo, GetModBotInstallationOutputStateInfo>(
				new GetModBotInstallationInputStateInfo()
				{
					AssemblyPath = assemblyPath,
					ModlibraryPath = modlibraryPath,
					LatestModBotVersion = DownloadedData.LatestModBotVersion
				},
				delegate (GetModBotInstallationInputStateInfo input)
				{
					bool foundModdedObject = false;
					Type[] types = Assembly.LoadFrom(input.AssemblyPath).GetTypes();
					foreach (Type type in types)
					{
						if (type.Name == "ModdedObject")
						{
							foundModdedObject = true;
							break;
						}
					}
					if (!foundModdedObject)
					{
						return new GetModBotInstallationOutputStateInfo()
						{
							ModBotVersion = null,
							State = ModBotInstallationState.NotInstalled
						};
					}

					Assembly modlibraryAssembly = Assembly.LoadFrom(input.ModlibraryPath);
					string version = getResurceFromAssembly(modlibraryAssembly, "ModBotVersion");
					if (version == null)
						return new GetModBotInstallationOutputStateInfo()
						{
							State = ModBotInstallationState.NotInstalled,
							ModBotVersion = version
						};

					if (version.Contains("beta"))
						return new GetModBotInstallationOutputStateInfo()
						{
							State = ModBotInstallationState.BetaVersion,
							ModBotVersion = version
						};

					if (isCloudVersionNewer(version, input.LatestModBotVersion))
						return new GetModBotInstallationOutputStateInfo()
						{
							State = ModBotInstallationState.OutOfDate,
							ModBotVersion = version
						};

					return new GetModBotInstallationOutputStateInfo()
					{
						State = ModBotInstallationState.UpToDate,
						ModBotVersion = version
					};
				}
			);

			modBotVersion = state.ModBotVersion;

			return state.State;
		}

		static string getResurceFromAssembly(Assembly assembly, string resourceName)
		{
			string[] resources = assembly.GetManifestResourceNames();
			foreach (string resource in resources)
			{
				ResourceManager resourceManager = new ResourceManager(resource, assembly);

				// Get the fully qualified resource type name
				// Resources are suffixed with .resource
				string rst = resource.Substring(0, resource.IndexOf(".resource"));
				Type type = assembly.GetType(rst, false);

				// if type is null then its not .resx resource
				if (null != type)
				{
					var res = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					foreach (PropertyInfo re in res)
					{
						// collect string type resources
						if (re.PropertyType == typeof(string))
						{
							if (re.Name == resourceName)
							{
								// get value from static property
								return re.GetValue(null, null) as string;
							}
						}
					}
				}

			}

			return null;
		}

		static bool isCloudVersionNewer(string installedVersion, string cloudVersion)
		{
			Version installed = new Version(installedVersion);
			Version cloud = new Version(cloudVersion);

			var result = installed.CompareTo(cloud);
			if (result > 0)
				return false;
			else if (result < 0)
				return true;
			
			return false;
		}

		[Serializable]
		public class GetModBotInstallationInputStateInfo
		{
			public string AssemblyPath;
			public string ModlibraryPath;
			public string LatestModBotVersion;
		}
		[Serializable]
		public class GetModBotInstallationOutputStateInfo
		{
			public ModBotInstallationState State;
			public string ModBotVersion;
		}
	}

	public enum ModBotInstallationState
	{
		NotInstalled,
		OutOfDate,
		UpToDate,
		BetaVersion
	}


}
