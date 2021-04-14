using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    public class HammerDownCadet : Synapse.Api.Roles.Role
    {
        public override int GetRoleID() => (int)RoleID.CdmCadet;

        public override string GetRoleName() => "CDMCadet";

        public override int GetTeamID() => (int)TeamID.CDM;

        public override List<int> GetFriendsID() => new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM, };

        public override List<int> GetEnemiesID() => new List<int> {  (int)TeamID.CHI };

        public override void Spawn()
        {
            Player.RoleType = RoleType.NtfCadet;
            Player.MaxHealth = Plugin.Config.HealthCadet;
            Player.Health = Plugin.Config.HealthCadet;
            Player.Ammo5 = Plugin.Config.AmmoCadet.Ammo5;
            Player.Ammo7 = Plugin.Config.AmmoCadet.Ammo7;
            Player.Ammo9 = Plugin.Config.AmmoCadet.Ammo9;

            

            Player.DisplayInfo = Plugin.Config.CustomRoleNameCadet.Replace("%Squad%", Player.UnitName);
            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.SpawnMessageCadet.Replace("\\n", "\n"));
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = string.Empty;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
        }
    } 
}
