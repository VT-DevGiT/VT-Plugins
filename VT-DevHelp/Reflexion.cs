using System;
using System.Reflection;

namespace VTDevHelp
{
    public static class Reflexion
    {
        public static string GetInfo(this object element)
        {
            Type type = element.GetType();
            string info = $"value of {type.Name} :\n";

            info += $"- Fields :\n";
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
                info += $"\t{field.Name} = {field.GetValue(field.IsStatic ? null : element)?.ToString() ?? "null"}\n";

            info += $"- Properties :\n";
            var props = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
                info += $"\t{prop.Name} = {prop.GetValue(prop.GetMethod.IsStatic ? null : element)?.ToString() ?? "null"}\n";

            return info;
        }

        public static string GetInfo(this Type element)
        {
            string info = $"value of {element.Name} :\n";

            info += $"- Fields :\n";
            var fields = element.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var field in fields)
                info += $"\t{field.Name} = {field.GetValue(null)?.ToString() ?? "null"}\n";

            info += $"- Properties :\n";
            var props = element.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var prop in props)
                info += $"\t{prop.Name} = {prop.GetValue(null)?.ToString() ?? "null"}\n";

            return info;
        }
    }
}
