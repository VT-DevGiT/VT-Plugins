using Interactables.Interobjects.DoorUtils;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;

using UnityEngine;
using Interactables.Interobjects.DoorUtils;
using vDoor = Interactables.Interobjects.DoorUtils.DoorVariant;
using Interactables.Interobjects;
using Mirror;
using System.Collections.Generic;

namespace VTEscape
{
    public class EventHandlers
    {
        private readonly SerializedMapPoint DoorSpawnPos = new SerializedMapPoint("EZ_Shelter", 0.04031754f, 1.495544f, 10.24014f);
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
        }

        private void OnPlayerDamageEvent(PlayerDamageEventArgs ev)
        {

        }
        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<NTFEscape>()?.Kill();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<NTFEscape>();
                }
            }
            if (Plugin.Config.ICEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<CHIEscape>()?.Kill();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<CHIEscape>();
                }
            }
        }
        private void Waiting()
        {

            if (Plugin.Config.ShelterIsEnabled)
            {
                //Synapse.Api.Room shelterRoom = Synapse.Api.Map.Get.GetRoom(RoomInformation.RoomType.EZ_SHELTER);


                //Synapse.Api.Door shelterDoor = Synapse.Api.Map.Get.GetDoor(Synapse.Api.Enum.DoorType.);

                //var door = Synapse.Api.Door.SpawnDoorVariant(DoorSpawnPos.Parse().Position, Synapse.Api.Map.Get.GetRoom(RoomInformation.RoomType.EZ_SHELTER).GameObject.transform.rotation);
            }
        }
    }
}