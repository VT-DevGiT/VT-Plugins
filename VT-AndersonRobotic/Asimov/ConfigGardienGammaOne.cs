using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VT_AndersonRobotic
{
    public class ConfigGardienGammaOne : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 86.47166f, -10.64563f, -69.14687f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(100, 100, 100),
            Items = new List<SerializedPlayerItem>()
            {
                new SerializedPlayerItem((int)ItemType.KeycardNTFLieutenant, 75, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.GunE11SR, 75, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Disarmer, 1, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one, 100, true),
                new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, 0, 0, Vector3.one, 100, true)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 50;
        public int MaxArtificialHealth = 100;

        [Description("The name of the class")]
        public string RoleName = "Gardien Gamma-1";
    }
}