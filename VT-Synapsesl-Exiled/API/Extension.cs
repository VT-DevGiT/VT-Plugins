using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExiledPlayer = Exiled.API.Features.Player;
using SynapsePlayer = Synapse.Api.Player;
using QurrePlayer = Qurre.API.Player;

namespace VT_MultieLoder.API
{
    static class Extension
    {
        public static ExiledPlayer ToExiled(this SynapsePlayer player) => player.GetComponent<PlayerInfoCompatiblity>().ExiledPlayer;
        
        public static ExiledPlayer ToExiled(this QurrePlayer player) => player.GameObject.GetComponent<PlayerInfoCompatiblity>().ExiledPlayer;
        
        public static QurrePlayer ToQurre(this SynapsePlayer player) => player.GetComponent<PlayerInfoCompatiblity>().QurrePlayer;
        
        public static QurrePlayer ToQurre(this ExiledPlayer player) => player.GameObject.GetComponent<PlayerInfoCompatiblity>().QurrePlayer;
        
        public static SynapsePlayer ToSynapse(this ExiledPlayer player) => player.GameObject.GetComponent<PlayerInfoCompatiblity>().SynapsePlayer;
        
        public static SynapsePlayer ToSynapse(this QurrePlayer player) => player.GameObject.GetComponent<PlayerInfoCompatiblity>().SynapsePlayer;

        public static List<ExiledPlayer> ToExiled(this IEnumerable<SynapsePlayer> players)
        {
            List<ExiledPlayer> Exiledplayers = new List<ExiledPlayer>();
            foreach (SynapsePlayer player in players) Exiledplayers.Add(player.ToExiled());
            return Exiledplayers;
        }
        public static List<ExiledPlayer> ToExiled(this IEnumerable<QurrePlayer> players) 
        {
            List<ExiledPlayer> Exiledplayers = new List<ExiledPlayer>();
            foreach (QurrePlayer player in players) Exiledplayers.Add(player.ToExiled());
            return Exiledplayers;
        }
        public static List<QurrePlayer> ToQurre(this IEnumerable<SynapsePlayer> players) 
        {
            List<QurrePlayer> Qurreplayers = new List<QurrePlayer>();
            foreach (SynapsePlayer player in players) Qurreplayers.Add(player.ToQurre());
            return Qurreplayers;
        }
        public static List<QurrePlayer> ToQurre(this IEnumerable<ExiledPlayer> players) 
        {
            List<QurrePlayer> Qurreplayers = new List<QurrePlayer>();
            foreach  (ExiledPlayer player in players) Qurreplayers.Add(player.ToQurre());
            return Qurreplayers;
        }
        public static List<SynapsePlayer> ToSynapse(this IEnumerable<ExiledPlayer> players) 
        {
            List<SynapsePlayer> Synapseplayers = new List<SynapsePlayer>();
            foreach (ExiledPlayer player in players) Synapseplayers.Add(player.ToSynapse());
            return Synapseplayers;
        }
        public static List<SynapsePlayer> ToSynapse(this IEnumerable<QurrePlayer> players)
        {
            List<SynapsePlayer> Synapseplayers = new List<SynapsePlayer>();
            foreach (QurrePlayer player in players) Synapseplayers.Add(player.ToSynapse());
            return Synapseplayers;
        }
    }
}
