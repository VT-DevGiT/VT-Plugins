using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using MEC;
using Respawning;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTProget_X
{
    public static class Methode
    {
        public static IEnumerator<float> Decontamination(int WaitForStart = 120, int AlertTime = 3)
        {
            Plugin.Instance.DeconatmiantionendProgress = true;
            foreach (var room in Server.Get.Map.Rooms.FindAll(p => p.Zone == ZoneType.LCZ))
            {
                foreach (var door in room.Doors)
                {
                    if (door.DoorPermissions.RequiredPermissions == KeycardPermissions.None)
                    {
                        door.Locked = true;
                        door.Open = true;
                    }
                }
            }
            while (AlertTime > 0)
            {
                Methods.PlayAmbientSound(7);
                AlertTime--;
                yield return Timing.WaitForSeconds(1f);
            }
            Map.Get.GlitchedCassie($"danger . LightContainmentZone decontamination start in 2 minutes .");
            WaitForStart += 5;
            while (WaitForStart > 0)
            {
                Methods.PlayAmbientSound(5);
                WaitForStart--;
                yield return Timing.WaitForSeconds(1f);
            }
            Map.Get.GlitchedCassie($"Light Containment Zone is locked down and ready for decontamination .");
            Server.Get.Map.Decontamination?.CallMethod("InstantStart");
            Server.Get.Map.Decontamination?.Controller?.CallMethod("FinishDecontamination");
            Plugin.Instance.DeconatmiantinEnd = true;
            yield break;
        }

        public static void SendInterComInfoGeneral(screenEnum screen)
        {
            try
            {
                int leftdecont;
                int leftautowarhead;
                int nextRespawnTime;
                int nSCP;
                int nCDP;
                int nRSC;
                int nVIP;
                int nFIM;
                int Voltage;
                bool isContain;
                bool isAlreadyUsed;
                string roundTime;
                string DecontTime;
                string TeslaMessage;
                string SCP106Message;
                string AlfaWarheadMessage;
                string RespawnMessage;
                string scpListMessage = string.Empty;
                string IntercomStatueMessage;
                string DecontMessage;
                string ScreenMessage;

                var _intercom = Server.Get.Host.GetComponent<Intercom>();

                #region int&bool
                List<int> SCPaditonelle = new List<int> { (int)RoleID.Scp056, (int)RoleID.Scp035 };
                List<int> RSCaditonelle = new List<int> { (int)RoleID.FacilityGuard, (int)RoleID.GardeSuperviseur, (int)RoleID.Technicien };

                nSCP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.SCP || p.TeamID == (int)TeamID.NetralSCP || p.TeamID == (int)TeamID.BerserkSCP  || SCPaditonelle.Contains(p.RoleID)).Count();
                nCDP = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.ClassD).Count();
                nRSC = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.RSC || RSCaditonelle.Contains(p.RoleID)).Count();
                nVIP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.VIP).Count();
                nFIM = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.MTF || p.TeamID == (int)TeamID.CDM && !SCPaditonelle.Contains(p.RoleID) && !RSCaditonelle.Contains(p.RoleID)).Count();
                leftdecont = (int)((Math.Truncate((15f * 60) * 100f) / 100f) - (Math.Truncate(DecontaminationController.GetServerTime * 100f) / 100f));
                leftautowarhead = AlphaWarheadController.Host != null
                    ? (int)Mathf.Clamp(AlphaWarheadController.Host.timeToDetonation - RoundSummary.roundTime, 0, AlphaWarheadController.Host.timeToDetonation)
                    : -1;
                nextRespawnTime = (int)Math.Truncate(RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.RespawnCooldown
                    ? RespawnManager.Singleton.GetFieldValueorOrPerties<float>("_timeForNextSequence") - RespawnManager.Singleton.GetFieldValueorOrPerties<Stopwatch>("_stopwatch").Elapsed.TotalSeconds
                    : 0);
                isContain = PlayerManager.localPlayer.GetComponent<CharacterClassManager>().GetFieldValueorOrPerties<LureSubjectContainer>("_lureSpj").allowContain;
                isAlreadyUsed = OneOhSixContainer.used;
                leftdecont = Mathf.Clamp(leftdecont, 0, leftdecont);
                #endregion

                #region AlfaWarheadMessage
                if (AlphaWarheadOutsitePanel.nukeside.enabled)
                    AlfaWarheadMessage = Plugin.PluginTranslation.ActiveTranslation.AlfaWarheadMessageReady;
                else
                    AlfaWarheadMessage = Plugin.PluginTranslation.ActiveTranslation.AlfaWarheadMessageDisabled;

                #endregion

                #region DecontaMessage
                if (Methods.GetVoltage() < 100)
                    DecontMessage = Plugin.PluginTranslation.ActiveTranslation.DecontMessageNotEnoughEnergy;
                else
                {
                    if (!Plugin.Instance.DeconatmiantionendProgress)
                        DecontMessage = Plugin.PluginTranslation.ActiveTranslation.DecontMessageReady;
                    else if (!Plugin.Instance.DeconatmiantinEnd)
                        DecontMessage = Plugin.PluginTranslation.ActiveTranslation.DecontMessageInProgress;
                    else
                        DecontMessage = Plugin.PluginTranslation.ActiveTranslation.DecontMessageFinished;
                }

                #endregion

                #region RespawnMessage
                if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                    RespawnMessage = Plugin.PluginTranslation.ActiveTranslation.RespawnMessageMTF
                        .Replace("%Name%", RespawnManager.Singleton.NamingManager.name).Replace("%Temps%", $"t-{ nextRespawnTime / 60:00}:{ nextRespawnTime % 60:00}");
                else
                    RespawnMessage = Plugin.PluginTranslation.ActiveTranslation.RespawnMessageNoMTF;
                #endregion

                #region SCPListMessage
                var listScp = Server.Get.Players.Where(p => p.Team == Team.SCP || SCPaditonelle.Contains(p.RoleID));
                foreach (var scp in listScp)
                {
                    if (scp.RoleID == (int)RoleType.Scp079)
                        scpListMessage += Plugin.PluginTranslation.ActiveTranslation.IntercomScpInformation079
                            .Replace("%Name%", scp.RoleName)
                            .Replace("%Tier%", scp.Hub.scp079PlayerScript.Lvl.ToString());
                    else
                        scpListMessage += Plugin.PluginTranslation.ActiveTranslation.IntercomScpInformation
                            .Replace("%Name%", scp.RoleName)
                            .Replace("%Zone%", scp.Zone.ToString())
                            .Replace("%Room%", scp.Room.RoomName);
                    scpListMessage += "\n";
                }
                #endregion

                #region IntercomStatue

                if (_intercom.Muted)
                {
                    IntercomStatueMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomStatueMute;
                }
                else if (Intercom.AdminSpeaking)
                {
                    IntercomStatueMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomStatueAdmin.
                        Replace("%Player%", _intercom.GetPlayer().NickName);
                }
                else if (_intercom.remainingCooldown > 0f)
                {
                    IntercomStatueMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomStatueRestart.
                        Replace("%Temps%", ((int)_intercom.remainingCooldown).ToString());
                }
                else if (_intercom.speaker != null)
                {
                    IntercomStatueMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomStatuePlayer.
                        Replace("%Player%", _intercom.GetPlayer().NickName).Replace("%Temps%", ((int)_intercom.speechRemainingTime).ToString());
                }
                else
                {
                    IntercomStatueMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomStatueReady;
                }
                #endregion

                #region GeneratorVoltage
                Voltage = Methods.GetVoltage();
                #endregion

                #region SCP106Message
                if (isContain)
                {
                    if (isAlreadyUsed)
                        SCP106Message = Plugin.PluginTranslation.ActiveTranslation.SCP106Use;
                    else
                        SCP106Message = Plugin.PluginTranslation.ActiveTranslation.SCP106Ready;
                }
                else
                {
                    SCP106Message = Plugin.PluginTranslation.ActiveTranslation.SCP106Empty;
                }
                #endregion

                #region Tesla
                if (Plugin.Instance.TeslaEnabled)
                    TeslaMessage = Plugin.PluginTranslation.ActiveTranslation.TeslaOn;
                else
                    TeslaMessage = Plugin.PluginTranslation.ActiveTranslation.TeslaOff;
                #endregion

                #region DecontTime
                DecontTime = $"T-{leftdecont / 60:00}:{leftdecont % 60:00}";
                #endregion

                #region BrecheTime
                roundTime = $"T+{ RoundSummary.roundTime / 60:00}:{ RoundSummary.roundTime % 60:00}";
                #endregion


                
                switch (screen)
                {
                    case screenEnum.GeneralInfo:
                        ScreenMessage = Plugin.PluginTranslation.ActiveTranslation.IntercomGeneralInformation
                            .Replace("\\n", "\n")
                            .Replace("%RoundTime%", roundTime)
                            .Replace("%nSCP%", nSCP.ToString())
                            .Replace("%nCDP%", nCDP.ToString())
                            .Replace("%nRSC%", nRSC.ToString())
                            .Replace("%nVIP%", nVIP.ToString())
                            .Replace("%nMTF%", nFIM.ToString())
                            .Replace("%TotalVoltage%", Voltage.ToString())
                            .Replace("%AlfaWarheadStatut%", AlfaWarheadMessage)
                            .Replace("%Tesla%", TeslaMessage)
                            .Replace("%IsContain%", SCP106Message)
                            .Replace("%DecontMessage%", DecontMessage)
                            .Replace("%DecontTime%", DecontTime.ToString())
                            .Replace("%RespawnMessage%", RespawnMessage)
                            .Replace("%IntercomStatue%", IntercomStatueMessage);
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.ListScp:
                        ScreenMessage = string.Concat(
                        $"{(scpListMessage.Any() ? scpListMessage : Plugin.PluginTranslation.ActiveTranslation.IntercomNoScpInformation)}\n",
                        $"─────────────────────────────────────\n");
                        ScreenMessage += IntercomStatueMessage;
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.Custom:
                        ScreenMessage = string.Concat(
                            $"<color=#9300FF>Azarus</color>\n",
                            $"───────────────────────────────────── \n ",
                            $"           [Insérée Message] ",
                            $"             I like train ! "
                            );
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.Defaux:
                        Map.Get.IntercomText = $"Error Please Screen {DateTime.Now}";
                        break;
                }
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Erreur Intercom{e.Message}");
                Map.Get.IntercomText = $"Error Please Screen {DateTime.Now}";
            }

        }
    }
}
