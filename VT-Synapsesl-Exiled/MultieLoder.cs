using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using VT_MultieLoder.Exiled;
using VT_MultieLoder.Qurre;

namespace VT_MultieLoder
{
    [PluginInformation(
    Name = "VT-MultiLoder",
    Author = "Oka",
    Description = "Run other plugin loder on Synapsesl",
    LoadPriority = -100,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.0.2.0"
    )]
    class MultieLoder : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "VT-MultieLoder")]
        public static Config Config;
        public static MultieLoder Instance { get; private set; }
        public static bool IsLoaded { get; private set; }
        public static ExiledHandler ExiledHandler { get; private set; }
        public static List<Assembly> ExiledPrimaryDll { get; private set; }
        public static QurreHandler QurreHandler { get; private set; }
        public static List<Assembly> QurrePrimaryDll { get; private set; }
        public static FileLocations Files { get; } = new FileLocations();

        public void PatchAll()
        {
            var instance = new Harmony("VT_Synapsesl_Exiled");
            instance.PatchAll();
        }

        public override void Load()
        {
            ExiledPrimaryDll = new List<Assembly>();
            QurrePrimaryDll = new List<Assembly>();
            Instance = this;
            //if (Config.Warning)
            {
                string message = $@"Loading...
____________________________________________________________________________Loader___________________________________________________________________________
Warning !
Don't use this plugin if you are not a developpeur of the team (this plugin is currently in development).
 _______ _     _        _______ _____ _______      |       _____  _______ ______  _______  ______
 |  |  | |     | |         |      |   |______      |      |     | |_____| |     \ |______ |_____/
 |  |  | |_____| |_____    |    __|__ |______      |_____ |_____| |     | |_____/ |______ |    \_

Le syteme changera régulerement. contactée Warkis si vous avez des question et passée en vocale sur le quartier générale des petits serveur...
Les tests se feront en locale.

Exiled enabled : {Config.EnabledExiled}
Querre enabled : {Config.EnabledQurre}
____________________________________________________________________________Loader___________________________________________________________________________
";
                Logger.Get.Send(message, ConsoleColor.Red);
                Thread.Sleep(5000);
                Server.Get.Configs.UpdateSection("VT-MultieLoder", new Config { Warning = false});
            }

            if (Config.EnabledExiled)
            { 
                foreach (string file in Directory.GetFiles(Files.ExiledDirectory, "*.dll"))
                    ExiledPrimaryDll.Add(Assembly.Load(File.ReadAllBytes(file)));
                
                ExiledHandler = new ExiledHandler();
            }
            if (Config.EnabledQurre)
            { 
                foreach (string file in Directory.GetFiles(Files.QuerryDirectory, "*.dll"))
                    QurrePrimaryDll.Add(Assembly.Load(File.ReadAllBytes(file)));
           
                QurreHandler = new QurreHandler();
            }


            PatchAll();
            base.Load();
        }

        public class FileLocations
        {
            internal FileLocations() => Refresh();

            #region var
            private string _multiLoderDirectory;
            
            private string _ExiledDirectory;
            private string _ExiledPluginDirectory;
            private string _ExiledConfigsFile;
            private string _ExiledConfigDirectory;
            private string _ExiledTranslationsFile;

            private string _QuerryDirectory;
            private string _QuerryPluginDirectory;
            private string _ExiledLogDirectory;

            #endregion

            #region prop

            public string MultiLoderDirectory
            {
                get
                {
                    if (!Directory.Exists(_multiLoderDirectory))
                        Directory.CreateDirectory(_multiLoderDirectory);

                    return _multiLoderDirectory;
                }
                private set => _multiLoderDirectory = value;
            }


            //Exiled
            public string ExiledDirectory
            {
                get
                {
                    if (!Directory.Exists(_ExiledDirectory))
                        Directory.CreateDirectory(_ExiledDirectory);

                    return _ExiledDirectory;
                }
                private set => _ExiledDirectory = value;
            }

            public string ExiledPluginDirectory
            {
                get
                {
                    if (!Directory.Exists(_ExiledPluginDirectory))
                        Directory.CreateDirectory(_ExiledPluginDirectory);

                    return _ExiledPluginDirectory;
                }
                private set => _ExiledPluginDirectory = value;
            }

            public string ExiledConfigsFile
            {
                get
                {
                    if (!File.Exists(_ExiledConfigsFile))
                        File.Create(_ExiledConfigsFile);

                    return _ExiledConfigsFile;
                }
                private set => _ExiledConfigsFile = value;
            }

            public string ExiledConfigDirectory
            {
                get
                {
                    if (!Directory.Exists(_ExiledConfigDirectory))
                        Directory.CreateDirectory(_ExiledConfigDirectory);

                    return _ExiledConfigDirectory;
                }
                private set => _ExiledConfigDirectory = value;
            }

            public string ExiledTranslationsFile
            {
                get
                {
                    if (!File.Exists(_ExiledTranslationsFile))
                        File.Create(_ExiledTranslationsFile);

                    return _ExiledTranslationsFile;
                }
                private set => _ExiledTranslationsFile = value;
            }

            public string ExiledLogFile
            {
                get
                {
                    Server.Get.Logger.Info(_ExiledLogDirectory);
                    Server.Get.Logger.Info(!File.Exists(_ExiledLogDirectory));

                    if (!File.Exists(_ExiledLogDirectory))
                        File.Create(_ExiledLogDirectory);

                    return _ExiledLogDirectory;
                }
                private set => _ExiledLogDirectory = value;
            }
            
            //Querry
            public string QuerryDirectory
            {
                get
                {
                    if (!Directory.Exists(_QuerryDirectory))
                        Directory.CreateDirectory(_QuerryDirectory);

                    return _QuerryDirectory;
                }
                private set => _QuerryDirectory = value;
            }

            public string QuerryPluginDirectory
            {
                get
                {
                    if (!Directory.Exists(_QuerryPluginDirectory))
                        Directory.CreateDirectory(_QuerryPluginDirectory);

                    return _QuerryPluginDirectory;
                }
                private set => _QuerryPluginDirectory = value;
            }
            #endregion

            public void Refresh()
            {
                MultiLoderDirectory = Path.Combine(Server.Get.Files.SynapseDirectory, "loders");

                ExiledDirectory = Path.Combine(MultiLoderDirectory, "Exiled");
                ExiledPluginDirectory = Path.Combine(ExiledDirectory, "Plugins");
                ExiledConfigDirectory = Path.Combine(ExiledDirectory, "Configs");
                ExiledLogFile = Path.Combine(ExiledDirectory, $"{Server.Get.Port}-RemoteAdminLog.txt");
                ExiledConfigsFile = Path.Combine(ExiledConfigDirectory, $"{Server.Get.Port}-config.yml");
                ExiledTranslationsFile = Path.Combine(ExiledConfigDirectory, $"{Server.Get.Port}-translations.yml");

                QuerryDirectory = Path.Combine(MultiLoderDirectory, "Querry");
                QuerryPluginDirectory = Path.Combine(QuerryDirectory, "Plugins");
            }
        }
    }
}
