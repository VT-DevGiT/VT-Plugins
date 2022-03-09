using Synapse;
using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

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
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        public override void Load()
        {
            base.Load();
            Translation.AddTranslation(new Translation()
            {
                SpawnMessage = "tu es un membre des <color=blue>HammerDown</color>. tu est %RoleName%\\nTon objectif est de stoper toute les intrus et de tuée les SCP 939\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
        }
    }
}
