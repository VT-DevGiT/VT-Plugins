using MEC;
using Synapse;
using System.Collections.Generic;
using VT_Referance.Method;

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
                Methods.ChangeRoomsLightColor(Plugin.Instance.firstColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
                Methods.ChangeRoomsLightColor(Plugin.Instance.secondColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
            }
            Methods.ResetRoomsLightColor();
            yield break;
        }
    }
}
