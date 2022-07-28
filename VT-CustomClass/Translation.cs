using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCustomClass
{
    public class Translation : IPluginTranslation
    {
        public string SpawnMessage { get; set; } = "<color=blue><b>You are now</b></color> <color=red><b>%RoleName%</b></color>\\n<b>Press Esc to close</b>";
        public string VentMessage { get; set; } = "you can stay another %Time% seconds in the ventilation";
        public string NoTimeVentMessage { get; set; } = "you are in the ventilation";
        public string GoInTheVent { get; set; } = "You are now in the ventilation !";
        public string ExitTheVent { get; set; } = "You exit the the ventilation !";
        public string PowerCooldown { get; set; } = "you can use this power in %Time% seconds";
        public string KilledMessage { get; set; } = "You are killed by %RoleName%";
        public string OnlyOnePower { get; set; } = "You ave only one power";
        public string TargetLock { get; set; } = "You ave lock a target";
        public string NeedToLookAPlayer { get; set; } = "You need to look a enemy player !";
        public string UnlockTarget { get; set; } = "You unlock your Target";
        public string NewTargetLock { get; set; } = "You ave lock a new Target";
        public string OnlyNPower { get; set; } = "You ave only {0} powers";
    }
}
