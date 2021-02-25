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

namespace CustomClass.Pouvoir
{
    class Invisible : NetworkBehaviour
    {
        public bool Enabled = true;
        private Player player;

        private void Start()
        {
            player = gameObject.GetPlayer();
        }
        private void Update()
        {   
            player.GiveEffect(Effect.Scp268);
        }
        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}

