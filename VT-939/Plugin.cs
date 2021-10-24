using Synapse.Api.Plugin;

namespace VT939
{
    [PluginInformation(
Name = "VT-939",
Author = "VT",
Description = "Better939 for SynapseSl",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.2.1"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public float Scp939SeeingAhpAmount { get; internal set; }

        [Synapse.Api.Plugin.Config(section = "VT-939")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
