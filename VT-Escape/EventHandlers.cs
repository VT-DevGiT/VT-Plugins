using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using UnityEngine;

namespace VTEscape
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
            Server.Get.Events.Player.PlayerEscapesEvent += OnEscapesEvent;
            if (Plugin.Config.ShelterIsEnabled)
                Server.Get.Events.Round.WaitingForPlayersEvent += OnWaiting;
        }

        private void OnWaiting()
        {
            GameObject.Find("Nodoor");
        }

        private void OnEscapesEvent(PlayerEscapeEventArgs ev)
        {
            if(Plugin.Config.MTFEscapeIsEnabled)
                ev.Allow = false;
        }

        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() != null)
                    ev.Player.gameObject.GetComponent<NTFEscape>()?.Kill();
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() == null)
                    ev.Player.gameObject.AddComponent<NTFEscape>();
            }
            if (Plugin.Config.ICEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() != null)
                    ev.Player.gameObject.GetComponent<CHIEscape>()?.Kill();
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() == null)
                    ev.Player.gameObject.AddComponent<CHIEscape>();
            }
        }
    }
}