using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigDirecteurSite : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Amount of Health the class have")]
        public int Health = 125;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.KeycardFacilityManager, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GunUSP, 18, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Adrenaline, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Painkillers, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 13;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Directeur du site";
    }
}