using Synapse.Api.Plugin;
using System.Collections.Generic;
using VT_Api.Core.Plugin;

namespace VTGrenad
{
    [PluginInformation(
        Author = "VT",
        Description = "Allows you to activate grenades remotely",
        LoadPriority = 5,
        Name = "VT-Grenade",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.3.2"
        )]
    public class Plugin : VtAbstractPlugin<EventHandlers, Config>
    {
        public override bool AutoRegister => false;

        public static Dictionary<int, List<AmorcableGrenade>> DictRadioGrenads = new Dictionary<int, List<AmorcableGrenade>>();

        public override void ReloadConfigs()
        {
            EventHandler.RealoadEventConfig();
            base.ReloadConfigs();
        }
    }
}
