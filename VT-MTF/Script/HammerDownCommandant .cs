using Synapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace VT_HammerDown
{
    public class HammerDownCommandant : Synapse.Api.Roles.Role
    {
        public override int GetRoleID() => (int)RoleID.CdmCommander;

        public override string GetRoleName() => "CDMCommandant";

        public override int GetTeamID() => (int)TeamID.CDM;

        public override List<int> GetFriendsID() => new List<int> { (int)TeamID.RSC, (int)TeamID.MTF, (int)TeamID.CDM, };

        public override List<int> GetEnemiesID() => new List<int> { (int)TeamID.CHI };

        public override void Spawn()
        {
            Player.RoleType = RoleType.NtfCommander;
            Player.MaxHealth = Plugin.Config.HealthCommander;
            Player.Health = Plugin.Config.HealthCommander;
            Player.Ammo5 = Plugin.Config.AmmoCommandant.Ammo5;
            Player.Ammo7 = Plugin.Config.AmmoCommandant.Ammo7;
            Player.Ammo9 = Plugin.Config.AmmoCommandant.Ammo9;

            foreach (var item in Plugin.Config.ItemsCommandant)
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

            Player.DisplayInfo = Plugin.Config.CustomRoleNameCommander.Replace("%Squad%", Player.UnitName);
            Player.RemoveDisplayInfo(PlayerInfoArea.Role);
            Player.OpenReportWindow(Plugin.PluginTranslation.ActiveTranslation.SpawnMessageCommandant.Replace("\\n", "\n"));
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = string.Empty;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
        }
    } 
}
