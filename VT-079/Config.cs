using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace VT079
{
    public class Config : AbstractConfigSection
    {

        [Description("Price for changed camera")]
        public float Scp079CostCamera = 0f;

        [Description("Price for lock a door")]
        public float Scp079CostLock = 5f;

        [Description("Price for start lock a door")]
        public float Scp079CostLockStart = 5f;

        [Description("Energy needed to lock the door (do not use energy)")]
        public float Scp079ConstLockMinimum = 15f;

        [Description("Prices for open a basic door")]
        public float Scp079CostDoorDefault = 5f;

        [Description("Price to open a containment door lv 1")]
        public float Scp079CostDoorContlv1 = 50f;

        [Description("Price to open a containment door lv 2")]
        public float Scp079CostDoorContlv2 = 40f;

        [Description("Price to open a containment door lv 3")]
        public float Scp079CostDoorContlv3 = 110f;

        [Description("Price to open a lv 1 armory")]
        public float Scp079CostDoorArmlv1 = 50f;

        [Description("Price to open a lv 2 armory")]
        public float Scp079CostDoorArmlv2 = 60f;

        [Description("Price to open a lv 3 armory")]
        public float Scp079CostDoorArmlv3 = 70f;

        [Description("Prix for open a Gate)")]
        public float Scp079CostDoorExit = 60f;

        [Description("Prix for open the intercom")]
        public float Scp079CostDoorIntercom = 30f;

        [Description("Prix for open the Checkpoint")]
        public float Scp079CostDoorCheckpoint = 10f;

        [Description("Prix for a blackout")]
        public float Scp079CostLockDown = 60f;

        [Description("Prix for use a teslas")]
        public float Scp079CostTesla = 65f;

        [Description("price to change floor")]
        public float Scp079CostElevatorTeleport = 0f;

        [Description("Prix for use eleveator")]
        public float Scp079CostElevatorUse = 10f;

        [Description("Prix for start use a speaker")]
        public float Scp079CostSpeakerStart = 25f;

        [Description("Prix for use a speaker")]
        public float Scp079CostSpeakerUpdate = 0f;

        [Description("manually confined from scp 079")]
        public bool Scp079AdvenceRecontain = true;

        [Description("The list of SCPs that 079 can confined for the Deconf command of VT-079Command")]
        public List<int> Scp079ScpDeconf = new List<int>() { (int)RoleID.SCP008, (int)RoleID.Scp173, (int)RoleID.SCP966, (int)RoleID.Scp93989
            , (int)RoleID.Scp93953, (int)RoleID.Scp106, (int)RoleID.Scp049, (int)RoleID.Scp096};
    }
}
