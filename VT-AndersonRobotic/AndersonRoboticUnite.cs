using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_AndersonRobotic
{
    public class AndersonRoboticUnite : Synapse.Api.Roles.Role
    {
        public override int GetRoleID() => (int)RoleID.AndersonUnite;

        public override string GetRoleName() => "AndersonUnite";

        public override int GetTeamID() => (int)TeamID.AndersneRobotic;

        public override List<int> GetFriendsID() => new List<int> { (int)TeamID.AndersneRobotic };

        public override List<int> GetEnemiesID() => new List<int> { (int)TeamID.RSC, (int)TeamID.SEC, (int)TeamID.MTF, (int)TeamID.NTF, (int)TeamID.CDM, (int)TeamID.CHI };

        public override void Spawn()
        {
            Player.RoleType = RoleType.Tutorial;
            Player.MaxHealth = Plugin.Config.Health;
            Player.Health = Plugin.Config.Health;
            Player.Ammo5 = Plugin.Config.Ammo5;
            Player.Ammo7 = Plugin.Config.Ammo7;
            Player.Ammo9 = Plugin.Config.Ammo9;

            Player.DisplayInfo = Plugin.Config.CustomRoleName;
            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.SpawnMessage.Replace("\\n", "\n"));
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = string.Empty;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
        }
    } 
}
