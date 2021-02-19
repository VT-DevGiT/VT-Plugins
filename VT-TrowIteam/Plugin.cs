using Synapse.Api.Plugin;

namespace VTTrowItem
{
    [PluginInformation(
Author = "VT",
Description = "allows you to throw item with key.",
LoadPriority = 0,
Name = "VT-TrowIteam",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.1"
)]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "BalanceIteam")]
        public static Config Config;


        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
