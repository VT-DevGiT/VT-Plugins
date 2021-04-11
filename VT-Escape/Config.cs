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

        [Description("Escape Config, help on the github")]
        public List<SerializedEscapeConfig> EscapeList = new List<SerializedEscapeConfig>()
        {
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.RSC, false, RoleID.NtfScientist),
            
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.DirecteurSite, TeamID.None, false, RoleID.NtfLieutenantColonel),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.GardeSuperviseur, TeamID.None, false, RoleID.CdmCommander),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.ScientifiqueSuperviseur, TeamID.None, false, RoleID.NtfExpertReconfinement),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Concierge, TeamID.None, false, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.FacilityGuard, TeamID.None, false, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.NetralSCP, false, RoleID.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CHI, true, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.MTF, true, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.AndersneRobotic, true, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.CDP, false, RoleID.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.Scp0492, TeamID.None, false, RoleID.Spectator),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleID.None, TeamID.SCP, false, RoleID.Spectator, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),

            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.ChiIntrus, TeamID.None, false, RoleID.ChiLeader),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.CDP, false, RoleID.ChiExpertPyrotechnie),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, true, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.MTF, true, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.MTF, true, RoleID.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.SCP, false, RoleID.Spectator),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleID.None, TeamID.RSC, false, RoleID.Spectator),
        };

        [Description("add the Shelter (work in progress)")]
        public bool ShelterIsEnabled { get; set; } = false;

        [Description("the WarHeadEscape can be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
