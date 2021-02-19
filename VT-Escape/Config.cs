using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;

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
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleEnum.FacilityGuard, TeamEnum.None, false, RoleEnum.NtfLieutenant),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleEnum.Scp0492, TeamEnum.None, false, RoleEnum.Spectator),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleEnum.None, TeamEnum.SCP, false, RoleEnum.Spectator, true, "LOKI - 5 procedure is engaged . the alfa warhead is started , evacuat immediately ."),
            new SerializedEscapeConfig(EscapeEnum.MTF, RoleEnum.None, TeamEnum.CHI, true, RoleEnum.NtfCadet),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleEnum.None, TeamEnum.SCP, false, RoleEnum.Spectator),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleEnum.None, TeamEnum.CDP, false, RoleEnum.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleEnum.None, TeamEnum.RSC, false, RoleEnum.Spectator),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleEnum.None, TeamEnum.RSC, true, RoleEnum.ChaosInsurgency),
            new SerializedEscapeConfig(EscapeEnum.CHI, RoleEnum.None, TeamEnum.MTF, true, RoleEnum.ChaosInsurgency),
        };

        [Description("add the Shelter (work in progress)")]
        public bool ShelterIsEnabled { get; set; } = false;

        [Description("the WarHeadEscape can be disabled")]
        public bool WarHeadLockEnabled = false;
    }
}
