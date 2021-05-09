using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
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
            int SpawnAgentLaison = 0;
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
            SerializedMapPoint SpawnPoint = new SerializedMapPoint("Root_*&*Outside Cams", 187.6646f, -5.909363f, -28.50043f);
            List<Player> players1 = players.Where(p => p.RankName != Plugin.Config.SpawnNeedRank).ToList();

            if (players.Count > Plugin.Config.SpawnSize)
                players = players1.GetRange(0, Plugin.Config.SpawnSize + players1.Count);
            
            if (players.Any())
            {
                if(!string.IsNullOrWhiteSpace(Plugin.Config.CassieSpawn))
                    Map.Get.Cassie(Plugin.Config.CassieSpawn, false, true);
                int rnd = UnityEngine.Random.Range(0, 12);
                
                foreach (var player in players)
                {
                    if (player.RankName == Plugin.Config.SpawnNeedRank && SpawnAgentLaison > Plugin.ConfigU2IAgentLiaison.MaxRespawn)
                    {
                        SpawnAgentLaison++;
                        player.RoleID = (int)RoleID.U2IAgentLiaison;
                        player.Position = SpawnPoint.Parse().Position;
                    }
                    else
                    {
                        player.RoleID = (int)RoleID.U2IAgent;
                        player.Position = SpawnPoints[rnd].Parse().Position;
                    }
                }
            }
        }
    }
}
