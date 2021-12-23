using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace VTEscape
{
    public class Config : AbstractConfigSection
    {
        [Description("Add the shelter (In dev... mabay ....)")]
        public bool ShelterIsEnabled { get; set; } = true;

        [Description("The player when escape keep they invotory")]
        public bool keepInvotory { get; set; } = true;

        [Description("Modify main escape")]
        public bool MTFEscapeIsEnabled { get; set; } = true;

        [Description("add new one out from IC side")]
        public bool ICEscapeIsEnabled { get; set; } = true;

        [Description("Escape Config, if the conditions are met, the player will become the chosen role")]
        public List<SerializedEscapeConfig> EscapeList = new List<SerializedEscapeConfig>()
        {
            // Vania Game
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.RSC, TeamID.None, RoleID.NtfSpecialist, EscapeType.MTF, 3),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.ChaosConscript, EscapeType.CHI, 2),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChaosRepressor, EscapeType.CHI, 3),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.CDP, TeamID.NTF, RoleID.NtfPrivate, EscapeType.MTF, 1),
            //Custom

            //escapeMTF Role
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.CDP, TeamID.CDM, RoleID.CdmCadet),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.CDP, TeamID.GOC, RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.RSC, TeamID.GOC, RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.DirecteurSite, TeamID.None, TeamID.None, RoleID.NtfLieutenantColonel),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.GardeSuperviseur, TeamID.None, TeamID.None, RoleID.NtfCommander),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.ScientifiqueSuperviseur, TeamID.None, TeamID.None, RoleID.NtfExpertReconfinement),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.Concierge, TeamID.None, TeamID.None, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.FacilityGuard, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.GardePrison, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.ZoneManager, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.Scp0492, TeamID.None, TeamID.None, RoleID.Spectator),
            //escapeMTF Team
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.NetralSCP, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.AND, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeType.MTF, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator, EscapeType.NONE, 0, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),
            
            //escapeCHI Role
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.ChaosIntrus, TeamID.None, TeamID.None, RoleID.ChaosLeader),
            //escapeCHI Team
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChaosExpertPyrotechnie),
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator),
            new SerializedEscapeConfig(EscapeType.CHI, RoleID.None, TeamID.RSC, TeamID.None, RoleID.Spectator),
        };

        [Description("the WarHeadEscape cant be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
    