using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using System;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{
   
    static class SynapseSetClassIdPatch
    {

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "CustomRole", MethodType.Setter)]
        private static bool CustomRolePatch(Player __instance, IRole value)
        {
            try
            {
                bool flag = true;
                int roleID = value.GetRoleID();

                VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, ref roleID, ref flag);

                if (flag) __instance.SetRoleID(roleID);

                return false;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Event: CustomRole failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Player), "RoleType", MethodType.Setter)]
        private static bool RoleTypePatch(Player __instance, RoleType value)
        {
            try
            {
                bool flag = true;
                int roleID = (int)value;

                VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, ref roleID, ref flag);

                __instance.ClassManager.SetPlayersClass(value, __instance.gameObject, CharacterClassManager.SpawnReason.None);

                if (flag) __instance.SetRoleID(roleID);

                return false;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Event: RoleType failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }

        private static void SetRoleID(this Player player, int Id)
        {
            IRole old_role = player.GetFieldValueorOrPerties<IRole>("_role");

            // Basic Role
            if (Id >= 0 && Id <= RoleManager.HighestRole)
            {
                player.SetField<IRole>("_role", null);
                player.ClassManager.SetPlayersClass((RoleType)Id, player.gameObject, CharacterClassManager.SpawnReason.None);
                return;
            }
            // Custom Role
            else
            {
                if (!Server.Get.RoleManager.IsIDRegistered(Id))
                    throw new Exception("Vt-Event: A Plugin tried to set the RoleId of a Player with an not registered RoldeID");

                IRole new_role = Server.Get.RoleManager.GetCustomRole(Id);

                player.SetField<IRole>("_role", new_role);

                if (old_role != null) old_role.DeSpawn();
                if (new_role == null) return;

                new_role.Player = player;
                new_role.Spawn();
            }
        }
    }
}
