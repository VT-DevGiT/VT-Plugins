using Scp914;
using Synapse.Api;
using Synapse.Api.Enum;
using System;

namespace Common_Utiles.Config
{
    [Serializable]
    public class Serialized914Effect
    {
        public int RoleID { get; set; }
        public Effect RoughEffect { get; set; }
        public Effect CorseEffect { get; set; }
        public Effect OneToOneEffect { get; set; }
        public Effect FineEffect { get; set; }
        public Effect VeryFineEffect { get; set; }
        public int Chance { get; set; }

        public Serialized914Effect(){}

        public Serialized914Effect(int roleID, int chance, Effect roughEffect, Effect corseEffect, Effect oneToOneEffect, Effect fineEffect, Effect veryFineRoleID)
        {
            RoleID = roleID;
            Chance = chance;
            RoughEffect = roughEffect;
            CorseEffect = corseEffect;
            OneToOneEffect = oneToOneEffect;
            FineEffect = fineEffect;
            VeryFineEffect = veryFineRoleID;
        }

        public void Apply(Player player, Scp914KnobSetting setting)
        {
            if ((player.RoleID == RoleID || player.RoleID == -1) && UnityEngine.Random.Range(0, 100) <= Chance)
            {
                switch (setting)
                {
                    case Scp914KnobSetting.Rough: player.GiveEffect(RoughEffect); return;
                    case Scp914KnobSetting.Coarse: player.GiveEffect(CorseEffect); return;
                    case Scp914KnobSetting.OneToOne: player.GiveEffect(OneToOneEffect); return;
                    case Scp914KnobSetting.Fine: player.GiveEffect(FineEffect); return;
                    case Scp914KnobSetting.VeryFine: player.GiveEffect(VeryFineEffect); return;
                }
            }
        }
    }
}
