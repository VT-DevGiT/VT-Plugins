using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using LightContainmentZoneDecontamination;
using MEC;
using Respawning;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Plugin;
using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VT_Api.Extension;

namespace VTProget_X
{

    [PluginInformation(
Name = "VT-Proget-X",
Author = "VT",
Description = "Adds functionality such as intercom information",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.3.2"
)]

    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => false;

        public bool DecontInProgress = false;
        public bool TeslaEnabled = true;
        public bool CustomScreen = false;

        private void PatchAll()
        {
            var instance = new Harmony("VTProget_X");
            instance.PatchAll();
            Server.Get.Logger.Info("VT-ProgetX Harmony Patch done!");
        }
        public override void Load()
        {
            base.Load();
            PatchAll();
            Translation.AddTranslation(new VTProget_X.Translation
            {
               IntercomGeneralInformation = 
@"─────────── Centre d'information FIM ───────────
Durée de la brèche : %RoundTime%
SCP restant(s) : %nSCP%
Classe D Restant(s) : %nCDP%
Personnel(s) à évacuée réstant(s) : %nRSC%
VIP à évacuée réstant(s) : %nVIP%
Personnel(s) militaire(s) déployé : %nMTF%
Puissance des générateurs actif(s) : %TotalVoltage% kVA
Statut de l'ogive nucléaire Oméga : PRÊTE
Statut de l'ogive nucléaire ALpha : %AlfaWarheadStatut%
Statut du briseur de fémur pour SCP-106 : %IsContain%
Statut des portes tesla : %Tesla%
Statut de la décontamination : %DecontMessage%
Temps avent la décontamination : %DecontTime%
%RespawnMessage%
─────────────────────────────────────
%IntercomStatue%",
                IntercomScpInformation = "%Name% : Zone %Zone% : Room %Room%",
                IntercomScpInformation079 = "%Name% : Tier %Tier%",
                IntercomNoScpInformation = "PAS DE SCP RESTANT",
                TeslaOn = "ACTIVÉES",
                TeslaOff = "DÉSSACTIVÉES",
                SCP106Ready = "ACTIVÉES",
                SCP106Use = "UTILISÉ",
                SCP106Empty = "VIDE",
                IntercomStatueRestart = "Temps avant redémarrage : %Temps%",
                IntercomStatuePlayer = "%Player% Diffuse : %Temps%",
                IntercomStatueAdmin = "%Player% Diffuse (prioritaire)",
                IntercomStatueReady = "Intercom prêt à l'emploi.",
                IntercomStatueMute = "Accès refusé",
                DecontMessageNotEnoughEnergy = "PAS ASSEZ D'ÉNERGIE",
                DecontMessageReady = "PRÊTE",
                DecontMessageInProgress = "ENCOURS",
                DecontMessageFinished = "FINALISÉE",
                AlfaWarheadMessageReady = "PRÊTE",
                AlfaWarheadMessageDisabled = "DÉSACTIVÉE",
                RespawnMessageMTF = "la division %Name% arrivera dans %Temps%",
                RespawnMessageNoMTF = "Aucune escouade n'est en route",
            }, "FRENCH");
        }

        public void SetIntercomScreen(ScreenType screen)
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

