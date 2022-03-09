using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;
using VT_Api.Core.Plugin;

namespace VT_Alpha
{

    [PluginInformation(
Author = "VT",
Description = "Add the Alpha team",
LoadPriority = 20,
Name = "VT-Alpha",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.0.0"
)]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        public override void Load()
        {
            base.Load();
            Translation.AddTranslation(new Translation()
            {
                SpawnMessage = "tu es un %RoleName%\\nTon objectif est de rétablire l'odre au nom des O5\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
        }
    }
}
