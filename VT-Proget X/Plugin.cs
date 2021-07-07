using HarmonyLib;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using Synapse.Translation;

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

    public class Plugin : AbstractPlugin
    {

        public static Plugin Instance { get; private set; }

        public bool DecontInProgress = false;
        public bool TeslaEnabled = true;
        public bool CustomScreen = false;

        [Synapse.Api.Plugin.Config(section = "VT-ProgetX")]
        public static Config Config;

        [SynapseTranslation]
        public static SynapseTranslation<PluginTranslation> PluginTranslation;
        private void PatchAll()
        {
            var instance = new Harmony("VTProget_X");
            instance.PatchAll();
            Server.Get.Logger.Info("VT-ProgetX Harmony Patch done!");
        }
        public override void Load()
        {
            Instance = this;
            PatchAll();
            base.Load();
            PluginTranslation.AddTranslation(new VTProget_X.PluginTranslation());
            PluginTranslation.AddTranslation(new VTProget_X.PluginTranslation
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
            new EventHandlers();
        }
    }
}
