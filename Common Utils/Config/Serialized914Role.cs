using Scp914;
using Synapse.Api;
using System;

namespace Common_Utiles.Config
{
    [Serializable]
    public class Serialized914Role
    {
        public int RoleID { get; set; }
        public int RoughRoleID { get; set; }
        public int CorseRoleID { get; set; }
        public int OneToOneRoleID { get; set; }
        public int FineRoleID { get; set; }
        public int VeryFineRoleID { get; set; }
        public int Chance { get; set; }

        public Serialized914Role()
        {

        }

        public Serialized914Role(int roleID, int chance, int roughRoleID, int corseRoleID, int oneToOneRoleID, int fineRoleID, int veryFineRoleID)
        {
            RoleID = roleID;
            Chance = chance;
            RoughRoleID = roughRoleID;
            CorseRoleID = corseRoleID;
            OneToOneRoleID = oneToOneRoleID;
            FineRoleID = fineRoleID;
            VeryFineRoleID = veryFineRoleID;
        }

        public void Apply(Player player, Scp914KnobSetting setting)
        {
            if ((player.RoleID == RoleID || player.RoleID == -1) && UnityEngine.Random.Range(0, 100) <= Chance)
            {
                switch (setting)
                {
                    case Scp914KnobSetting.Rough: player.RoleID = RoughRoleID; return;
                    case Scp914KnobSetting.Coarse: player.RoleID = CorseRoleID; return;
                    case Scp914KnobSetting.OneToOne: player.RoleID = OneToOneRoleID; return;
                    case Scp914KnobSetting.Fine: player.RoleID = FineRoleID; return;
                    case Scp914KnobSetting.VeryFine: player.RoleID = VeryFineRoleID; return;
                }
            }
        }
    }
}
