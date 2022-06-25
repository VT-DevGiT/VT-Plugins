using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VTCustomClass.Pouvoir
{
    public static class Cooldown
    {
        public static string Send(DateTime lastPower, int Time)
        {
            int cooldown = Time - (int)(DateTime.Now - lastPower).TotalSeconds;
            return Regex.Replace(Plugin.Instance.Translation.ActiveTranslation.PowerCooldown, "%Time%", cooldown.ToString(), RegexOptions.IgnoreCase);
        }
    }
}
