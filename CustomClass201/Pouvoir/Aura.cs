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
        public byte HerIntencty = 1;
        public byte HerTime = 1;
        public Effect? TargetEffect = null;
        public Effect? PlayerEffect = null;
        public int MyHp = 0;
        public int HerHp = 0;
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
                            target.GiveEffect((Effect)TargetEffect, HerIntencty, HerTime);
                        if (PlayerEffect != null)
                            player.GiveEffect((Effect)PlayerEffect, 1, 1);

                        if (MyHp < 0)
                            player.Heal(MyHp);
                        else if (MyHp > 0)
                            player.Hurt(MyHp);

                        if (HerHp < 0)
                            player.Heal(HerHp);
                        else if (MyHp > 0)
                            player.Hurt(HerHp);
                    }
                }
            }
            if (_timer > 1)
                _timer = 0f;
        }
    }
}

