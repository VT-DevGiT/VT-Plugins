using MEC;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VT079
{
    static class Methode
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
