using Synapse.Api;
using System;
using System.Reflection;

namespace VT_Referance.Method
{
    public static class Reflection
    {
        [API]
        public static object CallMethod(this Type o, string methodName, params object[] args)
        {
            var mi = o.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            if (mi != null)
            {
                return mi.Invoke(null, args);
            }
            return null;
        }

        [API]
        public static object CallMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }

        
        [API]
        public static T GetFieldValueorOrPerties<T>(this object element, string fieldName)
        {
            var prop = element.GetType().GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null)
            {
                return (T)prop.GetValue(element);
            }
            FieldInfo field = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return (T)field.GetValue(element);
            }
            return default(T);
        }


        [API]
        public static T GetFieldOrPropertyValue<T>(this Type element, string fieldName)
        {
            var prop = element.GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (prop != null)
            {
                return (T)prop.GetValue(null);
            }
            FieldInfo field = element.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (field != null)
            {
                return (T)field.GetValue(null);
            }
            return default(T);
        }

        [API]
        public static void SetProperty<T>(this Type element, string fieldName, object value)
        {
            var prop = element.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (prop != null)
            {
                prop.SetValue(null, value);
            }
        }

        [API]
        public static void SetProperty<T>(this object element, string fieldName, object value)
        {
            var prop = element.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null)
            {
                prop.SetValue(element, value);
            }
        }


        [API]
        public static void SetField<T>(this object element, string fieldName, object value)
        {
            var prop = element.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null)
            {
                prop.SetValue(element, value);
            }
        }

        [API]
        public static void SetField<T>(this Type element, string fieldName, T value)
        {
            var prop = element.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (prop != null)
            {
                prop.SetValue(null, value);
            }
        }
    }
}
