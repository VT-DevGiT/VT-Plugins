using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_Referance.Event
{
    public class Events
    { 
       
        private static bool _patched = false;

        public Events()
        {
            if (!_patched)
            {
                _patched = true;
                var instance = new Harmony("VT_Referance.Patch");
                instance.PatchAll();
            }
        }

        #region Instance
        private static class Singleton<T>
        where T : class, new()
        {
            private static T _instance;
            private static readonly object _lock = new object();
            public static T Instance
            {
                get
                {
                    lock (_lock)
                    {
                        return _instance ?? (_instance = new T());
                    }
                }
            }
        }
        public sealed class PlayerSingleton : VT_PlayerEvents
        {
            public static VT_PlayerEvents Instance
            {
                get
                {
                    return Singleton<VT_PlayerEvents>.Instance;
                }
            }
        }

        public sealed class GrenadeSingleton : VT_GrenadeEvents
        {
            public static VT_GrenadeEvents Instance
            {
                get
                {
                    return Singleton<VT_GrenadeEvents>.Instance;
                }
            }
        }


        #endregion
    }

}
