﻿using Synapse;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JetonClassManger
{
    [CommandInformation(
          Name = "UseCoin",
          Aliases = new[] { "Coin" },
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

            if (context.Arguments.FirstElement() == "Help" || context.Arguments.FirstElement() == "help")
            {
                string ListID = "";
                foreach (var RoleNom in Plugin.Config.RoleList)
                {
                    ListID += $"{RoleNom.NameRole} -> ID : {RoleNom.IDRole} \n";
                }
                result.State = CommandResultState.Ok;
                result.Message = ListID;
                return result;
            }
            else if (int.TryParse(context.Arguments.FirstElement(), out int ID) && Plugin.Instance.PlayerCanSwitch 
                && Plugin.Config.RoleList.Any(p => p.IDRole == ID) 
                && Server.Get.Players.Where(p =>p.RoleID == ID  ).Count() < Plugin.Config.RoleList.FirstOrDefault(p => p.IDRole == ID).MaxRole)

            { 
                context.Player.RoleID = ID ;
                result.State = CommandResultState.Ok ;
                result.Message = $"You switch you'r role to {Plugin.Config.RoleList.FirstOrDefault(p => p.IDRole == ID).NameRole}" ;
                return result;



            }

            return result;
        }
    }
}