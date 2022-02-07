using Synapse.Api.Plugin;

namespace VTLog
{
    [PluginInformation(
    Name = "VT-Log",
    Author = "VT",
    Description = "Log all events",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.0.0.1"
    )]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
            Method.CreateNewTXT();
            new EventHandlers();
        }
    }
}
