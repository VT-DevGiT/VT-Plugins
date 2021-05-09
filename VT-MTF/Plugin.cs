using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace VT_HammerDown
{

    [PluginInformation(
Author = "VT",
Description = "Add the HammerDiwn MTF Team",
LoadPriority = 20,
Name = "VT-HammerDown",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.3"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-HammerDown")]
        public static Config Config;

        [Synapse.Api.Plugin.Config(section = "VT-HammerDownCadet")]
        public static ConfigHammerDownCadet ConfigHammerDownCadet;

        [Synapse.Api.Plugin.Config(section = "VT-HammerDownLieutenant")]
        public static ConfigHammerDownLieutenant ConfigHammerDownLieutenant;

        [Synapse.Api.Plugin.Config(section = "VT-HammerDownCommandant")]
        public static ConfigHammerDownCommandant ConfigHammerDownCommandant;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation { get; set; }
        public override void Load()
        {
            Server.Get.TeamManager.RegisterTeam<HammerDownTeam>();
            Server.Get.RoleManager.RegisterCustomRole<HammerDownCadet>();
            Server.Get.RoleManager.RegisterCustomRole<HammerDownLieutenant>();
            Server.Get.RoleManager.RegisterCustomRole<HammerDownCommandant>();
            PluginTranslation.AddTranslation(new PluginTranslation());
            PluginTranslation.AddTranslation(new PluginTranslation()
            {
                SpawnMessage = "tu es un membre des <color=blue>HammerDown</color>. tu est %RoleName%\\nTon objectif est de stoper toute les intrus et de tuée les SCP 939\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
