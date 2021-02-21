using CustomPlayerEffects;
using MEC;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT939
{
    //https://github.com/iopietro/BetterScp939/releases/tag/1.0.7
    public class Scp939Controller : NetworkBehaviour
    {
        private Player player;
        private Scp207 scp207;
        private SinkHole sinkHole;
        private List<DamageTypes.DamageType> excludedDamages;
        private CoroutineHandle forceSlowDownCoroutine;
        private CoroutineHandle angerMeterDecayCoroutine;
        private const float forceSlowDownInterval = 0.1f;

        public float AngerMeter { get; private set; }

        private void Awake()
        {
            RegisterEvents();

            player = gameObject.GetPlayer();
            scp207 = player.Hub.playerEffectsController.GetEffect<Scp207>();
            sinkHole = player.Hub.playerEffectsController.GetEffect<SinkHole>();
            excludedDamages = new List<DamageTypes.DamageType>()
            {
                DamageTypes.Tesla,
                DamageTypes.Wall,
                DamageTypes.Nuke,
                DamageTypes.RagdollLess,
                DamageTypes.Contain,
                DamageTypes.Lure,
                DamageTypes.Recontainment,
                DamageTypes.Scp207,
                DamageTypes.None
            };
            AngerMeter = Plugin.Config.StartingAnger;
            sinkHole.slowAmount = Plugin.Config.SlowAmount;
        }

        private void Start()
        {
            player.Scale *= Plugin.Config.Size;

            if (Plugin.Config.ShowSpawnBroadcastMessage)
            {
                player.SendBroadcast(Plugin.Config.SpawnBroadcastMessageDuration, string.Format(Plugin.Config.SpawnBroadcastMessage, Plugin.Config.ForceSlowDownTime));
            }
        }

        private void Update()
        {
            if (player == null || !player.RoleType.Is939())
            {
                Destroy();
                return;
            }

            if (!scp207.Enabled && !sinkHole.Enabled && Plugin.Config.IsFasterThanHumans)
                player.GiveEffect(Synapse.Api.Enum.Effect.Scp207);
        }

        public void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == player)
            {
                if (ev.HitInfo.GetDamageType() != DamageTypes.Scp207)
                    player.Health += ev.DamageAmount < 0 ? -9999999f : -ev.DamageAmount;

                if (!excludedDamages.Contains(ev.HitInfo.GetDamageType()))
                {
                    AngerMeter += ev.DamageAmount;
                }
                else
                {
                    ev.DamageAmount = 0;
                    return;
                }

                ev.DamageAmount = 0;

                if (AngerMeter > Plugin.Config.AngerMeterMaximum)
                    AngerMeter = Plugin.Config.AngerMeterMaximum;

                player.ArtificialHealth = (byte)(AngerMeter / Plugin.Config.AngerMeterMaximum * player.MaxArtificialHealth);

                if (!angerMeterDecayCoroutine.IsRunning)
                    angerMeterDecayCoroutine = Timing.RunCoroutine(AngerMeterDecay(Plugin.Config.AngerMeterDecayTime), Segment.FixedUpdate);
            }
            else if (ev.Killer == player && ev.DamageAmount > 0)
            {
                ev.DamageAmount = Plugin.Config.BaseDamage + (AngerMeter / Plugin.Config.AngerMeterMaximum) * Plugin.Config.BonusAttackMaximum;

                forceSlowDownCoroutine = Timing.RunCoroutine(ForceSlowDown(Plugin.Config.ForceSlowDownTime, forceSlowDownInterval), Segment.FixedUpdate);
            }
        }

        private void OnDestroy() => PartiallyDestroy();

        public void PartiallyDestroy()
        {
            UnregisterEvents();
            KillCoroutines();

            if (player == null)
                return;

            scp207.ServerDisable();
            sinkHole.ServerDisable();

            AngerMeter = 0;

            player.Scale = new Vector3(1, 1, 1);
            player.ArtificialHealth = 0;
        }

        public void Destroy()
        {
            try
            {
                Destroy(this);
            }
            catch (Exception exception)
            {
                Synapse.Api.Logger.Get.Info($"Error, cannot destroy: {exception}");
            }
        }

        private void RegisterEvents() => Server.Get.Events.Player.PlayerDamageEvent += OnDomage;

        private void UnregisterEvents() => Server.Get.Events.Player.PlayerDamageEvent -= OnDomage;

        private IEnumerator<float> ForceSlowDown(float totalWaitTime, float interval)
        {
            var waitedTime = 0f;

            scp207.ServerDisable();

            while (waitedTime < totalWaitTime)
            {
                if (!sinkHole.Enabled && Plugin.Config.ShouldGetSlowed)
                    player.GiveEffect(Synapse.Api.Enum.Effect.SinkHole);

                waitedTime += interval;

                yield return Timing.WaitForSeconds(interval);
            }

            sinkHole.ServerDisable();

            if (Plugin.Config.ResetAngerAfterHitSlowDown)
                AngerMeter = player.ArtificialHealth = 0;
        }

        private IEnumerator<float> AngerMeterDecay(float waitTime)
        {
            while (AngerMeter > 0)
            {
                player.ArtificialHealth = (byte)(AngerMeter / Plugin.Config.AngerMeterMaximum * player.ArtificialHealth);

                yield return Timing.WaitForSeconds(waitTime);

                AngerMeter -= Plugin.Config.AngerMeterDecayValue;

                if (AngerMeter < 0)
                    AngerMeter = 0;
            }
        }

        private void KillCoroutines()
        {
            if (forceSlowDownCoroutine.IsRunning)
                Timing.KillCoroutines(forceSlowDownCoroutine);

            if (angerMeterDecayCoroutine.IsRunning)
                Timing.KillCoroutines(angerMeterDecayCoroutine);
        }
    }

}