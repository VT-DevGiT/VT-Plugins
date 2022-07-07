using Scp914;
using Synapse;
using Synapse.Api;
using System;
using System.Linq;
using System.Collections.Generic;
using PlayerStatsSystem;
using Subtitles;

namespace Common_Utiles.Config
{
    [Serializable]
    public class Serialized914Role
    {
        public TeamOrRole ID { get; set; }
        public TeamOrRole RoughID { get; set; }
        public TeamOrRole CorseID { get; set; }
        public TeamOrRole OneToOneID { get; set; }
        public TeamOrRole FineID { get; set; }
        public TeamOrRole VeryFineID { get; set; }
        public int Chance { get; set; }

        public Serialized914Role()
        {

        }

        public Serialized914Role(TeamOrRole roleID , int chance, TeamOrRole roughRoleID, TeamOrRole corseRoleID, TeamOrRole oneToOneRoleID, TeamOrRole fineRoleID, TeamOrRole veryFineRoleID)
        {
            ID = roleID;
            Chance = chance;
            RoughID = roughRoleID;
            CorseID = corseRoleID;
            OneToOneID = oneToOneRoleID;
            FineID = fineRoleID;
            VeryFineID = veryFineRoleID;
        }

        public void Apply(Player player, Scp914KnobSetting setting)
        {
            if ((ID.id == -1 || ID.isRoleID ? player.RoleID == ID.id : player.TeamID == ID.id) && UnityEngine.Random.Range(0, 100) <= Chance)
            {
            
                switch (setting)
                {
                    case Scp914KnobSetting.Rough:
                        SetTeamOrRole(player, RoughID);
                        return;
                    case Scp914KnobSetting.Coarse:
                        SetTeamOrRole(player, CorseID);
                        return;
                    case Scp914KnobSetting.OneToOne:
                        SetTeamOrRole(player, OneToOneID);
                        return;
                    case Scp914KnobSetting.Fine:
                        SetTeamOrRole(player, FineID);
                        return;
                    case Scp914KnobSetting.VeryFine:
                        SetTeamOrRole(player, VeryFineID);
                        return;
                }
            }
        }

        private void SetTeamOrRole(Player player, TeamOrRole roleID)
        {
            int newRoleID = roleID.isRoleID ?
                roleID.id :
                Plugin.Instance.TeamIDRolesID[roleID.id][UnityEngine.Random.Range(0, Plugin.Instance.TeamIDRolesID.Count - 1)];
            if (newRoleID == -1)
            {
                player.PlayerStats.KillPlayer(new UniversalDamageHandler(-1, DeathTranslations.Crushed, new DamageHandlerBase.CassieAnnouncement()
                {
                    Announcement = "SUCCESSFULLY TERMINATED . CRUSED BY SCP 914",
                    SubtitleParts = new[] { new SubtitlePart(SubtitleType.TerminatedBySCP, new[] { "914" }) } // A test
                }));
            }
            else
            {
                player.RoleID = newRoleID;
            } 
        }
    }
}
