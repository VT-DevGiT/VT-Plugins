using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Referance.Variable;

namespace VTGrenad
{
    public class Config : AbstractConfigSection
    {

        [Description("time before priming")]
        public float Time = 5;

        [Description("list of roles that do not have the right to use advenced grenades")]

        public List<int> NotAGrenadeRole = new List<int>
        {
            (int)RoleType.ClassD,
            (int)RoleType.Scientist,
            (int)RoleType.Spectator,
            (int)RoleID.FacilityGuard,
            (int)RoleID.Concierge,
            (int)RoleID.ScientifiqueSuperviseur,
            (int)RoleID.DirecteurSite
        };
        
        [Description("Config grenade")]
        public bool FlashbangFuseWithCollision = true;
        public bool ChaineFuseFragGrenad = true;

        [Description("If you want the Flashgrenade can be activated remotely")]
        public bool FlashRemot = true;

        [Description("If you want the Flashgrenade add new effect")]
        public bool BadFlash = true;

        [Description("The bindkey for .Boom")]
        public KeyCode Key = KeyCode.B;
    }
}
