using MEC;
using Respawning;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VT079
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode ==  UnityEngine.KeyCode.Keypad8)
            { 
                if (ev.Player.RoleID != (int)RoleType.NtfCommander)
                {
                    ev.Player.SendBroadcast(2, "you do not have the accreditation for this order");
                }
                else if (ev.Player.ItemInHand.ID != (int)ItemType.Radio)
                {
                    ev.Player.SendBroadcast(2, "you need a radio !");
                }
                else if (RespawnManager.Singleton.GetFieldValue<float>("_timeForNextSequence") <= 15)
                {
                    ev.Player.SendBroadcast(2, "too close to a respawn");
                }
                else if (!Methode._isAirBombGoing)
                {
                    Timing.RunCoroutine(Methode.AirSupportBomb(7, 5));
                    ev.Player.SendBroadcast(2, "Air Bomb Start");
                }
                else
                {
                    ev.Player.SendBroadcast(2, "there is already a bombardment");
                }
            }
        }

        private void OnSpeak(PlayerSpeakEventArgs ev)
        {
            if (ev.RadioTalk)
            {

            }
        }
    }
}