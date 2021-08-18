using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VT_Referance.Method;

using AbstractPlugin = Qurre.Plugin;
using PluginManager = Qurre.PluginManager;

namespace VT_MultieLoder.Qurre
{
    public class QurreHandler
    {
        List<AbstractPlugin> Plugins { get { return PluginManager.plugins; } }

        internal QurreHandler()
        {
            typeof(PluginManager).SetProperty<string>("QurreDirectory", MultieLoder.Files.QuerryDirectory);
            typeof(PluginManager).SetProperty<string>("PluginsDirectory", MultieLoder.Files.ExiledPluginDirectory);
            typeof(PluginManager).SetProperty<string>("ConfigsDirectory", MultieLoder.Files.ConfigsDirectory);


            loadPlugin();
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
                            if (PluginManager.Version < instance.NeededQurreVersion)
                                Synapse.Server.Get.Logger.Error($"TKT c'est pas toi c'est le plugin {pluginpath} qui n'est pas à la bonne vection \n mais t'est con pour quoi tu l'utilise ???");
                            else
                            {
                                PluginManager.plugins.Add(instance);
                                Synapse.Server.Get.Logger.Info($"Fait pas attenction c'est {instance.Name}, un plugin qui est loader...");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Synapse.Server.Get.Logger.Error($"Fait pas attenction c'est un plugin qui c'est pas loader ... \n t'est con fix sa ! le plugin c'est {pluginpath}... Boufon vas !\n{e}");
                }
            }
            typeof(PluginManager).CallMethod("DownloadPlugins");
            foreach (AbstractPlugin plugin in PluginManager.plugins.OrderByDescending(o => o.Priority))
            {
                try
                {
                    plugin.Enable();
                    Synapse.Api.Logger.Get.Info($"Le Plugin {plugin.Name} {plugin.Version} de {plugin.Developer} à réussie a se lancée !!! braveau à toi.");
                }
                catch (Exception e)
                {
                    Synapse.Api.Logger.Get.Error($"Le plugin {plugin.Name} n'arrive pas à se lancée !!! je crois que c'est pas toi pour une fois .... Ou pas :( \n{e}");
                }
            }
            Synapse.Api.Logger.Get.Info("C'est bon. C'est lancée... Mais te crois pas être un génie ! \n :P");
        }
    }
}
