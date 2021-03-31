using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace VT_HammerDown
{

    [PluginInformation(
Author = "VT",
Description = "add a new Escape and config",
LoadPriority = 0,
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
                SpawnMessageCadet = "tu es un membre de <color=blue>HammerDown</color>\\nTon objectif est de stioper toute les intrus et tuée les SCP\\n<b>Press esc pour fermé</b>",
                SpawnMessageLieutenant = "tu es un membre de <color=blue>HammerDown</color>\\nTon objectif est de stioper toute les intrus et tuée les SCP\\n<b>Press esc pour fermé</b>",
                SpawnMessageCommandant = "tu es un membre de <color=blue>HammerDown</color>\\nTon objectif est de stioper toute les intrus et tuée les SCP\\n<b>Press esc pour fermé</b>",

            }, "FRENCH");
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
