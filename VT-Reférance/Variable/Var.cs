using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Variable
{
    /// <summary>
    /// dictionary recording old player roles works with VT-CustomClass
    /// </summary>
    public static class Dictionary
    {
        public static Dictionary<Player, int> PlayerRole = new Dictionary<Player, int>();
    }

    public static class TeamGroupe
    {
        public static List<int> MTFEnemys { get; set; } = new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.AND, (int)TeamID.BerserkSCP, (int)TeamID.CDP };
        public static List<int> MTFFriends { get; set; } = new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.ASI, (int)TeamID.AL1, (int)TeamID.RSC };
        public static List<int> SCP { get; set; } = new List<int> { (int)TeamID.BerserkSCP, (int)TeamID.NetralSCP, (int)TeamID.SCP };
        public static List<int> SCPBerserkEnemis { get; set; } = new List<int> { (int)TeamID.AL1, (int)TeamID.AND, (int)TeamID.ASI, (int)TeamID.BerserkSCP, (int)TeamID.CDM, (int)TeamID.CDP, 
                                                                                 (int)TeamID.CHI, (int)TeamID.MTF, (int)TeamID.NetralSCP, (int)TeamID.RSC, (int)TeamID.SCP, (int)TeamID.SHA,
                                                                                 (int)TeamID.U2I, (int)TeamID.VIP};
        public static List<int> SCPEnemis { get; set; } = new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.VIP, (int)TeamID.ASI, (int)TeamID.AL1, (int)TeamID.RSC, (int)TeamID.CDP };
        public static List<int> CHIEnemis { get; set; } = new List<int> { };
        public static List<int> CHIFriends { get; set; } = new List<int> { };
    }

}
