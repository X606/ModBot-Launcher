using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mod_BotLauncher
{
	public class Program
	{
		public static void Main()
		{
			string tempPath = Path.GetTempPath() + "ModBotInstaller/";

			Directory.CreateDirectory(tempPath);

			File.WriteAllBytes(tempPath + "AppDomainDelegateHolder.dll", Properties.Resources.AppDomainDelegateHolder);
			File.WriteAllBytes(tempPath + "Microsoft.WindowsAPICodePack.dll", Properties.Resources.Microsoft_WindowsAPICodePack);
			File.WriteAllBytes(tempPath + "Microsoft.WindowsAPICodePack.Shell.dll", Properties.Resources.Microsoft_WindowsAPICodePack_Shell);
			File.WriteAllBytes(tempPath + "Microsoft.WindowsAPICodePack.ShellExtensions.dll", Properties.Resources.Microsoft_WindowsAPICodePack_ShellExtensions);
			File.WriteAllBytes(tempPath + "ModBot Launcher.exe", Properties.Resources.ModBot_Launcher);

			Directory.SetCurrentDirectory(tempPath);
			Process process = Process.Start(tempPath + "ModBot Launcher.exe");

			process.WaitForExit();
		}
	}
}
