using System.ComponentModel;
using Synapse.Config;
using UnityEngine;

namespace VT_FlickerLight
{
    public class Config : IConfigSection
    {
        [Description("How many times does the light flicker")]
        public int NumberOfLightFlickingAtTheBegining { get; set; } = 10;

        [Description("The color of the light when the light start flicker. (R G B)")]
        public SerializedColor FirstColor { get; set; } = new Color32(255, 0, 0 , 250);
        public SerializedColor SecondColor { get; set; } = new Color32(250, 200, 200, 250 );
        public SerializedColor ThirdColor { get; set; } = new Color32(245, 100, 100, 250);

        [Description("The numer of second between the two change color")]
        public float TimeBetweenFlicker { get; set; } = 0.5f;

        [Description("Play alert sound when light is flicker")]
        public bool PlaySound = true;

        [Description("Play cassie and after the light flickering")]
        public string CassieAnnonce = "BREACH DETECTED . EMERGENCY PROCEDURE ACTIVATED";
    }
}
