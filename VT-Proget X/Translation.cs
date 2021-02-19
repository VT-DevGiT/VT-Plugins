using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTProget_X
{
    public class Translation : IPluginTranslation
    {
        public string IntercomGeneralInformation = "─────────── Centre d'information FIM ───────────\n " +
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
                                                   "Temps avent la décontamination : %decont%\n"+
                                                   "%nextRespawnMessage%\n"+
                                                   "─────────────────────────────────────\n";

        public string IntercomScpInformation = "";

        public string IntercomNoScpInformation = "";
    
    }
}
