using MEC;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using VT_Api.Extension;

namespace VT_FlickerLight
{
    public class EventHandler
    {
        public EventHandler()
        {
            Server.Get.Events.Round.RoundStartEvent += () => Timing.RunCoroutine(LigthFlicking());
        }

        private IEnumerator<float> LigthFlicking()
        {
            yield return Timing.WaitForSeconds(1f);
            for (int i = 0; i < Plugin.Config.NumberOfLightFlickingAtTheBegining; i++)
            {
                if (Plugin.Config.PlaySound) Map.Get.PlayAmbientSound(7);
                Map.Get.ChangeRoomsLightColor(Plugin.Instance.firstColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
                Map.Get.ChangeRoomsLightColor(Plugin.Instance.secondColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
                Map.Get.ChangeRoomsLightColor(Plugin.Instance.thirdColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
            }
            if (!string.IsNullOrWhiteSpace(Plugin.Config.CassieAnnonce)) Map.Get.Cassie(Plugin.Config.CassieAnnonce);
            Map.Get.ResetRoomsLightColor();
            Map.Get.ResetRoomsLightColor();// for patch client bug
            yield break;
        }
    }
}
