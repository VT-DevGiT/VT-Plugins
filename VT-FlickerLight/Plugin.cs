using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VT_FlickerLight
{
    [PluginInformation(
        Name = "VT-FlickerLight",
        Author = "Antoniofo",
        Description = "Flicker the light at the start of the game",
        LoadPriority = 0,
        SynapseMajor = 2,
        SynapseMinor = 7,
        SynapsePatch = 1,
        Version = "v.1.0.0"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "VT-FlickerLight")]
        public static Config Config { get; set; }

        public override void Load()
        {
            base.Load();
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
        }

        public void OnRoundStart()
        {

            Timing.RunCoroutine(RedLigthFlicking());

        }


        private IEnumerator<float> RedLigthFlicking()
        {

            yield return Timing.WaitForSeconds(1f);
            for (int i = 0; i < Config.NumberOfLightFlickingAtTheBegining; i++)
            {
                ChangeRoomLightColor(new Color(Config.FirstColor[0] / (byte.MaxValue), Config.FirstColor[1] / (byte.MaxValue), Config.FirstColor[2] / (byte.MaxValue)));
                yield return Timing.WaitForSeconds(Config.TimeBetweenFlicker);
                ChangeRoomLightColor(new Color(Config.SecondColor[0] / (byte.MaxValue), Config.SecondColor[1] / (byte.MaxValue), Config.SecondColor[2] / (byte.MaxValue)));
                yield return Timing.WaitForSeconds(Config.TimeBetweenFlicker);
            }
            ResetLight();
            yield break;
        }

        public void ResetLight()
        {

            foreach (Room room in SynapseController.Server.Map.Rooms)
            {
                room.LightController.WarheadLightOverride = false;
            }
        }
        public void ChangeRoomLightColor(Color color)
        {
            foreach (Room room in SynapseController.Server.Map.Rooms)
            {
                room.LightController.WarheadLightOverride = true;
                room.LightController.WarheadLightColor = color;

            }
        }

        public override void ReloadConfigs()
        {
            base.ReloadConfigs();
        }
    }
}
