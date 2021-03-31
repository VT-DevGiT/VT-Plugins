using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.Behaviour;

namespace CustomClass.Pouvoir
{
    class MouveVent : BaseRepeatingBehaviour
    {
        private Player player;
        public int duraction = -1;

        private void Start()
        {
            player = gameObject.GetPlayer();
            player.Invisible = true;
            RegisterEvents();
        }
        protected override void BehaviourAction()
        {
            
            if (duraction > 0)
            {
                player.SendBroadcast(1, PluginClass.PluginTranslation.ActiveTranslation.
                    VentMessage.Replace("%Time%", duraction.ToString()));
                duraction--;
            }
            else if (duraction < 0)
            {
                player.BroadcastMessage(PluginClass.PluginTranslation.ActiveTranslation.NoTimeVentMessage, 1);
            }
            else
                Kill();
        }
        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            if (ev.Player == player)
            {
                ev.Allow = false;
                ev.Player.Position += (ev.Player.gameObject.transform.forward * 1.5f);
            }
        }

        private void OnPickUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == player)
            {
                ev.Allow = false;
            }
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == player)
            {
                ev.Allow = false;
            }
        }

        private void UnRegisterEvents()
        {
            Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUpItem;
            Server.Get.Events.Player.PlayerDamageEvent -= OnDamage;
        }
        private void RegisterEvents()
        {
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUpItem;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }
    }
}
