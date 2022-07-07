using Scp914;
using Synapse;
using Synapse.Api;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Common_Utiles.Config
{
    [Serializable]
    public class Serialized914Role
    {
        List<int> SCPs = new List<int>() { 0, 3, 5, 9, 10, 16, 17, 35, 56, 122, 123 };
        List<int> Ds = new List<int>() { 1, 100 };
        List<int> NTFs = new List<int>() { 11, 12, 13, 4, 105, 106, 107, 108, 109, 110, 111, 143, 144, 145 };
        List<int> Guards = new List<int>() { 15, 101, 145, 104 };
        List<int> Scientists = new List<int>() { 6, 102, 103, 147 };
        List<int> Chaoss = new List<int>() { 8, 18, 19, 20, 113, 114, 115, 116, 117, 118, 119, 120 };
        public int RoleID { get; set; }
        public int RoughRoleID { get; set; }
        public int CorseRoleID { get; set; }
        public int OneToOneRoleID { get; set; }
        public int FineRoleID { get; set; }
        public int VeryFineRoleID { get; set; }
        public int Chance { get; set; }

        public Serialized914Role()
        {

        }

        public Serialized914Role(int roleID , int chance, int roughRoleID, int corseRoleID, int oneToOneRoleID, int fineRoleID, int veryFineRoleID)
        {
            RoleID = roleID;
            Chance = chance;
            RoughRoleID = roughRoleID;
            CorseRoleID = corseRoleID;
            OneToOneRoleID = oneToOneRoleID;
            FineRoleID = fineRoleID;
            VeryFineRoleID = veryFineRoleID;
        }

        public void Apply(Player player, Scp914KnobSetting setting)
        {            
            if ((player.RoleID == RoleID || RoleID == -1) && UnityEngine.Random.Range(0, 100) <= Chance)
            {
            
                switch (setting)
                {
                    case Scp914KnobSetting.Rough: 
                        if(RoughRoleID != -1)
                            player.RoleID = RoughRoleID; 
                        return;
                    case Scp914KnobSetting.Coarse: 
                        if(CorseRoleID != -1)
                        {
                            player.RoleID = CorseRoleID;
                        }
                                                                                                         
                        return;
                    case Scp914KnobSetting.OneToOne:
                        if(OneToOneRoleID != -1) { 
                            if (OneToOneRoleID == -2)
                            {                   
                                
                                OneToOneRoleID = SCPs[UnityEngine.Random.Range(0, SCPs.Count - 1)];
                            }
                            if(OneToOneRoleID == -3)
                            {
                                OneToOneRoleID = Ds[UnityEngine.Random.Range(0, Ds.Count - 1)];
                            }
                            if (OneToOneRoleID == -4)
                            {
                                OneToOneRoleID = NTFs[UnityEngine.Random.Range(0, NTFs.Count - 1)];
                            }
                            if (OneToOneRoleID == -5)
                            {
                                OneToOneRoleID = Guards[UnityEngine.Random.Range(0, Guards.Count - 1)];
                            }
                            if (OneToOneRoleID == -6)
                            {
                                OneToOneRoleID = Scientists[UnityEngine.Random.Range(0, Scientists.Count - 1)];
                            }
                            if (OneToOneRoleID == -7)
                            {
                                OneToOneRoleID = Chaoss[UnityEngine.Random.Range(0, Chaoss.Count - 1)];
                            }
                            player.RoleID = OneToOneRoleID;
                        }
                        return;
                    case Scp914KnobSetting.Fine:
                        if(FineRoleID != -1)
                            player.RoleID = FineRoleID; 
                        return;
                    case Scp914KnobSetting.VeryFine:
                        if(VeryFineRoleID != -1)
                            player.RoleID = VeryFineRoleID; 
                        return;
                }
            }
        }
    }
}
