using VT_Api.Core.Items;
using VT_Api.Core.Plugin;

namespace VT_Item.Item
{
    [VtItemInformation(ID = 201, BasedItemType = ItemType.Coin, Name = "BuletPrufPlat")]
    [AutoRegisterManager.Ignore]
    class BulletproofPlateScript : AbstractItem
    {
        public override string ScreenName => Plugin.Instance.Translation.ActiveTranslation.NameBulletproofPlate;
        public override string MessagePickUp => Plugin.Instance.Translation.ActiveTranslation.MessageGetItem;

    }
}
