using Respawning;
using Respawning.NamingRules;
using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_U2I
{
    [SynapseTeamInformation(
        ID = (int)TeamID.U2I,
        Name = "U2I"
        )]
    public class U2ITeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {
            Player playerChef = players.FirstOrDefault(p => Plugin.Config.SpawnNeedRank.Contains(p.RankName));
            if (Plugin.Config.SpawnSize != 0 && players.Count > Plugin.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Config.SpawnSize - 1);

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
                if (playerChef != null)
                {
                    players.Remove(playerChef);
                    playerChef.RoleID = (int)RoleID.U2IAgentLiaison;
                    playerChef.UnitName = Unitname;
                }
                foreach (var player in players)
                {
                    player.RoleID = (int)RoleID.U2IAgent;
                    player.UnitName = Unitname;
                }
            }
        }
    }
}
