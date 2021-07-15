using Interactables.Interobjects.DoorUtils;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;

namespace VTDoor
{
    public class Config : AbstractConfigSection
    {

        public List<DoorConfig> DoorList = new List<DoorConfig>()
        {
            new DoorConfig() {
                Position = new SerializedMapPoint("Root_*&*Outside Cams", 14.5f, -3.469971f, -43.58254f),
                Rotation = null,
                Permissions = null
            },
            new DoorConfig() {
                Position = new SerializedMapPoint("Root_*&*Outside Cams", 14.5f, -3.469971f, -23.43554f),
                Rotation = null,
                Permissions = null
            },
        };
    }

    public class DoorConfig
    {
        public SerializedMapPoint Position { get; set; }
        public Vector2? Rotation { get; set; }
        public DoorPermissions Permissions { get; set; }
    }
}
