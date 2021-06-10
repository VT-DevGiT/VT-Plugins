using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System;
using System.Linq;
using VT_Referance.Behaviour;
using VT_Referance.ItemScript;
using VT_Referance.Variable;

namespace VT_Item.Item
{
    class BulletproofPlateScript : BaseItemScript
    {

        protected override int ID => (int)ItemID.BulletPlate;

        protected override ItemType ItemType => ItemType.WeaponManagerTablet;

        protected override string Name => "Bulletproof Plate";

        protected override void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            ShieldControler shieldPlayer;
            if (!ev.Player.gameObject.TryGetComponent(out shieldPlayer))
                shieldPlayer = ev.Player.gameObject.AddComponent<ShieldControler>();
            if (!shieldPlayer.ShieldLock && shieldPlayer.MaxShield != shieldPlayer.Shield)
            { 
                shieldPlayer.Shield = shieldPlayer.Shield + Plugin.BulletproofPlateConfig.AmoutSheld;
                ev.NewItem.Destroy();
                ev.Player.VanillaInventory.NetworkitemUniq = -1;
                ev.Player.VanillaInventory.Network_curItemSynced = ItemType.None;
            }
        }

        public BulletproofPlateScript() : base()
        {
            Server.Get.Events.Player.PlayerGeneratorInteractEvent += OnGenratorInteract;
        }

        private void OnGenratorInteract(PlayerGeneratorInteractEventArgs ev)
        {
            if (ev.GeneratorInteraction == GeneratorInteraction.TabletInjected)
            {
                if (!ev.Player.Inventory.Items.Where(p => p.ID != ID && p.ItemType == ItemType.WeaponManagerTablet).Any())
                    ev.Allow = false;
                else if (ev.Player.Inventory.Items.Where(p => p.ID == ID).Any())
                {
                    int numberBulProfPlat = ev.Player.Inventory.Items.Where(p => p.ID == this.ID).Count();
                    SynapseItem Copy = ev.Player.Inventory.Items.Where(p => p.ID == ID).FirstOrDefault();
                    SynapseItem backUp = new SynapseItem(ID, 0, 0, 0, 0);
                    backUp.Scale = Copy.Scale;
                    Timing.CallDelayed(0.2f, () =>
                    {
                        if (ev.Player.Inventory.Items.Where(p => p.ID == ID).Count() != numberBulProfPlat)
                        {
                            SynapseItem Tablet = ev.Player.Inventory.Items.Where(p => p.ID == (int)ItemType.WeaponManagerTablet).FirstOrDefault();
                            ev.Player.Inventory.RemoveItem(Tablet);
                            ev.Player.Inventory.AddItem(backUp);
                        }
                    });
                }
            }
            else if (ev.GeneratorInteraction == GeneratorInteraction.TabledEjected && ev.Generator.ConnectedTablet.ID == ID)
            {
                ev.Generator.ConnectedTablet = new SynapseItem(ItemType.WeaponManagerTablet, 0, 0, 0, 0);
            }
        }
    }
}
