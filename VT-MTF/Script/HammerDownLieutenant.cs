using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    public class HammerDownLieutenant : Synapse.Api.Roles.Role
    {
        public override int GetRoleID() => (int)RoleID.CDMLieutenant;

        public override string GetRoleName() => "CDMLieutenant";

        public override int GetTeamID() => (int)TeamID.CDM;

        public override List<int> GetFriendsID() => new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM, };

        public override List<int> GetEnemiesID() => new List<int> { (int)TeamID.CHI };

        public override void Spawn()
        {
            Player.RoleType = RoleType.NtfLieutenant;
            Player.MaxHealth = Plugin.Config.HealthLieutenant;
            Player.Health = Plugin.Config.HealthLieutenant;
            Player.Ammo5 = Plugin.Config.AmmoLieutenant.Ammo5;
            Player.Ammo7 = Plugin.Config.AmmoLieutenant.Ammo7;
            Player.Ammo9 = Plugin.Config.AmmoLieutenant.Ammo9;

            foreach (var item in Plugin.Config.ItemsLieutenant)
            {
                try
                {
                    var obj = item.Parse();
                    Player.Inventory.AddItem(item.Parse());
                }
                catch (Exception e)
                {
                    Server.Get.Logger.Error($"Error Congif inventory at {this.GetRoleName()} : { item.ID} {e} ");
                }
            }

            Player.DisplayInfo = Plugin.Config.CustomRoleNameLieutenant.Replace("%Squad%", Player.UnitName);
            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.SpawnMessageLieutenant.Replace("\\n", "\n"));
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = string.Empty;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
        }
    } 
}
