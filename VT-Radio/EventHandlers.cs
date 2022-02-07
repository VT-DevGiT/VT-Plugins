using Respawning;
using Synapse;
using Synapse.Api.Events.SynapseEventArguments;

namespace VTRadio
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode ==  UnityEngine.KeyCode.Alpha9)
            { 
                if (ev.Player.RoleType != RoleType.NtfCaptain)
                {
                    ev.Player.SendBroadcast(2, "you do not have the accreditation for this order");
                }
                else if (ev.Player.ItemInHand.ID != (int)ItemType.Radio)
                {
                    ev.Player.SendBroadcast(2, "you need a radio !");
                }
                else if (RespawnManager.Singleton._timeForNextSequence <= 15)
                {
                    ev.Player.SendBroadcast(2, "too close to a respawn");
                }
                else if (!VtController.Get.MapAction.isAirBombCurrently)
                {
                    VtController.Get.MapAction.StartAirBombardement(7, 5);
                    ev.Player.SendBroadcast(2, "Air Bomb Start");
                }
                else
                {
                    ev.Player.SendBroadcast(2, "there is already a bombardment");
                }
            }
        }
    }
}