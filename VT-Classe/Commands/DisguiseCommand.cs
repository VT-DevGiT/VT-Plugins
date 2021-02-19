using CustomClass.PlayerScript;
using Synapse.Command;
using Synapse.Config;
using System.Linq;

namespace CustomClass.Commands
{

    [CommandInformation(
    Name = "Deguise",
    Aliases = new[] { "Deguise" },
    Description = "",
    Permission = "",
    Platforms = new[] { Platform.ClientConsole },
    Usage = ".Deguise met/deg d/s/c/m/g (you need a body next to you with the role you are becoming)"
    )]
    public class DeguiseCommand : ISynapseCommand
    {

        private SerializedItem GetDeguiseConfig(MoreClasseID classId)
        {
            var result = MoreClasseClass.CommandsConfig.ConfigDisguise.FirstOrDefault(c => c.ID == (int)classId);

            return result;
        }
        //https://github.com/SynapseSL/Scp056
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Player.CustomRole is BasePlayerScript script)
            {
                SerializedItem LocaleConfig = GetDeguiseConfig(script.ClasseID);
                if (LocaleConfig != null)
                {
                    RoleType role;
                    uint _Ammo5 = context.Player.Ammo5;
                    uint _Ammo7 = context.Player.Ammo7;
                    uint _Ammo9 = context.Player.Ammo9;
                    int _MaxHealth = context.Player.MaxHealth;
                    int _MaxArtificialHealth = context.Player.MaxArtificialHealth;
                    float _Health = context.Player.Health;
                    float _ArtificialHealth = context.Player.ArtificialHealth;
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
                                "\nG => Guard" +
                                "\n WARNING : you need a body next to you with the role you are becoming";
                            result.State = CommandResultState.Error;
                            return result;
                    }
                    // testée si il y a le corp
                    if (true)
                    {
                        result.Message = "you need a corpse next to you with the role you are becoming";
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
                        result.Message = "You succesfully swaped your Role";
                        result.State = CommandResultState.Ok;
                    }
                }
                return result;
            }
            result.Message = "";
            result.State = CommandResultState.Error;
            return result;

        }
    }
}
