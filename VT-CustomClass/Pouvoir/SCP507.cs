using Mirror;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTCustomClass.Pouvoir
{
    class SCP507 : BaseRepeatingBehaviour
    {
        private Player player;

        public List<Vector3> rooms = new List<Vector3>();

        public SCP507()
        {
            this.RefreshTime = 20000 /*PluginClass.ConfigSCP507.PowerTime*1000 */;
        }

        protected override void Start()
        {
            foreach (var mapPoint in PluginClass.ConfigSCP507.ListRoom)
                rooms.Add(mapPoint.Parse().Position);

            player = gameObject.GetPlayer();
            base.Start();
        }
        protected override void BehaviourAction()
        {
            int rnd = UnityEngine.Random.Range(0, rooms.Count() - 1);
            player.MapPoint = PluginClass.ConfigSCP507.ListRoom[rnd].Parse();
        }
    }
}