                nSCP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.SCP || p.TeamID == (int)TeamID.NetralSCP || p.TeamID == (int)TeamID.BerserkSCP || SCPaditonelle.Contains(p.RoleID)).Count();
                nCDP = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.ClassD).Count();
                nRSC = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.RSC || RSCaditonelle.Contains(p.RoleID)).Count();
                nVIP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.VIP).Count();
                nFIM = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.NTF || p.TeamID == (int)TeamID.CDM && !SCPaditonelle.Contains(p.RoleID) && !RSCaditonelle.Contains(p.RoleID)).Count();
                leftdecont = (int)(Math.Truncate(TimeLeftDecon() * 100f) / 100f);
                leftautowarhead = AlphaWarheadController.Host != null
                    ? (int)Mathf.Clamp(AlphaWarheadController.Host.timeToDetonation - RoundSummary.roundTime, 0, AlphaWarheadController.Host.timeToDetonation)
                    : -1;
                nextRespawnTime = (int)Math.Truncate(RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.RespawnCooldown
                    ? RespawnManager.Singleton._timeForNextSequence - RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds
                    : 0);
                isContain = PlayerManager.localPlayer.GetComponent<CharacterClassManager>()._lureSpj.allowContain;
                isAlreadyUsed = OneOhSixContainer.used;
                leftdecont = Mathf.Clamp(leftdecont, 0, leftdecont);
                #endregion

                #region AlfaWarheadMessage
                if (AlphaWarheadOutsitePanel.nukeside.enabled)
                    AlfaWarheadMessage =  Translation.ActiveTranslation.AlfaWarheadMessageReady;
                else
                    AlfaWarheadMessage =  Translation.ActiveTranslation.AlfaWarheadMessageDisabled;

                #endregion

                #region DecontaMessage
                if (Map.Get.GetVoltage() < 100)
                    DecontMessage =  Translation.ActiveTranslation.DecontMessageNotEnoughEnergy;
                else
                {
                    if (DecontaminationController.Singleton._nextPhase != 0)
                        DecontMessage =  Translation.ActiveTranslation.DecontMessageReady;
                    else if (DecontaminationController.Singleton._nextPhase == DecontaminationController.Singleton.DecontaminationPhases.Length - 1)
                        DecontMessage =  Translation.ActiveTranslation.DecontMessageInProgress;
                    else
                        DecontMessage =  Translation.ActiveTranslation.DecontMessageFinished;
                }

                #endregion

                #region RespawnMessage
                if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                    RespawnMessage =  Translation.ActiveTranslation.RespawnMessageMTF
                        .Replace("%Name%", RespawnManager.Singleton.NamingManager.name).Replace("%Temps%", $"t-{ nextRespawnTime / 60:00}:{ nextRespawnTime % 60:00}");
                else
                    RespawnMessage =  Translation.ActiveTranslation.RespawnMessageNoMTF;
                #endregion

                #region SCPListMessage
                var listScp = Server.Get.Players.Where(p => p.Team == Team.SCP || SCPaditonelle.Contains(p.RoleID));
                foreach (var scp in listScp)
                {
                    if (scp.RoleID == (int)RoleType.Scp079)
                        scpListMessage +=  Translation.ActiveTranslation.IntercomScpInformation079
                            .Replace("%Name%", scp.RoleName)
                            .Replace("%Tier%", scp.Hub.scp079PlayerScript.Lvl.ToString());
                    else
                        scpListMessage +=  Translation.ActiveTranslation.IntercomScpInformation
                            .Replace("%Name%", scp.RoleName)
                            .Replace("%Zone%", scp.Zone.ToString())
                            .Replace("%Room%", scp.Room.RoomName);
                    scpListMessage += "\n";
                }
                #endregion

                #region IntercomStatue

                if (_intercom.Muted)
                {
                    IntercomStatueMessage =  Translation.ActiveTranslation.IntercomStatueMute;
                }
                else if (Intercom.AdminSpeaking)
                {
                    IntercomStatueMessage =  Translation.ActiveTranslation.IntercomStatueAdmin.
                        Replace("%Player%", _intercom.GetPlayer().NickName);
                }
                else if (_intercom.remainingCooldown > 0f)
                {
                    IntercomStatueMessage =  Translation.ActiveTranslation.IntercomStatueRestart.
                        Replace("%Temps%", ((int)_intercom.remainingCooldown).ToString());
                }
                else if (_intercom.speaker != null)
                {
                    IntercomStatueMessage =  Translation.ActiveTranslation.IntercomStatuePlayer.
                        Replace("%Player%", _intercom.speaker.GetPlayer().NickName).Replace("%Temps%", ((int)_intercom.speechRemainingTime).ToString());
                }
                else
                {
                    IntercomStatueMessage =  Translation.ActiveTranslation.IntercomStatueReady;
                }
                #endregion

                #region GeneratorVoltage
                Voltage = Map.Get.GetVoltage();
                #endregion

                #region SCP106Message
                if (isContain)
                {
                    if (isAlreadyUsed)
                        SCP106Message = Translation.ActiveTranslation.SCP106Use;
                    else
                        SCP106Message = Translation.ActiveTranslation.SCP106Ready;
                }
                else
                {
                    SCP106Message =  Translation.ActiveTranslation.SCP106Empty;
                }
                #endregion

                #region Tesla
                if (TeslaEnabled)
                    TeslaMessage = Translation.ActiveTranslation.TeslaOn;
                else
                    TeslaMessage = Translation.ActiveTranslation.TeslaOff;
                #endregion

                #region DecontTime
                DecontTime = $"T-{leftdecont / 60:00}:{leftdecont % 60:00}";
                #endregion

                #region BrecheTime
                roundTime = $"T+{ RoundSummary.roundTime / 60:00}:{ RoundSummary.roundTime % 60:00}";
                #endregion



                switch (screen)
                {
                    case ScreenType.GeneralInfo:
                        ScreenMessage = Translation.ActiveTranslation.IntercomGeneralInformation;
                        ScreenMessage = Regex.Replace(ScreenMessage, "\\n", "\n", RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%RoundTime%", roundTime, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%nSCP%", nSCP.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%nCDP%", nCDP.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%nRSC%", nRSC.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%nVIP%", nVIP.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%nMTF%", nFIM.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%TotalVoltage%", Voltage.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%AlfaWarheadStatut%", AlfaWarheadMessage, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%Tesla%", TeslaMessage, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%IsContain%", SCP106Message, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%DecontMessage%", DecontMessage, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%DecontTime%", DecontTime.ToString(), RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%RespawnMessage%", RespawnMessage, RegexOptions.IgnoreCase);
                        ScreenMessage = Regex.Replace(ScreenMessage, "%IntercomStatue%", IntercomStatueMessage, RegexOptions.IgnoreCase);
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case ScreenType.ListScp:
                        ScreenMessage = string.Concat(
                        $"{(scpListMessage.Any() ? scpListMessage : Translation.ActiveTranslation.IntercomNoScpInformation)}\n",
                        $"─────────────────────────────────────\n");
                        ScreenMessage += IntercomStatueMessage;
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case ScreenType.Custom:
                        ScreenMessage = string.Concat(
                            $"\n",
                            $"───────────────────────────────────── \n ",
                            $"           [Insérée Message] ",
                            $"             I like train ! "
                            );
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case ScreenType.Defaux:
                        Map.Get.IntercomText = $"Error Please Screen {DateTime.Now}";
                        break;
                }
            }
            catch (Exception e)
            {
                Synapse.Api.Logger.Get.Error($"Erreur Intercom{e.Message}");
                Map.Get.IntercomText = $"Error Please Screen {DateTime.Now}:\n{e.Message}";
            }

        }

        public IEnumerator<float> Decontamination(int WaitForStart = 120, int AlertTime = 3)
        {
            DecontInProgress = true;
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
                Map.Get.PlayAmbientSound(7);
                AlertTime--;
                yield return Timing.WaitForSeconds(1f);
            }
            Map.Get.GlitchedCassie($"danger . LightContainmentZone decontamination start in 2 minutes .");
            WaitForStart += 5;
            while (WaitForStart > 0)
            {
                Map.Get.PlayAmbientSound(5);
                WaitForStart--;
                yield return Timing.WaitForSeconds(1f);
            }
            Map.Get.GlitchedCassie($"Light Containment Zone is locked down and ready for decontamination .");
            Server.Get.Map.Decontamination?.InstantStart();
            Server.Get.Map.Decontamination?.Controller?.FinishDecontamination();
            yield break;
        }
        public static float TimeLeftDecon()
        {
            var phase = DecontaminationController.Singleton.DecontaminationPhases;
            return Starting.phase;
            //return Plugin. DecontInProgress ? phase[phase.Length - 1].TimeTrigger - (float)DecontaminationController.GetServerTime : 180 ;
        }
    }
}
