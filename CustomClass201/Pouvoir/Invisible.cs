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
        private float _timer;

        public List<Vector3> dimanction = new List<Vector3>();
        private void Start()
        {
            player = gameObject.GetPlayer();
        }
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > 0.1f)
            {
                player.GiveEffect(Effect.Scp268, 1, 0.1f);
            }
            if (_timer > 0.1f)
                _timer = 0f;
        }
        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}

