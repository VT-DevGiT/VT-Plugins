using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Enum;
using VT_Api.Extension;

namespace VTCustomClass.Pouvoir
{
    public class Aura : RepeatingBehaviour
    {
        private Player player;
        public byte effectIntencty { get; set; } = 1;
        public byte effectTime { get; set; } = 1;
        public Effect? targetEffect { get; set; } = null;
        public Effect? playerEffect { get; set; } = null;
        public int playerAddHp { get; set; } = 0;
        public int targetAddHp { get; set; } = 0;
        public int distance { get; set; } = 2;

        public List<int> ignoredRole = new List<int>() { (int)RoleID.NtfVirologue, };
        public List<int> ignoredTeam = new List<int>() { (int)TeamID.SCP, (int)TeamID.RIP };
        public bool ignorUTR = true;

        public List<Vector3> dimanction = new List<Vector3>();
        protected override void Start()
        {
            player = gameObject.GetPlayer();
            base.Start();
        }



        protected override void BehaviourAction()
        {
            foreach (var target in Server.Get.Players.Where(p => (!ignorUTR || !p.IsUTR()) && !ignoredRole.Contains(p.RoleID) && !ignoredTeam.Contains(p.TeamID)))
            {
                if (Vector3.Distance(target.Position, player.Position) < distance)
                {
                    if (targetEffect != null)
                        target.GiveEffect((Effect)targetEffect, effectIntencty, effectTime);
                    
                    if (playerEffect != null)
                        player.GiveEffect((Effect)playerEffect, 1, 1);
                    
                    if      (playerAddHp < 0)   player.Heal(playerAddHp);
                    else if (playerAddHp > 0)   player.Hurt(playerAddHp);
                    
                    if      (targetAddHp < 0)   player.Heal(targetAddHp);
                    else if (playerAddHp > 0)   player.Hurt(targetAddHp);
                }
            }
        }
    }
}

