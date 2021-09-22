using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VT_Referance.Method;
using AbstractPlugin = Exiled.API.Interfaces.IPlugin<Exiled.API.Interfaces.IConfig>;
using Loader = Exiled.Loader.Loader;
using Paths = Exiled.API.Features.Paths;

namespace VT_MultieLoder.Exiled
{
    public class ExiledHandler
    {
        public Event.EventHandler @eventExiled { get; set; }

        internal ExiledHandler()
        {
            int i = 0; Synapse.Server.Get.Logger.Send($"log : {i++}", ConsoleColor.Yellow);//0
 
            SetExiledPaths();
            PatchExiled(false);

            typeof(Loader).CallMethod("LoadDependencies");
            Loader.LoadPlugins();

            global::Exiled.Loader.ConfigManager.Reload();
            global::Exiled.Loader.TranslationManager.Reload();

            @eventExiled = new Event.EventHandler();
            PatchExiled(true);

            foreach (Assembly dll in MultieLoder.ExiledPrimaryDll)
            {
                var plugin = Loader.CreatePlugin(dll);
                if (plugin != null) Loader.Plugins.Add(plugin);
            }
            Loader.EnablePlugins();

            ServerConsole.ReloadServerName();

            Synapse.Server.Get.Logger.Info($"Stating...{GetAskiArt()}");
        }

        public string GetAskiArt() => @"
   ▄████████ ▀████    ▐████▀  ▄█   ▄█          ▄████████ ████████▄
  ███    ███   ███▌   ████▀  ███  ███         ███    ███ ███   ▀███
  ███    █▀     ███  ▐███    ███▌ ███         ███    █▀  ███    ███
 ▄███▄▄▄        ▀███▄███▀    ███▌ ███        ▄███▄▄▄     ███    ███
▀▀███▀▀▀        ████▀██▄     ███▌ ███       ▀▀███▀▀▀     ███    ███
  ███    █▄    ▐███  ▀███    ███  ███         ███    █▄  ███    ███
  ███    ███  ▄███     ███▄  ███  ███▌    ▄   ███    ███ ███   ▄███
  ██████████ ████       ███▄ █▀   █████▄▄██   ██████████ ████████▀";

        public void SetExiledPaths()
        {
            Paths.Exiled = MultieLoder.Files.ExiledDirectory;
            Paths.Plugins = MultieLoder.Files.ExiledPluginDirectory;
            Paths.Configs = MultieLoder.Files.ExiledConfigDirectory;
            Paths.Config = MultieLoder.Files.ExiledConfigsFile;
            Paths.Translations = MultieLoder.Files.ExiledTranslationsFile;
            Paths.Log = MultieLoder.Files.ExiledLogFile;
            Paths.Dependencies = Synapse.Server.Get.Files.ModuleDirectory;
        }

        public void PatchExiled(bool Event)
        {
            if (Event)
            {
                var instance = new Harmony("VT_MultieLoder.Exiled.Event");
                instance.PatchAll(); 
            }
            else
            {
                var instance = new Harmony("VT_MultieLoder.Exiled");
                instance.PatchAll();
            }
        }

        static public void loadPluginsAndEnable(string path)
        {
            var files = Directory.GetFiles(path, "*.dll");
            foreach (string pluginpath in files)
            {
                try
                {
                    foreach (Type type in Assembly.Load(File.ReadAllBytes(pluginpath)).GetTypes().Where(type => !type.IsAbstract && !type.IsInterface))
                    {
                        if (typeof(AbstractPlugin).IsAssignableFrom(type))
                        {
                            AbstractPlugin instance = (AbstractPlugin)Activator.CreateInstance(type);
                            if (!(bool)typeof(Loader).CallMethod("CheckPluginRequiredExiledVersion", instance))
                                Loader.Plugins.Add(instance);
                        }
                    }
                }
                catch (Exception e)
                {
                    Synapse.Server.Get.Logger.Error($"Error while initializing plugin {pluginpath}. {e}");
                }
            }
            foreach (AbstractPlugin plugin in Loader.Plugins)
            {
                try
                {
                    if (plugin.Config.IsEnabled)
                    {
                        plugin.OnEnabled();
                        plugin.OnRegisteringCommands();
                    }
                }
                catch (Exception e)
                {
                    Synapse.Server.Get.Logger.Error($"Plugin \"{plugin.Name}\" threw an exception while enabling: {e}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(Paths), nameof(Paths.Reload))]
    internal static class KillExiledPathsPatch
    {
        [HarmonyPrefix]
        private static bool KillExiledPathsRefresh()
        {
            try
            {
                Synapse.Server.Get.Logger.Info("Refresh");
                MultieLoder.Files.Refresh();
                Synapse.Server.Get.Logger.Info("SetExiledPaths");
                MultieLoder.ExiledHandler.SetExiledPaths();
            }
            catch(ArgumentException e)
            {
                Synapse.Server.Get.Logger.Error($"Vt-MultiLoader : KillExiledPathsRefresh failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(global::Exiled.API.Features.Log), nameof(global::Exiled.API.Features.Log.SendRaw))]
    internal static class LogPatch
    {
        [HarmonyPrefix]
        private static bool BridgeLogExiledSynapse(object message, ConsoleColor color)
        {
            Synapse.Server.Get.Logger.Send(message.ToString(), color);
            return false;
        }
    }
}
