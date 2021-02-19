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

namespace VTProget_X
{
    public static class Methode
    {

        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        public static T GetFieldValue<T>(this object element, string fieldName)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }

        public static void SetFieldValue<T>(this object element, string fieldName, T value)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(element, value);
        }

        public static int Voltage()
        {
            int totalvoltagefloat = 0f;
            foreach (var i in Generator079.Generators)
            {
                totalvoltagefloat += (int)i.localVoltage;
            }
            totalvoltagefloat *= 1000;
            return totalvoltagefloat;
        }

        public static bool DecontaminationStop = true;
        public static IEnumerator<float> Decontamination(int WaitForStart = 120, int AlertTime = 3)
        {

            foreach (var room in Server.Get.Map.Rooms.FindAll(p => p.Zone == ZoneType.LCZ))
            {
                foreach (var door in room.Doors)
                {
                    door.Locked = true;
                    door.Open = true;
                }
            }
            while (AlertTime > 0)
            {
                PlayAmbientSound(7);
                AlertTime--;
                yield return Timing.WaitForSeconds(1f);
            }
            Map.Get.Cassie($"danger . Light containment zone decontamination start in 2 minutes .", false);
            Plugin.Instance.DeconatmiantionendProgress = true;
            WaitForStart += 5;
            while (WaitForStart > 0)
            {
                PlayAmbientSound(5);
                WaitForStart--;
                yield return Timing.WaitForSeconds(1f);
            }
            if (!DecontaminationStop)
            {
                Map.Get.Cassie($"Light Containment Zone is locked down and ready for decontamination .", false);
                Server.Get.Map.Decontamination?.CallMethod("InstantStart");
                Server.Get.Map.Decontamination?.Controller?.CallMethod("FinishDecontamination");
                Plugin.Instance.DeconatmiantinEnd = true;
            }
            yield break;

        }

        public static void PlayAmbientSound(int id)
        {
            foreach (var player in Server.Get.Players)
            {
                player.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
                //player.GetComponent<AmbientSoundPlayer>().RpcPlaySound(id);
            }
            //PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
            //PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().RpcPlaySound(Mathf.Clamp(id, 0, 31));
        }

        public static void SendInterComInfoGeneral(screenEnum screen)
        {
            try
            {
                int leftdecont;
                int leftautowarhead;
                int nextRespawnTime;
                int nSCP;
                int nClassD;
                int nPersonnelle;
                int nVIP;
                int nFIM;
                bool isContain;
                bool isAlreadyUsed;
                string roundTime;
                string Voltage;
                string DecontTime;
                string TeslaMessage;
                string SCP106Message;
                string AlfaWarheadMessage;
                string nextRespawnMessage;
                string scpListMessage = string.Empty;
                string IntercomStatueMessage;
                string DecontMessage;
                string ScreenMessage;

                var _intercom = Server.Get.Host.GetComponent<Intercom>();

                nSCP = RoundSummary.singleton.CountTeam(Team.SCP);
                nClassD = RoundSummary.singleton.CountTeam(Team.CDP);
                nPersonnelle = RoundSummary.singleton.CountTeam(Team.RSC) + RoundSummary.singleton.CountRole(RoleType.FacilityGuard);
                nVIP = 0;
                nFIM = RoundSummary.singleton.CountTeam(Team.MTF) - RoundSummary.singleton.CountRole(RoleType.FacilityGuard);
                AlfaWarheadMessage = AlphaWarheadOutsitePanel.nukeside.enabled ? "PRÊTE" : "DÉSACTIVÉE";
                leftdecont = (int)((Math.Truncate((15f * 60) * 100f) / 100f) - (Math.Truncate(DecontaminationController.GetServerTime * 100f) / 100f));
                leftautowarhead = AlphaWarheadController.Host != null
                    ? (int)Mathf.Clamp(AlphaWarheadController.Host.timeToDetonation - RoundSummary.roundTime, 0, AlphaWarheadController.Host.timeToDetonation)
                    : -1;
                nextRespawnTime = (int)Math.Truncate(RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.RespawnCooldown
                    ? RespawnManager.Singleton.GetFieldValue<float>("_timeForNextSequence") - RespawnManager.Singleton.GetFieldValue<Stopwatch>("_stopwatch").Elapsed.TotalSeconds
                    : 0);
                isContain = PlayerManager.localPlayer.GetComponent<CharacterClassManager>().GetFieldValue<LureSubjectContainer>("_lureSpj").allowContain;
                isAlreadyUsed = OneOhSixContainer.used;
                leftdecont = Mathf.Clamp(leftdecont, 0, leftdecont);

                #region DecontaMessage
                if (Methode.Voltage() < 100)
                    DecontMessage = "PAS ASSEZ D'ÉNERGIE";
                else
                {
                    if (!Plugin.Instance.DeconatmiantionendProgress)
                        DecontMessage = "PRÊTE";
                    else if (!Plugin.Instance.DeconatmiantinEnd)
                        DecontMessage = "ENCOURS";
                    else
                        DecontMessage = "FINALISÉE";
                }

                #endregion

                #region RespawnMessage
                if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                    nextRespawnMessage = $"la division {RespawnManager.Singleton.NamingManager.name} arrivera dans  {nextRespawnTime / 60:00}:{nextRespawnTime % 60:00}";
                else
                    nextRespawnMessage = $"Aucune escouade n'est en route";
                #endregion

                #region SCPListMessage
                var listScp = Server.Get.Players.Where(p => p.Team == Team.SCP);
                foreach (var scp in listScp)
                {
                    if (scp.RoleType == RoleType.Scp079)
                        scpListMessage += $"{scp.RoleName} : Tier {scp.Hub.scp079PlayerScript.Lvl}\n";
                    else
                        scpListMessage += $"{scp.RoleName} : Zone {scp.Zone} : Room {scp.Room.RoomName}\n";
                }
                #endregion

                #region IntercomStatue

                if (_intercom.Muted)
                {
                    IntercomStatueMessage = "Accès refusé";
                }
                else if (Intercom.AdminSpeaking)
                {
                    IntercomStatueMessage = $"{_intercom.GetPlayer().NickName} Diffuse (prioritaire)";
                }
                else if (_intercom.remainingCooldown > 0f)
                {
                    IntercomStatueMessage = $"Temps avant redémarrage : { Mathf.CeilToInt(_intercom.remainingCooldown)} seconde(s)";
                }
                else if (_intercom.speaker != null)
                {
                    IntercomStatueMessage = $"{_intercom.speaker.GetPlayer().NickName} Diffuse : {Mathf.CeilToInt(_intercom.speechRemainingTime)} seconde(s)";
                }
                else
                {
                    IntercomStatueMessage = "Intercom prêt à l'emploi.";
                }
                #endregion

                #region GeneratorVoltage
                Voltage = $"{Methode.Voltage():0000}";
                #endregion

                #region SCP106Message
                if (isContain)
                { 
                    if (isAlreadyUsed)
                        SCP106Message = "UTILISÉ";
                    else
                        SCP106Message = "PRÊT";
                }
                else
                {
                    SCP106Message = "VIDE";
                }
                #endregion

                #region Tesla
                if (Plugin.Instance.TeslaEnabled)
                    TeslaMessage = "ACTIVÉES";
                else
                    TeslaMessage = "DÉSACTIVÉES";
                #endregion

                #region DecontTime
                DecontTime = $"{leftdecont / 60:00}:{leftdecont % 60:00}";

                #endregion

                #region BrecheTime
                roundTime = $"{ RoundSummary.roundTime / 60:00}:{ RoundSummary.roundTime % 60:00}";
                #endregion



                switch (screen)
                {
                    case screenEnum.GeneralInfo:
                        ScreenMessage = string.Concat(
                        $"─────────── Centre d'information FIM ───────────\n",
                        $"Durée de la brèche : {roundTime}\n",
                        $"SCP restant(s) : {nSCP}\n",
                        $"Classe D Restant(s) : {nClassD}\n",
                        $"Personnel(s) à évacuée réstant(s) : {nPersonnelle}\n",
                        $"VIP à évacuée réstant(s) : {nVIP}\n",
                        $"Personnel(s) militaire(s) déployé : {nFIM} \n",
                        $"Puissance des générateurs actif(s) : {Voltage}kVA\n",
                        $"Statut de l'ogive nucléaire Oméga : PRÊTE\n",
                        $"Statut de l'ogive nucléaire ALpha : {AlfaWarheadMessage}\n",
                        $"Statut du briseur de fémur pour SCP-106 : {SCP106Message}\n",
                        $"Statut des portes tesla : {TeslaMessage} \n",
                        $"Statut de la décontamination : {DecontMessage}\n",
                        $"Temps avent la décontamination : {DecontTime}\n",
                        $"{nextRespawnMessage}\n",
                        $"─────────────────────────────────────\n");
                        
                        ScreenMessage += IntercomStatueMessage;
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.ListScp:
                        ScreenMessage = string.Concat(
                        $"{(scpListMessage.Any() ? scpListMessage : "PAS DE SCP RESTANT")}\n",
                        $"─────────────────────────────────────\n");
                        ScreenMessage += IntercomStatueMessage;
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.Custom:
                        ScreenMessage = string.Concat(
                            "<color=#9300FF>Azarus</color>\n",
                            "───────────────────────────────────── \n ",
                            "           I LIKE TRAIN !! "
                            );
                        Map.Get.IntercomText = ScreenMessage;
                        break;

                    case screenEnum.Defaux:
                        Map.Get.IntercomText = "I LIKE TRAIN !";
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
