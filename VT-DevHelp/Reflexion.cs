using System;
using System.Reflection;

namespace VTDevHelp
{
    public static class Reflexion
    {
        public static string GetInfo(this object o)
        {
            Type type = o.GetType();
            string info = $"value of {type.Name} :\n";
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                info += $"\t{field.Name} = {field.GetValue(field.IsStatic ? null : o)}\n";
            }
            return info;
        }

        public static string GetInfo(this Type o)
        {
            string info = $"value of {o.Name} :\n";
            var fields = o.GetFields();
            foreach(var field in fields)
            {
                info += $"\t{field.Name} = {field.GetValue(null)}\n";
            }
            return info;
        }
    }
}
