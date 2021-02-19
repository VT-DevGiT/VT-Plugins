using Mirror;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VTGrenad
{
    [PluginInformation(
        Author = "VT",
        Description = "Allows you to activate grenades remotely",
        LoadPriority = 1,
        Name = "VT-Grenade",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public static Dictionary<int, List<AmorcableGrenade>> DictTabletteGrenades { get; set; }

        [Synapse.Api.Plugin.Config(section = "VT-Grenade")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            DictTabletteGrenades = new Dictionary<int, List<AmorcableGrenade>>();
            new EventHandlers();
        }

        public static void SpawnGrenade(Vector3 position, ReferenceHub player = null)
        {
            //Map.Explode()
            if (player == null) player = ReferenceHub.GetHub(PlayerManager.localPlayer);
            var gm = player.GetComponent<Grenades.GrenadeManager>();
            Grenades.FragGrenade component = UnityEngine.Object.Instantiate(gm.availableGrenades[0].grenadeInstance, position, Quaternion.Euler(Vector3.zero)).GetComponent<Grenades.FragGrenade>();
            component.fuseDuration = 0.1f;
            component.InitData(gm, Vector3.zero, Vector3.zero);
            component.transform.position = new Vector3(position.x, position.y, position.z);
            component.gameObject.transform.position = new Vector3(position.x, position.y, position.z);
            NetworkServer.Spawn(component.gameObject);
            component.transform.position = new Vector3(position.x, position.y, position.z);
            component.gameObject.transform.position = new Vector3(position.x, position.y, position.z);
        }
    }
}
