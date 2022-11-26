using HarmonyLib;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Plugin;

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
Version = "079v.1.4.1 079Commandv1.3.1"
)]

    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config>
    {
        public const int CASSIE = 3999;

        public override bool AutoRegister => true;

        public Harmony Harmony { get; private set; }

        public List<int> SCPRoleDeconf;

        public override void Load()
        {
            base.Load();
            SCPRoleDeconf = Config.Scp079ScpDeconf.ToList();
            Harmony = new Harmony("VT079");
            Harmony.PatchAll();
        }

        internal void ChangeCost(Scp079PlayerScript player079Script)
        {
            foreach (Scp079PlayerScript.Ability079 ability in player079Script.abilities)
            {
                switch (ability.label)
                {
                    case "Camera Switch":
                        ability.mana = Config.Scp079CostCamera;
                        break;
                    case "Door Lock":
                        ability.mana = Config.Scp079CostLock;
                        break;
                    case "Door Lock Start":
                        ability.mana = Config.Scp079CostLockStart;
                        break;
                    case "Door Lock Minimum":
                        ability.mana = Config.Scp079ConstLockMinimum;
                        break;
                    case "Door Interact ion DEFAULT":
                        ability.mana = Config.Scp079CostDoorDefault;
                        break;  
                    case "Door Interaction CONT_LVL_1":
                        ability.mana = Config.Scp079CostDoorContlv1;
                        break;
                    case "Door Interaction CONT_LVL_2":
                        ability.mana = Config.Scp079CostDoorContlv2;
                        break;
                    case "Door Interaction CONT_LVL_3":
                        ability.mana = Config.Scp079CostDoorContlv3;
                        break;
                    case "Door Interaction ARMORY_LVL_1":
                        ability.mana = Config.Scp079CostDoorArmlv1;
                        break;
                    case "Door Interaction ARMORY_LVL_2":
                        ability.mana = Config.Scp079CostDoorArmlv2;
                        break;
                    case "Door Interaction ARMORY_LVL_3":
                        ability.mana = Config.Scp079CostDoorArmlv3;
                        break;
                    case "Door Interaction EXIT_ACC":
                        ability.mana = Config.Scp079CostDoorExit;
                        break;
                    case "Door Interaction INCOM_ACC":
                        ability.mana = Config.Scp079CostDoorIntercom;
                        break;
                    case "Door Interaction CHCKPOINT_ACC":
                        ability.mana = Config.Scp079CostDoorCheckpoint;
                        break;
                    case "Room Lockdown":
                        ability.mana = Config.Scp079CostLockDown;
                        break;
                    case "Tesla Gate Burst":
                        ability.mana = Config.Scp079CostTesla;
                        break;
                    case "Elevator Teleport":
                        ability.mana = Config.Scp079CostElevatorTeleport;
                        break;
                    case "Elevator Use":
                        ability.mana = Config.Scp079CostElevatorUse;
                        break;
                    case "Speaker Start":
                        ability.mana = Config.Scp079CostSpeakerStart;
                        break;
                    case "Speaker Update":
                        ability.mana = Config.Scp079CostSpeakerUpdate;
                        break;
                }
            }
        }

    }

}
