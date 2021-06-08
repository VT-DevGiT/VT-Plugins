using Synapse.Api;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.NpcScript;
using VT_Referance.Variable.Npc;

namespace VT_Referance.Variable
{
    [API]
    public static class Data
    {
        /// <summary>
        /// dictionary recording old player roles works with VT-CustomClass
        /// </summary>
        public static Dictionary<Player, int> PlayerRole = new Dictionary<Player, int>();

        /// <summary>
        /// List of the points for the Air-Bomb
        /// </summary>
        public static List<Vector3> AirbombPos
        {
            get
            {
                return new List<Vector3>()
                {
                    new Vector3(UnityEngine.Random.Range(175, 182),  984, UnityEngine.Random.Range( 25,  29)),
                    new Vector3(UnityEngine.Random.Range(174, 182),  984, UnityEngine.Random.Range( 36,  39)),
                    new Vector3(UnityEngine.Random.Range(174, 182),  984, UnityEngine.Random.Range( 36,  39)),
                    new Vector3(UnityEngine.Random.Range(166, 174),  984, UnityEngine.Random.Range( 26,  39)),
                    new Vector3(UnityEngine.Random.Range(169, 171),  987, UnityEngine.Random.Range(  9,  24)),
                    new Vector3(UnityEngine.Random.Range(174, 175),  988, UnityEngine.Random.Range( 10,  -2)),
                    new Vector3(UnityEngine.Random.Range(186, 174),  990, UnityEngine.Random.Range( -1,  -2)),
                    new Vector3(UnityEngine.Random.Range(186, 189),  991, UnityEngine.Random.Range( -1, -24)),
                    new Vector3(UnityEngine.Random.Range(186, 189),  991, UnityEngine.Random.Range( -1, -24)),
                    new Vector3(UnityEngine.Random.Range(185, 189),  993, UnityEngine.Random.Range(-26, -34)),
                    new Vector3(UnityEngine.Random.Range(180, 195),  995, UnityEngine.Random.Range(-36, -91)),
                    new Vector3(UnityEngine.Random.Range(148, 179),  995, UnityEngine.Random.Range(-45, -72)),
                    new Vector3(UnityEngine.Random.Range(118, 148),  995, UnityEngine.Random.Range(-47, -65)),
                    new Vector3(UnityEngine.Random.Range( 83, 118),  995, UnityEngine.Random.Range(-47, -65)),
                    new Vector3(UnityEngine.Random.Range( 13,  15),  995, UnityEngine.Random.Range(-18, -48)),
                    new Vector3(UnityEngine.Random.Range( 84,  88),  988, UnityEngine.Random.Range(-67, -70)),
                    new Vector3(UnityEngine.Random.Range( 68,  83),  988, UnityEngine.Random.Range(-52, -66)),
                    new Vector3(UnityEngine.Random.Range( 53,  68),  988, UnityEngine.Random.Range(-53, -63)),
                    new Vector3(UnityEngine.Random.Range( 12,  49),  988, UnityEngine.Random.Range(-47, -66)),
                    new Vector3(UnityEngine.Random.Range( 38,  42),  988, UnityEngine.Random.Range(-40, -47)),
                    new Vector3(UnityEngine.Random.Range( 38,  43),  988, UnityEngine.Random.Range(-32, -38)),
                    new Vector3(UnityEngine.Random.Range(-25,  12),  988, UnityEngine.Random.Range(-50, -66)),
                    new Vector3(UnityEngine.Random.Range(-26, -56),  988, UnityEngine.Random.Range(-50, -66)),
                    new Vector3(UnityEngine.Random.Range( -3, -24), 1001, UnityEngine.Random.Range(-66, -73)),
                    new Vector3(UnityEngine.Random.Range(  5,  28), 1001, UnityEngine.Random.Range(-66, -73)),
                    new Vector3(UnityEngine.Random.Range( 29,  55), 1001, UnityEngine.Random.Range(-66, -73)),
                    new Vector3(UnityEngine.Random.Range( 50,  54), 1001, UnityEngine.Random.Range(-49, -66)),
                    new Vector3(UnityEngine.Random.Range( 24,  48), 1001, UnityEngine.Random.Range(-41, -46)),
                    new Vector3(UnityEngine.Random.Range(  5,  24), 1001, UnityEngine.Random.Range(-41, -46)),
                    new Vector3(UnityEngine.Random.Range( -4, -17), 1001, UnityEngine.Random.Range(-41, -46)),
                    new Vector3(UnityEngine.Random.Range(  4,  -4), 1001, UnityEngine.Random.Range(-25, -40)),
                    new Vector3(UnityEngine.Random.Range( 11, -11), 1001, UnityEngine.Random.Range(-18, -21)),
                    new Vector3(UnityEngine.Random.Range(  3,  -3), 1001, UnityEngine.Random.Range( -4, -17)),
                    new Vector3(UnityEngine.Random.Range(  2,  14), 1001, UnityEngine.Random.Range(  3,  -3)),
                    new Vector3(UnityEngine.Random.Range( -1, -13), 1001, UnityEngine.Random.Range(  4,  -3)),
                };
            }
        }


        /// <summary>
        /// for class ally and ennemy
        /// </summary>
        public static class TeamGroupe
        {
            public readonly static List<int> SCPally = new List<int> { (int)TeamID.SCP };

            public readonly static List<int> MTFally = new List<int> { (int)TeamID.NTF, (int)TeamID.RSC, (int)TeamID.VIP, (int)TeamID.CDM, (int)TeamID.U2I, (int)TeamID.ASI, (int)TeamID.AL1 };

            public readonly static List<int> CHIally = new List<int> { (int)TeamID.CHI, (int)TeamID.CDP };

            public readonly static List<int> RSCally = MTFally;

            public readonly static List<int> CDPally = CHIally;

            public readonly static List<int> SHAally = new List<int> { (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.NetralSCP };

            public readonly static List<int> VIPally = RSCally;

            public readonly static List<int> NetralSCPally = new List<int> { (int)TeamID.NetralSCP, (int)TeamID.SHA };

            public readonly static List<int> ANDally = new List<int> { (int)TeamID.AND };

            public readonly static List<int> SCPenemy = MTFally.Concat(new List<int> { (int)TeamID.CDP }).ToList();

            public readonly static List<int> MTFenemy = new List<int> { (int)TeamID.SCP, (int)TeamID.CHI, (int)TeamID.CDP, (int)TeamID.SHA, (int)TeamID.BerserkSCP, (int)TeamID.AND };

            public readonly static List<int> CHIenemy = MTFally.Concat(new List<int> { (int)TeamID.NetralSCP, (int)TeamID.BerserkSCP }).ToList();

            public readonly static List<int> RSCennemy = MTFenemy;

            public readonly static List<int> CDPennemy = MTFally.Concat(new List<int> { (int)TeamID.SCP, (int)TeamID.SHA, (int)TeamID.BerserkSCP }).ToList();

            public readonly static List<int> SHAennemy = MTFally.Concat(new List<int> { (int)TeamID.CDP, (int)TeamID.BerserkSCP }).ToList();

            public readonly static List<int> VIPennemy = MTFenemy;

            public static List<int> NetralSCPennemy = new List<int> { (int)TeamID.CHI };

            public static List<int> BerserkSCPennemy = MTFenemy.Concat(CHIenemy).ToList();

            public static List<int> ANDennemy = MTFally;
        }
    }    
}
