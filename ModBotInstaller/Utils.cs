using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBotInstaller
{
    public static class Utils
    {
        public static bool IsValidCloneDroneInstallationDirectory(string directory)
        {
            return Directory.Exists(directory) && IsValidCloneDroneInstallation(directory);
        }

        public static bool IsValidCloneDroneInstallation(string path)
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

        public static bool IsValidBetaInstallationDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory == null)
                return false;

            DirectoryInfo dataDirectory = GetDirectory(directory.GetDirectories(), "Clone Drone in the Danger Zone_Data");
            if (dataDirectory == null)
                return false;

            DirectoryInfo managedDirectory = GetDirectory(dataDirectory.GetDirectories(), "Managed");
            if (managedDirectory == null)
                return false;

            FileInfo assembly = GetFile(managedDirectory.GetFiles(), "ModLibrary.dll");
            if (assembly == null)
                return false;

            return true;
        }

        public static DirectoryInfo GetDirectory(DirectoryInfo[] infos, string name)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i].Name == name)
                    return infos[i];
            }

            return null;
        }

        public static FileInfo GetFile(FileInfo[] infos, string name)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i].Name == name)
                    return infos[i];
            }

            return null;
        }
    }
}
