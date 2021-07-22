using MEC;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTEscape
{
    public class NTFEscape : BaseRepeatingBehaviour
    {
        private Player player;
        private Vector3 _Escape;
        private int _Radius;
        public NTFEscape()
        {
            this.RefreshTime = 100;
        }
        protected override void Start()
        {
            player = gameObject.GetPlayer();
            _Escape = base.GetComponent<Escape>().worldPosition;
            _Radius = Escape.radius;
            base.Start();
        }
        protected override void BehaviourAction()
        {
            if (Vector3.Distance(base.transform.position, _Escape) <= _Radius)//AdvencedEscape.Config.rayonSortie)
            {
                var configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.RoleID == (int)p.Role && EscapeEnum.MTF == p.Escape && 
                    (player.Cuffer?.TeamID == (int)p.CufferTeam || (player.Cuffer == null && (int)p.CufferTeam == -1)));
                if (configEscape != null)
                {
                    configEscape.Use(player);
                    return;
                }
                configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.TeamID == (int)p.Team && EscapeEnum.MTF == p.Escape && 
                    (player.Cuffer?.TeamID == (int)p.CufferTeam || (player.Cuffer == null && (int)p.CufferTeam == -1)));
                if (configEscape != null)
                {
                    configEscape.Use(player);
                    return;
                }
                Method.ChangeRole(player, (int)RoleType.Spectator);
                return;
            }
        }
    }
}
