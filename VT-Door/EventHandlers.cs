using Synapse;
using System;
using System.Linq;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;
using Synapse.Api;
using Mirror;
using Synapse.Api.Events.SynapseEventArguments;

namespace VTDoor
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
            //Server.Get.Events.Map.DoorInteractEvent += OnDoor;
        }

        private void OnDoor(DoorInteractEventArgs ev)
        {
            // if the door is open it crashes the player (not cool) ;-; 
            if (ev.Door.Name == "buggyDoor")
            { 
                ev.Door.Open = !ev.Door.Open;
                ev.Allow = false;
            }
        }

        private void OnStart()
        {
            Synapse.Api.Door door;
             
            if (Plugin.Config.DoorList.Any())
               foreach (var Door in Plugin.Config.DoorList) 
               {
                    if (Server.Get.Map.Rooms.Where(p => p.RoomName == Door.Room).Count() != 0)
                    { 
                        door = Synapse.Api.Door.SpawnDoorVariant(Door.Parse().Position);
                        if (Door.Room == "LCZ_Airlock (1)" || Door.Room == "LCZ_Airlock (2)")
                            door.Name = "buggyDoor";
                    }
               }
        }
    }
}