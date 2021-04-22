using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VT_AdminHelp
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        { 
            Dictionary<Player, List<DateTime>> PlayerKillsDate = new Dictionary <Player, List<DateTime>>();
            if (ev.Killer.TeamID.IsAlly(ev.Victim.TeamID))
            { 
                if (!PlayerKillsDate.ContainsKey(ev.Killer))
                    PlayerKillsDate.Add(ev.Killer, new List<DateTime>());
                var KillerDate = PlayerKillsDate[ev.Killer];
                KillerDate.Add(DateTime.Now);
                var killedBy = KillerDate.Where(p => p >= DateTime.Now.AddMinutes(-(Plugin.Config.DeltaTime)));
                if (killedBy.Count() > Plugin.Config.FFMax)
                {
                    if (Plugin.Config.AlertKillAllyMessage != null)
                        ev.Killer.OpenReportWindow(Plugin.Config.AlertKillAllyMessage.Replace("\\n", "\n")
                        .Replace("%MaxKill%", Plugin.Config.FFMax.ToString()).Replace("%DeltaTime%", Plugin.Config.DeltaTime.ToString()));
                    if (Plugin.Config.AutoReport)
                    {
                        int reportedID = ev.Killer.PlayerId;
                        string Message = Plugin.Config.AutoReportMessage
                                .Replace("//n", "/n")
                                .Replace("%TeamKiller%", ((TeamID)ev.Killer.TeamID).ToString())
                                .Replace("%TeamVictime%", ((TeamID)ev.Victim.TeamID).ToString())
                                .Replace("%MaxKill%", Plugin.Config.FFMax.ToString())
                                .Replace("%DeltaTime%", Plugin.Config.DeltaTime.ToString());
                        CheaterReport.SubmitReport($"{Server.Get.Name}{Server.Get.Port}@Server", ev.Killer.UserId, $"VT-AdminHelp : {Message}", ref reportedID, "Server Plugin : VT-Adminhelp", ev.Killer.name, true);
                    }
                    if (Plugin.Config.Jail)
                        ev.Killer.Jail.JailPlayer(Server.Get.Host);
                    if (Plugin.Config.CallStaff)
                    {
                        var Staffs = Server.Get.Players.Where(p => p.RemoteAdminAccess == true);
                        if (Staffs != null)
                        {
                            foreach (var Staff in Staffs)
                            {
                                Staff.SendBroadcast(10, Plugin.Config.CallStaffMessage
                                .Replace("%Player%", $"{ev.Killer.name} ID : {ev.Killer.PlayerId}").Replace("\\n", "\n"));
                            }
                            ev.Killer.Jail.JailPlayer(Server.Get.Host);
                        }
                    }
                }
            }
        }   
    }    
}