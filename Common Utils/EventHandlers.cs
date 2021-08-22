using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace Common_Utiles
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.Scp914ActivateEvent += On914Activate;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart() => CommonUtiles.Instance.RespawnAllow = true;

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            if (!CommonUtiles.Instance.RespawnAllow)
                ev.Allow = false;
        }
        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Spectator && ev.Player.Scale != new Vector3(1, 1, 1))
                Timing.CallDelayed(0.5f, () => ev.Player.Scale = new Vector3(1, 1, 1));
            SerializedPlayerInventory nullSerializedPlayerInventory = new SerializedPlayerInventory();
            Timing.CallDelayed(1f, () =>
            {
                SerializedConfigClass config = CommonUtiles.Config.configClasses.Where(x => x.ClassID == ev.Player.RoleID).FirstOrDefault();
                config?.Applied(ev.Player);
            });
        }

        private void On914Activate(Scp914ActivateEventArgs ev)
        {
            if (!ev.Players.Any())
                return;

            foreach (var player in ev.Players)
            {
                if (CommonUtiles.Config.Rnd914Size)
                {
                    float newScaleX = UnityEngine.Random.Range(CommonUtiles.Config.Min914SizeX, CommonUtiles.Config.Max914SizeX);
                    float newScaleY = UnityEngine.Random.Range(CommonUtiles.Config.Min914SizeY, CommonUtiles.Config.Max914SizeY);
                    float newScaleZ = UnityEngine.Random.Range(CommonUtiles.Config.Min914SizeZ, CommonUtiles.Config.Max914SizeZ);
                    player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
                }
                if (CommonUtiles.Config.list914Effect.Any())
                {
                    Effect effect = CommonUtiles.Config.list914Effect[UnityEngine.Random.Range(0, CommonUtiles.Config.list914Effect.Count() - 1)];
                    player.GiveEffect(effect);
                }
                if (CommonUtiles.Config.Rnd914Life)
                {
                    float newLif = UnityEngine.Random.Range(CommonUtiles.Config.Min914Life, CommonUtiles.Config.Max914Life);
                    player.Health = newLif;
                }
                if (CommonUtiles.Config.Rnd914ArtificialLife)
                {
                    ushort newLif = (ushort)UnityEngine.Random.Range(CommonUtiles.Config.Min914ArtificialLife, CommonUtiles.Config.Max914ArtificialLife);
                    player.ArtificialHP = newLif;
                }
                if (CommonUtiles.Config.Rnd914ChanceDie != 0)
                {
                    float Rnd = UnityEngine.Random.Range(0, 100);
                    if (Rnd >= CommonUtiles.Config.Rnd914ChanceDie)
                        player.Kill();
                }
            }
        }
    }
}