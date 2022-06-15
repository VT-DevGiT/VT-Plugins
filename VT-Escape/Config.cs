using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VT_Api.Core.Enum;

namespace VTEscape
{
    public class Config : AbstractConfigSection
    {
        [Description("The player when escape keep they inventory")]
        public bool keepInventory { get; set; } = true;

        [Description("Modify main escape")]
        public bool MTFEscapeIsEnabled { get; set; } = true;

        [Description("Add new one out from IC side")]
        public bool ICEscapeIsEnabled { get; set; } = true;

        [Description("Escape Config, if the conditions are met, the player will become the chosen role")]
        public List<SerializedEscapeConfig> EscapeList = new List<SerializedEscapeConfig>()
        {
            // Vanilaa
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.RSC, (int)TeamID.None, (int)RoleID.NtfSpecialist,  Respawning.SpawnableTeamType.NineTailedFox, 3),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.RSC, (int)TeamID.CHI,  (int)RoleID.ChaosConscript, Respawning.SpawnableTeamType.ChaosInsurgency, 2),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CDP, (int)TeamID.None, (int)RoleID.ChaosRepressor, Respawning.SpawnableTeamType.ChaosInsurgency, 3),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CDP, (int)TeamID.NTF,  (int)RoleID.NtfPrivate,     Respawning.SpawnableTeamType.NineTailedFox, 1),
            //Custom

            //escapeMTF Role
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.DirecteurSite,           (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfLieutenantColonel),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.GardeSuperviseur,        (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfCommander),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.ScientifiqueSuperviseur, (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfExpertReconfinement),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.Janitor,                 (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.FacilityGuard,           (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.GardePrison,             (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.ZoneManager,             (int)TeamID.None, (int)TeamID.None, (int)RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.Scp0492,                 (int)TeamID.None, (int)TeamID.None, (int)RoleID.Spectator),
            
            //escapeMTF Team
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CDP,       (int)TeamID.CDM,  (int)RoleID.CdmCadet),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CDP,       (int)TeamID.GOC,  (int)RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.RSC,       (int)TeamID.GOC,  (int)RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.NetralSCP, (int)TeamID.None, (int)RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.NTF,       (int)TeamID.NTF,  (int)RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.NTF,       (int)TeamID.NTF,  (int)RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.NTF,       (int)TeamID.CHI,  (int)RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.AND,       (int)TeamID.NTF,  (int)RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.SCP,       (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.None, 0, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),
            
            //escapeCHI Role
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.ChaosIntrus, (int)TeamID.None, (int)TeamID.None, (int)RoleID.ChaosLeader),
            //escapeCHI Team
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.CDP, (int)TeamID.None, (int)RoleID.ChaosExpertPyrotechnie),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.RSC, (int)TeamID.CHI,  (int)RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.NTF, (int)TeamID.CHI,  (int)RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.SCP, (int)TeamID.None, (int)RoleID.Spectator),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.RSC, (int)TeamID.None, (int)RoleID.Spectator),

            //other
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.NTF, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CDM, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.GOC, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.U2I, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.NTF, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.CDM, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.GOC, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.U2I, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.NineTailedFox, 1),

            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.CHI, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.ChaosInsurgency, 1),
            new SerializedEscapeConfig(EscapeType.MTF, (int)RoleID.None, (int)TeamID.SHA, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.ChaosInsurgency, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.CHI, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.ChaosInsurgency, 1),
            new SerializedEscapeConfig(EscapeType.CHI, (int)RoleID.None, (int)TeamID.SHA, (int)TeamID.None, (int)RoleID.Spectator, Respawning.SpawnableTeamType.ChaosInsurgency, 1),

        };

        [Description("The postion of the Chaos Escape")]
        public SerializedVector3 ICEscapePostion = new SerializedVector3(-56.2f, 988.9f, -49.6f);

        [Description("The WarHeadEscape cant be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
    