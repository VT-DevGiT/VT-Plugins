using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Core.Items;
using VT_Api.Extension;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 201, BasedItemType = ItemType.Coin, Name = "BuletPrufPlat")]
    class BulletproofPlateScript : VT_Api.Core.Items.AbstractItem
    {
        public override string ScrenName => Plugin.PluginTranslation.ActiveTranslation.NameBulletproofPlate;
        public override string MessagePickUp => Plugin.PluginTranslation.ActiveTranslation.MessageGetItem;

        protected override void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
/*            var shieldPlayer = ev.Player.GetOrAddComponent<ShieldControler>();
            if (!shieldPlayer.ShieldLock && shieldPlayer.MaxShield != shieldPlayer.Shield)
            { 
                shieldPlayer.Shield = shieldPlayer.Shield + Plugin.BulletproofPlateConfig.AmoutSheld;
                ev.NewItem.Destroy();
                ev.Player.ItemInHand = null;
            }*/
        }
    }
}
