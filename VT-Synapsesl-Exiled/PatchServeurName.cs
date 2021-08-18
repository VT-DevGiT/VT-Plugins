using HarmonyLib;
using System;
using System.Text.RegularExpressions;

namespace VT_MultieLoder
{
    [HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]

    class PatchServeurName
    {
        private static void Postfix() => ServerConsole._serverName = GetName(ServerConsole._serverName);

        private static string GetName(string chaine)
        {
            var reg = new Regex(".*?<size=1>");
            MatchCollection matches = reg.Matches(chaine);
            if (matches.Count >= 1)
            {
                var modeLoader = GetModeLoader(chaine);
                return $"{matches[0]}{modeLoader}</size></color>";
            }
            else return chaine;
        }
        private static string GetModeLoader(string chaine)
        {
            string result = "";
            if (MultieLoder.Config.NameTracking)
                result = "VT 1.0.3.0 :";// you add the version in VT reference if you want it to update by itself :)
            var reg = new Regex("<size=1>.*");
            string[] separatingStrings = { "</size>" };
            var substr = chaine.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in substr)
            {
                MatchCollection matches = reg.Matches(str);
                foreach (var match in matches)
                {
                    var mod = match.ToString().Substring("<size=1>".Length);
                    result += $"{mod}-";
                }
            }
            return result;
        }
    }
}
