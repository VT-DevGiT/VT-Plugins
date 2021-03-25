using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigScientifiqueSuperviseur : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 110;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { 
            new SerializedItem((int)ItemType.KeycardScientistMajor, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.GunUSP, 18, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), 
            new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 50;
        public int MaxArtificialHealth = 20;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;


        [Description("The name of the class")]
        public string RoleName = " Scientifique Superviseur";

        [Description("the number of ammo to the class")]
        public uint Ammo9 = 20;
    }
}