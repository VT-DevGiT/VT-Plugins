using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;
using VT_Api.Core.Plugin;

namespace VT_U2I
{

    [PluginInformation(
    Author = "VT",
    Description = "Add the U2I team",
    LoadPriority = 20,
    Name = "VT-U2I",
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.0.0"
    )]
    public class Plugin : VtAbstractPlugin<EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        public override void Load()
        {
            Translation.AddTranslation(new Translation()
            {
                SpawnMessage = "tu es un membre de <color=blue>U2I %RoleName%</color>\\nTon objectif est de stoper toute les intrus et tuée les SCP\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
            base.Load();
        }
    }
}
