using Synapse.Api;
using Synapse.Config;
using System.Linq;

namespace VT_Referance.Method
{
    public static class Config_Extension
    {
        /// <summary>
        /// Check if the config of the Inventory is not empty
        /// </summary>
        [API]
        public static bool IsDefined(this SerializedPlayerInventory item)
        {
            return item.Ammo != null || (item.Items != null && item.Items.Any());
        }
    }
}
