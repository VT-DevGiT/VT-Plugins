using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VT_AndersonRobotic
{
    [PluginInformation(
    Name = "VT-AndersonRobotic",
    Author = "VT",
    Description = "Adds the team AndersonRobotic",
    LoadPriority = 20,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.3.3")]
    public class Plugin : VtAbstractPlugin<EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        public override void Load()
        {
            base.Load();
            Translation.AddTranslation(new Translation()
            {
                SpawnMessage = "tu es un membre de <color=yellow>AndersonRobotic</color>\\nTon objectif est de volée toute les donnée des serveurs!\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
        }
    }
}
