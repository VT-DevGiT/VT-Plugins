using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Api.Core.Enum;

namespace VT_Item.Config
{
    public class MiniGunConfig : AbstractConfigSection
    {
        [Description("The user can move when equip the MiniGun")]
        public bool CanMouveEquip = false;

        [Description("The Time for start fir")]
        public float TimeFir = 5.5f;

        [Description("The list of Role can mouve with the MiniGun")]
        public List<int> ByPasseID = new List<int>{
            (int)RoleID.ChaosMastodonte
        };

        [Description("How big the spread should be")]
        public float AimCone = 5;

        [Description("How many bullets get shoot every shot")]
        public int bullets = 3;

        [Description("The damage amount")]
        public int Damage = 25;

    }
}
