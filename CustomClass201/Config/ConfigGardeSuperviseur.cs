using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigGardeSuperviseur : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_EZ_Checkpoint", 7.9f, 2.143616f, 0.02f);

        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.KeycardSeniorGuard, 1, 3, 2, 1, Vector3.one), 
            new SerializedItem((int)ItemType.GunProject90, 50, 0, 0, 0, Vector3.one), 
            new SerializedItem(50, 1, 0, 1, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem(55, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.GrenadeFlash, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 100;
        public int MaxArtificialHealth = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Garde Superviseur";

        [Description("the number of ammo to the class")]
        public uint Ammo5 = 30;
        public uint Ammo7 = 70;
        public uint Ammo9 = 30;
    }
}