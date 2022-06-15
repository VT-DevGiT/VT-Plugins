using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Api.Extension;

namespace VT939
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerDamageEvent += OnDomage;
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
                ev.Player.GetOrAddComponent<Scp939Controller>().enabled = true;
            else if (ev.Player. TryGetComponent<Scp939Controller>(out var ctr) && ctr.enabled)
                ctr.enabled = false;
        }

        public void OnDomage(PlayerDamageEventArgs ev)
        {
            if (!ev.Allow)
                return;
            var bh = ev.Victim?.GetComponent<Scp939Controller>();
            
            if (bh != null)
            {
                ev.Allow = false;
                
                if (ev.DamageType != DamageType.Scp207)
                    ev.Victim.Health = ev.Damage < 0 ? 0 : ev.Victim.Health - ev.Damage;

                if (!bh.excludedDamages.Contains(ev.DamageType))
                {
                    bh.AngerMeter += ev.Damage;
                }
                else
                {
                    ev.Damage = 0;
                    return;
                }

                ev.Damage = 0;

                if (bh.AngerMeter > Plugin.Instance.Config.AngerMeterMaximum)
                    bh.AngerMeter = Plugin.Instance.Config.AngerMeterMaximum;

                ev.Victim.ArtificialHealth = bh.AngerMeter / Plugin.Instance.Config.AngerMeterMaximum * ev.Victim.MaxArtificialHealth;

                if (bh.angerMeterDecayCoroutine == null || !bh.angerMeterDecayCoroutine.IsRunning)
                    bh.angerMeterDecayCoroutine = Timing.RunCoroutine(bh.AngerMeterDecay(Plugin.Instance.Config.AngerMeterDecayTime), Segment.FixedUpdate);
            }
            else
            {
                bh = ev.Killer?.GetComponent<Scp939Controller>();
                if (bh != null && ev.Damage > 0)
                {
                    ev.Damage = Plugin.Instance.Config.BaseDamage + (bh.AngerMeter / Plugin.Instance.Config.AngerMeterMaximum) * Plugin.Instance.Config.BonusAttackMaximum;

                    bh.forceSlowDownCoroutine = Timing.RunCoroutine(bh.ForceSlowDown(Plugin.Instance.Config.ForceSlowDownTime, Scp939Controller.forceSlowDownInterval), Segment.FixedUpdate);
                }
            }
        }
    }
}