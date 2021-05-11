using Respawning;
using Respawning.NamingRules;
using Synapse.Api;
using Synapse.Api.Teams;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    [SynapseTeamInformation(
        ID = (int)TeamID.CDM,
        Name = "HammerDown"
        )]
    public class HammerDownTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
        {            
            if (players.Count > Plugin.Config.SpawnSize)
                players = players.GetRange(0, Plugin.Config.SpawnSize);
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

                //random Commander
                int chance = players.Count() > 1? UnityEngine.Random.Range(0, players.Count() - 1) : 0;
                var commander = players[chance];
                commander.RoleID = (int)RoleID.CdmCommander;
                commander.UnitName = Unitname;
                players.Remove(commander);

                // Spawn lieutenant
                int nbLieu = 0;
                while (players.Any() && nbLieu < Plugin.ConfigHammerDownLieutenant.MaxRespawn)
                {
                    int chanceLieu = UnityEngine.Random.Range(0, players.Count() - 1);
                    var lieutenant = players[chanceLieu];
                    lieutenant.RoleID = (int)RoleID.CdmLieutenant;
                    lieutenant.UnitName = Unitname;
                    players.Remove(lieutenant);
                    nbLieu++;
                }

                //For all last player
                foreach(var player in players)
                {
                    player.RoleID = (int)RoleID.CdmCadet;
                    player.UnitName = Unitname;
                    players.Remove(player);
                }
            }
        }
    }
}
