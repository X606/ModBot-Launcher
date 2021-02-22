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
        public static void InstallModBot(string managedFolderPath, Action<float> updateInjectorProgress)
        {
            updateInjectorProgress(0f);

            string assemblyPath = managedFolderPath + "Assembly-CSharp.dll";
            string injectionClassesPath = managedFolderPath + "InjectionClasses.dll";
            string modLibraryPath = managedFolderPath + "ModLibrary.dll";

            Console.WriteLine("Starting the installation of Mod-Bot...");
            if (!File.Exists(injectionClassesPath))
            {
                ErrorHandler.Crash("Could not find dll at path \"" + injectionClassesPath + "\"");
                return;
            }
            Console.WriteLine("Finding classes to inject...");
            ModuleDefinition module = ModuleDefinition.ReadModule(injectionClassesPath, new ReaderParameters()
            {
                ReadWrite = true
            });

            updateInjectorProgress(0.2f);

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

            updateInjectorProgress(0.4f);

            // Just to make the code a bit clearer, this is how much progress that should be added to the bar during the course of the for-loop
            const float PERCENTAGE_TO_NEXT_STEP = 0.4f;

            for (int i = 0; i < typesFullNames.Count; i++)
            {
                float startPercentage = 0.4f + (PERCENTAGE_TO_NEXT_STEP * (i / (float)typesFullNames.Count));
                float endPercentage = startPercentage + (PERCENTAGE_TO_NEXT_STEP / typesFullNames.Count);

                updateInjectorProgress(startPercentage);

                Injector.AddClassToAssembly(assemblyPath, typesNames[i], injectionClassesPath, typesFullNames[i], delegate (float progress)
                {
                    // progress is a value between 0.0f and 1.0f, we basically just want to lerp between startPercentage and endPercentage
                    updateInjectorProgress(Utils.Lerp(startPercentage, endPercentage, progress));
                });

                updateInjectorProgress(endPercentage);
            }
            Console.WriteLine("Finished injecting classes");

            Console.WriteLine("Injecting method calls...");

            Console.WriteLine("Injecting into GameFlowManager.Start...");
            Injector.AddCallToMethodInMethod(assemblyPath, "GameFlowManager", "Start", modLibraryPath, "InternalModBot.StartupManager", "OnStartUp").Write();

            updateInjectorProgress(0.8f);

            Console.WriteLine("Injecting into Resources.Load<T>...");
            Injection Resources_LoadT_Injection = Injector.AddCallToMethodInMethod(managedFolderPath + "/UnityEngine.CoreModule.dll", "UnityEngine.Resources", "Load", modLibraryPath, "InternalModBot.CalledFromInjections", "FromResourcesLoad", 0, false, false, null, true);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ret);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ldloc_0);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Brfalse_S, 4, 0, true);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Ldloc_0);
            Resources_LoadT_Injection.AddInstructionUnderSafe(OpCodes.Stloc_0);
            Resources_LoadT_Injection.AddInstructionOverSafe(OpCodes.Ldarg_0);
            Resources_LoadT_Injection.Write();

            updateInjectorProgress(1f);
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
