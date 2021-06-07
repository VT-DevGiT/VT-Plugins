using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Referance.Behaviour;
using VT_Referance.ItemScript;
using VT_Referance.Variable;

namespace VT_Item.Item
{
    class BulletproofPlate : BaseItemScript
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
                shieldPlayer.Shield = shieldPlayer.Shield + 25;
                ev.NewItem.Destroy();
            }
        }



    }
}
