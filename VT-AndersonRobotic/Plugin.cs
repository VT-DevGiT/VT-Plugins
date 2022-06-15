using Synapse;
using Synapse.Api.Plugin;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Core.Teams;

namespace VT_AndersonRobotic
{
    [PluginInformation(
    Name = "VT-AndersonRobotic",
    Author = "VT",
    Description = "Adds the team AndersonRobotic",
    LoadPriority = 20,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.3.3")]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => true;

        public override void Load()
        {
            base.Load();
            var andersonTeam = Synapse.Api.Teams.TeamManager.Get.GetTeam((int)TeamID.AND) as AndersonRoboticTeam;
            if (Server.Get.RoleManager.IsIDRegistered((int)RoleID.AndersonUTRheavy) && Server.Get.RoleManager.IsIDRegistered((int)RoleID.AndersonUTRlight))
            {
                andersonTeam.Roles.Add(new RespawnRoleInfo() { Priority = 2, Max = 3, Min = -1, RoleID = (int)RoleID.AndersonUTRlight });
                andersonTeam.Roles.Add(new RespawnRoleInfo() { Priority = 1, Max = 1, Min = -1, RoleID = (int)RoleID.AndersonUTRheavy });
            }
            Translation.AddTranslation(new Translation()
            {
                SpawnMessageAnderson = "tu es un membre de <color=yellow>AndersonRobotic</color>\\nTon objectif est de volée toute les donnée des serveurs!\\n<b>Press esc pour fermé</b>",
            }, "FRENCH");
        }
    }
}
