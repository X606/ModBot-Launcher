using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ModBotInstaller.Injector
{
    public static class ModBotInjector
    {
        public static void InstallModBot(string installPath, string sourceToCopyClassesFrom, string modLibraryPath, string baseManagedPath)
        {
            Console.WriteLine("Starting the installation of Mod-Bot...");
            if (!File.Exists(sourceToCopyClassesFrom))
            {
                ErrorHandler.Crash("Could not find dll at path \"" + sourceToCopyClassesFrom + "\"");
                return;
            }
            Console.WriteLine("Finding classes to inject...");
            ModuleDefinition module = ModuleDefinition.ReadModule(sourceToCopyClassesFrom, new ReaderParameters()
            {
                ReadWrite = true
            });

            List<TypeDefinition> types = module.GetTypes().ToList();
            List<string> typesFullNames = new List<string>();
            List<string> typesNames = new List<string>();
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].Namespace != "InjectionClasses")
                    continue;

                typesFullNames.Add(types[i].FullName);
                typesNames.Add(types[i].Name);
            }
            module.Dispose();
            Console.WriteLine("Found all classes to inject.");

            Console.WriteLine("Injecting classes...");

            for (int i = 0; i < typesFullNames.Count; i++)
            {
                Injector.AddClassToAssembly(installPath, typesNames[i], sourceToCopyClassesFrom, typesFullNames[i]);
            }
            Console.WriteLine("Finished injecting classes");

            /*
            Console.WriteLine("Injecting new methods...");

            Console.WriteLine("Injecting GetPositionForAIToAimAt into MortarWalker");
            Injector.AddMethodToClass(installPath, "MortarWalker", "GetPositionForAIToAimAt", sourceToCopyClassesFrom, "FixSpidertrons", "GetPositionForAIToAimAt");
            Console.WriteLine("Injected!");

            Console.WriteLine("Done injecting new methods!");
            */

            Console.WriteLine("Injecting method calls...");

            Console.WriteLine("Injecting into GameFlowManager.Start...");
            Injector.AddCallToMethodInMethod(installPath, "GameFlowManager", "Start", modLibraryPath, "InternalModBot.StartupManager", "OnStartUp").Write();

            Console.WriteLine("Injecting into Resources.Load<T>...");
            Injection Resources_LoadT_Injection = Injector.AddCallToMethodInMethod(baseManagedPath + "/UnityEngine.CoreModule.dll", "UnityEngine.Resources", "Load", modLibraryPath, "InternalModBot.CalledFromInjections", "FromResourcesLoad", 0, false, false, null, true);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ret);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ldloc_0);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Brfalse_S, 4, 0, true);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ldloc_0);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Stloc_0);
            Resources_LoadT_Injection.AddInstructionOverSafe(OpCodes.Ldarg_0);
            Resources_LoadT_Injection.Write();
        }
    }


    public static class ErrorHandler
    {
        public static void Crash(string errorMessage)
        {
            MessageBox.Show("ERRROR: " + errorMessage, "Error");

            MessageBox.Show("WARNING: Your game files may have been corrupted, please verify your game files before starting the game!!!");
            Application.Exit();
        }
    }
}
