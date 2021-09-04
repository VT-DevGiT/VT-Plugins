using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using System;
using VT_Referance.Method;

namespace VT_Referance.Patch.Event
{
    //[HarmonyPatch(typeof(Player), "CustomRole", MethodType.Setter)]
    class SynapseSetClassIdPatch1
    {
        //[HarmonyPrefix]
        private static bool CustomRole(Player __instance, IRole value)
        {
            try
            {
                if (value == null)
                    return true;
                bool flag = true;
                int roleID = value.GetRoleID();
                VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, ref roleID, ref flag);
                if (roleID == value.GetRoleID() || !flag)
                    return flag;
                if (roleID >= 0 && roleID <= RoleManager.HighestRole)
                {
                    value = null;
                    __instance.ClassManager.SetPlayersClass((RoleType)roleID, __instance.gameObject, CharacterClassManager.SpawnReason.None);
                    return true;
                }

                if (!Server.Get.RoleManager.IsIDRegistered(roleID))
                    Logger.Get.Error("Plugin tried to set the RoleId of a Player with an not registered RoldeID");

                value = Server.Get.RoleManager.GetCustomRole(roleID);

                return true;    
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Event: CustomRole failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }

    //[HarmonyPatch(typeof(Player), "RoleType", MethodType.Setter)]
    class SynapseSetClassIdPatch2
    {
        //[HarmonyPrefix]
        private static bool RoleType(Player __instance, RoleType value)
        {
            try
            {
                if (__instance.CustomRole != null)
                    return true;

                bool flag = true;
                int roleID = (int)value;
                VTController.Server.Events.Player.InvokeSetClassEvent(__instance, __instance.RoleID, ref roleID, ref flag);
                if (roleID == (int)value || !flag)
                    return flag;
                if (roleID >= 0 && roleID <= RoleManager.HighestRole)
                {
                    value = (RoleType)roleID;
                    return true;
                }

                if (!Server.Get.RoleManager.IsIDRegistered(roleID))
                    Logger.Get.Error("Plugin tried to set the RoleId of a Player with an not registered RoldeID");


                if (__instance.CustomRole != null)
                    __instance.CustomRole.DeSpawn();
                

                IRole _role = __instance.GetFieldValueorOrPerties<IRole>("_role");
                _role = Server.Get.RoleManager.GetCustomRole(roleID);


                __instance.CustomRole.Player = __instance;
                __instance.CustomRole.Spawn();
               
                return false;
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Vt-Event: RoleType failed!!\n{e}\nStackTrace:\n{e.StackTrace}");
                return true;
            }
        }
    }
}
