using Synapse.Config;
using System.ComponentModel;

namespace VT079
{
    public class Config : AbstractConfigSection
    {

        [Description("Prix pour changée de camera")]
        public float Scp079CostCamera = 0f;

        [Description("Prix pour gardée lock une port")]
        public float Scp079CostLock = 5f;

        [Description("Prix de base pour lock")]
        public float Scp079CostLockStart = 5f;

        [Description("Energie besoin pour lock")]
        public float Scp079ConstLockMinimum = 15f;

        [Description("Prix pour open porte de base")]
        public float Scp079CostDoorDefault = 5f;

        [Description("Prix pour open porte de lv 1")]
        public float Scp079CostDoorContlv1 = 50f;

        [Description("Prix pour open porte de lv 2)")]
        public float Scp079CostDoorContlv2 = 40f;

        [Description("Prix pour open porte de lv 3")]
        public float Scp079CostDoorContlv3 = 110f;

        [Description("Prix pour open l'armurie")]
        public float Scp079CostDoorArmlv1 = 50f;

        [Description("Prix pour open l'armurie Lv 2)")]
        public float Scp079CostDoorArmlv2 = 60f;

        [Description("Prix pour open l'armurie lv 3")]
        public float Scp079CostDoorArmlv3 = 70f;

        [Description("Prix pour open Gate)")]
        public float Scp079CostDoorExit = 60f;

        [Description("Prix pour open l'intercom")]
        public float Scp079CostDoorIntercom = 30f;

        [Description("Prix pour open le Checkpoint")]
        public float Scp079CostDoorCheckpoint = 10f;

        [Description("Prix de vérouilage")]
        public float Scp079CostLockDown = 60f;

        [Description("Prix pour les teslas")]
        public float Scp079CostTesla = 65f;

        [Description("Prix pour changée d'étage")]
        public float Scp079CostElevatorTeleport = 0f;

        [Description("Prix pour activer le gaz")]
        public int Scp079ExtendCostGaz = 30;

        [Description("Prix pour un assenceur")]
        public float Scp079CostElevatorUse = 10f;

        [Description("Prix pour un haut-parler")]
        public float Scp079CostSpeakerStart = 25f;

        [Description("Prix pour parlée avec un haut-parler")]
        public float Scp079CostSpeakerUpdate = 0f;

        [Description("Si 079 doit être reconfinée manuellement")]
        public bool Scp079AdvenceRecontain = true;
    }
}
