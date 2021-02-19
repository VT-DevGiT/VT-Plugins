using CustomClass.PlayerScript;
using Synapse;
using Synapse.Api;
using Synapse.Command;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "renfort ",
    Aliases = new[] { "renfort " },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".renfort "
    )]
    public class RenfortCommand : ISynapseCommand
    {
        private System.Random rnd = new System.Random();
        private SerializedItem GetRenfortConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigRenfort.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.CustomRole is BasePlayerScript script)
            {
                SerializedItem LocaleConfig = GetRenfortConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    // cooldown
                    List<Player> spectators = Server.Get.GetPlayers(p => p.RoleID == (int)RoleType.Spectator).ToList();

                    int min = LocaleConfig.Durabillity == -1 ? 3 : (int)LocaleConfig.Durabillity;
                    int max = LocaleConfig.Barrel == -1 ? 7 : LocaleConfig.Barrel;
                    int spawns = Mathf.Clamp((int)(rnd.NextDouble() * ((max - min) + 1)) + min, 0, spectators.Count);

                    if (spectators.Count > min)
                    {
                        result.State = CommandResultState.Error;
                        result.Message = "No enough spectator player";
                        return result;
                    }
                    switch (context.Player.Team)
                    {
                        case Team.CHI:
                            {
                                for (int i = 0; i < spawns; i++)
                                {
                                    int index = rnd.Next(spectators.Count);
                                    Player p = spectators[index];
                                    spectators.RemoveAt(index);

                                    p.RoleID = (int)RoleType.ChaosInsurgency;

                                }
                            }
                            break;
                        case Team.MTF:
                            {

                                int commanders = 1;
                                int lieutenants = 0;
                                int cadets = 0;

                                lieutenants = Mathf.Clamp(spawns - commanders, 0, 3);
                                cadets = spawns - lieutenants - commanders;

                                for (int i = 0; i < spawns; i++)
                                {
                                    int index = rnd.Next(spectators.Count);
                                    Player p = spectators[index];
                                    spectators.RemoveAt(index);
                                    if (commanders > 0)
                                    {
                                        p.RoleID = (int)RoleType.NtfCommander;
                                        commanders--;
                                    }
                                    else if (lieutenants > 0)
                                    {
                                        p.RoleID = (int)RoleType.NtfLieutenant;
                                        lieutenants--;
                                    }

                                    else p.RoleID = (int)RoleType.NtfCadet;
                                }
                            }
                            break;
                        case Team.SCP:
                            {

                            }
                            break;
                        case Team.TUT:
                            {

                            }
                            break;
                    }
                    result.Message = "reinforcements are arriving";
                    result.State = CommandResultState.Ok;
                    return result;
                }
            }
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;
        }
    }
}
