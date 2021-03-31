using System;

namespace JetonClassManger
{
    [Serializable]
    public class SerializedRoleConfig
    {
        public int IDRole { get; set; }
        public string NameRole { get; set; }
        public int MaxRole { get; set; }

        public SerializedRoleConfig(int role, string nameRole, int maxRole)
        {
            IDRole = role;
            NameRole = nameRole;
            MaxRole = maxRole;
        }
        public SerializedRoleConfig() 
        {

        }



    }
}
