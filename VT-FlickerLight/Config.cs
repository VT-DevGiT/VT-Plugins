using System.ComponentModel;
using Synapse.Config;

namespace VT_FlickerLight
{
    public class Config : IConfigSection
    {
        [Description("How many times does the light flicker")]
        public int NumberOfLightFlickingAtTheBegining { get; set; } = 10;

        [Description("The first color of the light when the light start flicker. (R G B)")]
        public int[] FirstColor { get; set; } = { 255, 0, 0 };

        [Description("The other color of the light when the light start flicker. (R G B) WARNING: do not put the same number in first and second color")]
        public int[] SecondColor { get; set; } = { 250, 200, 200 };
        public int[] ThirdColor { get; set; } = { 245, 100, 100 };

        [Description("The numer of second between the two change color")]
        public float TimeBetweenFlicker { get; set; } = 0.5f;

        [Description("Play alert sound when light is flicker")]
        public bool PlaySound = true;

        [Description("Play cassie and after the light flickering")]
        public string CassieAnnonce = "BREACH DETECTED . EMERGENCY PROCEDURE ACTIVATED";
    }
}
