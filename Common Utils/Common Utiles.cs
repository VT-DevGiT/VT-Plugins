using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using System.Linq;

namespace Common_Utiles
{

    [PluginInformation(
Name = "Common Utiles",
Author = "Oka",
Description = "add new functionality and config",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.2.2"
)]
    public class CommonUtiles : AbstractPlugin
    {
        public static CommonUtiles Instance { get; private set; }
        public bool RespawnAllow { get; internal set; }

        [Synapse.Api.Plugin.Config(section = "Common Utiles")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
            //LoadRecipes();
        }

        private void LoadRecipes()
        {
            if (Config.Recipes.Any())
            {
                // When the Recipes was back it was back
                //if (Config.RemouvRecipes)
                //    Map.Get.Scp914.Recipes.Clear();
                //foreach (var recipe in Config.Recipes)
                //{
                //    Map.Get.Scp914.Recipes.Add(recipe.Parse());
                //}
            }
        }
    }
}