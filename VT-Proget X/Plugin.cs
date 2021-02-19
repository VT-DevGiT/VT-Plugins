using Synapse.Api;
using Synapse.Api.Plugin;
using Synapse.Translation;


namespace VTProget_X
{

    [PluginInformation(
Name = "VT-Intercom",
Author = "VT",
Description = "Adds functionality such as intercom information",
LoadPriority = 0,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.1.0"
)]

    public class Plugin : AbstractPlugin
    {

        public static Plugin Instance { get; private set; }

        public Player IntercomPlayer = null;
        public bool DeconatmiantinEnd = false;
        public bool DeconatmiantionendProgress = false;
        public bool TeslaEnabled = true;
        public bool CustomScreen = false;

        [Synapse.Api.Plugin.Config(section = "VT-Intercom")]
        public static Config Config;

        [SynapseTranslation]
        public static SynapseTranslation<Translation> Translation;

        public override void Load()
        {
            Instance = this;
            base.Load();
            Translation.AddTranslation(new VTProget_X.Translation());
            Translation.AddTranslation(new VTProget_X.Translation
            {
                IntercomGeneralInformation = "─────────── Centre d'information FIM ───────────\n " +
                                             "Durée de la brèche : %roundTime%\n" +
                                             "SCP restant(s) : %nSCP%\n " +
                                             "Classe D Restant(s) : %nClassD%\n" +
                                             "Personnel(s) à évacuée réstant(s) : %nPersonnelle%\n" +
                                             "VIP à évacuée réstant(s) : %nVIP%\n" +
                                             "Personnel(s) militaire(s) déployé : %nFIM% \n" +
                                             "Puissance des générateurs actif(s) : %TotalVoltage% kVA\n " +
                                             "Statut de l'ogive nucléaire Oméga : PRÊTE\n " +
                                             "Statut de l'ogive nucléaire ALpha : %AlfaWarheadMessage%\n " +
                                             "Statut du briseur de fémur pour SCP-106 : %isContain% \n " +
                                             "Statut des portes tesla : %TeslaEnabled% \n " +
                                             "Statut de la décontamination : %DecontMessage%\n " +
                                             "Temps avent la décontamination : %decont%\n" +
                                             "%nextRespawnMessage%\n" +
                                             "─────────────────────────────────────\n",
                IntercomScpInformation = "",
                IntercomNoScpInformation = "",
            }, "FRENCH");

            new EventHandlers();
        }
    }
}
