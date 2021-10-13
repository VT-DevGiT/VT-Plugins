using Synapse.Api.Enum;
using System;

namespace VT_Referance.Script.ItemScript
{
    public class ItemInformation : Attribute
    {
        public int ID = -1;
        public ItemType ItemType = ItemType.None;
        public string Name = "Unknown";
    }
}
