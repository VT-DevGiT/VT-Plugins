using VT_Api.Core.Plugin;
using VT_Item.Item;
using VT_Item.Configs;

namespace VT_Item
{
    [Synapse.Api.Plugin.PluginInformation(
        Author = "VT",
        Description = "Add new Cool Item",
        LoadPriority = 100,
        Name = "VT-Item",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        [Synapse.Api.Plugin.Config(section = "VT-Item-BulletproofPlate")]
        public static BulletproofPlateConfig BulletproofPlateConfig;

        [Synapse.Api.Plugin.Config(section = "VT-Item-MiniGun")]
        public static MiniGunConfig MiniGunConfig;

        public override void Load()
        {
            Translation.AddTranslation(new Translation
            {
                MessageGetItem = "Vous avez récupéré un(e) %Name%",
                MessageHandItem = "Vous avez pris un(e) %Name%",
                NameBulletproofPlate = "plaque par balle",
                NameMiniGun = "Mini-Gun"
            }, "FRENCH");
            base.Load();
        }
    }
}
