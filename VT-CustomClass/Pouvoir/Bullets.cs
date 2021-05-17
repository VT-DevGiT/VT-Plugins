using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTCustomClass
{
    public static class Bullets
    {
        public static void HollowBullet(this PlayerDamageEventArgs ev, Player player)
        {
            if(ev.Killer == player && ev.Victim.IsUTR())
            {
                if (ev.Victim.TeamID == (int)TeamID.SCP || ev.Victim.ArtificialHealth != 0) 
                {
                    ev.Victim.GiveEffect(Effect.Disabled, 1, 4);
                }
                else
                {
                    ev.Victim.GiveEffect(Effect.Disabled, 3, 10);
                }
                ev.Victim.GiveEffect(Effect.Amnesia, 1, 2);
                ev.DamageAmount = ev.DamageAmount / 1.5f;
            }
        }
        public static void ChemicalBullet(this PlayerDamageEventArgs ev, Player player)
        {
            if (ev.Killer == player && ev.Victim.IsUTR())
            { 
                if (ev.Victim.ArtificialHealth == 0)
                {
                    ev.Victim.GiveEffect(Effect.Burned, 1, 10);
                    ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 10);
                    ev.Victim.GiveEffect(Effect.Concussed, 1, 10);
                    ev.Victim.GiveEffect(Effect.Poisoned, 1, 10);
                }
                ev.DamageAmount = ev.DamageAmount / 2;
            }
        }
    }




}
