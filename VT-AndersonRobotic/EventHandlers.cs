using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;
using VT_Referance.Variable;

namespace VT_AndersonRobotic
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += SetClass;
            Server.Get.Events.Round.TeamRespawnEvent += Respawn;
        }

        private void Respawn(TeamRespawnEventArgs ev)
        {
            if (ev.Team == Respawning.SpawnableTeamType.ChaosInsurgency /*&& UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance*/)
                ev.TeamID = (int)TeamID.AndersneRobotic;
        }

        private void SetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Player.RoleID == (int)RoleID.AndersonUnite)
            {
                ev.Position = Plugin.Config.SpawnPoint.Parse().Position;
                ev.Items = Plugin.Config.Items.Select(x => x.Parse()).ToList();
            }
        }
    }
}