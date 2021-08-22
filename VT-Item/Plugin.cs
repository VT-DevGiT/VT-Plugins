using Synapse.Api.Plugin;
using Synapse.Translation;
using VT_Item.Config;
using VT_Item.Item;

namespace VT_Item
{
    [PluginInformation(
        Author = "VT",
        Description = "Add new Cool Item",
        LoadPriority = 100,
        Name = "VT-Item",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-Item-Config")]
        public static PluginConfig PluginConfig;

        [Synapse.Api.Plugin.Config(section = "VT-Item-BulletproofPlate")]
        public static BulletproofPlateConfig BulletproofPlateConfig;

        [Synapse.Api.Plugin.Config(section = "VT-Item-MiniGun")]
        public static MiniGunConfig MiniGunConfig;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation;

        public override void Load()
        {
            Instance = this;
            new EventHandlers();
            base.Load();
            LoadItem();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation
            {
                MessageGetItem = "Vous avez récupéré un(e) %Name%",
                MessageHandItem = "Vous avez pris un(e) %Name%",
                NameBulletproofPlate = "plaque par balle",
                NameMiniGun = "Mini-Gun"
            }, "FRENCH");
        }

        private void LoadItem()
        {
            new MiniGunScript();
            new BulletproofPlateScript();
        }
    }
}
