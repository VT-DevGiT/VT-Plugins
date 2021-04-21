using MEC;
using Mirror;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTEscape
{
    public class NTFEscape : BaseRepeatingBehaviour
    {
        private Player player;
        public bool Enabled = true;

        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }

        protected override void BehaviourAction()
        {
            if (Vector3.Distance(base.transform.position, base.GetComponent<Escape>().worldPosition) <= Escape.radius)//AdvencedEscape.Config.rayonSortie)
            {
                var configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.RoleID == (int)p.Role
                    && EscapeEnum.MTF == p.Escape && player.IsCuffed == p.Handcuffed);
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true && !Server.Get.Map.Nuke.Detonated)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3));
                    if (configEscape.EscapeMessage != null && !Server.Get.Map.Nuke.Detonated)
                        Map.Get.Cassie(configEscape.EscapeMessage, false);
                    Method.ChangeRole(player, (int)configEscape.NewRole);
                    return;
                }
                configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.TeamID == (int)p.Team
                    && EscapeEnum.MTF == p.Escape && player.IsCuffed == p.Handcuffed);
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3)); 
                    if (configEscape.EscapeMessage != null)
                        Map.Get.Cassie(configEscape.EscapeMessage, false);
                    Method.ChangeRole(player, (int)configEscape.NewRole);
                    return;
                }
                Method.ChangeRole(player, (int)RoleType.Spectator);
                return;
            }
        }
    }
}
