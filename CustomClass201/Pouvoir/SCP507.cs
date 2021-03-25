using Mirror;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomClass.Pouvoir
{
    class SCP507 : NetworkBehaviour
    {
        public bool Enabled = true;
        private Player player;
        private float _timer;
        private int duraction = -1;
        private int rnd;
        public int minduraction { get; set; }
        public int maxduraction { get; set; }
        public List<Vector3> rooms = new List<Vector3>();
        private void Start()
        {
            minduraction = PluginClass.ConfigSCP507.MinTPower;
            maxduraction = PluginClass.ConfigSCP507.MaxTPower;
            foreach (var mapPoint in PluginClass.ConfigSCP507.ListRoom)
                rooms.Add(mapPoint.Parse().Position);

            player = gameObject.GetPlayer();
            duraction = UnityEngine.Random.Range(minduraction, maxduraction);
        }
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > duraction)
            {
                rnd = UnityEngine.Random.Range(0, rooms.Count() - 1);
                player.MapPoint = PluginClass.ConfigSCP507.ListRoom[rnd].Parse();
                duraction = UnityEngine.Random.Range(minduraction, maxduraction);
            }
            if (_timer > duraction)
                _timer = 0f;
        }
        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}

