using HarmonyLib;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using Scp914;
using Scp914.Processors;
using Synapse.Api.Items;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    
    class Scp914ItemProcessorPatch1
    {
        [HarmonyPatch(typeof(AmmoItemProcessor), nameof(AmmoItemProcessor.OnPickupUpgraded))]
        [HarmonyPrefix]
        private static bool AmmoUpgradePatch(Scp914KnobSetting setting, ItemPickupBase ipb, Vector3 newPosition)
            => ItemUpgrade(setting, ipb, newPosition);

        [HarmonyPatch(typeof(FirearmItemProcessor), nameof(FirearmItemProcessor.OnPickupUpgraded))]
        [HarmonyPrefix]
        private static bool FirearmUpgradePatch(Scp914KnobSetting setting, ItemPickupBase ipb, Vector3 newPosition)
            => ItemUpgrade(setting, ipb, newPosition);

        [HarmonyPatch(typeof(FirearmItemProcessor), nameof(FirearmItemProcessor.OnPickupUpgraded))]
        [HarmonyPrefix]
        private static bool ItemUpgradePatch(Scp914KnobSetting setting, ItemPickupBase ipb, Vector3 newPosition)
            => ItemUpgrade(setting, ipb, newPosition);

        private static bool ItemUpgrade(Scp914KnobSetting setting, ItemPickupBase ipb, Vector3 newPosition)
        {
            SynapseItem newItem;
            SynapseItem item = ipb.GetSynapseItem();

            VTController.Server.Events.Map.InvokeScp914UpgradeItemEvent(item, setting, out newItem);
            if (newItem != null) return true;

            if (newItem != SynapseItem.None)
            {
                newItem.Position = newPosition;
                newItem.Rotation = item.Rotation;
            }
            item.Destroy();

            return false;
        }
    }

    class Scp914ItemProcessorPatch2
    {
        [HarmonyPatch(typeof(AmmoItemProcessor), nameof(AmmoItemProcessor.OnInventoryItemUpgraded))]
        [HarmonyPrefix]
        private static bool AmmoUpgradePatch(Scp914KnobSetting setting, ReferenceHub hub, ushort serial)
            => ItemUpgrade(setting, hub, serial);

        [HarmonyPatch(typeof(FirearmItemProcessor), nameof(FirearmItemProcessor.OnInventoryItemUpgraded))]
        [HarmonyPrefix]
        private static bool FirearmUpgradePatch(Scp914KnobSetting setting, ReferenceHub hub, ushort serial)
            => ItemUpgrade(setting, hub, serial);

        [HarmonyPatch(typeof(StandardItemProcessor), nameof(StandardItemProcessor.OnInventoryItemUpgraded))]
        [HarmonyPrefix]
        private static bool ItemUpgradePatch(Scp914KnobSetting setting, ReferenceHub hub, ushort serial)
            => ItemUpgrade(setting, hub, serial);
        private static bool ItemUpgrade(Scp914KnobSetting setting, ReferenceHub hub, ushort serial)
        {
            SynapseItem newItem;
            SynapseItem item;

            if (!SynapseItem.AllItems.ContainsKey(serial))
            {
                Synapse.Server.Get.Logger.Error($"Vt-Event: ItemUpgradePatch failed!!\nAllItems dont containe {serial}");
                return false;
            }
            item = SynapseItem.AllItems[serial];
            if (item == null)
            {
                ItemBase itembase;
                hub.inventory.UserInventory.Items.TryGetValue(serial, out itembase);
                Synapse.Api.Logger.Get.Warn($"Vt-Event: ItemUpgradePatch Found unregistered ItemBase with Serial: {itembase.ItemSerial} - Create a new SynapseItem Instance");
                item = new SynapseItem(itembase);
            }

            VTController.Server.Events.Map.InvokeScp914UpgradeItemEvent(item, setting, out newItem);
            if (newItem != null) return true;

            item.Destroy();
            if (newItem != SynapseItem.None)
                hub.GetPlayer().Inventory.AddItem(newItem);

            return false;
        }
    }
}
