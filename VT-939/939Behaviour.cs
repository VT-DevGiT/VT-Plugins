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
    public class BetterScp939 : RepeatingBehaviour
    {
        public Player player;
        public Scp207 scp207;
        public SinkHole sinkHole;
        public DamageType[] excludedDamages;
        public CoroutineHandle forceSlowDownCoroutine;
        public CoroutineHandle angerMeterDecayCoroutine;
        public const float forceSlowDownInterval = 0.1f;

        public float AngerMeter { get; internal set; }

        private void Init()
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
            sinkHole.slowAmount = Plugin.Instance.Config.SlowAmount;
        }

        protected override void Start()
        {
            Init();
            base.Start();
        }

        protected override void OnEnable()
        {
            Timing.CallDelayed(1f, () =>
            {
                player.Scale *= Plugin.Instance.Config.Size;
                AngerMeter = Plugin.Instance.Config.StartingAnger;
            });


            if (Plugin.Instance.Config.ShowSpawnBroadcastMessage)
            {
                player.SendBroadcast(Plugin.Instance.Config.SpawnBroadcastMessageDuration, string.Format(Plugin.Instance.Translation.ActiveTranslation.SpawnMessage, Plugin.Instance.Config.ForceSlowDownTime));
            }
            base.OnEnable();
        }

        protected override void BehaviourAction()
        {
            if (!scp207.enabled && !sinkHole.enabled && Plugin.Instance.Config.IsFasterThanHumans)
                player.GiveEffect(Effect.Scp207);
        }

        protected override void OnDisable()
        {
            KillCoroutines();

            if (player == null)
                return;

            scp207.Disabled();
            sinkHole.Disabled();

            AngerMeter = 0;
            player.ArtificialHealth = 0;
            player.Scale = new Vector3(1, 1, 1);
            base.OnDisable();
        }

        private void OnDestroy()
        {
            OnDisable();
            Kill();
        }

        internal IEnumerator<float> ForceSlowDown(float totalWaitTime, float interval)
        {
            var waitedTime = 0f;

            scp207.Disabled();

            while (waitedTime < totalWaitTime)
            {
                if (!sinkHole.enabled && Plugin.Instance.Config.ShouldGetSlowed)
                    player.GiveEffect(Effect.SinkHole);

                waitedTime += interval;

                yield return Timing.WaitForSeconds(interval);
            }

            sinkHole.Disabled();
        }

        internal IEnumerator<float> AngerMeterDecay(float waitTime)
        {
            while (AngerMeter > 0)
            {
                yield return Timing.WaitForSeconds(waitTime);

                AngerMeter -= Plugin.Instance.Config.AngerMeterDecayValue;

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