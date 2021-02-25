using Mirror;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common_Utiles
{
    public class Lamps : NetworkBehaviour
    {
        float degat;
        private Player _joueur;
        bool Enabled = true;
        private float _timer;

        private void Start()
        {
            _joueur = gameObject.GetPlayer();
            
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (Enabled && _timer > 1f)
            {
                if (_joueur.ItemInHand != null && _joueur.ItemInHand.ItemType == ItemType.Flashlight)
                {
                    IEnumerable<Player> joueurs106 = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.Scp106);
                    foreach (var scp106 in joueurs106)
                    {
                        if (_joueur.IsTargetVisible(scp106.gameObject) && Vector3.Distance(scp106.Position, _joueur.Position) < CommonUtiles.Config.Disctance106)
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
