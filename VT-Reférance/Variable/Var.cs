using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Variable
{
    public static class Dictionary
    {
        /// <summary>
        /// dictionary recording old player roles works with VT-CustomClass
        /// </summary>
        public static Dictionary<Player, int> PlayerRole = new Dictionary<Player, int>();
    }

    /// <summary>
    /// for class ally and ennemy
    /// </summary>
    public static class TeamGroupe
    {
        public static List<int> SCPally = new List<int> { (int)TeamID.SCP };

        public static List<int> MTFally = new List<int> { (int)TeamID.NTF, (int)TeamID.RSC, (int)TeamID.VIP, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.ASI, (int)TeamID.AL1 };

        public static List<int> CHIally = new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

        public static List<int> RSCally = MTFally;

        public static List<int> CDPally = CHIally;

        public static List<int> SHAally = new List<int> { (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.NetralSCP };

        public static List<int> VIPally = RSCally;

        public static List<int> NetralSCPally = new List<int> { (int)TeamID.NetralSCP, (int)TeamID.SHA };

        public static List<int> ANDally = new List<int> { (int)TeamID.AND };

        public static List<int> SCPenemy = MTFally.Concat(new List<int> { (int)TeamID.CDP }).ToList();

        public static List<int> MTFenemy = new List<int> { (int)TeamID.SCP, (int)TeamID.CHI, (int)TeamID.CDP, (int)TeamID.SHA, (int)TeamID.BerserkSCP, (int)TeamID.AND };

        public static List<int> CHIenemy = MTFally.Concat(new List<int> { (int)TeamID.NetralSCP, (int)TeamID.BerserkSCP }).ToList();

        public static List<int> RSCennemy = MTFenemy;

        public static List<int> CDPennemy = MTFally.Concat(new List<int> { (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.BerserkSCP }).ToList();

        public static List<int> SHAennemy = MTFally.Concat(new List<int> { (int)TeamID.CDP, (int)TeamID.BerserkSCP }).ToList();

        public static List<int> VIPennemy = MTFenemy;

        public static List<int> NetralSCPennemy = new List<int> { (int)TeamID.CHI };

        public static List<int> BerserkSCPennemy = MTFenemy.Concat( CHIenemy ).ToList();

        public static List<int> ANDennemy = MTFally;
    }
}
