using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synapse.Api.Enum;

namespace VT_Referance.Method
{
    public static class Other_Extentsion
    {
        static DamageType[] PlayerDamage = { DamageType.Scp, DamageType.Firearm, DamageType.Scp049, DamageType.Scp096, DamageType.Scp173, DamageType.Scp939, DamageType.BulletWounds };

        public static bool IsPlayer(DamageType damageType)
            => PlayerDamage.Contains(damageType);


    }
}
