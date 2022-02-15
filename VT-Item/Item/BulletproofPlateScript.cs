using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Core.Items;
using VT_Api.Core.Plugin;
using VT_Api.Extension;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 201, BasedItemType = ItemType.Coin, Name = "BuletPrufPlat")]
    [AutoRegisterManager.Ignore]
    class BulletproofPlateScript : VT_Api.Core.Items.AbstractItem
    {
        public override string ScreenName => Plugin.Instance.Translation.ActiveTranslation.NameBulletproofPlate;
        public override string MessagePickUp => Plugin.Instance.Translation.ActiveTranslation.MessageGetItem;

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
