using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace JetonClassManger
{
    [CommandInformation(
          Name = "ChangeRole",
          Aliases = new[] { "Cr" },
          Description = "Pour changer de role, /!\\ Attention il utilise un jeton de Classe ",
          Permission = " ",
          Platforms = new[] { Platform.ClientConsole },
          Usage = ".Cr (ID : ID du role) (Help : vous affiche les ID et Role) "
          )] 

    public class ClassInfoCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            out int ID = 0;
            if (context.Arguments.FirstElement() == "Help")
            {
                string ListID = "";
                foreach (var RoleNom in Plugin.Config.RoleList)
                {
                    ListID += $"{RoleNom.NameRole} -> ID : {RoleNom.IDRole} + \n";
                }
                result.State = CommandResultState.Ok;
                result.Message = ListID;
                return result;
            }
            else if (int.TryParse(context.Arguments.FirstElement(), ID ))
            {




            }

            return result;
        }
    }
}
