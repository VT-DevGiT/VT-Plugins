using Respawning;
using Respawning.NamingRules;
using Synapse.Api;
using Synapse.Api.Teams;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VT_Api.Core.Enum;
using VT_Api.Core.Teams;

namespace VT_HammerDown
{
    [SynapseTeamInformation(
        ID = (int)TeamID.CDM,
        Name = "HammerDown"
        )]
    public class HammerDownTeam : AbstractTeam
    {
        public override List<RespawnRoleInfo> Roles { get; set; }

        public override string GetNewUniteName()
        {
            string unitName = VtController.Get.Team.GenerateNtfUnitName();
            RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit()
            {
                SpawnableTeam = 2,
                UnitName = Regex.Replace(Plugin.Config.UnitName, "%RandomName%", unitName, RegexOptions.IgnoreCase)
            });
            return unitName;
        }

        public override string GetSpawnAnnonce(string uniteName)
        {
            if (!string.IsNullOrWhiteSpace(Plugin.Config.CassieSpawn))
                return Regex.Replace(Plugin.Config.CassieSpawn, "%UnitName%", uniteName.Replace("-", " "), RegexOptions.IgnoreCase);
            return "";
        }

        public override void Initialise()
        {
            Roles = new List<RespawnRoleInfo>()
            {
                new RespawnRoleInfo((int)RoleID.CdmCommander, 2, 1),
                new RespawnRoleInfo((int)RoleID.CdmLieutenant, 1, Plugin.ConfigHammerDownLieutenant.MaxRespawn),
                new RespawnRoleInfo((int)RoleID.CdmCadet),
            }; 
        }
    }
}
