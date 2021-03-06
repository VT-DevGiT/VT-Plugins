using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Referance.Method;

namespace VTProget_X
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.TriggerTeslaEvent += OnTriggerTeslaEvent;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.KeyCode == UnityEngine.KeyCode.Keypad4)
            {
                if (Methods.Voltage() >= 1000 && ev.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
                    && ev.Player.Room.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
                {
                    Timing.RunCoroutine(Methode.Decontamination(), "Decont");
                }

            }
            else if (ev.KeyCode == UnityEngine.KeyCode.Keypad5)
            {
                if (Methods.Voltage() >= 2000 && ev.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
                    && ev.Player.Room.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
                {
                    if (Plugin.Instance.TeslaEnabled)
                    {
                        Map.Get.Cassie("all tesla doors have been deactivate .", false);
                    }
                    else
                    {
                        Map.Get.Cassie("all tesla doors have been enable .", false);
                    }
                    Plugin.Instance.TeslaEnabled = !Plugin.Instance.TeslaEnabled;
                }
            }
            else if (ev.KeyCode == UnityEngine.KeyCode.Keypad6)
            {
                if (Methods.Voltage() >= 3000 && ev.Player?.ItemInHand?.ItemType == ItemType.WeaponManagerTablet
                    && ev.Player.Room.RoomType == RoomInformation.RoomType.EZ_INTERCOM)
                {
                    Generator079.mainGenerator.ServerOvercharge(Plugin.Config.BlackOutTime, false);
                }
            }
        }

        private void OnRoundEnd()
        {
            Plugin.Instance.TeslaEnabled = true;
            Plugin.Instance.DeconatmiantinEnd = false;
            Plugin.Instance.DeconatmiantionendProgress = false;
            Timing.KillCoroutines("Decont");
        }

        private void OnRoundStart()
        {
            Server.Get.Host.GetComponent<Intercom>().gameObject.AddComponent<IntercomBehaviour>();
        }

        private void OnTriggerTeslaEvent(TriggerTeslaEventArgs ev)
        {
            if (ev.Player.Inventory.Items.FirstOrDefault(p => p.ID == (int)ItemType.WeaponManagerTablet) != null || !Plugin.Instance.TeslaEnabled)
                ev.Trigger = false;
            else
                ev.Trigger = true;
        }
    }

}
