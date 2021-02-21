using Synapse.Config;
using System.ComponentModel;

namespace VT939
{
    public class Config : AbstractConfigSection
    {
        [Description("If 939 should be faster that a human's sprint.")]
        public bool IsFasterThanHumans = true;

        [Description("If 939 should get slowed down after biting someone.")]
        public bool ShouldGetSlowed = true;

        [Description("The size multiplier for 939.")]
        public float Size = 0.74f;

        [Description("How much 939 should be slowed after biting someone.")]
        public float SlowAmount = 10f;

        [Description("The base amount of damage 939 should deal.")]
        public float BaseDamage = 40f;

        [Description("The duration of the slowdown after 939 bites.")]
        public float ForceSlowDownTime = 3f;

        [Description("The maximum amount of extra damage 939 can deal at full anger.")]
        public float BonusAttackMaximum = 150f;

        [Description("The maximum value for his anger meter, larger values means it takes longer to fill.")]
        public float AngerMeterMaximum = 500f;

        [Description("The time in between each time the anger meter will decay, larger values means it takes longer to decay.")]
        public float AngerMeterDecayTime = 1f;

        [Description("The amount of anger that will decay each anger_meter_decay_time seconds.")]
        public float AngerMeterDecayValue = 3f;

        [Description("How much anger 939 should spawn with.")]
        public float StartingAnger = 0f;

        [Description("Whether or not to show a broadcast message to 939 when he spawns, explaining this plugin to them.")]
        public bool ShowSpawnBroadcastMessage = true;

        [Description("The duration of the spawn broadcast.")]
        public ushort SpawnBroadcastMessageDuration = 15;

        [Description("The amount of anger 939 has when it sees a target")]
        public int Scp939SeeingAhpAmount = 10;

        [Description("The message to broadcast to the 939.")]
        public string SpawnBroadcastMessage =
            "<size=20><color=#00FFFF>You've spawned as an upgraded version of <color=#FF0000>SCP-939</color>!" +
            "\nYou're faster than humans, your <color=#FF0000>anger</color> will increase after taking damage from them." +
            "\nMore anger means more damage inflicted to humans." +
            "\nAfter <color=#FF0000>hurting</color> someone, you'll get slowed down for <color=#FF0000>{0}</color> seconds</color></size>";

        [Description("Whether or not 939's anger meter should be reset after his slowdown from biting someone ends.")]
        public bool ResetAngerAfterHitSlowDown = false;

        [Description("can see the person speaking")]
        public bool Scp939CanSeeVoiceChatting = true;
    }
}
