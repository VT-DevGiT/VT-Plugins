using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomClass.Pouvoir
{
    class MouveVent : NetworkBehaviour
    {
        public bool Enabled = true;
        private Player player;
        private float _timer;
        public int duraction = -1;

        private void Start()
        {
            player = gameObject.GetPlayer();
            player.GiveEffect(Effect.Scp268, 2);
            RegisterEvents();
        }
        private void Update()
        {
            _timer += Time.deltaTime;

            if (Enabled && _timer > 1f)
            {
                if(Enabled && duraction > 0)
                { 
                    player.SendBroadcast(1 ,PluginClass.PluginTranslation.ActiveTranslation.
                        VentMessage.Replace("%Time%", duraction.ToString()));
                    duraction--;
                }
                else if (Enabled && duraction < 0)
                {
                    player.BroadcastMessage(PluginClass.PluginTranslation.ActiveTranslation.NoTimeVentMessage, 1);
                }
            }

            if (_timer > 1f)
                _timer = 0f;
        }
        private void OnDoorInteract(DoorInteractEventArgs ev)
        {
           if (ev.Player == player)
            {
                ev.Allow = false;
                ev.Player.Position += (ev.Player.gameObject.transform.forward * 3.5f);
            }
        }

        public void Destroy()
        {
            UnRegisterEvents();
            player.Invisible = false;
            Enabled = false;
            DestroyImmediate(this, true);
        }

        private void UnRegisterEvents() => Server.Get.Events.Map.DoorInteractEvent -= OnDoorInteract;
        private void RegisterEvents() => Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
    }
}
