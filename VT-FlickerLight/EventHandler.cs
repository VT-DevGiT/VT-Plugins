using MEC;
using Synapse;
using Synapse.Api;
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
                if (Plugin.Config.PlaySound) Methods.PlayAmbientSound(7);
                Methods.ChangeRoomsLightColor(Plugin.Instance.firstColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
                Methods.ChangeRoomsLightColor(Plugin.Instance.secondColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
                Methods.ChangeRoomsLightColor(Plugin.Instance.thirdColor);
                yield return Timing.WaitForSeconds(Plugin.Config.TimeBetweenFlicker);
            }
            if (!string.IsNullOrWhiteSpace(Plugin.Config.CassieAnnonce)) Map.Get.Cassie(Plugin.Config.CassieAnnonce);
            Methods.ResetRoomsLightColor();
            Methods.ResetRoomsLightColor();// for patch client bug
            yield break;
        }
    }
}
