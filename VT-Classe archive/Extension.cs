using CustomClass.Config;
using System.Collections.Generic;
using System.Linq;

namespace CustomClass
{
    public static class Extension
    {
        public static void AddProba(this List<ProbaInfo> list, MoreClasseID role, IBaseConfig config)
        {
            if (config.ConfigSpawnChance > 0)
            {
                list.AddRange(Enumerable.Repeat(new ProbaInfo((int)role, config.ConfigRequiredPlayers), config.ConfigSpawnChance));
            }
        }

        public static void CompletProba(this List<ProbaInfo> list)
        {
            if (list.Count < 100)
            {
                list.AddRange(Enumerable.Repeat(new ProbaInfo(0, 0), 100 - list.Count));
            }
        }
        public static bool IsScpID(this int id) => id == (int)RoleType.Scp173 || id == (int)RoleType.Scp049 || id == (int)RoleType.Scp0492 || id == (int)RoleType.Scp079 || id == (int)RoleType.Scp096 || id == (int)RoleType.Scp106 || id == (int)RoleType.Scp93953 || id == (int)RoleType.Scp93989;

        public static bool IsMtfLieutenantID(this int id) => id == (int)RoleType.NtfLieutenant;

        public static bool IsMtfCadetID(this int id) => id == (int)RoleType.NtfCadet;

        public static bool IsMtfGardID(this int id) => id == (int)RoleType.FacilityGuard;

        public static bool IsChiID(this int id) => id == (int)RoleType.ChaosInsurgency;

        public static bool IsCldID(this int id) => id == (int)RoleType.ClassD;

        public static bool IsRcsID(this int id) => id == (int)RoleType.Scientist;

        public static bool IsRipID(this int id) => id == (int)RoleType.Spectator;

        public static bool IsTutID(this int id) => id == (int)RoleType.Tutorial;
    }
}
