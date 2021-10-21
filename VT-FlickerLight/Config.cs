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

        [Description("The second color of the light when the light start flicker. (R G B) WARNING: do not put the same number in first and second color")]
        public int[] SecondColor { get; set; } = { 254, 200, 200 };

        [Description("The numer of second between the two change color")]
        public float TimeBetweenFlicker { get; set; } = 0.75f;
    }
}
