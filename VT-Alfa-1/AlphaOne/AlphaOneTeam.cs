using Respawning;
using Respawning.NamingRules;
using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_Alpha
{
    [SynapseTeamInformation(
        ID = (int)TeamID.AL1,
        Name = "Alfa-1 Agent"
        )]
    public class AlphaOneTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            if (players.Any())
            { 
                string Unitname = Methods.GenerateNtfUnitName();
                RespawnManager.Singleton.NamingManager.AllUnitNames.Add(new SyncUnit()
                {
                    SpawnableTeam = 2,
                    UnitName = Plugin.Config.UnitName.Replace("%RandomName%", Unitname)
                });
                if (!string.IsNullOrWhiteSpace(Plugin.Config.CassieSpawn))
                {
                    string SpawnCassie = Plugin.Config.CassieSpawn.Replace("%UnitName%", Unitname.Replace("-", " "));
                    Map.Get.GlitchedCassie(SpawnCassie);
                }
                foreach (var player in players)
                {
                    player.RoleID = (int)RoleID.AlphaOneAgent;
                    player.UnitName = Unitname;
                    players.Remove(player);
                }
            }
        }
    }
}
