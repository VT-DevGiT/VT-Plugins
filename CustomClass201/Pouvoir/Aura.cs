using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Variable;

namespace CustomClass.Pouvoir
{
    class Aura : NetworkBehaviour
    {
        private Player player;
        private float _timer;
        public byte Intencty = 1;
        public Effect TargetEffect;
        public Effect PlayerEffect;
        public int MoiHealHp = -1;
        public int LuiHealHp = -1;
        public int Distance = 2;

        public List<Vector3> dimanction = new List<Vector3>();
        private void Start()
        {
            player = gameObject.GetPlayer();
        }
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > 1)
            {
                foreach(var target in Server.Get.Players.Where(p => p != player && p.Team != Team.SCP 
                    && p.RoleID != (int)RoleID.RoboticTacticalUnity && p.RoleID != (int)RoleID.NTFVirologue && p.RoleID != (int)RoleID.SCP999))
                { 
                    if (Vector3.Distance(target.Position, player.Position) < Distance)
                    {
                        target.GiveEffect(TargetEffect, Intencty, 1);
                        player.GiveEffect(PlayerEffect, 1, 1);
                        if (MoiHealHp != -1)
                            player.Heal(MoiHealHp);
                        if (LuiHealHp != -1)
                            target.Heal(LuiHealHp);
                    }
                }
            }
            if (_timer > 1)
                _timer = 0f;
        }
    }
}

