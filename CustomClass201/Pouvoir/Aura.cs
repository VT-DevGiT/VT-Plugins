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
using VT_Referance.Method;
using VT_Referance.Variable;

namespace CustomClass.Pouvoir
{
    class Aura : NetworkBehaviour
    {
        private Player player;
        private float _timer;
        public byte LuiIntencty = 1;
        public byte LuiTime = 1;
        public Effect? TargetEffect = null;
        public Effect? PlayerEffect = null;
        public int MoiHp = 0;
        public int LuiHp = 0;
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
                foreach (var target in Server.Get.Players.Where(p => p != player && p.TeamID != (int)TeamID.SCP && p.TeamID != (int)TeamID.RIP
                     && p.IsUTR() && p.RoleID != (int)RoleID.NTFVirologue))
                {
                    if (Vector3.Distance(target.Position, player.Position) < Distance)
                    {
                        if (TargetEffect != null)
                            target.GiveEffect((Effect)TargetEffect, LuiIntencty, LuiTime);
                        if (PlayerEffect != null)
                            player.GiveEffect((Effect)PlayerEffect, 1, 1);

                        if (MoiHp < 0)
                            player.Heal(MoiHp);
                        else if (MoiHp > 0)
                            player.Hurt(MoiHp);

                        if (LuiHp < 0)
                            player.Heal(LuiHp);
                        else if (MoiHp > 0)
                            player.Hurt(LuiHp);
                    }
                }
            }
            if (_timer > 1)
                _timer = 0f;
        }
    }
}

