using MEC;
using Respawning;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTRadio
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
                if (ev.Player.RoleID != (int)RoleID.NtfCommander && ev.Player.RoleID != (int)RoleID.CdmCommander 
                    && ev.Player.RoleID != (int)RoleID.NtfCommander && ev.Player.RoleID != (int)RoleID.NtfLieutenantColonel)
                {
                    ev.Player.SendBroadcast(2, "you do not have the accreditation for this order");
                }
                else if (ev.Player.ItemInHand.ID != (int)ItemType.Radio)
                {
                    ev.Player.SendBroadcast(2, "you need a radio !");
                }
                else if (RespawnManager.Singleton.GetFieldValueorOrPerties<float>("_timeForNextSequence") <= 15)
                {
                    ev.Player.SendBroadcast(2, "too close to a respawn");
                }
                else if (!Methods.isAirBombCurrently)
                {
                    Timing.RunCoroutine(Methods.AirBomb(7, 5));
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
        
        }
    }
}