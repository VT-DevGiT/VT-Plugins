using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;



namespace VTEscape
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
        }

        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<NTFEscape>()?.Kill();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<NTFEscape>();
                }
            }
            if (Plugin.Config.ICEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<CHIEscape>()?.Kill();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<CHIEscape>();
                }
            }
        }
    }
}