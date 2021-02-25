using CustomClass.PlayerScript;
using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Metamorf",
    Aliases = new[] { "Metamorf" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Metamorf met/deg d/s/c/m/g "
    )]
    public class MetamorfCommand : ISynapseCommand
    {
        private SerializedItem GetMetamorfConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigMetamorf.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }
        //https://github.com/SynapseSL/Scp056
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.CustomRole is BasePlayerScript script)
            {

                SerializedItem LocaleConfig = GetMetamorfConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    uint _Ammo5 = context.Player.Ammo5;
                    uint _Ammo7 = context.Player.Ammo7;
                    uint _Ammo9 = context.Player.Ammo9;
                    int _MaxHealth = context.Player.MaxHealth;
                    int _MaxArtificialHealth = context.Player.MaxArtificialHealth;
                    float _Health = context.Player.Health;
                    float _ArtificialHealth = context.Player.ArtificialHealth;
                    RoleType role;


                    //cooldown
                    switch (context.Arguments.ElementAt(1).ToLower())
                    {
                        case "d":
                            role = RoleType.ClassD;
                            break;
                        case "s":
                            role = RoleType.Scientist;
                            break;
                        case "c":
                            role = RoleType.ChaosInsurgency;
                            break;
                        case "cad":
                            role = RoleType.NtfCadet;
                            break;
                        case "ltn":
                            role = RoleType.NtfLieutenant;
                            break;
                        case "cmd":
                            role = RoleType.NtfCommander;
                            break;
                        case "g":
                            role = RoleType.FacilityGuard;
                            break;

                        default:
                            result.Message = "You have to enter a valid letter" +
                                "\nD => D-Personnel" +
                                "\nS => Scientist" +
                                "\nC => Chaos" +
                                "\nCAD => Mtf Cadet" +
                                "\nLTN => Mtf Lieutnant" +
                                "\nCMD => Mtf Commander" +
                                "\nG => Guard";
                            result.State = CommandResultState.Error;
                            return result;
                    }
                    {
                        context.Player.ChangeRoleAtPosition(role);
                        context.Player.Ammo5 = _Ammo5;
                        context.Player.Ammo7 = _Ammo7;
                        context.Player.Ammo9 = _Ammo9;
                        context.Player.MaxHealth = context.Player.MaxHealth;
                        context.Player.MaxArtificialHealth = context.Player.MaxArtificialHealth;
                        context.Player.Health = context.Player.Health;
                        context.Player.ArtificialHealth = context.Player.ArtificialHealth;
                        result.Message = "You successfully deguise";
                        result.State = CommandResultState.Ok;
                    }
                    return result;
                }
            }
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;

        }
    }
}
