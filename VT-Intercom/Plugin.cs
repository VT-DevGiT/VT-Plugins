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

namespace VTIntercom
{

    [PluginInformation(
Name = "VT-Intercom",
Author = "VT",
Description = "Adds functionality such as intercom information",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.3.3"
)]

    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public Intercom Intercom { get; set; }

        public override bool AutoRegister => false;

        public bool DecontInProgress { get; set; } = false;
        public bool TeslaEnabled { get; set; } = true;
        public bool CustomScreen { get; set; } = false;


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
            Translation.AddTranslation(new VTIntercom.Translation
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
                IntercomStatueRestart = "Temps avant redémarrage : %Time%",
                IntercomStatuePlayer = "%Player% Diffuse : %Time%",
                IntercomStatueAdmin = "%Player% Diffuse (prioritaire)",
                IntercomStatueReady = "Intercom prêt à l'emploi.",
                IntercomStatueMute = "Accès refusé",
                DecontMessageNotEnoughEnergy = "PAS ASSEZ D'ÉNERGIE",
                DecontMessageReady = "PRÊTE",
                DecontMessageInProgress = "ENCOURS",
                DecontMessageFinished = "FINALISÉE",
                AlfaWarheadMessageReady = "PRÊTE",
                AlfaWarheadMessageDisabled = "DÉSACTIVÉE",
                RespawnMessageMTF = "la division %Name% arrivera dans %Time%",
                RespawnMessageNoMTF = "Aucune escouade n'est en route",
            }, "FRENCH");
        }

        public void SetIntercomScreen(ScreenType screen)
        {
            try
            {
                string ScreenMessage;
                string IntercomStatueMessage;
                
                #region IntercomStatue
                if (Intercom.Muted)
                {
                    IntercomStatueMessage = Translation.ActiveTranslation.IntercomStatueMute;
                }
                else if (global::Intercom.AdminSpeaking)
                {
                    IntercomStatueMessage = Translation.ActiveTranslation.IntercomStatueAdmin.
                        Replace("%Player%", Intercom.GetPlayer().NickName);
                }
                else if (Intercom.remainingCooldown > 0f)
                {
                    IntercomStatueMessage = Regex.Replace(Translation.ActiveTranslation.IntercomStatueRestart, "%Time%", ((int)Intercom.remainingCooldown).ToString(), RegexOptions.IgnoreCase);
                }
                else if (Intercom.speaker != null)
                {
                    IntercomStatueMessage = Regex.Replace(Translation.ActiveTranslation.IntercomStatuePlayer, "%Player%", Intercom.speaker.GetPlayer().NickName, RegexOptions.IgnoreCase);
                    IntercomStatueMessage = Regex.Replace(IntercomStatueMessage, "%Time%", ((int)Intercom.speechRemainingTime).ToString(), RegexOptions.IgnoreCase);
                }
                else
                {
                    IntercomStatueMessage = Translation.ActiveTranslation.IntercomStatueReady;
                }
                #endregion

                switch (screen)
                {
                    case ScreenType.GeneralInfo:
                        Map.Get.IntercomText = GeneralInfo(IntercomStatueMessage);
                        break;

                    case ScreenType.ListScp:
                        string scpListMessage = string.Empty;
                        #region SCPListMessage
                        var listScp = Server.Get.Players.Where(p => p.TeamID == (int)Team.SCP);
                        foreach (var scp in listScp)
                        {
                            if (scp.RoleID == (int)RoleType.Scp079)
                            {
                                var scpMessage = Translation.ActiveTranslation.IntercomScpInformation079;
                                scpMessage = Regex.Replace(scpMessage, "%Name%", scp.RoleName, RegexOptions.IgnoreCase);
                                scpMessage = Regex.Replace(scpMessage, "%Tier%", scp.Hub.scp079PlayerScript.Lvl.ToString(), RegexOptions.IgnoreCase);
                                scpListMessage += scpMessage;
                            }
                            else
                            {
                                var scpMessage = Translation.ActiveTranslation.IntercomScpInformation;
                                scpMessage = Regex.Replace(scpMessage, "%Name%", scp.RoleName, RegexOptions.IgnoreCase);
                                scpMessage = Regex.Replace(scpMessage, "%Zone%", scp.Zone.ToString(), RegexOptions.IgnoreCase);
                                scpMessage = Regex.Replace(scpMessage, "%Room%", scp.Room.RoomName, RegexOptions.IgnoreCase);
                                scpListMessage += scpMessage;
                            }
                            scpListMessage += "\n";
                        }

                        #endregion
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
                            $"              LETS GO !!!",
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

        public string GeneralInfo(string IntercomStatueMessage)
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
            string decontTime;
            string teslaMessage;
            string scp106Message;
            string alfaWarheadMessage;
            string respawnMessage;
            string decontMessage;

            string screenMessage;

            #region int & bool
            nSCP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.SCP || p.TeamID == (int)TeamID.NetralSCP || p.TeamID == (int)TeamID.BerserkSCP).Count();
            nCDP = Server.Get.Players.Where(p => p.RoleID == (int)RoleType.ClassD).Count();
            nRSC = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.RSC).Count();
            nVIP = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.VIP).Count();
            nFIM = Server.Get.Players.Where(p => p.TeamID == (int)TeamID.NTF || p.TeamID == (int)TeamID.CDM && p.TeamID == (int)TeamID.AL1).Count();
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

            #region Alfa Warhead Message
            if (AlphaWarheadOutsitePanel.nukeside.enabled)
                alfaWarheadMessage = Translation.ActiveTranslation.AlfaWarheadMessageReady;
            else
                alfaWarheadMessage = Translation.ActiveTranslation.AlfaWarheadMessageDisabled;

            #endregion

            #region Deconta Message
            if (Map.Get.GetVoltage() < 100)
                decontMessage = Translation.ActiveTranslation.DecontMessageNotEnoughEnergy;
            else
            {
                if (DecontaminationController.Singleton._nextPhase != 0)
                    decontMessage = Translation.ActiveTranslation.DecontMessageReady;
                else if (DecontaminationController.Singleton._nextPhase == DecontaminationController.Singleton.DecontaminationPhases.Length - 1)
                    decontMessage = Translation.ActiveTranslation.DecontMessageInProgress;
                else
                    decontMessage = Translation.ActiveTranslation.DecontMessageFinished;
            }

            #endregion

            #region Respawn Message
            if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
            {
                respawnMessage = Translation.ActiveTranslation.RespawnMessageMTF;
                respawnMessage = Regex.Replace(respawnMessage, "%Name%", RespawnManager.Singleton.NamingManager.name, RegexOptions.IgnoreCase);
                respawnMessage = Regex.Replace(respawnMessage, "%Time%", $"t-{ nextRespawnTime / 60:00}:{ nextRespawnTime % 60:00}", RegexOptions.IgnoreCase);
            }
            else
                respawnMessage = Translation.ActiveTranslation.RespawnMessageNoMTF;
            #endregion

            #region Generator Voltage
            Voltage = Map.Get.GetVoltage();
            #endregion

            #region SCP106Message
            if (isContain)
            {
                if (isAlreadyUsed)
                    scp106Message = Translation.ActiveTranslation.SCP106Use;
                else
                    scp106Message = Translation.ActiveTranslation.SCP106Ready;
            }
            else
            {
                scp106Message = Translation.ActiveTranslation.SCP106Empty;
            }
            #endregion

            #region Tesla
            if (TeslaEnabled)
                teslaMessage = Translation.ActiveTranslation.TeslaOn;
            else
                teslaMessage = Translation.ActiveTranslation.TeslaOff;
            #endregion

            #region DecontTime
            decontTime = $"T-{leftdecont / 60:00}:{leftdecont % 60:00}";
            #endregion

            #region BrecheTime
            roundTime = $"T+{ RoundSummary.roundTime / 60:00}:{ RoundSummary.roundTime % 60:00}";
            #endregion

            screenMessage = Translation.ActiveTranslation.IntercomGeneralInformation;
            screenMessage = Regex.Replace(screenMessage, "\\n", "\n", RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%RoundTime%", roundTime, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%nSCP%", nSCP.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%nCDP%", nCDP.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%nRSC%", nRSC.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%nVIP%", nVIP.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%nMTF%", nFIM.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%TotalVoltage%", Voltage.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%AlfaWarheadStatut%", alfaWarheadMessage, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%Tesla%", teslaMessage, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%IsContain%", scp106Message, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%DecontMessage%", decontMessage, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%DecontTime%", decontTime.ToString(), RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%RespawnMessage%", respawnMessage, RegexOptions.IgnoreCase);
            screenMessage = Regex.Replace(screenMessage, "%IntercomStatue%", IntercomStatueMessage, RegexOptions.IgnoreCase);
            return screenMessage;
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
            return DecontPatch.phase;
            //return Plugin. DecontInProgress ? phase[phase.Length - 1].TimeTrigger - (float)DecontaminationController.GetServerTime : 180 ;
        }
    }
}
