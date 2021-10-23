using Synapse.Api;
using UnityEngine;

namespace VT_Referance.Method
{
    public static class Room_Extension
    {
        /// <summary>
        /// Change the color of the room
        /// </summary>
        /// <param name="color">The new color</param>
        public static void ChangeRoomLightColor(this Room room, Color color, bool activeColor = true)
        {
            room.LightController.WarheadLightColor = color;
            room.LightController.WarheadLightOverride = activeColor;
        }
    }
}
