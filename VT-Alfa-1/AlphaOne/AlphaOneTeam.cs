using Respawning;
using Respawning.NamingRules;
using Synapse.Api;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VT_Alpha
{
    [Synapse.Api.Teams.SynapseTeamInformation(
        ID = (int)TeamID.AL1,
        Name = "Alfa-1 Agent"
        )]
    public class AlphaOneTeam : AbstractTeam
    {
        public override List<RespawnRoleInfo> Roles { get => roles; set => roles = value; }

        private List<RespawnRoleInfo> roles = new List<RespawnRoleInfo>()
        {
            new RespawnRoleInfo() { Max = Plugin.Instance.Config.MaxRepsawn, Min = Plugin.Instance.Config.MinPlayer, RoleID = (int)RoleID.AlphaOneAgent }
        };

        public override string GetSpawnAnnonce(string uniteName)
            => Plugin.Instance.Config.CassieSpawn?.Replace("%UnitName%", uniteName.Replace("-", " "));

        public override string GetNewUniteName()
        {
            var unitName = Regex.Replace(Plugin.Instance.Config.UnitName, "%RandomName%", TeamManager.Get.GenerateNtfUnitName(), RegexOptions.IgnoreCase);

            RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit()
            {
                SpawnableTeam = 2,
                UnitName = unitName
            });
            return unitName;
        }

        const uint AmountCommander = 1;
        const uint AmountLieutenant = 3;

        public override void Spawn(List<Player> players)
        {
            for(var i = 0; i < players.Count; i++)
            {
                if (i < AmountCommander)
                    players[i].RoleID = ;//roleID of your commander
                else if (i < AmountLieutenant + AmountCommander)
                    players[i].RoleID = ;//roleID of your Lieutenant
                else
                    players[i].RoleID = ;//roleID of your Recruit
            }
        }
    }
}
