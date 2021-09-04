using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System;
using System.Linq;
using VT_Referance.Behaviour;
using VT_Referance.ItemScript;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_Item.Item
{
    class BulletproofPlateScript : BaseItemScript
    {
        public override float Weight => 1;

        public override string MessagePickUp => Plugin.PluginTranslation.ActiveTranslation.MessageGetItem;

        public override int ID => (int)ItemID.BulletPlate;

        public override ItemType ItemType => ItemType.ArmorLight;

        public override string Name => Plugin.PluginTranslation.ActiveTranslation.NameBulletproofPlate;

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
