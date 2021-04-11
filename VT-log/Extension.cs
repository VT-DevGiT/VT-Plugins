using Synapse.Api;
using Synapse.Api.Items;

namespace VTLog
{
    public static class Extension
    {
        internal static string GetInfoItems(this SynapseItem items)
        {
            return $"Items:({items.Name}#{items.ID}:{items.IsCustomItem}#{items.pickup.netId}//{items.pickup.previousId}:{items.pickup.NetworkitemId}#{items.Position}:{items.Scale}#{items.State}#{items.Durabillity}.{items.Barrel}.{items.Other}.{items.Sight}|{items.ItemHolder.UserId})";
        }

        internal static string GetInfoPlayer(this Player player)
        {
            return $"Player:(#{player?.UserId}.{player?.SecondUserID}#{player.name}//{player.NickName}.{player.DisplayName}#{player.RoleID}#{player.Room}//{player.Position})";
        }

    }
}
