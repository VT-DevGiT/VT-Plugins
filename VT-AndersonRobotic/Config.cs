using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VT_AndersonRobotic
{
    public class Config : AbstractConfigSection
    {

        [Description("The Health of Anderson Robotic members")]
        public int Health = 130;

        [Description("The Role Name that is displayed when you look at the Player")]
        public string CustomRoleName = "<color=yellow>Anderson Unit</color>";

        [Description("The chance that a Anderson Robotic Squad spawns instead of a Chaos")]
        public float SpawnChance = 50f;

        [Description("The Spawnpoint where Anderson Robotic spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f);

        [Description("The amount of ammo that Anderson Robotic spawns with")]
        public uint Ammo5 = 50;
        public uint Ammo7 = 50;
        public uint Ammo9 = 50;

        [Description("The maximal amount of players that can spawn as Anderson Robotic Unite in one squad")]
        public int SpawnSize = 7;

        [Description("The items that Anderson Robotic spawn with")]
        public List<SerializedItem> Items = new List<SerializedItem>
        {
            new SerializedItem((int)ItemType.KeycardChaosInsurgency,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Medkit,0,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.GunLogicer,75,0,0,0,Vector3.one),
            new SerializedItem((int)ItemType.Painkillers,0,0,0,0,Vector3.one)
        };
    }
}