using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JetonClassManger
{
    public class Config : AbstractConfigSection
    {
        [Description("list of possible roles")]
        public List<SerializedRoleConfig> RoleList = new List<SerializedRoleConfig>()
        {
            new SerializedRoleConfig( (int)RoleType.Scp049, "Scp049", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp079, "Scp079", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp096, "Scp096", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp106, "Scp106", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp173, "Scp173", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp93953, "Scp93953", 1 ),
            new SerializedRoleConfig( (int)RoleType.Scp93989, "Scp93989", 1 ),
            new SerializedRoleConfig( (int)RoleType.ClassD, "Classe-D", -1 ),
            new SerializedRoleConfig( (int)RoleType.FacilityGuard, "Garde", -1 ),
            new SerializedRoleConfig( (int)RoleType.Scientist, "Scientfique", -1 ),

        };
        [Description("Time to change role")]
        public float TempChangement = 45 ; 
    }
}
