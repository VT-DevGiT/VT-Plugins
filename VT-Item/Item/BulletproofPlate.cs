using Hints;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance;
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
            ev.Player.gameObject.GetComponent<ShieldControler>()
        }



    }
}
