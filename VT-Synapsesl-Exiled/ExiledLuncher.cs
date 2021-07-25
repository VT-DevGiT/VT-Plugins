using HarmonyLib;
using NorthwoodLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace VT_Synapsesl_Exiled
{
    [PluginInformation(
    Name = "VT-Exiled",
    Author = "Oka",
    Description = "Run Exiled on Synapsesl",
    LoadPriority = -100,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.0.0.1"
    )]
    class ExiledLuncher : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "VT-LoaderExiled")]
        public static Config Config;

        public static ExiledLuncher Instance { get; private set; }
        public static bool IsLoaded { get; private set; }


        public void PatchAll()
        {
            var instance = new Harmony("VT_Synapsesl_Exiled");
            instance.PatchAll();
        }

        public override void Load()
        {
            Instance = this;
            if (Config.Warning)
            {
                string message = @"
Warning ! 
All Exiled plugins are not necessarily stable and compatible for Synapse.
The plugin only loaded Exiled, it is possible that he has incompatibilities.
To use this plugin you must also install Exiled and not replaced the Sharp assembly
";
                Logger.Get.Send(message, ConsoleColor.Red);
                Thread.Sleep(5000);
                Server.Get.Configs.UpdateSection<Config>("VT-LoaderExiled", new Config { Warning = false});
            }
            try
            {
                if (IsLoaded)
                {
                    throw new Exception("Exiled has already been loaded!");
                }

           
                Logger.Get.Send("[Exiled.Bootstrap] Exiled is loading...", ConsoleColor.DarkRed);

                string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED");
                string dependenciesPath = Path.Combine(rootPath, "Plugins", "dependencies");

                if (Environment.CurrentDirectory.Contains("testing", StringComparison.OrdinalIgnoreCase))
                    rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED-Testing");

                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);

                if (File.Exists(Path.Combine(rootPath, "Exiled.Loader.dll")))
                {
                    if (File.Exists(Path.Combine(dependenciesPath, "Exiled.API.dll")))
                    {
                        if (File.Exists(Path.Combine(dependenciesPath, "YamlDotNet.dll")))
                        {
                            Assembly.Load(File.ReadAllBytes(Path.Combine(rootPath, "Exiled.Loader.dll")))
                                .GetType("Exiled.Loader.Loader")
                                .GetMethod("Run")
                                ?.Invoke(
                                    null,
                                    new object[]
                                    {
                                        new Assembly[]
                                        {
                                            Assembly.Load(File.ReadAllBytes(Path.Combine(dependenciesPath, "Exiled.API.dll"))),
                                            Assembly.Load(File.ReadAllBytes(Path.Combine(dependenciesPath, "YamlDotNet.dll"))),
                                        },
                                    });

                            IsLoaded = true;
                        }
                        else
                        {
                            throw new Exception("YamlDotNet.dll was not found, Exiled won't be loaded!");
                        }
                    }
                    else
                    {
                        throw new Exception("Exiled.API.dll was not found, Exiled won't be loaded!");
                    }
                }
                else
                {
                    throw new Exception("Exiled.Loader.dll was not found, Exiled won't be loaded!");
                }

                Logger.Get.Send("Exiled loaded...", ConsoleColor.Green);
                PatchAll();
                Logger.Get.Send("Exiled patch for synapse...", ConsoleColor.Green);
            }
            catch (Exception exception)
            {
                Logger.Get.Send($"[Exiled.Bootstrap] Exiled loading error: {exception}", ConsoleColor.DarkRed);
                return;
            }
            Logger.Get.Send("Exiled runing !", ConsoleColor.Green);

            base.Load();
        }
    }
}
