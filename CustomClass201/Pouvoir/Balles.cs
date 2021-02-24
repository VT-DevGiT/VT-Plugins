using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClass
{
    public static class Balles
    {
        public static void PointeCreuses(this PlayerDamageEventArgs ev, Player player)
        {
            if(ev.Killer == player)
            {
                if (ev.Victim.Team == Team.SCP || ev.Victim.ArtificialHealth != 0) 
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
        public static void Destabilisantes(this PlayerDamageEventArgs ev, Player player)
        {
            if (ev.Killer == player)
            { 
                if (ev.Victim.ArtificialHealth == 0)
                {
                    ev.Victim.GiveEffect(Effect.Burned, 1, 10);
                    ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 10);
                    ev.Victim.GiveEffect(Effect.Concussed, 1, 10);
                    ev.Victim.GiveEffect(Effect.Poisoned, 1, 10);
                }
            }
            ev.DamageAmount = ev.DamageAmount / 2;
        }

    }
}
