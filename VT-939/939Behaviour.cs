using CustomPlayerEffects;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Core.Behaviour;

namespace VT939
{
    //https://github.com/iopietro/BetterScp939/releases/tag/1.0.7
    public class Scp939Controller : RepeatingBehaviour
    {
        private Player player;
        private Scp207 scp207;
        private SinkHole sinkHole;
        private DamageType[] excludedDamages;
        private CoroutineHandle forceSlowDownCoroutine;
        private CoroutineHandle angerMeterDecayCoroutine;
        private const float forceSlowDownInterval = 0.1f;

        public float AngerMeter { get; private set; }

        private void init()
        {
            player = gameObject.GetPlayer();
            scp207 = player.Hub.playerEffectsController.GetEffect<Scp207>();
            sinkHole = player.Hub.playerEffectsController.GetEffect<SinkHole>();
            excludedDamages = new DamageType[]
            {
                DamageType.Tesla,
                DamageType.Falldown,
                DamageType.Warhead,
                DamageType.Crushed,
                DamageType.Recontainment,
                DamageType.Recontainment,
                DamageType.Scp207,
                DamageType.Unknown
            };
            AngerMeter = Plugin.Config.StartingAnger;
            sinkHole.slowAmount = Plugin.Config.SlowAmount;
        }

        protected override void Start()
        {
            init();
            base.Start();
        }

        protected override void OnEnable()
        {
            RegisterEvents();
            player.Scale *= Plugin.Config.Size;

            if (Plugin.Config.ShowSpawnBroadcastMessage)
            {
                player.SendBroadcast(Plugin.Config.SpawnBroadcastMessageDuration, string.Format(Plugin.Config.SpawnBroadcastMessage, Plugin.Config.ForceSlowDownTime));
            }
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            if (!scp207.enabled && !sinkHole.enabled && Plugin.Config.IsFasterThanHumans)
                player.GiveEffect(Effect.Scp207);
        }

        public void OnDomage(PlayerDamageEventArgs ev)
        {
            if (ev.Victim == player)
            {
                if (ev.DamageType != DamageType.Scp207)
                    player.Health += ev.Damage < 0 ? -9999999f : - ev.Damage;

                if (!excludedDamages.Contains(ev.DamageType))
                {
                    AngerMeter += ev.Damage;
                }
                else
                {
                    ev.Damage = 0;
                    return;
                }

                ev.Damage = 0;

                if (AngerMeter > Plugin.Config.AngerMeterMaximum)
                    AngerMeter = Plugin.Config.AngerMeterMaximum;

                player.ArtificialHealth = (byte)(AngerMeter / Plugin.Config.AngerMeterMaximum * player.MaxArtificialHealth);

                if (!angerMeterDecayCoroutine.IsRunning)
                    angerMeterDecayCoroutine = Timing.RunCoroutine(AngerMeterDecay(Plugin.Config.AngerMeterDecayTime), Segment.FixedUpdate);
            }
            else if (ev.Killer == player && ev.Damage > 0)
            {
                ev.Damage = Plugin.Config.BaseDamage + (AngerMeter / Plugin.Config.AngerMeterMaximum) * Plugin.Config.BonusAttackMaximum;

                forceSlowDownCoroutine = Timing.RunCoroutine(ForceSlowDown(Plugin.Config.ForceSlowDownTime, forceSlowDownInterval), Segment.FixedUpdate);
            }
        }

        protected override void OnDisable()
        {
            UnregisterEvents();
            KillCoroutines();

            if (player == null)
                return;

            scp207.Disabled();
            sinkHole.Disabled();

            AngerMeter = 0;

            player.Scale = new Vector3(1, 1, 1);
            player.ArtificialHealth = 0;
            base.OnDisable();
        }

        private void OnDestroy()
        {
            OnDisable();
            Kill();
        }

        private void RegisterEvents()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDomage;
        }
        private void UnregisterEvents()
        {
            Server.Get.Events.Player.PlayerDamageEvent -= OnDomage;
        }

        private IEnumerator<float> ForceSlowDown(float totalWaitTime, float interval)
        {
            var waitedTime = 0f;

            scp207.Disabled();

            while (waitedTime < totalWaitTime)
            {
                if (!sinkHole.enabled && Plugin.Config.ShouldGetSlowed)
                    player.GiveEffect(Effect.SinkHole);

                waitedTime += interval;

                yield return Timing.WaitForSeconds(interval);
            }

            sinkHole.Disabled();

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