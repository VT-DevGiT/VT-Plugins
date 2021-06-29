using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTProget_X
{
    public class PluginTranslation : IPluginTranslation
    {
        public string IntercomGeneralInformation = 
@"─────────── MTF Information Center ───────────
Duration of the breach : %RoundTime%
Remaining SCP : %nSCP%
Remaining Class D : %nCDP%
Remaining staff : %nRSC%
Remaining VIP : %nVIP%
Deployed military personnel : %nMTF%
Active generator power : %TotalVoltage% kVA
Status of the Omega nuclear warhead : PRÊTE
Status of the Alfa nuclear warhead : %AlfaWarheadStatut%
Status of the SCP containment 106 : %IsContain%
status of the Tesla doors : %Tesla%
status of the decontamination : %DecontMessage%
Time before decontamination : %DecontTime%
%RespawnMessage%
─────────────────────────────────────
%IntercomStatue%";

        public string IntercomScpInformation = "%Name% : Zone %Zone% : Room %Room%";

        public string IntercomScpInformation079 = "%Name% : Tier %Tier%";

        public string IntercomNoScpInformation = "NO SCP REMAINING";

        public string TeslaOn = "ENABLED";

        public string TeslaOff = "DISABLED";

        public string SCP106Ready = "READY";

        public string SCP106Use = "USE";

        public string SCP106Empty = "EMPTY";

        public string IntercomStatueRestart = "Time before restart : %Temps%";

        public string IntercomStatuePlayer = "%Player% broadcasts : %Temps%";

        public string IntercomStatueAdmin = "%Player% Admin broadcasts ";

        public string IntercomStatueReady = "Intercom ready to use.";

        public string IntercomStatueMute = "Access denied";

        public string DecontMessageNotEnoughEnergy = "NOT ENOUGH ENERGY";

        public string DecontMessageReady = "READ";

        public string DecontMessageInProgress = "IN PROGRESS";

        public string DecontMessageFinished = "FINALIZED";

        public string AlfaWarheadMessageReady = "READY";

        public string AlfaWarheadMessageDisabled = "DISABLED";

        public string RespawnMessageMTF = "%Name% division will arrive in %Temps%";

        public string RespawnMessageNoMTF = "No squad is on the way";
    }
}
