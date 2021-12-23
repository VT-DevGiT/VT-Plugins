using Synapse.Api;
using Synapse.Api.Items;
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

        public float Damage { get; set; }

        public SynapseItem Weapon { get; internal set; }

        public ItemType WeaponType { get; internal set; }
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

    public class PlayerSetClassEventArgs : Synapse.Api.Events.EventHandler.ISynapseEventArgs
    {
        public Player Player { get; internal set; }
        public int OldID { get; internal set; }
        public int NewID { get; internal set; }
    }
}
