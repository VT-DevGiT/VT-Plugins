using MEC;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using System.Reflection;

namespace VTEscape
{
    public static class Extanction   
    {
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        public static T GetFieldValue<T>(this object element, string fieldName)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }
    }
    public class Method
    {
        public void PlayAmbientSound(int id)
        {
            foreach (var player in Server.Get.Players)
            {
                player.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);

            }
        }

        public IEnumerator<float> WarHeadEscape(int waitforready = 3)
        {
            yield return Timing.WaitForSeconds(10f);
            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                waitforready--;
                yield return Timing.WaitForSeconds(1f);
            }
            if (Plugin.Config.WarHeadLockEnabled)
            {
                Map.Get.Nuke.InsidePanel.Locked = true;
            }
            AlphaWarheadController.Host.StartDetonation();
        }
    }
}
