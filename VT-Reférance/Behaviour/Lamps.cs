using Mirror;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.Method;

namespace Common_Utiles
{
    public class Lamps : BaseRepeatingBehaviour
    {
        float degat;
        private Player player;
        bool Enabled = true;
        private float _timer;

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }

        protected override void BehaviourAction()
        {
            _timer += Time.deltaTime;

            if (Enabled && _timer > 1f)
            {
                if (player.ItemInHand != null && player.ItemInHand.ItemType == ItemType.Flashlight)
                {
                    IEnumerable<Player> joueurs106 = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Scp106);
                    foreach (var scp106 in joueurs106)
                    {
                        if (player.IsTargetVisible(scp106.gameObject) && Vector3.Distance(scp106.Position, player.Position) < 120 /*placée ici une config a la palce */)
                        {
                            degat += 6.25f;
                            scp106.Health -= degat;
                        }
                        else
                            degat = 0;
                    }
                }
            }

            if (Enabled && _timer > 0.5f)
                _timer = 0;

        }

        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}
