using Synapse;
using Synapse.Api;
using Synapse.Api.Items;
using System;

namespace VTLog
{
    public static class Extension
    {
        internal static string GetInfoItems(this SynapseItem items)
        {
            try
            {
                return $"Items:({items?.Name}#{items?.ID}:{items?.IsCustomItem}#{items?.pickup?.netId}:{items?.pickup?.NetworkitemId}#{items?.Position}:{items?.Scale}#{items?.State}#{items?.Durabillity}.{items?.Barrel}.{items?.Other}.{items?.Sight}|{items?.ItemHolder?.UserId})";
            }
            catch (Exception e)
            {
                return $"Items:nullInfo!//?#serveur : {e}";
            }
        }

        internal static string GetInfoPlayer(this Player player)
        {
            try
            { 
            return $"Player:(#{player?.UserId}.{(player?.SecondUserID== null ? "#?null" : player?.SecondUserID)}#{player?.name}//{player?.NickName}.{player?.DisplayName}#{player?.RoleID}#{player?.Room}//{player?.Position}#{player?.IpAddress})";
            }
            catch (Exception e)
            { 
                return $"Player:nullInfo!//?#serveur {e}";
            }
        }

    }
}
