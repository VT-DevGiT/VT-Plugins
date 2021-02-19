using Synapse;
using System.Reflection;

namespace VT079
{
    public static class Method
    {
        public static void PlayAmbientSound(int id)
        {
            foreach (var player in Server.Get.Players)
            {
                player.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
            }
        }

        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }
        public static void SetFieldValue<T>(this object element, string fieldName, T value)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(element, value);
        }

        public static T GetFieldValue<T>(this object element, string fieldName)
        {

            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }
    }
}
