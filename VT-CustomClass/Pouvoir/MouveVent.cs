using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Core.Behaviour;

namespace VTCustomClass.Pouvoir
{
    class MouveVent : RepeatingBehaviour
    {
        private Player player;
        public int duraction = -1;
        public bool cantAtackAndKeepInviblity = false;

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            player.Invisible = true;
            RegisterEvents();
            base.Start();
        }

        protected override void BehaviourAction()
        {
            if (duraction > 0)
            {
                string message = Plugin.Instance.Translation.GetForPlayer(Player).VentMessage.Replace("%Time%", duraction.ToString());
                player.SendBroadcast(1, message);
                duraction--;
            }
            else if (duraction < 0)
            {
                player.BroadcastMessage(Plugin.Instance.Translation.GetForPlayer(Player).NoTimeVentMessage, 1);
            }
            else Kill();
        }

        public override void Kill()
        {
            player.Invisible = false;
            base.Kill();
        }

        private static void OnDoorInteract(DoorInteractEventArgs ev)
        {
            if (ev.Player.GetComponent<RepeatingBehaviour>()?.isActiveAndEnabled ?? false)
                ev.Allow = false;
                ev.Player.Position += (ev.Player.gameObject.transform.forward * 1.5f);
        }

        private static void OnPickUpItem(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player.GetComponent<RepeatingBehaviour>()?.isActiveAndEnabled ?? false)
                ev.Allow = false;
        }

        private static void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim.GetComponent<RepeatingBehaviour>()?.isActiveAndEnabled ?? false)
                ev.Allow = false;

            MouveVent bh = ev.Killer?.GetComponent<MouveVent>();
            
            if (bh?.isActiveAndEnabled ?? false && !bh.cantAtackAndKeepInviblity)
                bh.Kill();
        }

        private static bool _firstInit = true;

        private void RegisterEvents()
        {
            if (!_firstInit)
                return;
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUpItem;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            _firstInit = false;
        }
    }
}
