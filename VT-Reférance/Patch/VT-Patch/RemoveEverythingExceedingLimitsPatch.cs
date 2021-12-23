using HarmonyLib;
using InventorySystem.Configs;
using InventorySystem.Items.Armor;

namespace VT_Referance.Patch.VT_Patch
{
    //[HarmonyPatch(typeof(InventoryLimits), nameof(InventoryLimits.GetAmmoLimit))]
    internal class RemoveEverythingExceedingLimitsPatch
    {
        //[HarmonyPrefix]
        private static bool ByPasseLimit(BodyArmor armor, ItemType ammoType,out ushort __result)
        {
            __result = 999;
            return armor.GetSynapseItem().ItemHolder.CustomRole == null;
        }
    }
}
