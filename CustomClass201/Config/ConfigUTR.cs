using System.Collections.Generic;
using System.ComponentModel;
using VT_Referance.Variable;

namespace CustomClass.Config
{
    public class ConfigUTR
    {
        [Description("UTR ignor SCP 173")]
        public bool ingor173 = true;

        [Description("UTR ignor SCP 096")]
        public bool ingor096 = true;

        [Description("List of the SCPs can damage UTR")]
        public List<int> ListScpDamge = new List<int>()
        {
            (int)RoleID.Scp0492, (int)RoleID.Scp096, (int)RoleID.Scp106,
            (int)RoleID.Scp93953, (int)RoleID.Scp93989, (int)RoleID.SCP008,
            (int)RoleID.SCP966
        };

        public int damage = 20;

        [Description("List of the SCPs cannot damge UTR")]
        public List<int> ListScpNoDamge = new List<int>()
        {
            (int)RoleID.Scp173, (int)RoleID.Scp049
        };
    }
}
