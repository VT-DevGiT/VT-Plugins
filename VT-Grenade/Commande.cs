using Synapse.Api.Enum;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VTGrenad
{
    [CommandInformation(
        Name = "Grenade",
        Aliases = new[] { "Boom" },
        Description = "Allows you to activate a grenade remotely",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "drop a grenade with a tablet in your hand then .Boom"
        )]
    class AdvencedGrenadeCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            //SynapseController.Server.Logger.Info("AdvencedGrenadeCommand : run");
            var result = new CommandResult();
            if (!Plugin.Config.NotAGrenadeRole.Contains(context.Player.RoleID))
            {
                if (Plugin.DictTabletteGrenades.ContainsKey(context.Player.PlayerId))
                {
                    //DebugLog.LogWarning($"{context.Player.name} Command Valide player with grenade {context.Player.name}");
                    List<AmorcableGrenade> listGrenade = Plugin.DictTabletteGrenades[context.Player.PlayerId];
                    foreach (AmorcableGrenade grenade in listGrenade)
                    {
                        try
                        {
                            if (grenade.IsArmed)
                            {
                                //SynapseController.Server.Logger.Info("AdvencedGrenadeCommand : Grenade Is Amerd");
                                Synapse.Api.Map.Get.SpawnGrenade(grenade.GrItem.Position, Vector3.zero, 0.2f, grenade.IsFlash ? GrenadeType.Flashbang : GrenadeType.Grenade, context.Player);
                                grenade.Used = true;
                                grenade.GrItem.Destroy();
                            }
                        }
                        catch
                        {

                        }
                    }

                    var listGrenadeNotUsed = listGrenade.Where(p => !p.Used);
                    if (listGrenadeNotUsed == null || !listGrenadeNotUsed.Any())
                    {
                        Plugin.DictTabletteGrenades.Remove(context.Player.PlayerId);
                    }
                    else
                    {
                        Plugin.DictTabletteGrenades[context.Player.PlayerId] = listGrenadeNotUsed.ToList();
                    }
                    result.Message = "BOOM";
                    result.State = CommandResultState.Ok;
                }
            }
            else
            {
                result.Message = "Your role is restricted";
                result.State = CommandResultState.NoPermission;
            }
            return result;

        }
    }
}
