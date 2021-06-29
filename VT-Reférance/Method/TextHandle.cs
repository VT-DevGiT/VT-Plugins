using HarmonyLib;
using Hints;
using MEC;
using Mirror;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Patch.VT_Patch;

namespace VT_Referance.Method
{
    public class TextHintTimed
    {
        public TextHint Hint { get; private set; }
        public bool NotRemouv { get; private set; }
        public int IdText { get; private set; }
        public uint Scall { get; private set; }
        public HintTextPos Pos { get; private set; }
        public uint Ligne { get; private set; }
        public float Length { get { return Text.Length * (float)Scall / 100; }}
        public string Text {
            get { return Hint.GetFieldValueorOrPerties<string>("Text"); }
            set { Hint.SetProperty<string>("Text", value); }
        }


        public TextHintTimed(TextHint hint, int Id = 0, HintTextPos pos = HintTextPos.CENTER, bool notRemouv = false, uint ligne = 12)
        {
            Hint = hint;
            IdText = Id;
            Pos = pos;
            NotRemouv = notRemouv;
            Ligne = ligne;
        }

        public override string ToString()
        {
            return $"{Text}";
        }

        
    }

    public class TextHandle
    {

        private Dictionary<NetworkIdentity, List<TextHintTimed>> _Messages = new Dictionary<NetworkIdentity, List<TextHintTimed>>();

        public Dictionary<NetworkIdentity, List<TextHintTimed>> Messages
        {
            get { return _Messages; }
        }

        private void CreateList(NetworkIdentity netIdentity)
        {
            if (!_Messages.ContainsKey(netIdentity))
            {
                _Messages.Add(netIdentity, new List<TextHintTimed>());
            }
        }

        public void AddMessage(Player player, TextHintTimed hint) => AddMessage(player.Hub.networkIdentity, hint);

        public void AddMessage(NetworkIdentity netIdentity, TextHintTimed hint)
        {
            CreateList(netIdentity);
            _Messages[netIdentity].RemoveAll(p => p.IdText == hint.IdText);

            /*
            uint ligne = hint.Ligne;
            List<TextHintTimed> TextToAdd = new List<TextHintTimed>();

            var reg = new Regex("<.?>");
            var regEqual = new Regex("=.?>");
            MatchCollection matches = reg.Matches(hint.Text);

            foreach (string entry in matches)
            {
                switch(true)
                {
                    case true when entry.Contains("/voffset"):

                        break;
                    case true when entry.Contains("voffset"):
                        Match stravecequal = regEqual.Match(entry);
                        if (stravecequal != null && stravecequal.Success)
                        {
                            string apresEqual = stravecequal.ToString().Substring(1, stravecequal.ToString().Length - 2);
                            uint.TryParse(apresEqual, out uint addligne);
                            ligne += addligne;
                            
                            TextToAdd.Add(new TextHintTimed(new TextHint(/* *//*, new HintParameter[] { new StringHintParameter("")), 
                                hint.IdText, HintTextPos.LEFT, hint.NotRemouv, ligne));
                        }
                        break;
                    case true when entry.Contains("indent"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("line-"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("case"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("margin"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("mspace"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("pos"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("style"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break;
                    case true when entry.Contains("width"):
                        hint.Text = hint.Text.Replace(entry, string.Empty);
                        break; 
                }
            }
            
            _Messages[netIdentity].Add(hint);
            Refresh(netIdentity);
            if (!hint.NotRemouv)
                Remove(netIdentity, hint, hint.Hint.DurationScalar);
            */
        }
        
        public void Remove(NetworkIdentity netIdentity, TextHintTimed text, float time)
        {
            Timing.CallDelayed(time + 0.01f, () =>
            {
                if (Messages[netIdentity].Contains(text))
                    Messages[netIdentity].Remove(text);
                Refresh(netIdentity);
            });
        }

        public void Refresh(NetworkIdentity netIdentity)
        {
            string logMessage = $"Refresh ! \nMessages : {Messages.Any()}";

            // longueur : 72 ; hauteur : 24 ; zone : L24
            string Text = "";
            if (Messages.Any() && Messages[netIdentity].Any())
            {
                logMessage += $"\nMessages[{netIdentity.assetId}] : {Messages[netIdentity].Any()}";
                Text += "<align=left><voffset=0.35em>";
                List<TextHintTimed> listMessage = Messages[netIdentity].OrderBy(p => p.Ligne)
                                                                       .ThenBy(p => (int)p.Pos).ToList();

                Server.Get.Logger.Send($"{listMessage.Any()}", ConsoleColor.Cyan);
                for (int i = 0; i < 24 && listMessage.Any(); i++)
                {
                    Server.Get.Logger.Send($"{i}", ConsoleColor.Cyan);
                    Server.Get.Logger.Send($"{listMessage.Any()}", ConsoleColor.Cyan);
                    float TextOfligneength = 0;
                    string TextOfLigne = "";
                    foreach(var message in listMessage)
                    {
                        int additionalSpace = 0;
                        switch (message.Pos)
                        {



                        }

                        if (message.Ligne <= i && additionalSpace + TextOfligneength + message.Length <= 72)
                        {
                            Server.Get.Logger.Send($"add message !", ConsoleColor.Cyan);
                            TextOfligneength += message.Length;
                            Server.Get.Logger.Send($"1", ConsoleColor.Cyan);
                            TextOfLigne = $"{TextOfLigne}{message}";
                            Server.Get.Logger.Send($"2", ConsoleColor.Cyan);
                            listMessage.Remove(message);
                            Server.Get.Logger.Send($"3", ConsoleColor.Cyan);
                        }
                        else break;
                    }
                    logMessage += $"\nTextOfLigne{i} : {TextOfLigne}";
                    TextOfLigne += '\n';
                }
                Text += "</voffset></align>";
                logMessage += $"\nText : {Text}";
                Server.Get.Logger.Send(logMessage, ConsoleColor.Yellow);
            }
            TextHint Hint = new TextHint(Text, new HintParameter[] { new StringHintParameter("") }, null, 36827);//36827? nobody will take (I think)
            netIdentity.GetPlayer().HintDisplay.Show(Hint);
        }
    }

    public enum HintTextPos
    {
        LEFT = 0,
        CENTER = 1,
        RIGHT = 2,
        
    }

    internal static class TextHandleSingleton
    {       
        private static TextHandle _instance;
        private static readonly object _lock = new object();

        public static TextHandle Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TextHandle();
                        var instance = new Harmony("VT_Referance.Patch.VT_Patch");
                        //instance.PatchAll();
                    }
                    return _instance;
                }
            }
        }
    }
}
