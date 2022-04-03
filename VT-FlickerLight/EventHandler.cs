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
            for (int i = 0; i < Plugin.Instance.Config.NumberOfLightFlickingAtTheBegining; i++)
            {
                var waitTime = Plugin.Instance.Config.TimeBetweenFlicker;

                if (Plugin.Instance.Config.PlaySound) 
                    Map.Get.PlayAmbientSound(7);

                Map.Get.ChangeRoomsLightColor(Plugin.Instance.Config.FirstColor);
                
                yield return Timing.WaitForSeconds(waitTime);
                
                Map.Get.ChangeRoomsLightColor(Plugin.Instance.Config.SecondColor);
                
                yield return Timing.WaitForSeconds(waitTime);
                
                Map.Get.ChangeRoomsLightColor(Plugin.Instance.Config.ThirdColor);
                
                yield return Timing.WaitForSeconds(waitTime);
            }
            if (!string.IsNullOrWhiteSpace(Plugin.Instance.Config.CassieAnnonce)) 
                Map.Get.Cassie(Plugin.Instance.Config.CassieAnnonce);

            Map.Get.ResetRoomsLightColor();
            Map.Get.ResetRoomsLightColor();// for patch client bug
            yield break;
        }
    }
}
