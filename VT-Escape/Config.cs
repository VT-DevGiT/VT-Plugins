using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace VTEscape
{
    public class Config : AbstractConfigSection
    {
        [Description("Modify main escape")]
        public bool MTFEscapeIsEnabled { get; set; } = true;

        [Description("add new one out from IC side")]
        public bool ICEscapeIsEnabled { get; set; } = true;

        [Description("Escape Config, if the conditions are met, the player will become the chosen role")]
        public List<SerializedEscapeConfig> EscapeList = new List<SerializedEscapeConfig>()
        {
            // Vania Game
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, TeamID.None, RoleID.NtfScientist),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.NtfScientist),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.NTF, RoleID.NtfCadet),

            //Custom

            //escapeMTF Role
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, TeamID.CDM, RoleID.CdmCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.DirecteurSite, TeamID.None, TeamID.None, RoleID.NtfLieutenantColonel),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.GardeSuperviseur, TeamID.None, TeamID.None, RoleID.NtfCommander),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.ScientifiqueSuperviseur, TeamID.None, TeamID.None, RoleID.NtfExpertReconfinement),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Concierge, TeamID.None, TeamID.None, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.FacilityGuard, TeamID.None, TeamID.None, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.GardePrison, TeamID.None, TeamID.None, RoleID.NtfSergent),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.ZoneManager, TeamID.None, TeamID.None, RoleID.NtfSergent),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Scp0492, TeamID.None, TeamID.None, RoleID.Spectator),
            //escapeMTF Team
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NetralSCP, TeamID.None, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.NTF, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.AND, TeamID.NTF, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),
            
            //escapeCHI Role
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.ChiIntrus, TeamID.None, TeamID.None, RoleID.ChiLeader),
            //escapeCHI Team
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.CDP, TeamID.None, RoleID.ChiExpertPyrotechnie),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, TeamID.CHI, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.NTF, TeamID.CHI, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.SCP, TeamID.None, RoleID.Spectator),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, TeamID.None, RoleID.Spectator),
        };

        [Description("the WarHeadEscape can be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
    