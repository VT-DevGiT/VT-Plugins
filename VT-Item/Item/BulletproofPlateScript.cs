using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.Behaviour;
using VT_Referance.Script.ItemScript;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_Item.Item
{
    [ItemInformation(ID = 201, ItemType = ItemType.Coin, Name = "BuletPrufPlat")]
    class BulletproofPlateScript : BaseItemScript
    {
        public override string ScrenName => Plugin.PluginTranslation.ActiveTranslation.NameBulletproofPlate;
        public override string MessagePickUp => Plugin.PluginTranslation.ActiveTranslation.MessageGetItem;

        protected override void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            var shieldPlayer = ev.Player.GetOrAddComponent<ShieldControler>();
            if (!shieldPlayer.ShieldLock && shieldPlayer.MaxShield != shieldPlayer.Shield)
            { 
                shieldPlayer.Shield = shieldPlayer.Shield + Plugin.BulletproofPlateConfig.AmoutSheld;
                ev.NewItem.Destroy();
                ev.Player.ItemInHand = null;
            }
        }
    }
}
