using MEC;
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
        private Vector3 _Escape = new Vector3(-56.2f, 988.9f, -49.6f);
        private int _Radius = 1;
        public CHIEscape()
        {
            this.RefreshTime = 100;
        }
        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }
        protected override void BehaviourAction()
        {
            if (Vector3.Distance(base.transform.position, _Escape) < _Radius)
            {
                var configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.RoleID == (int)p.Role && EscapeEnum.CHI == p.Escape &&
                    (player.Cuffer?.TeamID == (int)p.CufferTeam || (player.Cuffer == null && (int)p.CufferTeam == -1)));
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true && !Server.Get.Map.Nuke.Detonated)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3));
                    if (configEscape.EscapeMessage != null && !Server.Get.Map.Nuke.Detonated)
                        Map.Get.Cassie(configEscape.EscapeMessage, false);
                    Method.ChangeRole(player, (int)configEscape.NewRole);
                    return;
                }
                configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.TeamID == (int)p.Team && EscapeEnum.CHI == p.Escape && 
                    (player.Cuffer?.TeamID == (int)p.CufferTeam || (player.Cuffer == null && (int)p.CufferTeam == -1)));
                if (configEscape != null)
                {
                    if (configEscape.StartWarHead == true && !Server.Get.Map.Nuke.Detonated)
                        Timing.RunCoroutine(new Method().WarHeadEscape(3));
                    if (configEscape.EscapeMessage != null && !Server.Get.Map.Nuke.Detonated)
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

