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
            List<SerializedMapPoint> SpawnPoints = new List<SerializedMapPoint>()
            {
                new SerializedMapPoint("Root_*&*Outside Cams", 187.5507f, -8.07251f, -6.48763f),
                new SerializedMapPoint("Root_*&*Outside Cams", 185.9299f, -8.444763f, -1.784706f),
                new SerializedMapPoint("Root_*&*Outside Cams", 183.4525f, -8.931152f, -1.332999f),
                new SerializedMapPoint("Root_*&*Outside Cams", 180.3424f, -9.680847f, -1.697027f),
                new SerializedMapPoint("Root_*&*Outside Cams", 177.6069f, -10.34033f, -1.783141f),
                new SerializedMapPoint("Root_*&*Outside Cams", 175.2793f, -10.65308f, -1.589358f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
                new SerializedMapPoint("Root_*&*Outside Cams", 172.6244f, -12.77948f, 9.865286f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.8324f, -12.73206f, 7.958255f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.9952f, -12.05273f, 5.345505f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.7792f, -11.40039f, 2.836424f),
                new SerializedMapPoint("Root_*&*Outside Cams", 174.6662f, -11.2254f, 2.163314f),
            };
            SerializedMapPoint SpawnPointChef = new SerializedMapPoint("Root_*&*Outside Cams", 187.6646f, -5.909363f, -28.50043f);
            Player playerChef = players.First(p => Plugin.Config.SpawnNeedRank.Contains(p.RankName));
            if (playerChef != null)
                players.Remove(playerChef);
            if (Plugin.Config.SpawnSize != 0 && players.Count > (Plugin.Config.SpawnSize - 1))
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
                int rnd = UnityEngine.Random.Range(0, 12);
                if (playerChef != null)
                { 
                    playerChef.RoleID = (int)RoleID.U2IAgentLiaison;
                    playerChef.Position = SpawnPointChef.Parse().Position;
                }
                foreach (var player in players)
                {
                    player.RoleID = (int)RoleID.U2IAgent;
                    player.Position = SpawnPoints[rnd].Parse().Position;
                    player.UnitName = Unitname;
                }
            }
        }
    }
}
