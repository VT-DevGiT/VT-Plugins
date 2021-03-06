using System.Reflection;

namespace VT_Referance.Method
{
    public static class Reflection
    {
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
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

        public static float TotalVoltageFloat()
        {
            float totalvoltagefloat = 0f;
            foreach (var i in Generator079.Generators)
            {
                totalvoltagefloat += i.localVoltage;
            }
            totalvoltagefloat *= 1000f;
            return totalvoltagefloat;
        }

    }
}
