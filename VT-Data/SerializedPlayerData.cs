using System;

namespace JetonClassManger
{
    [Serializable]
    public class SerializedPlayerData
    {
        public int Steam { get; set; }
        public string NameRole { get; set; }
        public int MaxRole { get; set; }

        public SerializedPlayerData(int role, string nameRole, int maxRole)
        {
            //IDRole = role;
            NameRole = nameRole;
            MaxRole = maxRole;
        }
    }
}
