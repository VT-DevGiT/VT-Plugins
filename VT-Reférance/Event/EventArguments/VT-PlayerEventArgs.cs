using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Event.EventArguments
{
    public class PlayerDamagePostEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Killer { get; internal set; }
        public Player Victim { get; internal set; }

        public float DamageAmount
        {
            get => HitInfo.Amount;
            set
            {
                var info = HitInfo;
                info.Amount = value;
                HitInfo = info;
            }
        }

        public PlayerStats.HitInfo HitInfo { get; set; }
        public bool Allow { get; set; }
    }
}
