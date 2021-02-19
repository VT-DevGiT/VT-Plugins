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
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.0"
)]
    public class CommonUtiles : AbstractPlugin
    {
        public static CommonUtiles Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "Common Utiles")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            if (Config.Recipes.Any())
            {
                Map.Get.Scp914.Recipes.Clear();
                foreach (var recipe in Config.Recipes)
                {
                    Map.Get.Scp914.Recipes.Add(recipe.Parse());
                }
            }
        }

        public void SetItem(Player player, List<SerializedItemProba> config)
        {
            int nbCree = 0;
            foreach (var item in config)
            {
                if (nbCree < 8)
                {
                    var obj = item.Parse();
                    if (obj != null)
                    {
                        player.Inventory.AddItem(obj);
                        nbCree++;
                    }
                }
            }
        }

    }
}