using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VT_Referance.Method;

using Loader = Exiled.Loader.Loader;
using AbstractPlugin = Exiled.API.Interfaces.IPlugin<Exiled.API.Interfaces.IConfig>;
using Paths = Exiled.API.Features.Paths;

namespace VT_MultieLoder.Exiled
{
    public class ExiledHandler
    {
        List<AbstractPlugin> Plugins { get { return Loader.Plugins.ToList(); } }

        internal ExiledHandler()
        {
            Paths.Exiled = MultieLoder.Files.ExiledPluginDirectory;
            Paths.Plugins = MultieLoder.Files.ExiledPluginDirectory;
            Paths.Configs = MultieLoder.Files.ConfigsDirectory;


            loadPlugin();
            new EventHandler();

        }

        private void loadPlugin()
        {
            List<string> paths = Directory.GetFiles(MultieLoder.Files.QuerryPluginDirectory, "*.dll").ToList();
            foreach (string pluginpath in paths)
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
}
