using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace VTEscape
{
    public class Config : AbstractConfigSection
    {
        [Description("Add the shelter (In dev...)")]
        public bool ShelterIsEnabled { get; set; } = true;

        [Description("Modify main escape")]
        public bool MTFEscapeIsEnabled { get; set; } = true;

        [Description("add new one out from IC side")]
        public bool ICEscapeIsEnabled { get; set; } = true;

        [Description("Escape Config, if the conditions are met, the player will become the chosen role")]
        public List<SerializedEscapeConfig> EscapeList = new List<SerializedEscapeConfig>()
        {
            // Vania Game
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, TeamID.None, RoleID.NtfSpecialist, EscapeEnum.MTF, 3),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.ChaosConscript, EscapeEnum.CHI, 2),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChaosRepressor, EscapeEnum.CHI, 3),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.NTF, RoleID.NtfPrivate, EscapeEnum.MTF, 1),
            //Custom

            //escapeMTF Role
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.CDM, RoleID.CdmCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.GOC, RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, TeamID.GOC, RoleID.GOCMember),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.DirecteurSite, TeamID.None, TeamID.None, RoleID.NtfLieutenantColonel),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.GardeSuperviseur, TeamID.None, TeamID.None, RoleID.NtfCommander),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.ScientifiqueSuperviseur, TeamID.None, TeamID.None, RoleID.NtfExpertReconfinement),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Concierge, TeamID.None, TeamID.None, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.FacilityGuard, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.GardePrison, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.ZoneManager, TeamID.None, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Scp0492, TeamID.None, TeamID.None, RoleID.Spectator),
            //escapeMTF Team
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NetralSCP, TeamID.None, RoleID.NtfSergeant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.AND, TeamID.NTF, RoleID.NtfPrivate),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator, EscapeEnum.NONE, 0, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),
            
            //escapeCHI Role
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.ChaosIntrus, TeamID.None, TeamID.None, RoleID.ChaosLeader),
            //escapeCHI Team
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChaosExpertPyrotechnie),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosConscript),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, TeamID.None, RoleID.Spectator),
        };

        [Description("the WarHeadEscape cant be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
    