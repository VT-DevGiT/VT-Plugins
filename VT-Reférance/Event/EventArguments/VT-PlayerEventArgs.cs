using Synapse.Api;
using Synapse.Permission;
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

    public class PlayerDestroyEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
    }

    public class PlayerVerifEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
    }

    public class PlayerSpeakIntercomEventEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; set; }
        public bool Allow { get; set; }
    }
}
