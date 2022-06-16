using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VTEscape
{

    [PluginInformation(
Author = "VT",
Description = "add a new Escape and config",
LoadPriority = 20,
Name = "VT-Escape",
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.5.3"
)]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config>
    {
        public override bool AutoRegister => false;

        [Synapse.Api.Plugin.Config(section = "VT-Escape")]
        public override Config Config { get; protected set; }


        public event Synapse.Api.Events.EventHandler.OnSynapseEvent<CustomEscapeEventArgs> CustomEscapePostEvent;

        internal void CallCustomEscapeEvent(Player player, Player cuffer, EscapeType escape, string message, bool nuck, int curentRole, int newRole)
        {
            var ev = new CustomEscapeEventArgs()
            {
                Player = player,
                Cuffer = cuffer,
                Escape = escape,
                EscapeMessage = message,
                StartWarHead = nuck,
                CurentRole = curentRole,
                NewRole = newRole,
            };

            CustomEscapePostEvent?.Invoke(ev);
        }
    }
}
