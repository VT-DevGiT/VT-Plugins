using Synapse;
using System;
using System.Linq;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;
using Synapse.Api;
using Mirror;
using Synapse.Api.Events.SynapseEventArguments;

namespace VTMap
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            if (Plugin.Instance.Config.DoorList.Any())
               foreach (var Door in Plugin.Instance.Config.DoorList) 
               {
                    Quaternion? Roation;
                    if (Door.Rotation != null)
                        Roation = new Quaternion(Door.Rotation.Value.x, Door.Rotation.Value.y, 0, 0);
                    else Roation = null;

                    if (Server.Get.Map.Rooms.Where(p => p.RoomName == Door.Position.Room).Count() != 0)
                    {
                        //if (Door.Position.Room == "LCZ_Airlock (1)" || Door.Position.Room == "LCZ_Airlock (2)")
                        //    Server.Get.Logger.Warn("You cant sapwn door in AirLock !");
                        //else 
                            
                    }
               }
        }
    }
}