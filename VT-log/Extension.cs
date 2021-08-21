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
                return $"Items:({items?.Name}#{items?.ID}:{items?.IsCustomItem}#{items?.PickupBase?.netId}:{items?.PickupBase?.NetworkInfo}#{items?.Position}:{items?.Scale}#{items?.State}#{items?.Durabillity}.{"null"/*items?.Barrel*/}.{"null"/*items?.Other*/}.{"null"/*items?.Sight*/}|{"null"/*items?.ItemHolder?.UserId*/})";
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
                if (!player.IsDummy) return $"Player:(#{player?.UserId}.{(player?.SecondUserID== null ? "#?null" : player?.SecondUserID)}#{player?.name}//{player?.NickName}.{player?.DisplayName}#{player?.RoleID}#{player?.Room}//{player?.Position}#{player?.IpAddress})";
                else return $"Dummy:(#{player?.UserId}.{(player?.SecondUserID == null ? "#?null" : player?.SecondUserID)}#{player?.name}//{player?.NickName}.{player?.DisplayName}#{player?.RoleID}#{player?.Room}//{player?.Position}#{player?.IpAddress})";
            }
            catch (Exception e)
            { 
                return $"Player:nullInfo!//?#serveur {e}";
            }
        }

    }
}
