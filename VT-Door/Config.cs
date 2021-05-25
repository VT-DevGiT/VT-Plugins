using Interactables.Interobjects.DoorUtils;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VTDoor
{
    public class Config : AbstractConfigSection
    {

        public List<DoorConfig> DoorList = new List<DoorConfig>()
        {
            //BUR :( Crash Game need PATCH !
            new DoorConfig() {
                Position = new SerializedMapPoint("LCZ_Airlock (2)", 0f, 0f, -4.876313f),
                Rotation = null,
                Permissions = null
            },
            new DoorConfig() {
                Position = new SerializedMapPoint("LCZ_Airlock (1)", 0f, 0f, -4.876313f),
                Rotation = null,
                Permissions = null
            },

            new DoorConfig() {
                Position = new SerializedMapPoint("Root_*&*Outside Cams", 15f, -6f, -23.34f),
                Rotation = null,
                Permissions = null
            },
            new DoorConfig() {
                Position = new SerializedMapPoint("Root_*&*Outside Cams", 15f, -6f, -43.5f),
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
