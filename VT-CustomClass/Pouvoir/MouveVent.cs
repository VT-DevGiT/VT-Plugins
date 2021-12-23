using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.Behaviour;

namespace VTCustomClass.Pouvoir
{
    class MouveVent : BaseRepeatingBehaviour
    {
        private Player player;
        public int duraction = -1;

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            player.Invisible = true;
            RegisterEvents();
            base.Start();
        }

        protected override void OnDisable()
        {
            UnRegisterEvents();
            base.OnDisable();
        }

        protected override void BehaviourAction()
        {
            if (duraction > 0)
            {
                string message = Plugin.PluginTranslation.ActiveTranslation.VentMessage.Replace("%Time%", duraction.ToString());
                player.SendBroadcast(1, message);
                duraction--;
            }
            else if (duraction < 0)
            {
                player.BroadcastMessage(Plugin.PluginTranslation.ActiveTranslation.NoTimeVentMessage, 1);
            }
            else
                Kill();
        }
        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
            if (ev.Player == player && isActiveAndEnabled)
            {
                ev.Allow = false;
                ev.Player.Position += (ev.Player.gameObject.transform.forward * 1.5f);
            }
        }

        private void OnPickUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == player && isActiveAndEnabled)
            {
                ev.Allow = false;
            }
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == player && isActiveAndEnabled)
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
