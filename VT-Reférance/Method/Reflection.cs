using System.Reflection;

namespace VT_Referance.Method
{
    public static class Reflection
    {
        /// <summary>
        ///  calls method by reflection
        /// </summary>
        /// <param name="o"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        /// <summary>
        /// reviews the value of a variable by reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFieldValue<T>(this object element, string fieldName)
        {
            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }

        public static T GetProperties<T>(this object element, string fieldName)
        {
            var field = element.GetType().GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(element);
        }
    }
}
