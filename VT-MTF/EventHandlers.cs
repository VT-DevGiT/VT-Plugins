using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Linq;
using VT_Referance.Variable;
using VT_Referance.Method;

namespace VT_HammerDown
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Player.TeamID == (int)TeamID.CDM)
            {
                
                switch (ev.Player.RoleID)
                {
                    case (int)RoleID.CdmCadet:
                        ev.Items = Plugin.Config.ItemsCadet.Select(x => x.Parse()).ToList();
                        break;
                    case (int)RoleID.CdmLieutenant:
                        ev.Items = Plugin.Config.ItemsLieutenant.Select(x => x.Parse()).ToList();
                        break;
                    case (int)RoleID.CdmCommander:
                        ev.Items = Plugin.Config.ItemsCadet.Select(x => x.Parse()).ToList();
                        break;
                }
            }
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (ev.Team == Respawning.SpawnableTeamType.NineTailedFox) //&& UnityEngine.Random.Range(1f, 100f) <= Plugin.Config.SpawnChance)
                ev.TeamID = (int)TeamID.CDM;
        }
    }
}