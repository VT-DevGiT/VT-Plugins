using Synapse.Api.Plugin;

namespace AdvencedKeycard
{
    [PluginInformation(
    Name = "VT-Keycard",
    Author = "VT",
    Description = "Mort Door option and new Door !",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.1.0"
    )]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public float Scp939SeeingAhpAmount { get; internal set; }

        [Synapse.Api.Plugin.Config(section = "VT-KeyCard")]
        public static Config Config;


        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
