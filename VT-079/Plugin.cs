using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace VT079
{
    [PluginInformation(
Name = "VT-079",
Author = "VT",
Description = "Sanya 079ExHud for SynapseSl",
LoadPriority = 15,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "079v.1.3.1 079Commandv1.3.1"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public static List<int> SCPRoleDeconf = new List<int>() { 5, 9, 3, 0, 16, 17, 56 };

        [Synapse.Api.Plugin.Config(section = "VT-079")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            new EventHandlers();
        }

        internal void ChangeCoutUnScripte(Scp079PlayerScript player079Script)
        {
            foreach (Scp079PlayerScript.Ability079 ability in player079Script.abilities)
            {
                switch (ability.label)
                {
                    case "Camera Switch":
                        ability.mana = Plugin.Config.Scp079CostCamera;
                        break;
                    case "Door Lock":
                        ability.mana = Plugin.Config.Scp079CostLock;
                        break;
                    case "Door Lock Start":
                        ability.mana = Plugin.Config.Scp079CostLockStart;
                        break;
                    case "Door Lock Minimum":
                        ability.mana = Plugin.Config.Scp079ConstLockMinimum;
                        break;
                    case "Door Interaction DEFAULT":
                        ability.mana = Plugin.Config.Scp079CostDoorDefault;
                        break;
                    case "Door Interaction CONT_LVL_1":
                        ability.mana = Plugin.Config.Scp079CostDoorContlv1;
                        break;
                    case "Door Interaction CONT_LVL_2":
                        ability.mana = Plugin.Config.Scp079CostDoorContlv2;
                        break;
                    case "Door Interaction CONT_LVL_3":
                        ability.mana = Plugin.Config.Scp079CostDoorContlv3;
                        break;
                    case "Door Interaction ARMORY_LVL_1":
                        ability.mana = Plugin.Config.Scp079CostDoorArmlv1;
                        break;
                    case "Door Interaction ARMORY_LVL_2":
                        ability.mana = Plugin.Config.Scp079CostDoorArmlv2;
                        break;
                    case "Door Interaction ARMORY_LVL_3":
                        ability.mana = Plugin.Config.Scp079CostDoorArmlv3;
                        break;
                    case "Door Interaction EXIT_ACC":
                        ability.mana = Plugin.Config.Scp079CostDoorExit;
                        break;
                    case "Door Interaction INCOM_ACC":
                        ability.mana = Plugin.Config.Scp079CostDoorIntercom;
                        break;
                    case "Door Interaction CHCKPOINT_ACC":
                        ability.mana = Plugin.Config.Scp079CostDoorCheckpoint;
                        break;
                    case "Room Lockdown":
                        ability.mana = Plugin.Config.Scp079CostLockDown;
                        break;
                    case "Tesla Gate Burst":
                        ability.mana = Plugin.Config.Scp079CostTesla;
                        break;
                    case "Elevator Teleport":
                        ability.mana = Plugin.Config.Scp079CostElevatorTeleport;
                        break;
                    case "Elevator Use":
                        ability.mana = Plugin.Config.Scp079CostElevatorUse;
                        break;
                    case "Speaker Start":
                        ability.mana = Plugin.Config.Scp079CostSpeakerStart;
                        break;
                    case "Speaker Update":
                        ability.mana = Plugin.Config.Scp079CostSpeakerUpdate;
                        break;
                }
            }
        }
    }

}
