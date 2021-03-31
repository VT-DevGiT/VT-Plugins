using MEC;
using Mirror;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTEscape
{
    public class CHIEscape : BaseRepeatingBehaviour
    {
        private Player player;
        public bool Enabled = true;
        private float _timer;
        private Vector3 _Escape = new Vector3(-56.2f, 988.9f, -49.6f);

        private void Awake()
        {
            player = gameObject.GetPlayer();
        }
        protected override void BehaviourAction()
        {
            if (Vector3.Distance(base.transform.position, _Escape) < 1)
            {
                var configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.RoleID == (int)p.Role
                    && EscapeEnum.CHI == p.Escape && player.IsCuffed == p.Handcuffed);
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3));
                    if (configEscape.EscapeMessage != null)
                        Map.Get.Cassie(configEscape.EscapeMessage, false);
                    player.Inventory.Clear();
                    player.RoleID = (int)configEscape.NewRole;
                    _timer = 0f;
                    return;
                }
                configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.TeamID == (int)p.Team
                    && EscapeEnum.CHI == p.Escape && player.IsCuffed == p.Handcuffed);
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3));
                    if (configEscape.EscapeMessage != null)
                        Map.Get.Cassie(configEscape.EscapeMessage, false);
                    player.Inventory.Clear();
                    player.RoleID = (int)configEscape.NewRole;
                    _timer = 0f;
                    return;
                }
                player.Inventory.Clear();
                player.RoleID = (int)RoleType.Spectator;
            }
        }
    }
}

