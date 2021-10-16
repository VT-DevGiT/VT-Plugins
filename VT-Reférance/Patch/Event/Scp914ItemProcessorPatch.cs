using HarmonyLib;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using Scp914;
using Scp914.Processors;
using Synapse.Api.Items;
using UnityEngine;

namespace VT_Referance.Patch.Event
{
    [HarmonyPatch(typeof(Scp914ItemProcessor), nameof(Scp914ItemProcessor.OnPickupUpgraded))]
    class Scp914ItemProcessorPatch1
    {
        [HarmonyPrefix]
        private static bool ItemUpgradePatch(Scp914KnobSetting setting, ItemPickupBase ipb, Vector3 newPosition)
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

    [HarmonyPatch(typeof(Scp914ItemProcessor), nameof(Scp914ItemProcessor.OnInventoryItemUpgraded))]
    class Scp914ItemProcessorPatch2
    {
        [HarmonyPrefix]
        private static bool ItemUpgradePatch(Scp914KnobSetting setting, ReferenceHub hub, ushort serial)
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
