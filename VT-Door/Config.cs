using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTDoor
{
    public class Config : AbstractConfigSection
    {

        public List<SerializedMapPoint> DoorList = new List<SerializedMapPoint>()
        {
            new SerializedMapPoint("LCZ_Airlock (2)", 0f, 0f, -4.876313f),
            new SerializedMapPoint("LCZ_Airlock (1)", 0f, 0f, -4.876313f),
            new SerializedMapPoint("Root_*&*Outside Cams", 14.3f, -6f, -23.34f),
            new SerializedMapPoint("Root_*&*Outside Cams", 14.3f, -6f, -43.5f),
        };
    }
}
