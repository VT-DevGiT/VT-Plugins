using Synapse.Api.Enum;
using System;

namespace VT_Referance.Script.ItemScript
{
    public class WeaponInformation : Attribute
    {
        public ushort Ammos;
        public AmmoType AmmoType;
        
        public int DamageAmmont;
        public DamageTypes.DamageType DamageType = DamageTypes.Logicer;

        public float ArmorPenetration;
        public bool UseHitboxMultipliers = false;
    }

    public class ItemInfomation : Attribute
    {
        public int ID = -1;
        public ItemType ItemType = ItemType.None;
        public string Name = "Unknown";

        public float Weight;

        public string MessagePickUp;
        public string MessageChangeTo;
    }
}
